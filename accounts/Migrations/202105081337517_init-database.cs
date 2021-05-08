namespace accounts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accountcurrencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account_id = c.Int(nullable: false),
                        Currency_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_id, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.Currency_id, cascadeDelete: true)
                .Index(t => t.Account_id)
                .Index(t => t.Currency_id);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Account_id = c.Int(nullable: false, identity: true),
                        Account_name = c.String(nullable: false),
                        Account_number = c.Int(nullable: false),
                        Main_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Account_id)
                .ForeignKey("dbo.Mainaccounts", t => t.Main_id, cascadeDelete: true)
                .Index(t => t.Main_id);
            
            CreateTable(
                "dbo.Mainaccounts",
                c => new
                    {
                        Main_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Main_id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Currency_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        symbol = c.String(),
                    })
                .PrimaryKey(t => t.Currency_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accountcurrencies", "Currency_id", "dbo.Currencies");
            DropForeignKey("dbo.Accounts", "Main_id", "dbo.Mainaccounts");
            DropForeignKey("dbo.Accountcurrencies", "Account_id", "dbo.Accounts");
            DropIndex("dbo.Accounts", new[] { "Main_id" });
            DropIndex("dbo.Accountcurrencies", new[] { "Currency_id" });
            DropIndex("dbo.Accountcurrencies", new[] { "Account_id" });
            DropTable("dbo.Users");
            DropTable("dbo.Currencies");
            DropTable("dbo.Mainaccounts");
            DropTable("dbo.Accounts");
            DropTable("dbo.Accountcurrencies");
        }
    }
}
