using System;
using System.Collections.Generic;


namespace R5T.T0061.X0002
{
    public class ServiceDescriptorDescriptionServiceTypeEqualityComparer : EqualityComparer<ServiceDescriptorDescription>
    {
        public override bool Equals(ServiceDescriptorDescription x, ServiceDescriptorDescription y)
        {
            var output = x.ServiceType == y.ServiceType;
            return output;
        }

        public override int GetHashCode(ServiceDescriptorDescription obj)
        {
            var output = obj.ServiceType.GetHashCode();
            return output;
        }
    }
}
