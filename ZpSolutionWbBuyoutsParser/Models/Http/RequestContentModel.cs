using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZpSolutionWbBuyoutsParser.Models.Http
{
    internal class RequestContentModel
    {
        public string ContentType { get; }
        public StringContent Content { get; }

        public RequestContentModel(string contentType, StringContent content)
        {
            ContentType = contentType;
            Content = content;
        }
    }
}
