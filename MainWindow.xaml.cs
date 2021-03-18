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
        public bool fileDialogOpen = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!fileDialogOpen)
                ShowFileDialog();
        }

        private async void ShowFileDialog()
        {
            fileDialogOpen = true;

            await Task.Run(() =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog();

                fileDialog.ValidateNames = false;
                fileDialog.CheckFileExists = false;
                fileDialog.CheckPathExists = true;

                fileDialog.FileName = "Folder Path";

                if (fileDialog.ShowDialog() == true)
                {
                    fileDialogOpen = false;
                    ShowBox(fileDialog.FileName);
                }
                else
                {
                    fileDialogOpen = false;
                    MessageBox.Show("what");
                }
            });
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
