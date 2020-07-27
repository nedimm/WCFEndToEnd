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
            
            hostGeoManager.Open();

            Console.WriteLine($"Service (App.config) started on:\n");
            foreach (var endpoint in hostGeoManager.Description.Endpoints)
            {
                Console.WriteLine($"{endpoint.Address}");
            }

            Console.WriteLine("\nPress [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
