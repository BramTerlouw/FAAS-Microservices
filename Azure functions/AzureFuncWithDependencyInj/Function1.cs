using System.Net;
using AzureFuncWithDependencyInj.DAL;
using AzureFuncWithDependencyInj.Models;
using AzureFuncWithDependencyInj.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFuncWithDependencyInj
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;

        public Function1(ILoggerFactory loggerFactory, IProductService productService)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _productService = productService;
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            IEnumerable<Product> products = _productService.GetProducts();

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(products.First().ProductName + DateTime.Now);

            return response;
        }
    }
}
