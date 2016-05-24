using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synapsr.Extensions
{
    /// <summary>
    /// Applying Singleton pattern to access our DatabaseContext.
    /// Because why creating multiple instances when you can create only one when needed.
    /// Don't waste memory baby.
    /// </summary>
    public sealed class DBInstance
    {
        private static volatile Synapsr.Models.DatabaseStore instance;
        private static object syncRoot = new object();
        private DBInstance() { }

        public static Models.DatabaseStore Current
        {
            get
            {
                if (instance==null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Models.DatabaseStore();
                    }
                }
                return instance;
            }
        }

    }
}