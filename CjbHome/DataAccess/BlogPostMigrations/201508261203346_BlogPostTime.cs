namespace CjbHome.DataAccess.BlogPostMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogPostTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "PostDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPosts", "PostDate");
        }
    }
}
