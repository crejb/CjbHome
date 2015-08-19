namespace CjbHome.DataAccess.BlogPostMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBlogPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "LinkText", c => c.String(nullable: false));
            AlterColumn("dbo.BlogPosts", "Content", c => c.String(nullable: false));
            DropColumn("dbo.BlogPosts", "IsDraft");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogPosts", "IsDraft", c => c.Boolean(nullable: false));
            AlterColumn("dbo.BlogPosts", "Content", c => c.String());
            DropColumn("dbo.BlogPosts", "LinkText");
        }
    }
}
