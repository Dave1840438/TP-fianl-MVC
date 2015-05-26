namespace ForumDImage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class strLenPhotos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "Titre", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "Titre", c => c.String(nullable: false));
        }
    }
}
