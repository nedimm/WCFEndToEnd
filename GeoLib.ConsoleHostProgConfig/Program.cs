using GeoLib.Contracts;
using GeoLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.ConsoleHostProgConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));
            string address = "net.tcp://localhost:8009/GeoService";
            Binding binding = new NetTcpBinding();

            //string address = "http://localhost:8009/GeoService";
            //Binding binding = new WSHttpBinding();

            Type contract = typeof(IGeoService);
            hostGeoManager.AddServiceEndpoint(contract, binding, address);

            hostGeoManager.Open();

            Console.WriteLine($"Service (programmatic config) started on \n\n'{hostGeoManager.Description.Endpoints[0].Address}'. \n\nPress [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
