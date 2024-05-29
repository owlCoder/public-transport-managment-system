using Common.Contracts;
using Common.Interfaces;
using Service.Loggers;
using Service.Services.AutobusService;
using Service.Services.LinijaService;
using Service.Services.VozacLinijeService;
using Service.Services.VozacService;
using System;
using System.ServiceModel;

namespace Service
{
    public class Program
    {
        public static ILogger logger { get; } = new FileLogger();

        static void Main(string[] args)
        {
            Uri baseAddressAutobus = new Uri("net.tcp://localhost:9080/AutobusService");
            Uri baseAddressLinija = new Uri("net.tcp://localhost:9081/LinijaService");
            Uri baseAddressVozac = new Uri("net.tcp://localhost:9082/VozacService");
            Uri baseAddressVozaciLinije = new Uri("net.tcp://localhost:9083/VozaciLinijeService");

            ServiceHost hostAutobus = new ServiceHost(typeof(AutobusService), baseAddressAutobus);
            ServiceHost hostLinija = new ServiceHost(typeof(LinijaService), baseAddressLinija);
            ServiceHost hostVozac = new ServiceHost(typeof(VozacService), baseAddressVozac);
            ServiceHost hostVozaciLinije = new ServiceHost(typeof(VozacLinijaService), baseAddressVozaciLinije);

            try
            {
                // Configure bindings
                NetTcpBinding binding = new NetTcpBinding();
                binding.MaxReceivedMessageSize = 2147483647; // Set max received message size to 2GB

                // Hosting AutobusService
                hostAutobus.AddServiceEndpoint(typeof(IAutobusService), binding, "");
                hostAutobus.Open();
                Console.WriteLine("AutobusService is ready at {0}", baseAddressAutobus);

                // Hosting LinijaService
                hostLinija.AddServiceEndpoint(typeof(ILinijaService), binding, "");
                hostLinija.Open();
                Console.WriteLine("LinijaService is ready at {0}", baseAddressLinija);

                // Hosting VozacService
                hostVozac.AddServiceEndpoint(typeof(IVozacService), binding, "");
                hostVozac.Open();
                Console.WriteLine("VozacService is ready at {0}", baseAddressVozac);

                // Hosting VozacService
                hostVozaciLinije.AddServiceEndpoint(typeof(IVozacLinijaService), binding, "");
                hostVozaciLinije.Open();
                Console.WriteLine("VozacLinijeService is ready at {0}", baseAddressVozac);

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
