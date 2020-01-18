using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace DatEx.Skelya.GUI.CustomCtrls.Helpers
{
    public class EnumAndDescription
    {
        private readonly Dictionary<Enum, String> Forvard = new Dictionary<Enum, String>();
        private readonly Dictionary<String, Enum> Back = new Dictionary<String, Enum>();

        private readonly Type _enumType;
        public Type EnumType { get => _enumType; }

        public EnumAndDescription(Type enumType)
        {
            _enumType = enumType;
            foreach(var val in System.Enum.GetValues(enumType).Cast<Enum>())
            {
                var memInfo = enumType.GetMember(enumType.GetEnumName(val));
                var descriptionAttribute = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                Enum enumValue = (Enum)val;
                String enumDescription = descriptionAttribute.Description;
                //
                Forvard.Add(enumValue, enumDescription);
                Back.Add(enumDescription, enumValue);
            }
        }

        public Enum GetValue(String description) => Back[description];

        public IEnumerable<Enum> GetAllValues() => Forvard.Keys.Select(x => x);

        public String GetDescription(Enum value) => Forvard[value];

        public IEnumerable<String> GetAllDescriptions() => Back.Keys.Select(x => x);
    }

    public static class EnumHelper
    {
        private static Dictionary<Type, EnumAndDescription> EnumsAndDescriptions = new Dictionary<Type, EnumAndDescription>();

        public static T GetValue<T>(String description) where T : Enum
        {
            Type enumType = InitializeIfEmpty<T>();
            return (T)EnumsAndDescriptions[enumType].GetValue(description);
        }

        public static IEnumerable<T> GetAllValues<T>() where T : Enum
        {
            Type enumType = InitializeIfEmpty<T>();
            return EnumsAndDescriptions[enumType].GetAllValues().Cast<T>();
        }

        public static String GetDescription<T>(T value) where T : Enum
        {
            Type enumType = InitializeIfEmpty<T>();
            return EnumsAndDescriptions[enumType].GetDescription(value);
        }

        public static IEnumerable<String> GetAllDescriptions<T>() where T : Enum
        {
            Type enumType = InitializeIfEmpty<T>();
            return EnumsAndDescriptions[enumType].GetAllDescriptions();
        }

        private static Type InitializeIfEmpty<T>() where T : Enum
        {
            Type enumType = typeof(T);
            if(!EnumsAndDescriptions.ContainsKey(enumType)) EnumsAndDescriptions.Add(enumType, new EnumAndDescription(enumType));
            return enumType;
        }
    }

}
