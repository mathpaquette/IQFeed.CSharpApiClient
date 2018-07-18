using System;

namespace IQFeed.CSharpApiClient.Streaming
{
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
}