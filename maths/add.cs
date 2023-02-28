//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;

//namespace maths
//{
//    public static class add
//    {
//        [FunctionName("add")]
//        public static async Task<IActionResult> Run(
//            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
//            ILogger log)
//        {
//            log.LogInformation("C# HTTP trigger function processed a request.");

//            string name = req.Query["name"];

//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            dynamic data = JsonConvert.DeserializeObject(requestBody);
//            name = name ?? data?.name;

//            string responseMessage = string.IsNullOrEmpty(name)
//                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
//                : $"Hello, {name}. This HTTP triggered function executed successfully.";

//            return new OkObjectResult(responseMessage);
//        }
//    }
//}

using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace maths
{
    public static class add
    {
        public class AddRequest
        {
            public int Number1 { get; set; }
            public int Number2 { get; set; }
        }

        public class AddResponse
        {
            public int Result { get; set; }
        }

        [FunctionName("add")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            AddRequest data = JsonConvert.DeserializeObject<AddRequest>(requestBody);

            int result = data.Number1 + data.Number2;

            AddResponse response = new AddResponse { Result = result };

            return new OkObjectResult(response);
        }
    }

    public static class multiply
    {
        public class MultiplyRequest
        {
            public int Number1 { get; set; }
            public int Number2 { get; set; }
        }

        public class MultiResponse
        {
            public int Result { get; set; }
        }

        [FunctionName("multiply")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MultiplyRequest data = JsonConvert.DeserializeObject<MultiplyRequest>(requestBody);

            int result = data.Number1 * data.Number2;

            MultiResponse response = new MultiResponse { Result = result };

            return new OkObjectResult(response);
        }
    }
}


