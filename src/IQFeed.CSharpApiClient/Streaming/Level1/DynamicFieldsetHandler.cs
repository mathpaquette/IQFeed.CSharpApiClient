using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class DynamicFieldsetHandler
    {
        public DynamicFieldsetHandler(DynamicFieldset[] fields)
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

        public IDictionary<string, object> ParseDynamicFields<TD>(string[] values)
        {
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

                if (fieldsetDescriptor.Type == typeof(double))
                {
                    // if it's a double, then convert to T
                    dynamicFields.Add(dynamicField.ToString(), Convert.ChangeType(values[index++], typeof(TD)));
                }
                else
                {
                    dynamicFields.Add(dynamicField.ToString(), Convert.ChangeType(values[index++], fieldsetDescriptor.Type));
                }
            }

            return dynamicFields;
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
