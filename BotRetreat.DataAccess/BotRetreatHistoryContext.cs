using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotRetreat.Domain;

namespace BotRetreat.DataAccess
{
    public class BotRetreatHistoryContext : DbContext, IBotRetreatHistoryContext
    {
        public IDbSet<History> History { get; set; }

        public BotRetreatHistoryContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BotRetreatContext>());
            Configuration.ValidateOnSaveEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //
            modelBuilder.Entity<History>().ToTable("HISTORY")
                .HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //
            base.OnModelCreating(modelBuilder);
        }
    }
}
