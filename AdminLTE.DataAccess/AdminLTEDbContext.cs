using AdminLTE.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLTE.DataAccess
{
    public class DbModelContext : DbContext
    {
        public DbModelContext()
            : base("name=DefaultConnection")
        {

        }

        public virtual DbSet<KeyType> KeyTypes { get; set; }
        public virtual DbSet<KeyValue> KeyValues { get; set; }
        public virtual DbSet<TranslationLanguage> TranslationLanguages { get; set; }
        public virtual DbSet<TranslationWord> TranslationWords { get; set; }

        public static DbModelContext Create()
        {
            return new DbModelContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KeyType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<KeyType>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<KeyType>()
                .HasMany(e => e.KeyValues)
                .WithRequired(e => e.KeyType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KeyValue>()
                .Property(e => e.Key)
                .IsUnicode(false);

            modelBuilder.Entity<KeyValue>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<KeyValue>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<KeyValue>()
                .Property(e => e.CreatedUser)
                .IsUnicode(false);

            modelBuilder.Entity<KeyValue>()
                .Property(e => e.ModifiedUser)
                .IsUnicode(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
