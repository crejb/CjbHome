namespace CjbHome.DataAccess.BlogPostMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLinkImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "HeaderImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPosts", "HeaderImageUrl");
        }
    }
}
