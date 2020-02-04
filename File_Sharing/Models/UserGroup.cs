using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Models
{
    public class UserGroup
    {
        public UserGroup()
        {
            Users = new HashSet<User>();
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public int GroupId { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
