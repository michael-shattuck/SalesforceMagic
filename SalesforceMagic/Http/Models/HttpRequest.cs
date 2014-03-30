using System.Collections.Generic;
using SalesforceMagic.Http.Enums;

namespace SalesforceMagic.Http.Models
{
    public class HttpRequest
    {
        public HttpRequest()
        {
            Method = RequestType.POST;
            ContentType = "text/xml";
        }

        public string Url { get; set; }
        public RequestType Method { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Headers { get; set; }
        public string Body { get; set; }
        public string ContentType { get; set; }

        public bool IsValid
        {
            get
            {
                bool success = Url != null;

                if (Method != RequestType.GET && Body == null)
                    success =  false;

                return success;
            }
        }
    }
}