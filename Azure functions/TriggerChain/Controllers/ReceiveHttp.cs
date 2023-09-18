using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TriggerChain.Models;
using TriggerChain.Services;

namespace TriggerChain.Controllers
{
    public class ReceiveHttp
    {
        private readonly ILogger _logger;
        private readonly IQueueService _queueService;

        public ReceiveHttp(ILoggerFactory loggerFactory, IQueueService queueService)
        {
            _logger = loggerFactory.CreateLogger<ReceiveHttp>();
            _queueService = queueService;
        }

        [Function("ReceiveHttp")]
        public CustomOutputType Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Create product to be added. (!!Ideally in body of post req!!)
            Product product = new Product(1, "NVIDIA RTX 4090", false);
            string serializedProduct = JsonConvert.SerializeObject(product);

            // Create response with headers
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            // Add serialized product and res to type with output binding to queue.
            return new CustomOutputType
            {
                Product = serializedProduct,
                HttpResponse = response
            };
        }

        public class CustomOutputType
        {
            [QueueOutput("productqueue")]
            public string? Product { get; set; }
            public HttpResponseData? HttpResponse { get; set; }
        }
    }
}
