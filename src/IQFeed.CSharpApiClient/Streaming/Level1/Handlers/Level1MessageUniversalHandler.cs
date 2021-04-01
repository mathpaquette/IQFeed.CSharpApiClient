using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageUniversalHandler
        : BaseLevel1MessageHandler, ILevel1MessageDynamicHandler
    {
        public event Action<IUpdateSummaryMessage> Summary;
        public event Action<IUpdateSummaryMessage> Update;

        private Func<string, IUpdateSummaryMessage> _messageParser;

        public Level1MessageUniversalHandler()
        {
            // by default initialize the handler for UpdateSummaryMessage
            CreateMessageParser(typeof(UpdateSummaryMessage));
        }        

        public void SetDynamicFields(params DynamicFieldset[] fieldNames)
        {
            // generate a class definition on the fly that will only have the specified fields and create a parser for it
            CreateMessageParser(UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(fieldNames));
        }

        protected override void ProcessSummaryMessage(string msg)
        {
            Summary?.Invoke(_messageParser(msg));
        }

        protected override void ProcessUpdateMessage(string msg)
        {
            Update?.Invoke(_messageParser(msg));
        }

        private void CreateMessageParser(Type updateSummaryMessageType)
        {
            if (updateSummaryMessageType == null)
            {
                throw new ArgumentNullException(nameof(updateSummaryMessageType));
            }

            if (!typeof(IUpdateSummaryMessage).IsAssignableFrom(updateSummaryMessageType))
            {
                throw new ArgumentException($"The specified type {updateSummaryMessageType.FullName} doesn't implement {nameof(IUpdateSummaryMessage)} interface!");
            }

            // get the static Parse function that takes a single string 
            var parseMethod = updateSummaryMessageType.GetMethod("Parse", new Type[] { typeof(string) });
            if (parseMethod == null || !parseMethod.IsStatic)
            {
                throw new ArgumentException($"The specified type {updateSummaryMessageType.FullName} doesn't have a static Parse(string) method needed for parsing level 1 messages!");
            }

            // create the parser function
            _messageParser = (string message) =>
            {
                // execute the static Parse method with the received message
                return parseMethod.Invoke(null, new object[] { message }) as IUpdateSummaryMessage;
            };
        }
    }
}