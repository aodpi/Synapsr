using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synapsr.Logistics
{
    public class GroupsManagement
    {
        public static void AddGroupsForCurrentYear(int count,string sp)
        {
            Synapsr.Models.DatabaseStore db = new Models.DatabaseStore();
            for (int i = 0; i < count; i++)
            {
                db.Groups.Add(
                    new Models.Group { Name = sp + (DateTime.Now.Year % 2000).ToString() + (i + 1), Year = 1 });
            }
            db.SaveChanges();
        }
    }
}