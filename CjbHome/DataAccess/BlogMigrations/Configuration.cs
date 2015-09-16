namespace CjbHome.DataAccess.BlogMigrations
{
    using CjbHome.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CjbHome.DataAccess.BlogPostDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"DataAccess\BlogMigrations";
        }

        protected override void Seed(CjbHome.DataAccess.BlogPostDb context)
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

            context.Tags.AddOrUpdate(
                t => t.Title,
                new Tag { Title = "C#" },
                new Tag { Title = "WPF" },
                new Tag { Title = "ASP.NET" }
                );

            context.BlogPosts.AddOrUpdate(
                p => p.Title,
                new BlogPost
                {
                    Title = "TestPost3",
                    LinkText = "TestPost3",
                    Content = "This is another post from code",
                    PostDate = DateTime.Now,
                    PostTime = DateTime.Now,
                    Tags = new[] { context.Tags.First(t => t.Title == "C#"), context.Tags.First(t => t.Title == "WPF"), context.Tags.First(t => t.Title == "ASP.NET") }
                });
        }
    }
}
