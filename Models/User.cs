using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessionalNetwork.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Description { get; set; }
        [Key]
        public int UserId { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        [InverseProperty("Connector")]
        public List<Connection> ConnectedTo { get; set; }
        [InverseProperty("ConnectedTo")]
        public List<Connection> ConnectedBy { get; set; }

        public User()
        {
            ConnectedTo = new List<Connection>();
            ConnectedBy = new List<Connection>();
        }
    }
}