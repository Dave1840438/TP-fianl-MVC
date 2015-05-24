namespace ForumDImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitrePhotoAjoute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "Titre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "Titre");
        }
    }
}
