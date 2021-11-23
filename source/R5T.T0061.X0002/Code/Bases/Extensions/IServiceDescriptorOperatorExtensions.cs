using System;
using System.Text;

using MicrosoftServiceDescriptor = Microsoft.Extensions.DependencyInjection.ServiceDescriptor;


namespace R5T.T0061.X0002
{
    public static class IServiceDescriptorOperatorExtensions
    {
        /// <summary>
        /// Note: the last implementation of a service type will be the service that is actually provided if multiple implementations for a service type are registered, but only one is requested.
        /// </summary>
        public static string Describe(this IServiceDescriptorOperator _,
            MicrosoftServiceDescriptor serviceDescriptor,
            bool isLastImplementationOfServiceType = true)
        {
            var stringBuilder = new StringBuilder();

            var serviceNameUnmodified = Instances.NamespacedTypeName.GetTypeName(serviceDescriptor.ServiceType.FullName);

            var serviceName = isLastImplementationOfServiceType
                ? $"{Strings.Asterix} {serviceNameUnmodified}"
                : serviceNameUnmodified
                ;

            var serviceTypeStandardRepresentation = serviceDescriptor.GetServiceTypeStandardRepresentation();
            var implementationTypeStandardRepresentation = serviceDescriptor.GetImplementationTypeStandardRepresentation();
            var lifetimeStandardRepresentation = serviceDescriptor.GetLifetimeStandardRepresentation();

            stringBuilder
                .AppendLine("-----")
                .AppendLine(serviceName)
                .AppendLine($"Service Type: {serviceTypeStandardRepresentation}")
                .AppendLine($"Implementation Type: {implementationTypeStandardRepresentation}")
                .AppendLine($"{nameof(serviceDescriptor.Lifetime)}: {lifetimeStandardRepresentation}")
                .AppendLine($"Implementation Instance: {serviceDescriptor.ImplementationInstance?.ToString() ?? "<null>"}")
                .AppendLine($"Implementation Factory: {serviceDescriptor.ImplementationFactory}")
                ;

            var description = stringBuilder.ToString();
            return description;
        }

        public static string GetServiceTypeStandardRepresentation(this IServiceDescriptorOperator _,
            MicrosoftServiceDescriptor serviceDescriptor)
        {
            var output = serviceDescriptor.ServiceType.FullName;
            return output;
        }

        public static string GetImplementationTypeStandardRepresentation(this IServiceDescriptorOperator _,
            MicrosoftServiceDescriptor serviceDescriptor)
        {
            var output = serviceDescriptor.ImplementationType?.FullName ?? NullHelper.StandardStringRepresentation;
            return output;
        }

        public static string GetLifetimeStandardRepresentation(this IServiceDescriptorOperator _,
            MicrosoftServiceDescriptor serviceDescriptor)
        {
            var output = serviceDescriptor.Lifetime.ToStringStandard();
            return output;
        }
    }
}
