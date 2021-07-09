using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FAHTTPTrigger
{
    public static class BlobTrigger
    {
        [FunctionName("BlobTrigger")]
        public static void Run(
            [BlobTrigger("input-blob/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob, string BlobTrigger,
            [Blob("output-blob/output-{name}", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream outBlob,
            string name,  
            ILogger log)

        {
            StreamReader reader = new StreamReader(myBlob);

            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes\n Content: {reader.ReadToEnd()}\n Path:{BlobTrigger}");
            var len = myBlob.Length;
            myBlob.CopyTo(outBlob);
        }
    }
}
