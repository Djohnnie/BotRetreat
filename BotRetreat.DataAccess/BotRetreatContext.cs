using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using BotRetreat.Domain;

namespace BotRetreat.DataAccess
{
    public class BotRetreatContext : DbContext, IBotRetreatContext
    {
        public IDbSet<Arena> Arenas { get; set; }
        public IDbSet<Team> Teams { get; set; }
        public IDbSet<Deployment> Deployments { get; set; }
        public IDbSet<Bot> Bots { get; set; }

        public BotRetreatContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BotRetreatContext>());
            Configuration.ValidateOnSaveEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //
            modelBuilder.Entity<Arena>().ToTable("ARENAS")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //
            modelBuilder.Entity<Team>().ToTable("TEAMS")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //
            modelBuilder.Entity<Deployment>().ToTable("DEPLOYMENTS")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //
            modelBuilder.Entity<Bot>().ToTable("BOTS")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //
            base.OnModelCreating(modelBuilder);
        }
    }
}