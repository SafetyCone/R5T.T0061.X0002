﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using R5T.Magyar.IO;


namespace R5T.T0061.X0002
{
    public static class IServiceCollectionOperatorExtensions
    {
        public static void DescribeToTextFileSynchronous(this IServiceCollectionOperator _,
            string textFilePath,
            IServiceCollection services)
        {
            var stringBuilder = new StringBuilder();

            _.DescribeToStringBuilder(
                stringBuilder,
                services);

            var text = stringBuilder.ToString();

            using var fileWriter = StreamWriterHelper.NewWrite(textFilePath);

            fileWriter.WriteLine(text);
        }

        public static async Task DescribeToTextFile(this IServiceCollectionOperator _,
            string textFilePath,
            IServiceCollection services)
        {
            var stringBuilder = new StringBuilder();

            _.DescribeToStringBuilder(
                stringBuilder,
                services);

            var text = stringBuilder.ToString();

            using var fileWriter = StreamWriterHelper.NewWrite(textFilePath);

            await fileWriter.WriteLineAsync(text);
        }

        public static void DescribeToStringBuilder(this IServiceCollectionOperator _,
            StringBuilder stringBuilder,
            IServiceCollection services)
        {
            var serviceCount = services.Count;

            stringBuilder.AppendLine($"Services count: {serviceCount}\nNote: services with '{Strings.Asterix}' are the last implementation type for services with multiple implementation types.\n\n");

            var servicesByServiceName = services.GetServicesByServiceNamePreservingImplementationTypeOrder();

            var servicesByServiceNameOrderedByServiceName = servicesByServiceName
                .OrderAlphabetically(xPair => Instances.NamespacedTypeName.GetTypeName(xPair.Key))
                ;

            foreach (var pair in servicesByServiceNameOrderedByServiceName)
            {
                var implementationsForService = pair.Value;

                var implementationCount = implementationsForService.Count;
                if (implementationCount > 1)
                {
                    var currentImplementationCounter = 1;

                    foreach (var serviceDescriptor in implementationsForService)
                    {
                        if (currentImplementationCounter == implementationCount)
                        {
                            var description = Instances.ServiceDescriptorOperator.Describe(serviceDescriptor, true);

                            stringBuilder.AppendLine(description);
                        }
                        else
                        {
                            var description = Instances.ServiceDescriptorOperator.Describe(serviceDescriptor, false);

                            stringBuilder.AppendLine(description);

                            currentImplementationCounter++;
                        }
                    }
                }
                else
                {
                    // Only one implementation for the service.
                    var serviceDescriptor = implementationsForService.Single();

                    var description = Instances.ServiceDescriptorOperator.Describe(serviceDescriptor, false);

                    stringBuilder.AppendLine(description);
                }
            }
        }

        public static IEnumerable<ServiceDescriptorDescription> GetDescriptions(this IServiceCollectionOperator _,
            IServiceCollection services)
        {
            var output = services.Select(xService => xService.ToServiceDescriptorDescription());
            return output;
        }
    }
}