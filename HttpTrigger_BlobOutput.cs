using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace FunctionAppTest
{
    public static class HttpTrigger_BlobOutput
    {
        [FunctionName("HttpTrigger_BlobOutput")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Blob("output-blob/httpRequests.txt", FileAccess.Write, Connection = "AzureWebJobsStorage")]Stream outBlob,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage;

            //StreamReader reader = new StreamReader(outBlob);
            //string content = reader.ReadToEnd();
            
            if (string.IsNullOrEmpty(name))
            {
                responseMessage = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";
                outBlob.Write(Encoding.ASCII.GetBytes($"No name entered"));
            }
            else
            {
                responseMessage = $"Hello, {name}. This HTTP triggered function executed successfully and new string is added to the output blob.";
                outBlob.Write(Encoding.ASCII.GetBytes($"Name: {name}"));
            }

            return new OkObjectResult(responseMessage);
        }
    }
}
