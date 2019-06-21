namespace ERP_testTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        Year = c.Int(nullable: false),
                        Director = c.String(),
                        PosterURL = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Movies", new[] { "UserId" });
            DropTable("dbo.Movies");
        }
    }
}
