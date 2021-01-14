namespace AdminLTE.Migrations
{
    using AdminLTE.Model;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AdminLTE.DataAccess.DbModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? new ApplicationUserManager(new UserStore<User>(new AdminLTE.DataAccess.DbModelContext()));
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Seed(AdminLTE.DataAccess.DbModelContext context)
        {
            #region Default Translation
            var languageTR = context.TranslationLanguages.Add(new TranslationLanguage
            {
                Code = "TR",
                Description = "Türkçe",
                IsDeleted = false
            });
            var languageEN = context.TranslationLanguages.Add(new TranslationLanguage
            {
                Code = "EN",
                Description = "English",
                IsDeleted = false
            });
            List<TranslationWord> turkceWords = new List<TranslationWord>();
            turkceWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageTR.TranslationLanguageId,
                Code = "del",
                Description = "Sil"
            });
            turkceWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageTR.TranslationLanguageId,
                Code = "save",
                Description = "Kaydet"
            });
            turkceWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageTR.TranslationLanguageId,
                Code = "cancel",
                Description = "İptal"
            });
            turkceWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageTR.TranslationLanguageId,
                Code = "home",
                Description = "Ana Sayfa"
            });
            context.TranslationWords.AddRange(turkceWords);

            List<TranslationWord> englishWords = new List<TranslationWord>();
            englishWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageEN.TranslationLanguageId,
                Code = "del",
                Description = "Sil"
            });
            englishWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageEN.TranslationLanguageId,
                Code = "save",
                Description = "Kaydet"
            });
            englishWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageEN.TranslationLanguageId,
                Code = "cancel",
                Description = "İptal"
            });
            englishWords.Add(new TranslationWord
            {
                TranslationLanguageId = languageEN.TranslationLanguageId,
                Code = "home",
                Description = "Ana Sayfa"
            });
            context.TranslationWords.AddRange(englishWords);
            #endregion

            #region Default User / Role
            var adminRole = new Role
            {
                Name = "Admin"
            };
            var userRole = new Role
            {
                Name = "User"
            };
            context.Roles.Add(adminRole);
            context.Roles.Add(userRole);
            context.SaveChanges();

            var admin = new User()
            {
                UserName = "admin",
                Email = "admin@adminlte.com",
                LockoutEnabled = true
            };

            var user = new User()
            {
                UserName = "user",
                Email = "user@adminlte.com",
                LockoutEnabled = true
            };

            UserManager.Create(admin, "Admin123!");
            UserManager.Create(user, "User123!");
            UserManager.AddToRoles(admin.Id, adminRole.Name, userRole.Name);
            UserManager.AddToRoles(user.Id, userRole.Name);
            #endregion
        }
    }
}
