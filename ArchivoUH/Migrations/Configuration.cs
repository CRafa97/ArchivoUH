namespace ArchivoUH.Migrations
{
    using ArchivoUH.Domain.Auth;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArchivoUH.Contexts.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ArchivoUH.Contexts.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

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
