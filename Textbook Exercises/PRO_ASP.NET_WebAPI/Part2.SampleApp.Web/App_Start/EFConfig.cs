using System.Data.Entity.Migrations;

namespace PingYourPackage.Web
{
    public class EFConfig
    {
        public static void Initialize()
        {
            RunMigrations();
        }

        private static void RunMigrations()
        {
            var efMigrationSettings = new Domain.Migrations.Configuration();
            var efMigrator = new DbMigrator(efMigrationSettings);
            efMigrator.Update();
        }
    }
}
