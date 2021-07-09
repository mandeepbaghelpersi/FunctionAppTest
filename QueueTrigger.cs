using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppTest
{
    public static class QueueTrigger
    {
        [FunctionName("QueueTrigger")]
        public static void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")]string myQueueItem, string id, DateTimeOffset ExpirationTime, string QueueTrigger, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}\n Id: {id}\n Expiring in: {ExpirationTime}");
        }
    }
}

