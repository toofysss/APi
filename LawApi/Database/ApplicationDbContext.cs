using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Database
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public  DbSet<Users> Users { get; set; }
        public DbSet<RightGuide> RightGuide { get; set; }
        public DbSet<RightDetails> RightDetails { get; set; }
        public DbSet<DecisionsCouncil> DecisionsCouncil { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<IraqBank> IraqBank { get; set; }
        public DbSet<IraqBankDetails> IraqBankDetails { get; set; }
        public DbSet<DecisionsCourtCassation> DecisionsCourtCassation { get; set; }
        public DbSet<DecisionsCourtCassationDetails> DecisionsCourtCassationDetails { get; set; }
        public DbSet<DecisionsCommand> DecisionsCommand { get; set; }
        public DbSet<CoalitionProvisional> CoalitionProvisional { get; set; }
        public DbSet<Law> Law { get; set; }
        public DbSet<LawDetails> LawDetails { get; set; }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Consultation> Consultation { get; set; }
        public DbSet<Laweys> Laweys { get; set; }
        public DbSet<IraqiGazette> IraqiGazette { get; set; }
        public DbSet<PetitionsLegalRequests> PetitionsLegalRequests { get; set; }
        public DbSet<DecisionsFederalSupremeCourt> DecisionsFederalSupremeCourt { get; set; }  
        public DbSet<Discriminatoryregulations> Discriminatoryregulations { get; set; }
        public DbSet<DiscriminatoryregulationsDetails> DiscriminatoryregulationsDetails { get; set; }
    }
}
