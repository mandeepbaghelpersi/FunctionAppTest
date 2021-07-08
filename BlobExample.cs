using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FAHTTPTrigger
{
    public static class BlobExample
    {
        [FunctionName("BlobExample")]
        public static void Run(
            [BlobTrigger("input-blob/{name}", Connection = "AzureWebJobsStorage")]Stream myBlob,
            [Blob("output-blob/{name}", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream outBlob,
            string name,  
            ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            var len = myBlob.Length;
            myBlob.CopyTo(outBlob);
        }
    }
}
