namespace CjbHome.DataAccess.BlogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CjbHome.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkText = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        PostDate = c.DateTime(nullable: false),
                        PostTime = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                        HeaderImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("CjbHome.BlogPosts");
        }
    }
}
