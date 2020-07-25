using GeoLib.Contracts;
using GeoLib.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace GeoLib.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));
            //string address = "net.tcp://localhost:8009/GeoService";
            //Binding binding = new NetTcpBinding();

            string address = "http://localhost:8009/GeoService";
            Binding binding = new WSHttpBinding();

            Type contract = typeof(IGeoService);
            hostGeoManager.AddServiceEndpoint(contract, binding, address);


            hostGeoManager.Open();
            
            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
