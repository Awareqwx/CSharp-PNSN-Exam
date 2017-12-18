using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfessionalNetwork.Models;

namespace ProfessionalNetwork.Controllers
{
    public class ConnectionController : Controller
    {

        private NetworkContext _context;
        private readonly DbConnector _dbConnector;

        public ConnectionController(DbConnector connect, NetworkContext context)
        {
            _dbConnector = connect;
            _context = context;
        }

        private bool isLoggedIn()
        {
            int? currId = HttpContext.Session.GetInt32("currUser");
            if(currId == null)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        [Route("/professional_profile")]
        public IActionResult Profile()
        {
            if(!isLoggedIn()) return Redirect("/");
            User currUser = _context.Users
                .Include(user => user.ConnectedTo).ThenInclude(conn => conn.ConnectedTo)
                .Include(user => user.ConnectedBy).ThenInclude(conn => conn.Connector)
                .Single(user => user.UserId == (int)HttpContext.Session.GetInt32("currUser"));
            return View("Profile", currUser);
        }

        [HttpGet]
        [Route("/users")]
        public IActionResult AllUsers()
        {
            if(!isLoggedIn()) return Redirect("/");
            User currUser = _context.Users
                .Include(user => user.ConnectedTo).ThenInclude(conn => conn.ConnectedTo)
                .Include(user => user.ConnectedBy).ThenInclude(conn => conn.Connector)
                .Single(user => user.UserId == (int)HttpContext.Session.GetInt32("currUser"));
            List<User> ShowUsers = _context.Users
                .Include(user => user.ConnectedTo).ThenInclude(conn => conn.ConnectedTo)
                .Include(user => user.ConnectedBy).ThenInclude(conn => conn.Connector)
                .Where(user => user.UserId != currUser.UserId)
                .Where(user => UserFilter(user, currUser)).ToList();
            ViewBag.ShowUsers = ShowUsers;
            return View("AllUsers", currUser);
        }

        [HttpGet]
        [Route("/connect/{id}")]
        public IActionResult Connect(int id)
        {
            if(!isLoggedIn()) return Redirect("/");
            User currUser = _context.Users
                .Include(user => user.ConnectedTo).ThenInclude(conn => conn.ConnectedTo)
                .Include(user => user.ConnectedBy).ThenInclude(conn => conn.Connector)
                .Single(user => user.UserId == (int)HttpContext.Session.GetInt32("currUser"));
            Console.WriteLine(currUser.ConnectedBy.Count);
            Console.WriteLine(currUser.ConnectedTo.Count);
            try
            {
                User connectUser = _context.Users
                    .Include(user => user.ConnectedTo).ThenInclude(conn => conn.ConnectedTo)
                    .Include(user => user.ConnectedBy).ThenInclude(conn => conn.Connector)
                    .Where(user => user.UserId != currUser.UserId)
                    .Where(user => UserFilter(user, currUser)).Single(user => user.UserId == id);
            }
            catch
            {
                return RedirectToAction("Profile");
            }
            Connection connect = new Connection
            {
                ConnectorId = currUser.UserId,
                ConnectedToId = id,
                Accepted = 0
            };
            _context.Connections.Add(connect);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpGet]
        [Route("/users/{id}")]
        public IActionResult ShowUser(int id)
        {
            if(!isLoggedIn()) return Redirect("/");
            User showUser;
            try
            {
                showUser = _context.Users.Single(user => user.UserId == id);
            }
            catch
            {
                return RedirectToAction("Profile");
            }
            return View("ShowUser", showUser);
        }

        [HttpGet]
        [Route("/accept/{id}")]
        public IActionResult Accept(int id)
        {
            Connection connect;
            try
            {
                connect = _context.Connections.Include(conn => conn.ConnectedTo).Include(conn => conn.Connector).Single(conn => conn.ConnectionId == id);
            }
            catch
            {
                return RedirectToAction("Profile");
            }
            if(connect.ConnectedToId != (int)HttpContext.Session.GetInt32("currUser"))
            {
                return RedirectToAction("Profile");
            }
            connect.Accepted = 1;
            _context.Update(connect);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpGet]
        [Route("/cancel/{id}")]
        public IActionResult Cancel(int id)
        {
            return Redirect("/ignore/" + id);
        }

        [HttpGet]
        [Route("/remove/{id}")]
        public IActionResult Remove(int id)
        {
            return Redirect("/ignore/" + id);
        }

        [HttpGet]
        [Route("/ignore/{id}")]
        public IActionResult Ignore(int id)
        {
            Connection connect;
            try
            {
                connect = _context.Connections.Include(conn => conn.ConnectedTo).Include(conn => conn.Connector).Single(conn => conn.ConnectionId == id);
            }
            catch
            {
                return RedirectToAction("Profile");
            }
            if(connect.ConnectedToId != (int)HttpContext.Session.GetInt32("currUser") && connect.ConnectorId != (int)HttpContext.Session.GetInt32("currUser"))
            {
                return RedirectToAction("Profile");
            }
            _context.Remove(connect);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        private bool UserFilter(User user, User currUser)
        {
            Console.WriteLine(currUser.ConnectedTo.Count);
            foreach(Connection conn in currUser.ConnectedTo)
            {
                Console.WriteLine("3: " + conn.ConnectedTo.UserId + ", " + user.UserId);
                if(conn.ConnectedTo.UserId == user.UserId)
                {
                    return false;
                }
            }
            Console.WriteLine(currUser.ConnectedBy.Count);
            foreach(Connection conn in currUser.ConnectedBy)
            {
                Console.WriteLine("4: " + conn.Connector.UserId + ", " + user.UserId);
                if(conn.Connector.UserId == user.UserId)
                {
                    return false;
                }
            }
            return true;
        }

    }
}