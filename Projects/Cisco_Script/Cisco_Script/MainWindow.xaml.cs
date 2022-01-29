using Cisco_Script.Model;
using Cisco_Script.ViewModel;
using Cisco_Script.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Cisco_Script {


    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private CiscoController controller = new CiscoController();


        /* Default Window Builder */
        public MainWindow() {
            InitializeComponent();
            my_datagrid.ItemsSource = this.controller.CiscoDevices;
        }

        /*----------------- < Events Sections > ------------- */

        private void Button_Click(object sender, RoutedEventArgs e) { Menu((sender as Button).Content.ToString()); }

        private void My_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e) { this.controller.CurrentDevice = (sender as Cisco_Device); }

        private void datagridServices_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        
        }

        

        private void Menu(string Action)
        {
            switch (Action)
            {
                case "Ajouter": this.controller.AddDevice(); break;
                case "Modifier": this.controller.ModifyDevice(); break;
                case "Supprimer": this.controller.DeleteDevice(); break;
                case "Vider":  this.Clear();  break;
                case "Presse Papier": MessageBox.Show("TODO"); break;
            }
            this.Refresh_Screen();
        }

        private void Clear()
        {
            if(this.IsClose()) {
                Refresh_Screen();
                this.controller.DeleteAllDevices();
            }
        }

        // Refresh Screen
        private void Refresh_Screen()
        {
            my_datagrid.ItemsSource = null;
            my_datagrid.ItemsSource = this.controller.CiscoDevices;
        }


        #region COMMANDS

        /// <summary>
        /// Command Biding 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Executed(object sender, ExecutedRoutedEventArgs e) { if(IsClose()) this.Clear(); }

        /// <summary>
        /// Close Executed Command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) { if(this.IsClose()) Application.Current.Shutdown(); }

        /// <summary>
        /// Save Executed Command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e) { this.Export(); }

        /// <summary>
        /// Save Executed Command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e) {  this.SaveAsDialog();  }

        /// <summary>
        /// Import Executed Command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Import_Executed(object sender, ExecutedRoutedEventArgs e) { this.Import(); }

        /// <summary>
        /// Display an warning for unsaved data
        /// </summary>
        /// <returns></returns>
        private bool IsClose() {

            MessageBoxResult result = MessageBox.Show("Your data will be forget, Are you sure ?", "Questions ", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) return true;
            else return false;
        }

        #endregion

        #region IMPORT/EXPORT

        private void SaveAsDialog()
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Cisco db (*.csdb)|*.csdb",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "" || saveFileDialog.FileName != null)
            {

                try
                {
                    this.controller.CurrentDB = saveFileDialog.FileName;
                    this.controller.Export();
                }
                catch (Exception e) { MessageBox.Show("Error : " + e.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else { MessageBox.Show("Warning : No selected db ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }

        private void Export()
        {
            if (this.controller.CurrentDB != "" && this.controller.CurrentDB != null)
            {
                try { this.controller.Export(); }
                catch (Exception e) { MessageBox.Show("Error : " + e.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else { this.SaveAsDialog(); }
        }

        private void Import()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Cisco db (*.csdb)|*.csdb",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "" && openFileDialog.FileName != null)
            {

                try
                {
                    this.controller.CurrentDB = openFileDialog.FileName;
                    this.controller.Import();
                    this.Refresh_Screen();
                }
                catch (Exception e) { MessageBox.Show("Error : " + e.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else { MessageBox.Show("Warning : No selected db ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); }

        }

        #endregion

    }
}
