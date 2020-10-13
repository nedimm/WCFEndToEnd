using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using GeoLib.Contracts;
using GeoLib.Proxies;
using GeoLib.Client.Contracts;

namespace GeoLib.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Title = "UI Running on Thread " + Thread.CurrentThread.ManagedThreadId +
                " | Process " + Process.GetCurrentProcess().Id.ToString();
        }

        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text != "")
            {
                GeoClient proxy = new GeoClient("tcpEP");
                
                ZipCodeData data = proxy.GetZipInfo(txtZipCode.Text);
                if (data != null)
                {
                    lblCity.Content = data.City;
                    lblState.Content = data.State;
                }

                proxy.Close();
            }
        }

        private void btnGetZipCodes_Click(object sender, RoutedEventArgs e)
        {
            if (txtState.Text != null)
            {
                EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                NetTcpBinding binding = new NetTcpBinding();
                binding.MaxReceivedMessageSize = 2000000;

                GeoClient proxy = new GeoClient(binding, address);
                IEnumerable<ZipCodeData> data = proxy.GetZips(txtState.Text);
                if (data != null)
                    lstZips.ItemsSource = data;

                proxy.Close();
            }
        }

        private void btnMakeCall_Click(object sender, RoutedEventArgs e)
        {
            //ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
            //IMessageService proxy = factory.CreateChannel();
            //proxy.ShowMsg(txtMessage.Text);

            //factory.Close();

            EndpointAddress address = new EndpointAddress("net.tcp://localhost:8010/MessageService");
            Binding binding = new NetTcpBinding();

            ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>(binding, address);
            IMessageService proxy = factory.CreateChannel();
            proxy.ShowMsg(txtMessage.Text);
            factory.Close();

        }
    }
}
