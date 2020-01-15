using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace File_Sharing.Models
{
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<FileModel> FileModels { get; set; }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
