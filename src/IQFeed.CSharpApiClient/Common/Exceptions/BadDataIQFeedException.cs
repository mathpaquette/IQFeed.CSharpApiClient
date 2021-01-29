using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Common.Exceptions
{
    public class BadDataIQFeedException : IQFeedException
    {
        public BadDataIQFeedException(string message, string badFieldName) : this("", message, badFieldName)
        {

        }

        public BadDataIQFeedException(string request, string message, string badFieldName) : base(request, message, String.Format("Cannot parse field \"{0}\" from IqFeed", badFieldName), "")
        {
            BadFieldName = badFieldName;
        }

        public string BadFieldName { get; }
    }
}
