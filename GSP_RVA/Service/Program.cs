using Common.Interfaces;
using Service.Services.Autobus;
using Service.Services.Linija;
using Service.Services.Vozac;
using System;
using System.ServiceModel;

namespace Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddressAutobus = new Uri("net.tcp://localhost:8080/AutobusService");
            Uri baseAddressLinija = new Uri("net.tcp://localhost:8081/LinijaService");
            Uri baseAddressVozac = new Uri("net.tcp://localhost:8082/VozacService");

            ServiceHost hostAutobus = new ServiceHost(typeof(AutobusService), baseAddressAutobus);
            ServiceHost hostLinija = new ServiceHost(typeof(LinijaService), baseAddressLinija);
            ServiceHost hostVozac = new ServiceHost(typeof(VozacService), baseAddressVozac);

            try
            {
                // Hosting AutobusService
                hostAutobus.AddServiceEndpoint(typeof(IAutobusService), new NetTcpBinding(), "");
                hostAutobus.Open();
                Console.WriteLine("AutobusService is ready at {0}", baseAddressAutobus);

                // Hosting LinijaService
                hostLinija.AddServiceEndpoint(typeof(ILinijaService), new NetTcpBinding(), "");
                hostLinija.Open();
                Console.WriteLine("LinijaService is ready at {0}", baseAddressLinija);

                // Hosting VozacService
                hostVozac.AddServiceEndpoint(typeof(IVozacService), new NetTcpBinding(), "");
                hostVozac.Open();
                Console.WriteLine("VozacService is ready at {0}", baseAddressVozac);

                Console.WriteLine("Press <Enter> to stop the services.");
                Console.ReadLine();

                hostAutobus.Close();
                hostLinija.Close();
                hostVozac.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                hostAutobus.Abort();
                hostLinija.Abort();
                hostVozac.Abort();
            }
        }
    }
}
