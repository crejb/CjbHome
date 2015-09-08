namespace CjbHome.DataAccess.IdentityMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using CjbHome.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CjbHome.DataAccess.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"DataAccess\IdentityMigrations";
        }

        protected override void Seed(CjbHome.DataAccess.IdentityDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Uncomment this to add a dummy admin user
            //var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new ApplicationUserManager(userStore);
            //var user = new ApplicationUser { UserName = "username" };
            //userManager.Create(user, "password");
        }
    }
}
