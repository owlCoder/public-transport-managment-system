﻿using Common.Interfaces;
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

            // Configure the binding
            NetTcpBinding binding = new NetTcpBinding();
            binding.MaxReceivedMessageSize = 2147483647; // Set max received message size to 2GB

            // Create channel factory for AutobusService
            ChannelFactory<IAutobusService> af = new ChannelFactory<IAutobusService>
            (
                binding,
                new EndpointAddress("net.tcp://localhost:9080/AutobusService")
            );

            // Create channel factory for LinijaService
            ChannelFactory<ILinijaService> lf = new ChannelFactory<ILinijaService>
            (
                binding,
                new EndpointAddress("net.tcp://localhost:9081/LinijaService")
            );

            // Create channel factory for VozacService
            ChannelFactory<IVozacService> vf = new ChannelFactory<IVozacService>
            (
                binding,
                new EndpointAddress("net.tcp://localhost:9082/VozacService")
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
