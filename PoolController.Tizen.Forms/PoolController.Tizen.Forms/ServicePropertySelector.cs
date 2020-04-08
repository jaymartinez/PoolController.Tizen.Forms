using System.Reflection;
using Autofac.Core;

namespace PoolController.Tizen.Forms
{
    public class ServicePropertySelector : IPropertySelector
    {
        bool IPropertySelector.InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            if (propertyInfo.GetCustomAttribute(typeof(InjectAttribute)) != null)
                return true;

            return false;
        }
    }
}
