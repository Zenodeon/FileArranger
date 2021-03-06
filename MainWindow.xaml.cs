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

        private Progress<TransferProgressData> transferProgress = new Progress<TransferProgressData>();

        public MainWindow()
        {
            InitializeComponent();
            DLog.Instantiate();

            scanProgress.ProgressChanged += ShowScanDetails;
            transferProgress.ProgressChanged += ShowTransferDetails;
        }

        private void OnClosingWindow(object sender, CancelEventArgs e)
        {
            DLog.Close();
        }

        private void ShowScanDetails(object sender, ScanProgressData e)
        {
            DLog.Log("Bug Test Before : " + scanProgressData.directoriesCount);
            scanProgressData += e;
            DLog.Log("Bug Test After : " + scanProgressData.directoriesCount);

            DirCount.Content = "Directory Count : " + scanProgressData.directoriesCount;
            FileCount.Content = "File Count : " + scanProgressData.fileCount;
            SelectedFileCount.Content = "Selected File Count : " + scanProgressData.selectedFileCount;

            if (e.scanDone)
            {
                DLog.Log("Scan Done");
                DLog.Log("Directory Count : " + selectedDir.totalSubDirectoryCount);
                DLog.Log("File Count : " + selectedDir.totalSubFileCount);
                DLog.Log("Selected File Count : " + selectedDir.totalSelectedSubFileCount);
            }
        }

        private void ShowTransferDetails(object sender, TransferProgressData TD)
        {
            Bar2.Value = TD.dataTransferPercentage;
            Bar1.Value = TD.fileTransferPercentage;
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

                    selectedDir.ScanDirectory(true, scanProgress);
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
            selectedDir.TransferContentTo(distinationDir, transferProgress);
        }

        private void MoveToDistination(object sender, RoutedEventArgs e)
        {

        }

        private void TestSite1Setup(object sender, RoutedEventArgs e)
        {
            //Site
            selectedDir = new DirectoryTree(@"D:\TestSite\Site1");
            DLog.Log("Selected Directory : " + selectedDir.directoryPath);

            //Dump
            distinationDir = new DirectoryTree(@"D:\TestSite\TestSiteDump\MainDump"); 
            DLog.Log("Distination Directory : " + distinationDir.directoryPath);

            distinationDir.DeleteContent();

            ScanDirectory();
        }

        private void TestSite2Setup(object sender, RoutedEventArgs e)
        {
            //Site
            selectedDir = new DirectoryTree(@"D:\TestSite\Site2");
            DLog.Log("Selected Directory : " + selectedDir.directoryPath);

            //Dump
            distinationDir = new DirectoryTree(@"D:\TestSite\TestSiteDump\MainDump");
            DLog.Log("Distination Directory : " + distinationDir.directoryPath);

            distinationDir.DeleteContent();

            ScanDirectory();
        }
    }
}
