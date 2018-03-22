using System;

namespace RAM.Domain.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static string ConvertToString(this Enum eff)
            => Enum.GetName(eff.GetType(), eff);

        public static TEnumType ConverToEnum<TEnumType>(this string enumValue)
            => (TEnumType) Enum.Parse(typeof(TEnumType), enumValue);
    }
}