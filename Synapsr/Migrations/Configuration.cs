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
                new Models.Elevation { ElevationName = "Supervisor" },
                new Models.Elevation { ElevationName = "Student" },
                new Models.Elevation { ElevationName = "Teacher" },
                new Models.Elevation { ElevationName = "GroupSupervisor" },
                new Models.Elevation { ElevationName = "Gues" });
            context.SaveChanges();

            context.Specialities.AddOrUpdate(e => e.Name,
                new Models.Specialitate { Name = "Informatica Aplicata" },
                new Models.Specialitate { Name = "Management Informational" });
            context.SaveChanges();

            context.Groups.AddOrUpdate(f => f.Name,
                new Models.Group { Name = "IA131", Year = Convert.ToInt32(13) == DateTime.Now.Year ? 1 : (DateTime.Now.Year - 13) % 2000 },
                new Models.Group { Name = "IA151", Year = Convert.ToInt16(15) == DateTime.Now.Year ? 1 : (DateTime.Now.Year - 15) % 2000 });
            context.SaveChanges();

            context.RegCodes.AddOrUpdate(a => a.code,
                new Models.RegCode { code = "123alpha", GroupId = 1 },
                new Models.RegCode { code = "alpha_omegaqwe", GroupId = 1 });
            context.SaveChanges();

            context.Users.AddOrUpdate(u => u.UserName,
                new Models.User
                {
                    UserName = "aodpi",
                    Password = "YWNlNjU1MTc4ZjIwNTc5Y2E3Y2E0M2U3NDA5NTI2OTYwMmNhOTA2Yg==",
                    IdSpecialitate = 1,
                    avatar_uri = "/male.png",
                    ElevationId = 3,
                    FirstName = "Valeriu",
                    LastName = "Balan",
                    Email = "balan.valeriu@live.com",
                    Sex = "Male",
                    GroupId = 1,
                });
            context.SaveChanges();

            context.Users.AddOrUpdate(usr => usr.UserName,
                new Models.User
                {
                    FirstName = "Guest",
                    LastName = "Guest",
                    Password = "yoloswag",
                    Email = "guest@guest.com",
                    IdSpecialitate = 1,
                    GroupId = 1,
                    ElevationId = 1,
                    UserName = "guest",
                    avatar_uri = "/male.png",
                    Sex = "Male"
                });
            context.SaveChanges();

            context.Users.AddOrUpdate(usr => usr.UserName,
            new Models.User
            {
                FirstName = "Victoria",
                LastName = "Lazu",
                Password = "123",
                Email = "guest@guest.com",
                IdSpecialitate = 1,
                GroupId = 1,
                ElevationId = 3,
                UserName = "tchr",
                avatar_uri = "/female.png",
                Sex = "Female"
            });
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
