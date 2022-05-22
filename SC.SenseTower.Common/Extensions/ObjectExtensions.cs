using System.Reflection;

namespace SC.SenseTower.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, string?> ToDictionary(this object source, string parentName = "", BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => parentName + propInfo.Name,
                propInfo => (string?)propInfo.GetValue(source, null)
            );

        }
    }
}
