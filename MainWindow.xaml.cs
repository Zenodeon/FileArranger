using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
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
using FileArranger.DebugLogger;

namespace FileArranger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryTree selectedDir;

        private Progress<ScanProgressData> scanProgress = new Progress<ScanProgressData>();

        public MainWindow()
        {
            InitializeComponent();
            DLog.Instantiate();

            scanProgress.ProgressChanged += UpdateScanDetails;
        }

        private void OnClosingWindow(object sender, CancelEventArgs e)
        {
            DLog.Close();
        }

        private void UpdateScanDetails(object sender, ScanProgressData e)
        {
            DirCount.Content = "Directory Count : " + e.directoriesCount;
            FileCount.Content = "File Count : " + e.fileCount;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedDir = ShowFileDialog();

            DLog.Log("Selected Directory : " + selectedDir.directoryPath);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (selectedDir.vaild)
                selectedDir.ScanDirectory(true, scanProgress);
        }

        private DirectoryTree ShowFileDialog()
        {
            VistaFolderBrowserDialog fileDialog = new VistaFolderBrowserDialog();

            bool choosed;

            if (fileDialog.ShowDialog().Value)
                choosed = true;
            else
                choosed = false;

            return new DirectoryTree(fileDialog.SelectedPath, choosed);
        }       
    }
}
