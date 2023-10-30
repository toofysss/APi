using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AD> ad { get; set; }
        public DbSet<Features> Features { get; set; }
        public DbSet<Websiteinfo> Websiteinfo { get; set; }
        public DbSet<Color> ColorsSetting { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Social> Social { get; set; }

        public DbSet<Project> Project { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}