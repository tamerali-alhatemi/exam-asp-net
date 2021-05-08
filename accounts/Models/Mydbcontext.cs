using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace accounts.Models
{
    public class Mydbcontext : DbContext
    {
        public DbSet<Mainaccounts> main_account { get; set; }
        public DbSet<Currencies> currencies { get; set; }
        public DbSet<Accounts> accounts { get; set; }
        public DbSet<Accountcurrencies> accountcurrencies { get; set; }
        public DbSet<Users> users { get; set; }
    }
}