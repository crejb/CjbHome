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
    }
}