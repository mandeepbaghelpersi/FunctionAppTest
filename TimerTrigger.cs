using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppTest
{
    public static class TimerTrigger
    {
        [FunctionName("TimerTrigger")]
        public static void Run([TimerTrigger("0 0 */1 * * *")]TimerInfo myTimer,
            [Queue("timer-queue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> timerQ,
            ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            timerQ.Add($"Timer Function Executed at : {DateTime.Now}");
        }
    }
}
