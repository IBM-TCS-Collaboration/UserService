using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Database.Entities;

namespace UserService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        

        public DbSet<Roles> Roles { get; set; }

        public DbSet<AssignedRole> AssignedRoles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=ts-management-db; port=3360; database=userdb; user=root; password=password");
        }
    }
}
