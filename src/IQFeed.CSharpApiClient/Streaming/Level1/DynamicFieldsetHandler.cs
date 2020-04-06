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
    }
}
