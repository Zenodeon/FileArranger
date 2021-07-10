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
using DebugLogger.Wpf;

namespace FileArranger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DirectoryTree selectedDir;
        private DirectoryTree distinationDir;

        private Progress<ScanProgressData> scanProgress = new Progress<ScanProgressData>();
        private ScanProgressData scanProgressData = new ScanProgressData();

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
            SelectedFileCount.Content = "Selected File Count : " + scanProgressData.selectedFileCount;

            if (e.scanDone)
            {
                DLog.Log("Scan Done");
            }
        }      

        private void ScanDirectory(object sender, RoutedEventArgs e)
        {
            ScanDirectory();
        }

        private void ScanDirectory()
        {
            if (selectedDir != null)
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

            LoadCache(CFileInfoCacheHandler.tempCacheLocation[0] , scanProgress);
        }

        public void LoadCache(string cacheLocation, IProgress<ScanProgressData> progress)
        //public async void LoadCache(string cacheLocation, IProgress<ScanProgressData> progress)
        {
            DLog.Log("Function Disabled Temporarily");
            /*
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
            */
        }

        private void ChooseFolder(object sender, RoutedEventArgs e)
        {
            selectedDir = DirectoryAction.ShowFolderDialog();

            DLog.Log("Selected Directory : " + selectedDir.directoryPath);
        }

        private void SetDistination(object sender, RoutedEventArgs e)
        {
            distinationDir = DirectoryAction.ShowFolderDialog();

            DLog.Log("Distination Directory : " + distinationDir.directoryPath);
        }

        private void CopyToDistination(object sender, RoutedEventArgs e)
        {
            selectedDir.TransferContentTo(distinationDir);
        }

        private void MoveToDistination(object sender, RoutedEventArgs e)
        {

        }

        private void TestSiteSetup(object sender, RoutedEventArgs e)
        {
            //Site
            selectedDir = new DirectoryTree(@"D:\TestSite\Site1");
            DLog.Log("Selected Directory : " + selectedDir.directoryPath);

            //Dump
            distinationDir = new DirectoryTree(@"D:\TestSite\TestSiteDump\MainDump"); 
            DLog.Log("Distination Directory : " + distinationDir.directoryPath);

            ScanDirectory();
        }
    }
}
