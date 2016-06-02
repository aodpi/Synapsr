using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Synapsr.Logistics
{
    public class TimeTable:IDisposable
    {
        private class Rootobject
        {
            public string oldDate { get; set; }
            public Event[] events { get; set; }
        }

        public class Event:IEquatable<Event>
        {
            public string obiect { get; set; }
            public string zi { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public string link { get; set; }
            public string long_name { get; set; }
            public string auditoriu { get; set; }
            public string profesor { get; set; }
            public bool impara { get; set; }

            public bool Equals(Event other)
            {
                return zi.Equals(other.zi) && obiect.Equals(other.obiect) && start_date.Equals(other.start_date) && end_date.Equals(other.end_date) ? true : false;
            }
        }

        private List<Event> _events = new List<Event>();
        private string _filename = string.Empty;
        public TimeTable(string filename)
        {
            _filename = filename;
            string json = System.IO.File.ReadAllText(filename);
            _events = JsonConvert.DeserializeObject<Rootobject>(json).events.ToList();
        }

        private void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(new Rootobject { events = Events.ToArray() }, Formatting.Indented);
            System.IO.File.WriteAllText(_filename, json);
        }
        public void AddEvent(Event ev)
        {
            if (_events.Contains(ev))
                return;
            _events.Add(ev);
            SaveChanges();
        }

        public void Dispose()
        {
            Events.Clear();
            _events.Clear();
        }

        public List<Event> Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
            }
        }
    }
}