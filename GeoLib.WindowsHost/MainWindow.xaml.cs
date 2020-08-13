using GeoLib.Services;
using GeoLib.WindowsHost.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Windows;

namespace GeoLib.WindowsHost
{
    public partial class MainWindow : Window
    {
        public static MainWindow MainUI { get; set; }
        ServiceHost _hostGeoManager = null;
        ServiceHost _hostMessageManager = null;

        public MainWindow()
        {
            InitializeComponent();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;

            MainUI = this;

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
        }
                
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager = new ServiceHost(typeof(GeoManager));
            _hostGeoManager.Open();

            _hostMessageManager = new ServiceHost(typeof(MessageManager));
            _hostMessageManager.Open();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _hostGeoManager.Close();
            _hostMessageManager.Close();

            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            lblMessage.Content = message + Environment.NewLine + $" (shown on thread {threadId} | Process {Process.GetCurrentProcess().Id.ToString()})";
        }
    }
}
