using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
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
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Create product to be added (!!Ideally in body of post req!!)
            Product product = new Product(1, "NVIDIA RTX 4090", false);
            await _queueService.SendQueueMessageAsync("productqueue", product);

            // Send response code 200 to caller of http req
            return req.CreateResponse(HttpStatusCode.OK); ;
        }
    }
}
