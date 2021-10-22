using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Demmo_ApiPixIntegration.Models;


namespace Demmo_ApiPixIntegration.Services
{
    public class APIPixFactory
    {

        public static IAPIPixService CreateAPIPixService(string campaignId , IConfiguration configuration, APIPixSettings options)
        {
            switch (campaignId)
            {
                case "DEMMOGN":
                    return FactoryGN(configuration, options);
                    break;
                default:
                    return null;
                    break;
            }
        }

        private static GerenciaNetService FactoryGN(IConfiguration configuration, APIPixSettings options)
        {
            var service = new GerenciaNetService(configuration, options);
            return service;
        }
    }
}
