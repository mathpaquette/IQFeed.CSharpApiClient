using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    [Obsolete("Please use ILevel1DynamicClient instead. This handler will be removed soon!")]
    public class Level1MessageDynamicHandler : BaseLevel1MessageHandler, ILevel1MessageDynamicHandler
    {
        public event Action<IUpdateSummaryMessage> Summary;
        public event Action<IUpdateSummaryMessage> Update;

        private DynamicFieldset[] _dynamicFieldsets; // not thread-safe

        public void SetDynamicFields(params DynamicFieldset[] fieldNames)
        {
            if (fieldNames[0] != DynamicFieldset.Symbol)
                throw new ArgumentException("Symbol must be the first dynamic field specified.");

            foreach (var fieldName in fieldNames)
            {
                if (fieldName == DynamicFieldset.Type)
                    throw new ArgumentException("Type is implicitly included in dynamic fields and must not be included in the dynamic fields requested.");
            }
            
            _dynamicFieldsets = fieldNames;
        }

        protected override void ProcessSummaryMessage(string msg)
        {
            var dynamicFields = Level1DynamicFields.Parse(msg, _dynamicFieldsets);
            Summary?.Invoke(new UpdateSummaryDynamicMessage(dynamicFields));
        }

        protected override void ProcessUpdateMessage(string msg)
        {
            var dynamicFields = Level1DynamicFields.Parse(msg, _dynamicFieldsets);
            Update?.Invoke(new UpdateSummaryDynamicMessage(dynamicFields));
        }
    }
}