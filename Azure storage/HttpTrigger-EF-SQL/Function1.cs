using System.Net;
using HttpTrigger_EF_SQL.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace HttpTrigger_EF_SQL
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IOrderService _orderService;

        public Function1(ILoggerFactory loggerFactory, IOrderService orderService)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _orderService = orderService;
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var orders = _orderService.GetAllOrders();
            response.WriteString(orders.First().OrderID.ToString() + orders.First().OrderType.ToString());

            return response;
        }
    }
}
