using CjbHome.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public BlogPostDb()
            : base("DefaultConnection")
        {
        }

        public static BlogPostDb Create()
        {
            return new BlogPostDb();
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