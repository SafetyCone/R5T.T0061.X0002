using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Magyar;


namespace R5T.T0061.X0002
{
    public static class ServiceLifetimeExtensions
    {
        public static string ToStringStandard(this ServiceLifetime serviceLifetime)
        {
            return serviceLifetime switch
            {
                ServiceLifetime.Scoped => ServiceLifetimeRepresentations.ScopedStandardRepresentation,
                ServiceLifetime.Singleton => ServiceLifetimeRepresentations.SingletonStandardRepresentation,
                ServiceLifetime.Transient => ServiceLifetimeRepresentations.TransientStandardRepresentation,
                _ => throw EnumerationHelper.UnexpectedEnumerationValueException(serviceLifetime),
            };
        }
    }
}
