namespace ForumDImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableVotesnomee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo_Id = c.Int(nullable: false),
                        Utilisateur_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photos", t => t.Photo_Id, cascadeDelete: true)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id, cascadeDelete: true)
                .Index(t => t.Photo_Id)
                .Index(t => t.Utilisateur_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Utilisateur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Votes", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.Votes", new[] { "Utilisateur_Id" });
            DropIndex("dbo.Votes", new[] { "Photo_Id" });
            DropTable("dbo.Votes");
        }
    }
}
