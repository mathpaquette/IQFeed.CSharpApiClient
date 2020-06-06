using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Examples.Common;
using IQFeed.CSharpApiClient.Streaming;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Extensions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace IQFeed.CSharpApiClient.Examples.Examples.CodeGenerators
{
    public class DynamicFieldsCodeGenerator : IExample
    {
#if DYNAMIC_FIELD_GENERATOR
        public bool Enable => true; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
#else
        public bool Enable => false; // *** SET TO TRUE TO RUN THIS EXAMPLE ***
#endif
        public string Name => typeof(DynamicFieldsCodeGenerator).Name;

        /// <summary>
        /// This example does not illustrate usage of any API interfaces.
        /// It is used to generate code that can be pasted into code files IUpdateSummaryMessageDynamic.cs and UpdateSummaryMessageDynamic.cs
        /// </summary>
        public void Run()
        {
            var fieldInfos = GetDynamicFieldMetaInfo();

            Console.WriteLine("***Interface Property Definitions****");
            Console.WriteLine("-------------------------------------");
            foreach (var fieldInfo in fieldInfos)
            {
                Console.WriteLine(InterfacePropertyDef(fieldInfo));
            }
            Console.WriteLine();
            Console.WriteLine("***Property Definitions****");
            Console.WriteLine("-------------------------------------");
            foreach (var fieldInfo in fieldInfos)
            {
                Console.WriteLine(PropertyDef(fieldInfo));
            }

            Console.WriteLine();
            Console.WriteLine("***Property.ToString() switch cases****");
            Console.WriteLine("---------------------------------------");
            foreach (var fieldInfo in fieldInfos)
            {
                Console.WriteLine(PropertyToStringSwitchCase(fieldInfo));
            }

        }

        private string InterfacePropertyDef(DynamicFieldMetaInfo field)
        {
            return $"{field.FieldType} {field.PropertyName} {{ get; }}";
        }

        private string PropertyDef(DynamicFieldMetaInfo field)
        {
            return $"public {field.FieldType} {field.PropertyName} {{ get {{ if (_valuesByFieldType.TryGetValue(DynamicFieldset.{field.PropertyName}, out var value)) {{ {PropertyParserDef(field)} }} throw new Exception(\"Dynamic field [{field.FieldName}] not found\"); }} }}";
        }

        private string PropertyParserDef(DynamicFieldMetaInfo field)
        {
            switch (field.FieldType)
            {
                case "DateTime":
                    return $"DateTime.TryParseExact(value, UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue;";
                case "double":
                    return $"double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue;";
                case "int":
                    return $"int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var fieldValue); return fieldValue;";
                case "string":
                    return $"return value;";
                case "TimeSpan":
                    return $"DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var fieldValue); return fieldValue.TimeOfDay;";
                default:
                    throw new NotSupportedException($"FieldType {field.FieldType} not supported");
            }

        }
        private string PropertyToStringSwitchCase(DynamicFieldMetaInfo field)
        {
            return $"case DynamicFieldset.{field.PropertyName}: return {field.PropertyName}.ToString();";
        }
        private List<DynamicFieldMetaInfo> GetDynamicFieldMetaInfo()
        {
            var fieldInfos = new List<DynamicFieldMetaInfo>();
            var fields = new List<DynamicFieldset>();

            var dynFields = typeof(DynamicFieldset).GetEnumNames();
            int i = 0;
            foreach (var df in dynFields)
                fields.Add((DynamicFieldset)i++);
            i = 0;
            foreach (var field in fields)
            {
                var fieldName = field.GetAttribute<FieldsetDescriptionAttribute>().Name;
                var fieldType = field.GetAttribute<FieldsetDescriptionAttribute>().Type.ToString()
                    .Replace("System.", "")
                    .Replace("Double", "double")
                    .Replace("Int32", "int")
                    .Replace("String", "string");

                fieldInfos.Add(new DynamicFieldMetaInfo(dynFields[i++], fieldType, fieldName));
            }
            return fieldInfos;
        }

        private class DynamicFieldMetaInfo
        {
            public DynamicFieldMetaInfo( string propertyName, string fieldType, string fieldName)
            {
                PropertyName = propertyName;
                FieldType = fieldType;
                FieldName = fieldName;
            }
            public string PropertyName { get; private set; }
            public string FieldType { get; private set; }
            public string FieldName { get; private set; }
        }
    }

    // KLUDGE - This class needs to be excluded except when using the DynamicFieldsCodeGenerator.cs example
#if (!DYNAMIC_FIELD_GENERATOR)
    internal class FieldsetDescriptionAttribute : Attribute
    {
        public string Name { get; }
        public Type Type { get; }

        public FieldsetDescriptionAttribute(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
#endif
}