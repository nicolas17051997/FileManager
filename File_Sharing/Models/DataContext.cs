using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using File_Sharing.Models;

namespace File_Sharing.Models
{
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<ConversationRoom> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
