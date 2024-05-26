using Common.Interfaces;
using System;
using System.ServiceModel;

namespace Client.Provider
{
    public sealed class ServiceProvider : IDisposable
    {
        public static IAutobusService AutobusService { get; private set; }

        public static ILinijaService LinijaService { get; private set; }

        public static IVozacService VozacService { get; private set; }

        private static ChannelFactory<IAutobusService> autobusFactory { get; set; }

        private static ChannelFactory<ILinijaService> linijaFactory { get; set; }

        private static ChannelFactory<IVozacService> vozacFactory { get; set; }

        static ServiceProvider()
        {
            #region CREATE FACTORIES FOR SERVICE PROVIDER

            // Create channel factory for AutobusService
            ChannelFactory<IAutobusService> af = new ChannelFactory<IAutobusService>
            (
                new NetTcpBinding(),
                "net.tcp://localhost:9080/AutobusService"
            );

            // Create channel factory for LinijaService
            ChannelFactory<ILinijaService> lf = new ChannelFactory<ILinijaService>
            (
                new NetTcpBinding(),
                "net.tcp://localhost:9081/LinijaService"
            );

            // Create channel factory for VozacService
            ChannelFactory<IVozacService> vf = new ChannelFactory<IVozacService>
            (
                new NetTcpBinding(),
                "net.tcp://localhost:9082/VozacService"
            );
            #endregion

            autobusFactory = af;
            linijaFactory = lf;
            vozacFactory = vf;

            // Create a WCF communication channel
            AutobusService = autobusFactory.CreateChannel();
            LinijaService = linijaFactory.CreateChannel();
            VozacService = vozacFactory.CreateChannel();
        }

        // Clean up unused resources
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (autobusFactory != null)
                autobusFactory.Close();

            if (linijaFactory != null)
                linijaFactory.Close();

            if (vozacFactory != null)
                vozacFactory.Close();
        }
    }
}
