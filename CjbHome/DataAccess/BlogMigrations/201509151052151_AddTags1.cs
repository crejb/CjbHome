namespace CjbHome.DataAccess.BlogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTags1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CjbHome.TagBlogPosts",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        BlogPost_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.BlogPost_Id })
                .ForeignKey("CjbHome.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("CjbHome.BlogPosts", t => t.BlogPost_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.BlogPost_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("CjbHome.TagBlogPosts", "BlogPost_Id", "CjbHome.BlogPosts");
            DropForeignKey("CjbHome.TagBlogPosts", "Tag_Id", "CjbHome.Tags");
            DropIndex("CjbHome.TagBlogPosts", new[] { "BlogPost_Id" });
            DropIndex("CjbHome.TagBlogPosts", new[] { "Tag_Id" });
            DropTable("CjbHome.TagBlogPosts");
        }
    }
}
