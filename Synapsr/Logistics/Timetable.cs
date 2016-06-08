using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Synapsr.Logistics
{
    public class TimeTable:IDisposable
    {
        #region SerializationStructure
        private class Rootobject
        {
            public string oldDate { get; set; }
            public string type { get; set; }
            public List<GroupSubject> events { get; set; }
        }

        public class GroupSubject
        {
            public string groupname { get; set; }
            public List<Event> evs { get; set; }
            public List<Event> evsi { get; set; }
        }
        public class Event:IEquatable<Event>
        {
            public string obiect { get; set; }
            public string zi { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public string long_name { get; set; }
            public string auditoriu { get; set; }
            public string profesor { get; set; }
            [JsonIgnore]
            public string grname { get; set; }
            [JsonIgnore]
            public bool isodd { get; set; }
            public bool Equals(Event other)
            {
                return zi.Equals(other.zi) && obiect.Equals(other.obiect) && start_date.Equals(other.start_date) && end_date.Equals(other.end_date) ? true : false;
            }
        }
        #endregion

        #region Fields
        private Rootobject _obj;
        private string _groupname = string.Empty;
        private string _filename = string.Empty;
        public static string timeformat = "MM/dd/yy HH:mm:00";
        #endregion

        #region Methods
        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(_obj, Formatting.Indented);
            System.IO.File.WriteAllText(_filename, json);
        }

        public void DeleteEvent(int id,bool isodd=false)
        {
            if (isodd)
                _obj.events.First(f => f.groupname == _groupname).evsi.RemoveAt(id);
            else
                _obj.events.First(f => f.groupname == _groupname).evs.RemoveAt(id);
            SaveChanges();
        }
        public void Dispose()
        {
            Events = null;
        }
        public void AddEvent(Event ev,bool isodd=false)
        {
            if (isodd)
            {
                if (_obj.events.First(f=>f.groupname==_groupname).evsi.Contains(ev))
                    return;
                _obj.events.First(f => f.groupname == _groupname).evsi.Add(ev);
                _obj.events.First(f => f.groupname == _groupname).evsi.Sort((a, b) => string.Compare(a.zi, b.zi));
            }
            else
            {
                if (_obj.events.First(f => f.groupname == _groupname).evs.Contains(ev))
                    return;
                _obj.events.First(f => f.groupname == _groupname).evs.Add(ev);
                _obj.events.First(f => f.groupname == _groupname).evs.Sort((a, b) => string.Compare(a.zi, b.zi));
            }
            SaveChanges();
        }
        #endregion
        
        #region Properties
        public string TipSaptamina
        {
            get
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    var diff = DateTime.Now.Subtract(DateTime.Parse(_obj.oldDate)).Days;
                    if (diff == 7)
                    {
                        _obj.oldDate = DateTime.Now.ToString(timeformat);
                        _obj.type = _obj.type == "pară" ? "impară" : "pară";
                        SaveChanges();
                    }
                }
                return _obj.type;
            }
            private set
            {
                _obj.type = value;
                SaveChanges();
            }
        }

        public List<Event> EventsI
        {
            get
            {
                var result = _obj.events.First(f => f.groupname == _groupname).evsi;
                return result;
            }
            set
            {
                _obj.events.First(f=>f.groupname==_groupname).evsi = value;
            }
            
        }
        public List<Event> Events
        {
            get
            {
                return _obj.events.First(f => f.groupname == _groupname).evs;
            }
            set
            {
                _obj.events.First(f=>f.groupname==_groupname).evs = value;
            }
        }
        #endregion

        private void BuildGroups()
        {
            var ls = new Models.DatabaseStore().Groups.ToList();
            foreach (var item in ls)
            {
                if (_obj.events.FirstOrDefault(f => f.groupname == item.Name) == null)
                {
                    _obj.events.Add(new GroupSubject { groupname = item.Name, evs = new List<Event>(), evsi = new List<Event>() });
                }
            }
            SaveChanges();
        }
        public TimeTable(string filename,string grname)
        {
            _filename = filename;
            _groupname = grname;
            string json = System.IO.File.ReadAllText(filename);
            _obj = JsonConvert.DeserializeObject<Rootobject>(json);
            BuildGroups();
        }
    }
}