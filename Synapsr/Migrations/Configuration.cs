namespace Synapsr.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Synapsr.Models.DatabaseStore>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Synapsr.Models.DatabaseStore context)
        {
            //  This method will be called after migrating to the latest version.
            context.Elevations.AddOrUpdate(e => e.ElevationName,
                new Models.Elevation { ElevationName = "Supervisor" });
            context.SaveChanges();
            context.Users.AddOrUpdate(u => u.UserName,
                new Models.User { UserName = "aodpi", Password = Security.Encryption.Sha1Encode("aodpiram1994"), avatar_uri = "/deepcoil.jpg", ElevationId=1 });
            context.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
