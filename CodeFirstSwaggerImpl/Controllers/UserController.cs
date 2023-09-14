using System.Net;
using Azure.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CodeFirstSwaggerImpl.Controllers
{
    [OpenApiExample(typeof(UserBodyExample))]
    public class UserBodyDTO
    {
        [JsonRequired]
        [OpenApiProperty(Default = "Default Name", Description = "The name of the person to greet.")]
        public string Name { get; set; }
    }

    public class UserBodyExample : OpenApiExample<UserBodyDTO>
    {
        public override IOpenApiExample<UserBodyDTO> Build(NamingStrategy NamingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Erwin", new UserBodyDTO() { Name = "Erwin" }, NamingStrategy));
            Examples.Add(OpenApiExampleResolver.Resolve("Student", "Student summary", new UserBodyDTO() { Name = "Student" }, NamingStrategy));
            Examples.Add(OpenApiExampleResolver.Resolve("Lecturer", "Lecturer summary", "Lecturer description", new UserBodyDTO() { Name = "Lecturer" }, NamingStrategy));

            return this;
        }
    }

    [OpenApiExample(typeof(UserResponseExample))]
    public class UserResponseDTO
    {
        [JsonRequired]
        [OpenApiProperty(Default = "Hello, Erwin", Description = "The message")]
        public string Name { get; set; }
    }

    public class UserResponseExample : OpenApiExample<UserResponseDTO>
    {
        public override IOpenApiExample<UserResponseDTO> Build(NamingStrategy NamingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Erwin", new UserResponseDTO() { Name = "Erwin" }, NamingStrategy));
            Examples.Add(OpenApiExampleResolver.Resolve("Student", "Student response summary", new UserResponseDTO() { Name= "Student" }, NamingStrategy));
            Examples.Add(OpenApiExampleResolver.Resolve("Lecturer", "Lecturer response summary", "Lecturer response description", new UserResponseDTO() { Name = "Lecturer" }, NamingStrategy));

            return this;
        }
    }



    public class UserController
    {
        private readonly ILogger _logger;

        public UserController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UserController>();
        }

        [Function("getUsers")]
        [OpenApiOperation(operationId: "GetUsers", tags: new[] { "Users" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain",
                   bodyType: typeof(UserResponseDTO), Description = "The OK response")]
        public async Task<HttpResponseData> GetUsers([HttpTrigger(AuthorizationLevel.Function, "get", Route = "users")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            UserResponseDTO responseBody = new();
            responseBody.Name= "Bram";

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(responseBody);

            return response;
        }

        [Function("createUsers")]
        [OpenApiOperation(operationId: "CreareUser", tags: new[] { "Users" })]
        [OpenApiRequestBody("application/json", typeof(UserBodyDTO), Required = true, Example = typeof(UserBodyExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
                   bodyType: typeof(UserResponseDTO), Description = "The OK response")]
        public async Task<HttpResponseData> CreateUser([HttpTrigger(AuthorizationLevel.Function, "post", Route = "users")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UserBodyDTO? body = JsonConvert.DeserializeObject<UserBodyDTO>(requestBody);

            UserResponseDTO responseBody = new();
            responseBody.Name = $"Hello, {body.Name}. This HTTP triggered function executed successfully.";

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(responseBody);

            return response;
        }
    }
}
