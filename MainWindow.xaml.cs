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
using DebugLogger.Wpf;

namespace FileArranger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryTree selectedDir;

        private Progress<ScanProgressData> scanProgress = new Progress<ScanProgressData>();
        private ScanProgressData scanProgressData = new ScanProgressData();

        //private MediaInfoCacheHandler cacheHandler = new MediaInfoCacheHandler();

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
            scanProgressData += e;

            DirCount.Content = "Directory Count : " + scanProgressData.directoriesCount;
            FileCount.Content = "File Count : " + scanProgressData.fileCount;

            if (e.scanDone)
            {
                DLog.Log("Scan Done");
            }
        }

        private void ChooseFolder(object sender, RoutedEventArgs e)
        {
            selectedDir = ShowFolderDialog();

            //DLog.Log("Selected Directory : " + selectedDir.directoryPath);
        }

        private void ScanDirectory(object sender, RoutedEventArgs e)
        {
            if (selectedDir.vaild)
            {
                scanProgressData = new ScanProgressData();   

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                selectedDir.ScanDirectory(true, scanProgress, true);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private void LoadCache(object sender, RoutedEventArgs e)
        {
            scanProgressData = new ScanProgressData();

            LoadCache(MediaInfoCacheHandler.tempCacheLocation[0] , scanProgress);
        }

        public async void LoadCache(string cacheLocation, IProgress<ScanProgressData> progress)
        {
            await Task.Run(() =>
            {
                ScanProgressData scanData = new ScanProgressData();

                var files = Directory.GetFiles(cacheLocation).ToList();
                scanData.fileCount += files.Count;

                foreach (string file in files)
                {
                    DLog.Log("Cache : " + file);
                    new MediaFile(file).mediaInfo.MakeCache();
                }

                progress.Report(scanData);
            });
        }

        private DirectoryTree ShowFolderDialog()
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
