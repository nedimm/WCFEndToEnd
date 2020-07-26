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
            
            Console.WriteLine($"Service (App.config) started on \n\n'{hostGeoManager.Description.Endpoints[0].Address}'. \n\nPress [Enter] to exit.");
            Console.ReadLine();

            hostGeoManager.Close();
        }
    }
}
