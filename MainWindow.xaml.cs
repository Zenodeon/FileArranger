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
            DebugC.Initialize();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedDir = ShowFileDialog();

            if (selectedDir.vaild)
                labelS.Content = selectedDir.path;
            else
            {
                labelS.Content = "none";
                MessageBox.Show($"File Directory - { selectedDir.path } does not exist");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (selectedDir.vaild)
                selectedDir.ScanDirectory();
        }

        private DirectoryInventory ShowFileDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.ValidateNames = false;
            fileDialog.CheckFileExists = false;
            fileDialog.CheckPathExists = true;

            fileDialog.FileName = "Folder Path";

            bool choosed;
            string filePath = "";

            if (fileDialog.ShowDialog() == true)
            {
                choosed = true;
                filePath = ValidataPath(fileDialog.FileName);
            }
            else
                choosed = false;

            return new DirectoryInventory(filePath, choosed);
        }

        public void ShowBox(string path)
        {
            path = ValidataPath(path);

            MessageBox.Show(path);
        }

        private string ValidataPath(string path)
        {
            List<string> pathOrder = path.Split(@"\").ToList();

            string fileName = pathOrder[pathOrder.Count - 1];

            if (fileName.Contains('.') | fileName == "Folder Path")
                path = path.Substring(0, path.Length - (fileName.Length + 1));

            //string texts = "";
            //foreach (string text in pathOrder)
            //    texts += "|" + text + "| ";

            return path;
        }

       
    }
}
