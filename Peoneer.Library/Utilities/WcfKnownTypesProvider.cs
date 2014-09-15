using System;
using System.Linq;
using System.Reflection;
using Peoneer.Library.Messages;

namespace Peoneer.Library.Utilities
{
    public static class WcfKnownTypesProvider
    {
        public static readonly Type BaseType = typeof (TimeStampedMessage);

        public static Type[] LoadKnownMessages(ICustomAttributeProvider provider)
        {
            var types = from t in Assembly.GetAssembly(BaseType).GetTypes()
                        where t.IsSubclassOf(BaseType)
                select t;

            return types.ToArray();
        }
    }
}