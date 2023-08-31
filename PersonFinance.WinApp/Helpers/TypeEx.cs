using System.Collections.Generic;
using System;
using PersonFinance.WinApp.PersonFinanceModels.ObjectValues;

namespace PersonFinance.WinApp.Helpers
{
    public static class TypeEx
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),  typeof(double),  typeof(decimal),
            typeof(long), typeof(short),   typeof(sbyte),
            typeof(byte), typeof(ulong),   typeof(ushort),
            typeof(uint), typeof(float), typeof(Money)
        };

        public static bool IsNumeric(this Type myType)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(myType) ?? myType);
        }
    }
}
