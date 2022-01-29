using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class DBConfig
    {
        [JsonProperty("ip", Required = Required.Always)]
        public string IP { get; set; }

        [JsonProperty("username", Required = Required.Always)]
        public string Username { get; set; }

        [JsonProperty("db_name", Required = Required.Always)]
        public string  DBName { get; set; }

        [JsonProperty("password", Required = Required.Always)]
        public string Password { get; set; }

        [JsonProperty("port")]
        public uint Port { get; set; }

        [JsonProperty("type", Required = Required.Always)]
        public DBType Type { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is DBConfig cfg)
            {
                return
                (this.DBName == cfg.DBName) &&
                (this.IP == cfg.IP) &&
                (this.Password == cfg.Password) &&
                (this.Port == cfg.Port) &&
                (this.Username == cfg.Username) &&
                (this.Type == cfg.Type);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
