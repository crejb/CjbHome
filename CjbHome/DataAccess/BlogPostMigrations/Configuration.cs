namespace CjbHome.DataAccess.BlogPostMigrations
{
    using CjbHome.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CjbHome.DataAccess.BlogPostDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataAccess\BlogPostMigrations";
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

            context.BlogPosts.AddOrUpdate(
                p => p.Title,
                new BlogPost { Title = "First post", LinkText="FirstPost", PostTime = new DateTime(2015, 8, 15), Content = "Here are some words" },
                new BlogPost { Title = "Second post", LinkText = "SecondPost", PostTime = new DateTime(2015, 8, 16), Content = "Here are some more words" },
                new BlogPost { Title = "Even more post", LinkText = "EvenMorePost", PostTime = new DateTime(2015, 8, 17), Content = "Here are some different words" }
                );
        }
    }
}
