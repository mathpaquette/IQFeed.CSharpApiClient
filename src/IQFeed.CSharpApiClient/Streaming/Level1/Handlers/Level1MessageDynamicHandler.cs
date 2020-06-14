using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageDynamicHandler : BaseLevel1MessageHandler, ILevel1MessageDynamicHandler
    {
        public event Action<IUpdateSummaryMessage> Summary;
        public event Action<IUpdateSummaryMessage> Update;

        private DynamicFieldset[] _dynamicFieldsets; // not thread-safe

        public void SetDynamicFields(params DynamicFieldset[] fieldNames)
        {
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