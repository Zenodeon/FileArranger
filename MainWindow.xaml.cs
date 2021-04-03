using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;

namespace FileArranger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryInventory selectedDir;

        public MainWindow()
        {
            DebugLog.Instantiate();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedDir = ShowFileDialog();

            labelS.Content = selectedDir.path;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (selectedDir.vaild)
                selectedDir.ScanDirectory();
        }

        private DirectoryInventory ShowFileDialog()
        {
            VistaFolderBrowserDialog fileDialog = new VistaFolderBrowserDialog();

            bool choosed;

            if (fileDialog.ShowDialog().Value)
                choosed = true;
            else
                choosed = false;

            return new DirectoryInventory(fileDialog.SelectedPath, choosed);
        }       
    }
}
