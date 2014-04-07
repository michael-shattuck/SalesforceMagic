using System.Xml.Serialization;
using SalesforceMagic.BulkApi.RequestTemplates;

namespace SalesforceMagic.ORM.BaseRequestTemplates
{
    public partial class XmlBody
    {
        [XmlElement("jobInfo", Namespace = SalesforceNamespaces.JobInfo)]
        public JobTempate JobTemplate { get; set; }
    }
}