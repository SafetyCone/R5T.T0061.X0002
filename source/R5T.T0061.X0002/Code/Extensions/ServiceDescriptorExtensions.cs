using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.T0061.X0002
{
    public static class ServiceDescriptorExtensions
    {
        /// <summary>
        /// Get services by service type name, but preserving the order of implementation types of the same service type.
        /// This is important, since only the last service type for an implementation will be provided by the service provider if multiple implementations for the same service are registered and a single service is requested (as opposed to an IEnumerable of the service, in which case all implementation types will be provided).
        /// </summary>
        public static Dictionary<string, List<ServiceDescriptor>> GetServicesByServiceNamePreservingImplementationTypeOrder(this IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            var serviceDescriptorsByServiceTypeFullName = new Dictionary<string, List<ServiceDescriptor>>();

            foreach (var serviceDescriptor in serviceDescriptors)
            {
                var serviceFullName = serviceDescriptor.ServiceType.FullName;

                var serviceDescriptorsForServiceTypeFullName = serviceDescriptorsByServiceTypeFullName.AcquireValue(serviceFullName, () => new List<ServiceDescriptor>());

                serviceDescriptorsForServiceTypeFullName.Add(serviceDescriptor);
            }

            return serviceDescriptorsByServiceTypeFullName;
        }

        /// <summary>
        /// Orders services alphabetically by service type name, but preserves the order of implementation types of the same service type.
        /// This is important, since only the last service type for an implementation will be provided by the service provider if multiple implementations for the same service are registered and a single service is requested (as opposed to an IEnumerable of the service, in which case all implementation types will be provided).
        /// </summary>
        public static IEnumerable<ServiceDescriptor> OrderAlphabeticallyByServiceNamePreservingImplementationTypeOrder(this IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            var serviceDescriptorsByServiceTypeFullName = serviceDescriptors.GetServicesByServiceNamePreservingImplementationTypeOrder();

            var output = serviceDescriptorsByServiceTypeFullName
                .OrderAlphabetically(xPair => Instances.NamespacedTypeName.GetTypeName(xPair.Key))
                .SelectMany(xPair => xPair.Value)
                ;

            return output;
        }

        /// <summary>
        /// Chooses <see cref="OrderAlphabeticallyByServiceNamePreservingImplementationTypeOrder(IEnumerable{ServiceDescriptor})"/> as the default order-by-service.
        /// </summary>
        public static IEnumerable<ServiceDescriptor> OrderByService(this IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            var output = serviceDescriptors.OrderAlphabeticallyByServiceNamePreservingImplementationTypeOrder();
            return output;
        }

        public static string GetServiceTypeStandardRepresentation(this ServiceDescriptor serviceDescriptor)
        {
            var output = Instances.ServiceDescriptorOperator.GetServiceTypeStandardRepresentation(serviceDescriptor);
            return output;
        }

        public static string GetImplementationTypeStandardRepresentation(this ServiceDescriptor serviceDescriptor)
        {
            var output = Instances.ServiceDescriptorOperator.GetImplementationTypeStandardRepresentation(serviceDescriptor);
            return output;
        }

        public static string GetLifetimeStandardRepresentation(this ServiceDescriptor serviceDescriptor)
        {
            var output = Instances.ServiceDescriptorOperator.GetLifetimeStandardRepresentation(serviceDescriptor);
            return output;
        }

        public static ServiceDescriptorDescription ToServiceDescriptorDescription(this ServiceDescriptor serviceDescriptor)
        {
            var serviceTypeStandardRepresentation = serviceDescriptor.GetServiceTypeStandardRepresentation();
            var implementationTypeStandardRepresentation = serviceDescriptor.GetImplementationTypeStandardRepresentation();

            var serviceDescriptorDescription = new ServiceDescriptorDescription
            {
                ImplementationType = implementationTypeStandardRepresentation,
                ServiceType = serviceTypeStandardRepresentation,
                ServiceLifetime = serviceDescriptor.Lifetime,
            };
            return serviceDescriptorDescription;
        }
    }
}
