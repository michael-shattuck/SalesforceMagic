using System.Collections.Generic;
using System.IO;
using LINQtoCSV;
using SalesforceMagic.BulkApi.Configuration;
using SalesforceMagic.BulkApi.Enum;
using SalesforceMagic.BulkApi.RequestTemplates;
using SalesforceMagic.Http;
using SalesforceMagic.ORM.BaseRequestTemplates;

namespace SalesforceMagic.BulkApi
{
    internal static class BulkCommands
    {
        internal static string CreateBatch<T>(T[] items)
        {
            return GetCsvString(items);
        }

        internal static string CreateJob(JobConfig config, string objectName)
        {
            return XmlRequestGenerator.GenerateRequest(new XmlBody
            {
                JobTemplate = new JobTempate
                {
                    ContentType = "CSV",
                    Object = objectName,
                    Operation = config.Operation.ToString().ToLower(),
                    ConcurrencyMode = config.ConcurrencyMode.ToString().ToLower(),
                    ExternalIdFieldName = config.Operation == BulkOperations.Upsert ? config.ExternalIdFieldName : null
                }
            });
        }

        internal static string CloseJob()
        {
            return XmlRequestGenerator.GenerateRequest(new XmlBody
            {
                JobTemplate = new JobTempate
                {
                    State = "Closed"
                }
            });
        }

        private static string GetCsvString<T>(IEnumerable<T> objects)
        {
            using (StringWriter writer = new StringWriter())
            {
                CsvContext context = new CsvContext();
                context.Write(objects, writer);

                return writer.ToString();
            }
        }
    }
}
