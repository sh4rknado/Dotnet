using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Logique d'interaction pour Ajouter_NI.xaml
    /// </summary>
    public partial class Ajouter_NI : Window
    {
        private Network_Interface ni;
        internal Network_Interface Ni { get => ni; set => ni = value; }

        public Ajouter_NI()
        {
            InitializeComponent();
            combo_name.Items.Add("FastEthernet");
            combo_name.Items.Add("GigabitEthernet");
            combo_name.Items.Add("Ethernet");
        }

        /* Manage Event */
        private void Textbox_number_KeyDown(object sender, KeyEventArgs e)
        {
            switch ((sender as TextBox).Name)
            {
                case "textbox_number":
                    if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Divide) e.Handled = false;
                    else e.Handled = true;
                    break;
                case "Textbox_ip":
                    if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal || e.Key == Key.OemQuestion) e.Handled = false;
                    else e.Handled = true;
                    break;
                case "Textbox_Netmask":
                    if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Decimal) e.Handled = false;
                    else e.Handled = true;
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) { MenuAction((sender as Button).Content.ToString()); }

        private void MenuAction(string action)
        {
            switch (action)
            {
                case "Confirmer": if (Verify()) Ni = new Network_Interface(combo_name.Text, textbox_number.Text, TextboxDescription.Text, Textbox_ip.Text, Textbox_Netmask.Text, Convert.ToBoolean(active_interface.IsChecked), Convert.ToBoolean(ipv6_check_box.IsChecked)); this.Close(); break;
                case "Réinitialiser": Reinit(); break;
                case "Annuler": this.Close(); break;
            }
        }

        private void Reinit()
        {
            textbox_number.Text = "";
            Textbox_Netmask.Text = "";
            TextboxDescription.Text = "";
            Textbox_ip.Text = "";
            TextboxDescription.Text = "";
        }

        private bool Verify()
        {
            bool check = false;
            if (combo_name.Text != "" && Textbox_ip.Text != "" && textbox_number.Text != "" && Textbox_Netmask.Text != "" && TextboxDescription.Text != "") check = true;
            return check;
        }
    }
}
