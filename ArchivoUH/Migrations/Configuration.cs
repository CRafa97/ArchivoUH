namespace ArchivoUH.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using ArchivoUH.Domain.Auth;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ArchivoUH.Contexts;
    using Microsoft.AspNet.Identity.Owin;

    internal sealed class Configuration : DbMigrationsConfiguration<ArchivoUH.Contexts.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ArchivoUH.Contexts.ApplicationDbContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<User>(context));

            var adms = (from user in manager.Users.ToList()
                       where user.UserName == "admin"
                       select user);

            var admin = new User()
            {
                UserName = "admin",
                Email = "admin@uh.cu",
                FirstName = "adminName",
                LastName = "adminLastName",
                PasswordHash = new PasswordHasher().HashPassword("admin")
            };

            if (adms.Count() != 0)
            {
                manager.Delete(adms.Single());
            }
            
            manager.Create(admin);
            admin = manager.FindByName("admin");
            
            manager.UpdateSecurityStamp(admin.Id);
        }
    }
}
