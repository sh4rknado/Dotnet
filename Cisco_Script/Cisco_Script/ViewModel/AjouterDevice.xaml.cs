using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cisco_Script.Model;

namespace Cisco_Script.ViewModel
{
    /// <summary>
    /// Logique d'interaction pour AjouterDevice.xaml
    /// </summary>
    public partial class AjouterDevice : Window
    {
        private List<Network_Interface> listeNi = new List<Network_Interface>();
        private List<Network_Route> routing_list = new List<Network_Route>();
        private Network_Route routing_list_old = new Network_Route();
        private bool modify = false;
        private Cisco_Device cisco_dev= new Cisco_Device();
        private Network_Interface ni = new Network_Interface();
        private Network_Interface ni_old = new Network_Interface();
        private List<MacAddressTable> Mac_address_List = new List<MacAddressTable>();

        internal List<Network_Interface> ListeNi { get => listeNi; set => listeNi = value; }
        internal Cisco_Device Cisco_dev { get => cisco_dev; set => cisco_dev = value; }
        public bool Modify { get => modify; set => modify = value; }
        internal List<Network_Route> Routing_list { get => routing_list; set => routing_list = value; }
        internal Network_Route Routing_list_old { get => routing_list_old; set => routing_list_old = value; }

        public AjouterDevice()
        {
            InitializeComponent();
            ComboDevice.Items.Add("Router");
            ComboDevice.Items.Add("Switch");
            dataGrid_interface.ItemsSource = ListeNi;
            Datagrid_Route.ItemsSource = Routing_list;
        }

        /* Events Manage */
        private void Button_Click(object sender, RoutedEventArgs e) { Menu((sender as Button).Content.ToString()); }
        private void ComboDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboDevice.SelectedItem.ToString() == "Router")
            {
                router_ipv6.IsEnabled = true;
                switch_ipv6.IsEnabled = false;
                ajouterbtn.IsEnabled = true;
                modifierbtn.IsEnabled = true;
                clonerbtn.IsEnabled = true;
                supprimerbtn.IsEnabled = true;
                AjouterRouteBtn.IsEnabled = true;
                modifRouteBtn.IsEnabled = true;
                suppRouteBtn.IsEnabled = true;
            }
            else
            {
                router_ipv6.IsEnabled = false;
                switch_ipv6.IsEnabled = true;
                ajouterbtn.IsEnabled = false;
                modifierbtn.IsEnabled = false;
                clonerbtn.IsEnabled = false;
                supprimerbtn.IsEnabled = false;
                AjouterRouteBtn.IsEnabled = false;
                modifRouteBtn.IsEnabled = false;
                suppRouteBtn.IsEnabled = false;
            }
        }
        private void RefreshScreen()
        {
            dataGrid_interface.ItemsSource = null;
            dataGrid_interface.ItemsSource = ListeNi;
            Datagrid_Route.ItemsSource = null;
            Datagrid_Route.ItemsSource = Routing_list;
            dataGrid_interface.SelectedIndex = 0;
            Datagrid_Route.SelectedIndex = 0;
        }
        private void DataGrid_interface_SelectionChanged(object sender, SelectionChangedEventArgs e) { ni = dataGrid_interface.SelectedItem as Network_Interface; }

        /* Méthodes of Programs */
        private void Menu(string action)
        {
            switch (action)
            {
                case "Ajouter route": Add_route(); break;
                case "Modifier route":
                    Routing_list_old = Datagrid_Route.SelectedItem as Network_Route;
                    if (Add_route()) Routing_list.Remove(Routing_list_old); RefreshScreen(); break;
                case "Supprimer route":
                    Routing_list_old = Datagrid_Route.SelectedItem as Network_Route;
                    Routing_list.Remove(Routing_list_old); RefreshScreen(); break;
                case "Cloner": ListeNi.Add(ni); RefreshScreen(); break;
                case "Ajouter": AjouterNi(); break;
                case "Modifier":
                    ni_old = dataGrid_interface.SelectedItem as Network_Interface;
                    if (ni != null) if (AjouterNi()) Delete_device(); break;
                case "Supprimer":
                    ni_old = dataGrid_interface.SelectedItem as Network_Interface;
                    Delete_device(); break;
                case "Confirmer": if (Verify()) Add_device(); break;
                case "Annuler": Annuler(); break;
                case "Réinitialiser": Reinit(); break;
            }
        }

        private bool Add_route()
        {
            bool check = false;
            AjouterRoute ajr = new AjouterRoute();
            ajr.ShowDialog();
            if (ajr.NR != null)
            {
                Routing_list.Add(ajr.NR);
                check = true;
            }
            RefreshScreen();
            return check;
        }

        private bool AjouterNi()
        {
            bool ajouter = false;
            Ajouter_NI ajouter_ni = new Ajouter_NI();
            ajouter_ni.ShowDialog();

            if (ajouter_ni.Ni != null)
            {
                ListeNi.Add(ajouter_ni.Ni);
                ajouter = true;
            }

            RefreshScreen();

            return ajouter;
        }

        private bool Verify()
        {
            bool Is_verify = false;
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            int element = 0;
            Network_Interface nc = new Network_Interface();


            if (pass1.Password != "" && pass1.Password.Count() > 0 && pass2.Password != "" && pass2.Password.Count() > 0 && pass3.Password != "" &&
                pass3.Password.Count() > 0 && banniere_textbox.Text != "" && hostname_texbox.Text != "" && regexItem.IsMatch(hostname_texbox.Text))
            {
                if (ListeNi.Count == 0 && ComboDevice.SelectedItem.ToString() == "Router") MessageBox.Show("Aucune Interface du routeur est introduite !\nVeuillez corriger le probleme !", "Erreur d'interface", MessageBoxButton.OK, MessageBoxImage.Error);
                if (ListeNi.Count > 0)
                {
                    if (ListeNi.Count == 1) return true;
                    foreach (Network_Interface ni_temp in ListeNi)
                    {
                        element++;
                        if (element == 1) nc = ni_temp;
                        else if (ni_temp.Numero == nc.Numero)
                        {
                            MessageBox.Show("Pluseurs Interfaces du routeur ont le même port !\nVeuillez corriger le probleme !", "Erreur d'interface", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                        else Is_verify = true;
                    }
                }
                else if (ComboDevice.SelectedItem.ToString() == "Switch") return true;
            }
            return Is_verify;
        }

        private void Add_device()
        {
            Cisco_dev = new Cisco_Device(ComboDevice.SelectedItem.ToString(), hostname_texbox.Text, pass1.Password, pass2.Password, pass3.Password,
                                         banniere_textbox.Text, domain_lookup.IsChecked.HasValue, router_ipv6.IsChecked.HasValue, switch_ipv6.IsChecked.HasValue,
                                         ListeNi, Routing_list, Mac_address_List);
            this.Close();
            RefreshScreen();
        }

        private void Reinit()
        {
            MessageBoxResult Msr = MessageBox.Show("Est-ce que vous êtes sur de vouloir réinitialiser ?\n Cela implique la perte de toutes les donnée y compris les interfaces !", "Confirmation de suppression", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (Msr == MessageBoxResult.Yes)
            {
                if (ComboDevice.SelectedItem.ToString() == "Router")
                {
                    router_ipv6.IsEnabled = true;
                    switch_ipv6.IsEnabled = false;
                }
                else
                {
                    router_ipv6.IsEnabled = false;
                    switch_ipv6.IsEnabled = true;
                }

                domain_lookup.IsChecked = false;
                router_ipv6.IsChecked = false;
                switch_ipv6.IsChecked = false;

                banniere_textbox.Text = "";
                hostname_texbox.Text = "";
                pass1.Password = "";
                pass2.Password = "";
                pass3.Password = "";

                dataGrid_interface.ItemsSource = null;
            }
        }

        private void Annuler()
        {
            if (!Modify)
            {
                MessageBoxResult Msr = MessageBox.Show("Est-ce que vous êtes sur de vouloir Annuler ?\n Cela implique la perte de toutes les donnée y compris les interfaces !", "Confirmation d'annulation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Msr == MessageBoxResult.Yes) this.Close();
            }
            else this.Close();
        }

        private void Delete_device() { ListeNi.Remove(ni_old); RefreshScreen(); }
    }
}
