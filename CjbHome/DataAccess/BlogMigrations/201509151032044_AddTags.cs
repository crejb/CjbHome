namespace CjbHome.DataAccess.BlogMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CjbHome.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("CjbHome.Tags");
        }
    }
}
