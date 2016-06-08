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
                new Models.Elevation { ElevationName = "GroupSupervisor" });
            context.SaveChanges();

            context.Specialities.AddOrUpdate(e => e.Name,
                new Models.Specialitate { Name = "Informatica Aplicata" },
                new Models.Specialitate { Name = "Management Informational" });
            context.SaveChanges();

            context.Groups.AddOrUpdate(f=>f.Name,
                new Models.Group { Name = "IA131", Year = Convert.ToInt32(13) == DateTime.Now.Year ? 13 : (DateTime.Now.Year - 13) % 2000 });
            context.SaveChanges();

            context.RegCodes.AddOrUpdate(a => a.code,
                new Models.RegCode { code = "123alpha", GroupId = 1 });
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

            context.Teachers.AddOrUpdate(u => u.Firstname, new Models.Teacher
            {
                Firstname = "Lazu",
                Lastname = "Victoria",
                Grade = "Lector Superior"
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
