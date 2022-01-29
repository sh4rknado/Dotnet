using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [AttributeUsage(AttributeTargets.All)]
    public class DatabaseAttribute : Attribute
    {
        public DatabaseAttribute() { }

        public DatabaseAttribute(string dbName) 
        {
            this.DatabaseName = dbName;
        }

        public string DatabaseName { get; set; }

    }
}
