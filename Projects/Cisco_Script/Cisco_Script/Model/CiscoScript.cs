using System;
using System.Collections.Generic;
using System.IO;

namespace Cisco_Script.Model
{
    class CiscoScript {

        private List<string> config = new List<string>();
        private Cisco_Device cisco;

        #region GETTER/SETTER

        public List<string> Config { get => config; set => config = value; }
        internal Cisco_Device Cisco { get => cisco; set => cisco = value; }

        #endregion

        /// <summary>
        /// Default Builder
        /// </summary>
        public CiscoScript(Cisco_Device _cisco = null) {
            this.Cisco = _cisco;
        }


        #region CiscoHelper

        private void EnterModeConf(bool password=false) {
            Config.Add("enable");
            if(password) Config.Add(this.cisco.Password_enable);
            Config.Add("configure terminal");
        }

        private void Logout()
        {
            Config.Add("end");
            Config.Add("copy run start");
            Config.Add(" ");
            Config.Add("exit");
        }

        #endregion


        #region BASIC_CONFG

        public void SetHostname() { 
            Config.Add("hostname " + this.cisco.Nom); 
        }

        private void NoIpDomainLookup() { 
            if(!cisco.Domain_lookup) 
                Config.Add("no ip domain-lookup");
        }

        private void SetClock()
        {
            DateTime moment = new DateTime();
            Config.Add("clock set " + moment.Hour + ":" + moment.Minute + ":" + moment.Second + " " + moment.Day + " " + moment.Month + " " + moment.Year);
            this.Logout();
        }

        private void SetBanner() { 
            Config.Add("banner motd #" + this.cisco.Banniere + " #"); 
        }

        public void SetDomain() { 
            Config.Add("ip domain-name " + this.cisco.DomainName); 
        }

        public void ShutdownPort(string _portStart, int _portEnd, string _typeInterface)
        {
            Config.Add("int range " + _typeInterface + " " + _portStart + "-" + _portEnd);
            Config.Add("shutdown");
        }

        #endregion

        #region VLAN_CONFIG

        public void SetVlanIP(int _vlan, string _ip, string _netmask)
        {
            Config.Add("interface vlan " + _vlan);
            Config.Add("ip address " + _ip + " " + _netmask);
            Config.Add("no shutdown");
            Config.Add("exit");
        }

        public void SetVlanName(string _vlanName, int _vlanNumber)
        {
            Config.Add("vlan " + _vlanNumber);
            Config.Add("name " + _vlanName);
            Config.Add("no shutdown");
            Config.Add("exit");
        }

        #endregion

        #region AUTH_CONFIG

        public void SetPasswordConsole()
        {
            Config.Add("line console 0");
            Config.Add("password " + this.cisco.Password_console);
            Config.Add("login");
            Config.Add("exit");
        }

        public void SetPasswordVty()
        {
            Config.Add("line vty 0 15");
            Config.Add("password " + this.cisco.Password_vty);
            Config.Add("login");
            Config.Add("exit");
        }

        public void SetPasswordEnable() { 
            Config.Add("enable secret " + this.cisco.Password_enable); 
        }

        public void CreateUser(string _user, string _password) { 
            Config.Add("username " + _user + " secret " + _password);
        }


        #endregion

        #region CISCO_SERVICES

        public void EnableSSH(string _user, string _password, int _keySize)
        {
            this.SetDomain();
            this.CreateUser(_user, _password);
            Config.Add("ip ssh version 2");
            Config.Add("crypto key generate rsa general-keys modulus " + _keySize);
        }

        public void SetOnlySSH() {
            Config.Add("line vty 0 15");
            Config.Add("transport input ssh");
            Config.Add("login local");
            Config.Add("exit");
        }

        public void SetSSHMaxRetries(int _max_retries) { 
            Config.Add("ip ssh auth " + _max_retries); 
        }

        public void SetSSHTime(int _time) { 
            Config.Add("ip ssh time " + _time); 
        }
        
        public void ServicePasswordEncryption() {
            Config.Add("service password-encryption"); 
        }

        #endregion

        #region AUTO_CONFIG

        public void AutoConfig(int _managementVlan, string _ip, string _netmask, string _user, string _passwordUser, int _keySize, int _max_ssh_retries, int _ssh_time)
        {
            this.EnterModeConf();
            this.SetHostname();
            this.NoIpDomainLookup();
            this.SetBanner();

            this.SetPasswordVty();
            this.SetPasswordEnable();

            this.SetVlanIP(_managementVlan, _ip, _netmask);

            this.EnableSSH(_user, _passwordUser, _keySize);
            this.SetOnlySSH();
            this.SetSSHMaxRetries(_max_ssh_retries);
            this.SetSSHTime(_ssh_time);

            this.SetPasswordConsole();
            this.ServicePasswordEncryption();
            this.Logout();
        }

        #endregion

    }
}
