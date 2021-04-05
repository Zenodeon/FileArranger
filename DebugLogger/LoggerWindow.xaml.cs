using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FileArranger.DebugLogger
{
    /// <summary>
    /// Interaction logic for LoggerWindow.xaml
    /// </summary>
    public partial class LoggerWindow : Window
    {
        public LoggerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogList.Items.Clear();
        }

        public void NewLog(string log)
        {
            Frame frame = new Frame();
            frame.Width = Width;

            LogMessage message = new LogMessage(log);
            frame.Content = message;

            LogList.Items.Add(frame);
        }
    }
}
