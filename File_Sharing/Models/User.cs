using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace File_Sharing.Models
{
    public class User
    {       
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string Name { get; set; }
        public string LasttName { get; set; }
        public string ConnectionId { get; set; }
        //public HashSet<string> ConnectionIds { get; set; }
        public ICollection<Connection> Connections { get; set; }
        public virtual ICollection<ConversationRoom> Rooms { get; set; }

    }
}
