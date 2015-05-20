namespace ForumDImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photosModifies : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "Commentaire", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "Commentaire", c => c.String(nullable: false, maxLength: 300));
        }
    }
}
