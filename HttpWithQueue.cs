using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAppTest
{
    public static class HttpWithQueue
    {
        [FunctionName("HttpWithQueue")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Queue("output-queue"), StorageAccount("AzureWebJobsStorage")] ICollector<string> msg,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger with blob binding function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage, queueMessage;

            if (string.IsNullOrEmpty(name))
            {
                responseMessage = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";
                queueMessage = "No name parameter was passed while making the request.";
            }
            else
            {
                responseMessage = $"Hello, {name}. This HTTP triggered function executed successfully and new string is added to the output queue.";
                queueMessage = $"New Request for {name} added in the queue";

            }

            msg.Add(queueMessage);
            return new OkObjectResult(responseMessage);
        }
    }
}
