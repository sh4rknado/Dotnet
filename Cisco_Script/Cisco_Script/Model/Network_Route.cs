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
    class Network_Route : ISerializable {

        private string ip, next_ip, netmask;

        public string Ip { get => ip; set => ip = value; }
        public string Next_ip { get => next_ip; set => next_ip = value; }
        public string Netmask { get => netmask; set => netmask = value; }

        /// <summary>
        /// Default  Builder
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="next_ip"></param>
        /// <param name="netmask"></param>
        public Network_Route(string ip = null, string next_ip = null, string netmask = null)
        {
            Ip = ip;
            Next_ip = next_ip;
            Netmask = netmask;
        }

        #region Serialize

        /// <summary>
        /// GetInfos Serialize
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Ip", Ip);
            info.AddValue("Next_Ip", Next_ip);
            info.AddValue("Netmask", Netmask);
        }
        
        /// <summary>
        /// Build From Serialize datas
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Network_Route(SerializationInfo info, StreamingContext context)
        {
            this.Ip = (string)info.GetValue("Ip", typeof(string));
            this.Next_ip = (string)info.GetValue("Next_Ip", typeof(string));
            this.Netmask = (string)info.GetValue("Netmask", typeof(string));

        }
        
        #endregion
    }
}
