using System;
using System.ComponentModel.DataAnnotations;

namespace ProfessionalNetwork.Models
{
    public class Connection
    {
        [Key]
        public int ConnectionId { get; set; }
        public int ConnectorId { get; set; }
        public User Connector { get; set; } 
        public int ConnectedToId { get; set; }
        public User ConnectedTo { get; set; }
        public int Accepted { get; set; }
    }
}