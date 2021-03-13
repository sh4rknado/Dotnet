using System;
using System.Collections.Generic;
using System.IO;

namespace Cisco_Script.Model
{
    class CiscoScript {

        private List<string> config = new List<string>();
        private string scriptFilePath = null;

        #region GETTER/SETTER

        public string ScriptFilePath { get => scriptFilePath; set => scriptFilePath = value; }

        #endregion

        #region CiscoHelper

        private void EnterModeConf()
        {
            config.Add("enable");
            config.Add("configure terminal");
        }

        private void EnterModeConf(string _password)
        {
            config.Add("enable");
            config.Add(_password);
            config.Add("configure terminal");
        }

        private void Logout()
        {
            config.Add("end");
            config.Add("copy run start");
            config.Add(" ");
            config.Add("exit");
        }

        #endregion

        #region BASIC_CONFG

        public void SetHostname(string _hostname) { config.Add("hostname " + _hostname); }

        private void NoIpDomainLookup() { config.Add("no ip domain-lookup"); }

        private void SetClock()
        {
            DateTime moment = new DateTime();
            config.Add("clock set " + moment.Hour + ":" + moment.Minute + ":" + moment.Second + " " + moment.Day + " " + moment.Month + " " + moment.Year);
            this.Logout();
        }

        private void SetBanner(string _banner) { config.Add("banner motd #" + _banner + " #"); }

        public void SetDomain(string _domain) { config.Add("ip domain-name " + _domain); }

        public void ShutdownPort(string _portStart, int _portEnd, string _typeInterface)
        {
            config.Add("int range " + _typeInterface + " " + _portStart + "-" + _portEnd);
            config.Add("shutdown");
        }

        #endregion

        #region VLAN_CONFIG

        public void SetVlanIP(int _vlan, string _ip, string _netmask)
        {
            config.Add("interface vlan " + _vlan);
            config.Add("ip address " + _ip + " " + _netmask);
            config.Add("no shutdown");
            config.Add("exit");
        }

        public void SetVlanName(string _vlanName, int _vlanNumber)
        {
            config.Add("vlan " + _vlanNumber);
            config.Add("name " + _vlanName);
            config.Add("no shutdown");
            config.Add("exit");
        }

        #endregion

        #region AUTH_CONFIG

        public void SetPasswordConsole(string _password)
        {
            config.Add("line console 0");
            config.Add("password " + _password);
            config.Add("login");
            config.Add("exit");
        }

        public void SetPasswordVty(string _password)
        {
            config.Add("line vty 0 15");
            config.Add("password " + _password);
            config.Add("login");
            config.Add("exit");
        }

        public void SetPasswordEnable(string _password) { config.Add("enable secret " + _password); }

        public void CreateUser(string _user, string _password) { config.Add("username " + _user + " secret " + _password); }


        #endregion

        #region CISCO_SERVICES

        public void EnableSSH(string _domain, string _user, string _password, int _keySize)
        {
            this.SetDomain(_domain);
            this.CreateUser(_user, _password);
            config.Add("ip ssh version 2");
            config.Add("crypto key generate rsa general-keys modulus " + _keySize);
        }
        public void SetOnlySSH()
        {
            config.Add("line vty 0 15");
            config.Add("transport input ssh");
            config.Add("login local");
            config.Add("exit");
        }
        public void SetSSHMaxRetries(int _max_retries) { config.Add("ip ssh auth " + _max_retries); }
        public void SetSSHTime(int _time) { config.Add("ip ssh time " + _time); }
        public void ServicePasswordEncryption() { config.Add("service password-encryption"); }

        #endregion

        #region AUTO_CONFIG

        public void AutoConfig(string _hostname, string _banner, string _passwordConsole, string _passwordVty, string _passwordEnable, int _managementVlan, string _ip, string _netmask, string _user, string _passwordUser, string _domainName, int _keySize, int _max_ssh_retries, int _ssh_time)
        {
            this.EnterModeConf();
            this.SetHostname(_hostname);
            this.NoIpDomainLookup();
            this.SetBanner(_banner);

            this.SetPasswordVty(_passwordVty);
            this.SetPasswordEnable(_passwordEnable);

            this.SetVlanIP(_managementVlan, _ip, _netmask);

            this.EnableSSH(_domainName, _user, _passwordUser, _keySize);
            this.SetOnlySSH();
            this.SetSSHMaxRetries(_max_ssh_retries);
            this.SetSSHTime(_ssh_time);

            this.SetPasswordConsole(_passwordConsole);
            this.ServicePasswordEncryption();
            this.Logout();
        }

        #endregion

        #region WRITE/READ

        public void Savingconfig()
        { 
            if(CheckConfig()) {
                try
                {
                    StreamWriter Sw = new StreamWriter(this.ScriptFilePath);

                    foreach (string s in config) Sw.WriteLine(s);
                    
                    Sw.Flush();
                    Sw.Close();
                    Sw.Dispose();
                }
                catch(Exception e) { throw new Exception(e.Message.ToString()); }
            }
            else throw new Exception("The ScriptFilePath is not set !"); 
        }

        public void ReadingConfig() {

            if (CheckConfig()) {
                try {

                    StreamReader Sr = new StreamReader(this.ScriptFilePath);

                    while (!Sr.EndOfStream) config.Add(Sr.ReadLine());

                    Sr.Close();
                    Sr.Dispose();
                }
                catch (Exception e) { throw new Exception(e.Message.ToString()); }
            }
            else throw new Exception("The ScriptFilePath is not set !");

        }

        private bool CheckConfig()
        {
            if (this.ScriptFilePath != "" && this.ScriptFilePath != null) return true;
            else { return false; }
        }

        #endregion

    }
}
