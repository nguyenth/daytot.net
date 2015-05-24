namespace daytot.dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DaytotDbContext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "IsPublic");
        }
    }
}
