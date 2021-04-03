using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Handlers
{
    public class Level1DynamicMessageHandler : BaseLevel1MessageHandler, ILevel1DynamicMessageHandler
    {
        public event Action<IUpdateSummaryDynamicMessage> Summary;
        public event Action<IUpdateSummaryDynamicMessage> Update;

        private Func<string, IUpdateSummaryDynamicMessage> _messageParser;
        
        public void SetDynamicFields(params DynamicFieldset[] fieldNames)
        {
            if (_messageParser != null)
            {
                throw new InvalidOperationException($"It is NOT allowed to call {nameof(SetDynamicFields)} more than once!");
            }

            // generate a class definition on the fly that will only have the specified fields and create a parser for it
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(fieldNames);
            
            // sanity check
            if (!typeof(IUpdateSummaryDynamicMessage).IsAssignableFrom(updateSummaryMessageType))
            {
                throw new ArgumentException($"The specified type {updateSummaryMessageType.FullName} doesn't implement {nameof(IUpdateSummaryDynamicMessage)} interface!");
            }

            // get the static Parse function that takes a single string 
            var parseMethod = updateSummaryMessageType.GetMethod("Parse", new Type[] { typeof(string) });
            if (parseMethod == null || !parseMethod.IsStatic)
            {
                throw new ArgumentException($"The specified type {updateSummaryMessageType.FullName} doesn't have a static Parse(string) method needed for parsing level 1 messages!");
            }

            // create the parser function
            // Note: this is much faster than calling MethodInfo.Invoke!
            // For more info see: https://blogs.msmvps.com/jonskeet/2008/08/09/making-reflection-fly-and-exploring-delegates/
            _messageParser = (Func<string, IUpdateSummaryDynamicMessage>)Delegate.CreateDelegate(typeof(Func<string, IUpdateSummaryDynamicMessage>), null, parseMethod);
        }

        protected override void ProcessSummaryMessage(string msg)
        {
            Summary?.Invoke(_messageParser(msg));
        }

        protected override void ProcessUpdateMessage(string msg)
        {
            Update?.Invoke(_messageParser(msg));
        }
    }
}