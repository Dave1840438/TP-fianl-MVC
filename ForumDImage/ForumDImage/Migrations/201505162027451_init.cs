namespace ForumDImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commentaire = c.String(nullable: false, maxLength: 300),
                        Utilisateur_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id)
                .Index(t => t.Utilisateur_Id);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomUtilisateur = c.String(nullable: false, maxLength: 20),
                        MotDePasse = c.String(nullable: false, maxLength: 20),
                        NomComplet = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "Utilisateur_Id", "dbo.Utilisateurs");
            DropIndex("dbo.Photos", new[] { "Utilisateur_Id" });
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Photos");
        }
    }
}
