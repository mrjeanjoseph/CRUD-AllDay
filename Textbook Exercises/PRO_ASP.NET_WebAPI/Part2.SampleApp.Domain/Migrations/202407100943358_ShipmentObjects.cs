namespace PingYourPackage.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ShipmentObjects : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.Affliliates",
                c => new
                {
                    Key = c.Guid(nullable: false),
                    CompanyName = c.String(nullable: false, maxLength: 64),
                    PhisicalAddress = c.String(nullable: false, maxLength: 256),
                    PhoneNumber = c.String(maxLength: 16),
                    CreatedOne = c.DateTime(nullable: false),
                })
            .PrimaryKey(t => t.Key)
            .ForeignKey("dbo.Users", t => t.Key)
            .Index(t => t.Key);

            CreateTable("dbo.Shipments",
                c => new
                {
                    Key = c.Guid(nullable: false),
                    AffliateKey = c.Guid(nullable: false),
                    ShipmentTypeKey = c.Guid(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ReceiverName = c.String(nullable: false, maxLength: 64),
                    ReceiverSurname = c.String(nullable: false, maxLength: 64),
                    ReceiverAddress = c.String(nullable: false, maxLength: 64),
                    ReceiverZipCode = c.String(nullable: false, maxLength: 64),
                    ReceiverCity = c.String(nullable: false, maxLength: 64),
                    ReceiverCountry = c.String(nullable: false, maxLength: 64),
                    ReceiverPhoneNumber = c.String(nullable: false, maxLength: 64),
                    ReceiverEmailAddress = c.String(nullable: false, maxLength: 256),
                    CreatedOn = c.DateTime(nullable: false),
                    Affliliate_Key = c.Guid(),
                })
            .PrimaryKey(t => t.Key)
            .ForeignKey("dbo.Affliliates", t => t.Affliliate_Key)
            .ForeignKey("dbo.ShipmentTypes", t => t.ShipmentTypeKey, cascadeDelete: true)
            .Index(t => t.ShipmentTypeKey)
            .Index(t => t.Affliliate_Key);

            CreateTable("dbo.ShipmentStates",
                c => new
                {
                    Key = c.Guid(nullable: false),
                    ShipmentKey = c.Guid(nullable: false),
                    ShipmentStatus = c.Int(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                })
            .PrimaryKey(t => t.Key)
            .ForeignKey("dbo.Shipments", t => t.ShipmentKey, cascadeDelete: true)
            .Index(t => t.ShipmentKey);

            CreateTable("dbo.ShipmentTypes",
                c => new
                {
                    Key = c.Guid(nullable: false),
                    ShipmentName = c.String(nullable: false, maxLength: 64),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CreatedOne = c.DateTime(nullable: false),
                })
            .PrimaryKey(t => t.Key);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Affliliates", "Key", "dbo.Users");
            DropForeignKey("dbo.Shipments", "ShipmentTypeKey", "dbo.ShipmentTypes");
            DropForeignKey("dbo.ShipmentStates", "ShipmentKey", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "Affliliate_Key", "dbo.Affliliates");
            DropIndex("dbo.ShipmentStates", new[] { "ShipmentKey" });
            DropIndex("dbo.Shipments", new[] { "Affliliate_Key" });
            DropIndex("dbo.Shipments", new[] { "ShipmentTypeKey" });
            DropIndex("dbo.Affliliates", new[] { "Key" });
            DropTable("dbo.ShipmentTypes");
            DropTable("dbo.ShipmentStates");
            DropTable("dbo.Shipments");
            DropTable("dbo.Affliliates");
        }
    }
}
