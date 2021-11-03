using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.T0061.X0002
{
    public class ServiceDescriptorDescription
    {
        public string ServiceType { get; set; }
        public string ImplementationType { get; set; }
        public ServiceLifetime ServiceLifetime { get; set; }
    }
}
