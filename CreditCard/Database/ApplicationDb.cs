using CreditCard.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Database
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options){}
        
        public DbSet<Ads> AD { get; set; }
        public DbSet<User> User { get; set; }

    }
}
