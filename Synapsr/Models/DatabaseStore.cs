using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Synapsr.Models
{
    public class DatabaseStore : DbContext
    {
        public DatabaseStore() : base("name=SConn")
        {
        }
        public DbSet<Elevation> Elevations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Specialitate> Specialities { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<RegCode> RegCodes { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}