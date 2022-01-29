using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cisco_Script.Model
{
    [Serializable()]
    class Network_Interface : ISerializable
    {
        private string name, numero, description, ip, netmask;
        private bool is_active, is_ipv6;

        public string Name { get => name; set => name = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Description { get => description; set => description = value; }
        public string Ip { get => ip; set => ip = value; }
        public string Netmask { get => netmask; set => netmask = value; }
        public bool Is_active { get => is_active; set => is_active = value; }
        public bool Is_ipv6 { get => is_ipv6; set => is_ipv6 = value; }


        /// <summary>
        /// Default Builder 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="numero"></param>
        /// <param name="description"></param>
        /// <param name="ip"></param>
        /// <param name="netmask"></param>
        /// <param name="active"></param>
        /// <param name="ipv6"></param>
        public Network_Interface(string name = null, string numero = null, string description = null, string ip = null, string netmask = null, bool active = false, bool ipv6 = false) {
            Name = name;
            Numero = numero;
            Description = description;
            Ip = ip;
            Netmask = netmask;
            Is_active = active;
            Is_ipv6 = ipv6;
        }


        #region Serialize

        /// <summary>
        /// GetInfos Serialize
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Name", Name);
            info.AddValue("Numero", Numero);
            info.AddValue("Description", Description);
            info.AddValue("Ip", Ip);
            info.AddValue("Netmask", Netmask);
            info.AddValue("Is_active", Is_active);
            info.AddValue("Is_ipv6", Is_ipv6);
        }

        /// <summary>
        /// Build From Serialize datas
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Network_Interface(SerializationInfo info, StreamingContext context) {
           this.Ip = (string)info.GetValue("Ip", typeof(string));
           this.Name = (string)info.GetValue("Name", typeof(string));
           this.Numero = (string)info.GetValue("Numero", typeof(string));
           this.Description = (string)info.GetValue("Description", typeof(string));
           this.Ip = (string)info.GetValue("Ip", typeof(string));
           this.Netmask = (string)info.GetValue("Netmask", typeof(string));
           this.Is_active = (bool)info.GetValue("Is_active", typeof(bool));
           this.Is_ipv6 = (bool)info.GetValue("Is_ipv6", typeof(bool));
        }

        #endregion

    }
}
