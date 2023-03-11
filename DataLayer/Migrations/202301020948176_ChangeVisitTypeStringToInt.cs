namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVisitTypeStringToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Blogs.Blog", "Visit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("Blogs.Blog", "Visit", c => c.String());
        }
    }
}
