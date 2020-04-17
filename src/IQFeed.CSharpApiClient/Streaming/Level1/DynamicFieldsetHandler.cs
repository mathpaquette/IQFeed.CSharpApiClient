using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class DynamicFieldsetHandler
    {
        public DynamicFieldsetHandler()
        {

        }

        public DynamicFieldsetHandler(params DynamicFieldset[] fields)
        {
            SetFields(fields);
        }

        public void SetFields(params DynamicFieldset[] fields) 
        { 
            foreach(var field in fields)
            {
                if (IQFeedDefault.DefaultLevel1SummaryFields.Contains(field))
                {
                    throw new Exception("Default field detected in Dynamic Fieldset List");
                }
            }

            Fields = fields;
        }

        public DynamicFieldset[] Fields { get; private set; }

        public DynamicFieldset[] GetFullFieldsetList()
        {
            return IQFeedDefault.DefaultLevel1SummaryFields.Union(Fields).ToArray();
        }

        public IDictionary<string, object> ParseDynamicFields<T>(string[] values)
        {
            if (Fields == null || Fields.Length == 0)
                return null;

            // Dynamic Fieldset Handler will contain additional fields we need to parse (in order)
            var index = 17;
            var dynamicFields = new Dictionary<string, object>();
            foreach (var dynamicField in Fields)
            {
                var fieldsetDescriptor = GetFieldsetDescriptionAttribute(dynamicField);
                if (fieldsetDescriptor == null)
                {
                    throw new Exception($"Dynamic Field {dynamicField} has no FieldSetDescriptionAttribute!");
                }

                var value = values[index++];
                if (string.IsNullOrEmpty(value))
                {
                    // deal with empty string or null
                    dynamicFields.Add(dynamicField.ToString(), GetDefault(fieldsetDescriptor.Type));
                }
                else
                {
                    if (fieldsetDescriptor.Type == typeof(double))
                    {
                        // if it's a double, then convert to T
                        dynamicFields.Add(dynamicField.ToString(), Convert.ChangeType(value, typeof(T)));
                    }
                    else if (fieldsetDescriptor.Type == typeof(DateTime))
                    {
                        if (!string.IsNullOrEmpty(fieldsetDescriptor.Format))
                            dynamicFields.Add(dynamicField.ToString(), DateTime.ParseExact(value, fieldsetDescriptor.Format, CultureInfo.InvariantCulture));
                        else
                        {
                            if (value.StartsWith("99:99:99"))
                            {
                                dynamicFields.Add(dynamicField.ToString(), DateTime.MaxValue);
                            }
                            else
                            {
                                dynamicFields.Add(dynamicField.ToString(), Convert.ChangeType(value, fieldsetDescriptor.Type));
                            }
                        }
                    }
                    else
                    {
                        // otherwise just convert it
                        dynamicFields.Add(dynamicField.ToString(), Convert.ChangeType(value, fieldsetDescriptor.Type));
                    }
                }
            }

            return dynamicFields;
        }

        private object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        private FieldsetDescriptionAttribute GetFieldsetDescriptionAttribute(DynamicFieldset dynamicField)
        {
            var members = dynamicField.GetType().GetMember(dynamicField.ToString());
            if (members.Length > 0)
            {
                var attributes = members[0].GetCustomAttributes(typeof(FieldsetDescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return (FieldsetDescriptionAttribute)attributes[0];
                }
            }

            return null;
        }
    }
}
