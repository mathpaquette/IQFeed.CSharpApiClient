using System;

namespace IQFeed.CSharpApiClient.Streaming
{
    internal class FieldsetDescriptionAttribute : Attribute
    {
        public string Name { get; }
        public Type Type { get; }

        public string Format { get; }

        public FieldsetDescriptionAttribute(string name, Type type, string optionalFormat = null)
        {
            Name = name;
            Type = type;
            Format = optionalFormat;
        }
    }
}