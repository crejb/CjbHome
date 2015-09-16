using CjbHome.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Linq;
using System.Web;

namespace CjbHome.DataAccess
{
    public class BlogPostDb : DbContext
    {
        public DbSet<BlogPost> BlogPosts
        {
            get
            {
                return Set<BlogPost>();
            }
        }

        public DbSet<Tag> Tags
        {
            get
            {
                return Set<Tag>();
            }
        }

        public BlogPostDb()
            : base("DefaultConnection")
        {
        }

        public static BlogPostDb Create()
        {
            return new BlogPostDb();
        }

        public IEnumerable<BlogPost> GetSortedBlogPosts()
        {
            return BlogPosts.ToOrderedList();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CjbHome");
        }
    }

    public static class BlogPostExtensions
    {
        public static IEnumerable<BlogPost> ToOrderedList(this IEnumerable<BlogPost> posts)
        {
            return posts.OrderByDescending(p => p.PostDate)
                .ThenByDescending(p => p.PostTime);
        }
    }

    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("CjbHome");
        }
    }

    public class MyHistoryContext : HistoryContext
    {
        public MyHistoryContext(DbConnection dbConnection, string defaultSchema)
            : base(dbConnection, defaultSchema)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<HistoryRow>().ToTable("__MigrationHistory", schemaName: "CjbHome");
        }
    }

    public class ModelConfiguration : DbConfiguration
    {
        public ModelConfiguration()
        {
            this.SetHistoryContext("System.Data.SqlClient",
                (connection, defaultSchema) => new MyHistoryContext(connection, defaultSchema));
        }
    } 
}