using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Web;
using AdminLTE;

namespace AdminLTE.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class DbModelContext : IdentityDbContext<ApplicationUser>
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
