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

        public ServiceProvider(ChannelFactory<IAutobusService> _as, ChannelFactory<ILinijaService> _ls, ChannelFactory<IVozacService> _vs)
        {
            autobusFactory = _as;
            linijaFactory = _ls;
            vozacFactory = _vs;

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
