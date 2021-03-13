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
    /// Logique d'interaction pour AjouterRoute.xaml
    /// </summary>
    public partial class AjouterRoute : Window
    {
        private Network_Route nR;
        internal Network_Route NR { get => nR; set => nR = value; }

        public AjouterRoute() { InitializeComponent(); }

        private void Button_Click(object sender, RoutedEventArgs e) { Menu((sender as Button).Content.ToString()); }

        private void Menu(string action)
        {
            switch (action)
            {
                case "Confirmer": Add_route(); break;
                case "Annuler": this.Close(); break;
            }
        }

        private void Add_route()
        {
            if (textboxip.Text != "" && textboxcidr.Text != "" && textboxinter.Text != "")
            {
                NR = new Network_Route(textboxip.Text, textboxinter.Text, textboxcidr.Text);
                this.Close();
            }
        }
    }
}
