using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Cisco_Script.Model {


    [Serializable()]
    class Cisco_Device {


        #region Attributes

        private string nom, password_enable, password_vty, password_console, banniere, type, domainName;
        private bool domain_lookup, switch_ipv6, route_ipv6;
        private List<Network_Interface> network_Interfaces = new List<Network_Interface>();
        private List<Network_Route> network_Routes = new List<Network_Route>();
        private List<MacAddressTable> mac_tables = new List<MacAddressTable>();
        private List<ICiscoServices> ciscoServices;

        #endregion

        #region Getter/Setter

        public string Nom { get => nom; set => nom = value; }
        public string Password_enable { get => password_enable; set => password_enable = value; }
        public string Password_vty { get => password_vty; set => password_vty = value; }
        public string Password_console { get => password_console; set => password_console = value; }
        public string Banniere { get => banniere; set => banniere = value; }
        public string Type { get => type; set => type = value; }
        public string DomainName { get => domainName; set => domainName = value; }
        public bool Domain_lookup { get => domain_lookup; set => domain_lookup = value; }
        public bool Switch_ipv6 { get => switch_ipv6; set => switch_ipv6 = value; }
        public bool Route_ipv6 { get => route_ipv6; set => route_ipv6 = value; }
        internal List<Network_Interface> Network_Interfaces { get => network_Interfaces; set => network_Interfaces = value; }
        internal List<Network_Route> Network_Routes { get => network_Routes; set => network_Routes = value; }
        internal List<MacAddressTable> Mac_tables { get => mac_tables; set => mac_tables = value; }
        internal List<ICiscoServices> CiscoServices { get => ciscoServices; set => ciscoServices = value; }

        #endregion

        /// <summary>
        /// Default Builder
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_nom"></param>
        /// <param name="_password_enable"></param>
        /// <param name="_password_console"></param>
        /// <param name="_password_vty"></param>
        /// <param name="_banniere"></param>
        /// <param name="_domain_lookup"></param>
        /// <param name="_route_ipv6"></param>
        /// <param name="_switch_ipv6"></param>
        /// <param name="_net_inter"></param>
        /// <param name="_net_routes"></param>
        /// <param name="_mac_list"></param>
        public Cisco_Device(string _type = null, string _nom = null, string _password_enable = null, string _password_console = null, string _password_vty = null,  string _banniere = null, bool _domain_lookup = false, bool _route_ipv6 = false, bool _switch_ipv6 = false, List<Network_Interface> _net_inter = null, List<Network_Route> _net_routes = null, List<MacAddressTable> _mac_list = null) {
            Type = _type;
            Nom = _nom;
            Password_enable = _password_enable;
            Password_console = _password_console;
            Password_vty = _password_vty;
            Banniere = _banniere;
            Domain_lookup = _domain_lookup;

            if(Type == "Router") 
            {
                Route_ipv6 = _route_ipv6;
                Switch_ipv6 = false;
            }
            else 
            {
                Route_ipv6 = false;
                Switch_ipv6 = _switch_ipv6;
            }

            Network_Interfaces = _net_inter;
            Network_Routes = _net_routes;
            Mac_tables = _mac_list;
        }


        public List<string> ToScript()
        {
            CiscoScript cs = new CiscoScript(this);

            return cs.Config;
        }


        #region Serialize

        /// <summary>
        /// GetInfos Serialize
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Type", Type);
            info.AddValue("Nom", Nom);
            info.AddValue("Password_enable", Password_enable);
            info.AddValue("Password_console", Password_console);
            info.AddValue("Password_vty", Password_vty);
            info.AddValue("Banniere", Banniere);
            info.AddValue("Domain_lookup", Domain_lookup);
            info.AddValue("Route_ipv6", Route_ipv6);
            info.AddValue("Switch_ipv6", Switch_ipv6);
            info.AddValue("Network_Interfaces", Network_Interfaces);
            info.AddValue("Network_Routes", Network_Routes);
            info.AddValue("Mac_tables", Mac_tables);
        }


        /// <summary>
        /// Build From Serialize datas
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public Cisco_Device(SerializationInfo info, StreamingContext context) {

            this.Type= (string)info.GetValue("Type", typeof(string));
            this.Nom = (string)info.GetValue("Nom", typeof(string));
            this.Password_enable = (string)info.GetValue("Password_enable", typeof(string));
            this.Password_console = (string)info.GetValue("Password_console", typeof(string));
            this.Password_vty = (string)info.GetValue("Password_vty", typeof(string));
            this.Banniere = (string)info.GetValue("Banniere", typeof(string));
            this.Domain_lookup = (bool)info.GetValue("Domain_lookup", typeof(bool));
            this.Route_ipv6 = (bool)info.GetValue("Route_ipv6", typeof(bool));
            this.Switch_ipv6 = (bool)info.GetValue("Switch_ipv6", typeof(bool));

            this.Network_Interfaces = (List<Network_Interface>)info.GetValue("Network_Interfaces", typeof(List<Network_Interface>));
            this.Network_Routes = (List<Network_Route>)info.GetValue("Network_Routes", typeof(List<Network_Route>));
            this.Mac_tables = (List<MacAddressTable>)info.GetValue("Mac_tables", typeof(List<MacAddressTable>));
        }

        #endregion

    }
}
