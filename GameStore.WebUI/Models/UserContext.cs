using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("EFDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }
}