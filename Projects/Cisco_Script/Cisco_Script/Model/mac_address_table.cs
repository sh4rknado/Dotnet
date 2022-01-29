using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Cisco_Script.Model {

    [Serializable()]
    class MacAddressTable {

        /// <summary>
        /// Attributs
        /// </summary>
        private string macAddress, vlan, interfaceName;

        #region GetterSetter

        /// <summary>
        /// Getter / Setter
        /// </summary>
        public string MacAddress { get => macAddress; set => macAddress = value; }
        
        /// <summary>
        /// Getter / Setter
        /// </summary>
        public string Vlan { get => vlan; set => vlan = value; }

        /// <summary>
        /// Getter / Setter
        /// </summary>
        public string InterfaceName { get => interfaceName; set => interfaceName = value; }

        #endregion

        /// <summary>
        /// Default Builder
        /// </summary>
        /// <param name="_mac"></param>
        /// <param name="_vlan"></param>
        /// <param name="_interfaceName"></param>
        public MacAddressTable(string _mac = null, string _vlan = null, string _interfaceName = null) {
            MacAddress = _mac;
            Vlan = _vlan;
            InterfaceName = _interfaceName;
        }

        #region Serialize

        /// <summary>
        /// GetInfos Serialize
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("MacAddress", MacAddress);
            info.AddValue("Vlan", Vlan);
            info.AddValue("InterfaceName", InterfaceName);
        }

        /// <summary>
        /// Build From Serialize datas
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public MacAddressTable(SerializationInfo info, StreamingContext context)
        {
            this.MacAddress = (string)info.GetValue("MacAddress", typeof(string));
            this.Vlan = (string)info.GetValue("Vlan", typeof(string));
            this.InterfaceName = (string)info.GetValue("InterfaceName", typeof(string));
        }

        #endregion

    }
}
