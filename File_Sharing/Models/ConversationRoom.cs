using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace File_Sharing.Models
{
    public class ConversationRoom
    {
      
        [Key]
        public string RoomName { get; set; }
        [NotMapped]
        public virtual ICollection<User> Users { get; set; }
    }
}
