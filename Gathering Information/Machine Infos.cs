using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace Gathering_information
{
    class InfosMachine
    {

        private string IP_External = new WebClient().DownloadString("https://api.ipify.org"),
                       IP_Address = Dns.GetHostEntry(Environment.MachineName).AddressList[0].ToString(),
                       Mac_Address = "",
                       Nic_Name = "", UserName = Environment.UserName.ToString(),
                       Netbios_Name = Environment.MachineName,
                       WindowsMajorVersion = Environment.OSVersion.Version.Major.ToString(),
                       WindowsServicesPack = "",
                       WindowsArchitecture = "";
        
        public InfosMachine() { initializeInfos(); }

        private void initializeInfos()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    Mac_Address = nic.GetPhysicalAddress().ToString();
                    Nic_Name = nic.Name.ToString();
                    break;
                }
            }

            if (Environment.OSVersion.ServicePack.ToString() == "") WindowsServicesPack = "Service pack Windows : Aucun service pack installé";
            else WindowsServicesPack = "Service pack Windows : " + Environment.OSVersion.ServicePack.ToString();
            if (Environment.Is64BitOperatingSystem) WindowsArchitecture ="Architecture : Windows 64 bit ";
            else WindowsArchitecture = "Architecture : Windows 32 bit ";
        }

        public string GetAllInfos()
        {
            string infos = "Information Collectées :\n"
                         + "Nom d'utilisateur  : " + UserName + "\n"
                         + "Addresse IP publique : " + IP_External
                         + "Addresse IP locale : " + IP_Address
                         + "Addresse Mac : " + Mac_Address
                         + "Nom de la carte réseaux : " + Nic_Name
                         + "Nom Netbios  : " + Netbios_Name
                         + "Version Majeur windows : " + WindowsMajorVersion
                         + WindowsServicesPack
                         + WindowsArchitecture;
            return infos;
        }

        public List<string> GetInfosList()
        {
            List<string> mylist = new List<string>();
            mylist.Add("Information Collectées :");
            mylist.Add("Nom d'utilisateur  : " + UserName);
            mylist.Add("Addresse IP publique : " + IP_External);
            mylist.Add("Addresse IP locale : " + IP_Address);
            mylist.Add("Addresse Mac : " + Mac_Address);
            mylist.Add("Nom de la carte réseaux : " + Nic_Name);
            mylist.Add("Nom Netbios  : " + Netbios_Name);
            mylist.Add("Version Majeur windows : " + WindowsMajorVersion);
            mylist.Add(WindowsServicesPack);
            mylist.Add(WindowsArchitecture);
            return mylist;
        }
    }
}