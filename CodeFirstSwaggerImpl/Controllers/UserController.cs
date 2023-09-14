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

    public class Settings
    {
        public bool DarkMode { get; set; }
        public bool BigFont { get; set; }
    }

    public class ExampleAuthAttribute : OpenApiSecurityAttribute
    {
        public ExampleAuthAttribute() : base("ExampleAuth", SecuritySchemeType.Http)
        {
            Description = "JWT for authorization";
            In = OpenApiSecurityLocationType.Header;
            Scheme = OpenApiSecuritySchemeType.Bearer;
            BearerFormat = "JWT";
        }
    }

    [OpenApiExample(typeof(UserBodyExample))]
    public class UserBodyDTO
    {
        [JsonRequired]
        [OpenApiProperty(Default = "Default Name", Description = "The name of the user.")]
        public string? Name { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Email", Description = "The Email of the user.")]
        public string? Email { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Password", Description = "The password of the user.")]
        public string? Password { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Role ID", Description = "The ID of the role of the user.")]
        public int Role_id { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Settings", Description = "The personal settings of the user.")]
        public Settings? Settings { get; set; }
    }

    public class UserBodyExample : OpenApiExample<UserBodyDTO>
    {
        public override IOpenApiExample<UserBodyDTO> Build(NamingStrategy NamingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(
                "Student", 
                "This is an example of a student", 
                new UserBodyDTO() { 
                    Name = "Bram Terlouw", 
                    Email = "bramterlouw@outlook.com", 
                    Password = "******", 
                    Role_id = 1, 
                    Settings = new Settings() { 
                        DarkMode = false, 
                        BigFont = true
                    } 
                }, 
                NamingStrategy));

            Examples.Add(OpenApiExampleResolver.Resolve(
                "Teacher",
                "This is an example of a teacher",
                new UserBodyDTO()
                {
                    Name = "Frank Dersjant",
                    Email = "frankdersjant@outlook.com",
                    Password = "******",
                    Role_id = 2,
                    Settings = new Settings()
                    {
                        DarkMode = false,
                        BigFont = true
                    }
                },
                NamingStrategy));

            Examples.Add(OpenApiExampleResolver.Resolve(
                "Admin",
                "This is an example of a admin",
                new UserBodyDTO()
                {
                    Name = "Joe Biden",
                    Email = "joebiden@outlook.com",
                    Password = "vergeten",
                    Role_id = 3,
                    Settings = new Settings()
                    {
                        DarkMode = false,
                        BigFont = true
                    }
                },
                NamingStrategy));

            return this;
        }
    }

    [OpenApiExample(typeof(UserResponseExample))]
    public class UserResponseDTO
    {
        [JsonRequired]
        [OpenApiProperty(Default = "Default User_ID", Description = "The unique user ID of the user")]
        public string? User_ID { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Name", Description = "The name of the user.")]
        public string? Name { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Email", Description = "The email of the user.")]
        public string? Email { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Password", Description = "The password of the user.")]
        public string? Password { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Role_id", Description = "The role ID of the user.")]
        public int Role_id { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Rating", Description = "The rating of the user.")]
        public int Rating { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Settings", Description = "The personal settings of the user.")]
        public Settings? Settings { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Created_at", Description = "Date time of when user was created.")]
        public DateTime Created_at { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Updated_at", Description = "Date time of when user was updated.")]
        public DateTime Updated_at { get; set; }
        [JsonRequired]
        [OpenApiProperty(Default = "Default Soft_deleted", Description = "Indicates if user was soft deleted.")]
        public DateTime Soft_deleted { get; set; }
    }

    public class UserResponseExample : OpenApiExample<UserResponseDTO>
    {
        public override IOpenApiExample<UserResponseDTO> Build(NamingStrategy NamingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(
                "Student",
                "This is an example of a student",
                new UserResponseDTO()
                {
                    User_ID = "59f2b2f4-51a2-11ee-be56-0242ac120002",
                    Name = "Bram Terlouw",
                    Email = "bramterlouw@outlook.com",
                    Password = "******",
                    Role_id = 1,
                    Rating = 1500,
                    Settings = new Settings()
                    {
                        DarkMode = false,
                        BigFont = true
                    },
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                    Soft_deleted = DateTime.Now
                },
                NamingStrategy));

            Examples.Add(OpenApiExampleResolver.Resolve(
                "Teacher",
                "This is an example of a teacher",
                new UserResponseDTO()
                {
                    User_ID = "59f2b2f4-51a2-11ee-be56-0242ac120003",
                    Name = "Frank Dersjant",
                    Email = "frankdersjant@outlook.com",
                    Password = "******",
                    Role_id = 2,
                    Rating = 0,
                    Settings = new Settings()
                    {
                        DarkMode = false,
                        BigFont = true
                    },
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                    Soft_deleted = DateTime.Now
                },
                NamingStrategy));

            Examples.Add(OpenApiExampleResolver.Resolve(
                "Admin",
                "This is an example of a admin",
                new UserResponseDTO()
                {
                    User_ID = "59f2b2f4-51a2-11ee-be56-0242ac120004",
                    Name = "Joe Biden",
                    Email = "joebiden@outlook.com",
                    Password = "vergeten",
                    Role_id = 3,
                    Rating = 0,
                    Settings = new Settings()
                    {
                        DarkMode = false,
                        BigFont = true
                    },
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                    Soft_deleted = DateTime.Now
                },
                NamingStrategy));

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
        [OpenApiOperation(operationId: "GetUsers", tags: new[] { "Users" }, Summary = "Get all users")]
        [OpenApiSecurity("Authentication", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header, 
            Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", 
            bodyType: typeof(UserResponseDTO), Description = "The OK response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Unauthorized, contentType: "text/plain", 
            bodyType: typeof(string), Description = "You are not authorized to perform this operation.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Forbidden, contentType: "text/plain", 
            bodyType: typeof(string), Description = "You are forbidden to perform this operation.")]
        public async Task<HttpResponseData> GetUsers([HttpTrigger(AuthorizationLevel.Function, "get", Route = "users")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            IEnumerable<string> Authorization;
            if (!req.Headers.TryGetValues("Authorization", out Authorization))
            {
                return req.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (Authorization.FirstOrDefault("") != "Bearer abc")
            {
                return req.CreateResponse(HttpStatusCode.Forbidden);
            }


            UserResponseDTO responseBody = new();
            responseBody.Name= "Bram";

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(responseBody);

            return response;
        }

        [Function("createUsers")]
        [OpenApiOperation(operationId: "CreareUser", tags: new[] { "Users" }, Summary = "Create new user")]
        [OpenApiSecurity("Authentication", SecuritySchemeType.Http, In = OpenApiSecurityLocationType.Header,
            Scheme = OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
        [OpenApiRequestBody("application/json", typeof(UserBodyDTO), Required = true, Example = typeof(UserBodyExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", 
            bodyType: typeof(UserResponseDTO), Description = "The OK response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Unauthorized, contentType: "text/plain",
            bodyType: typeof(string), Description = "You are not authorized to perform this operation.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Forbidden, contentType: "text/plain",
            bodyType: typeof(string), Description = "You are forbidden to perform this operation.")]
        public async Task<HttpResponseData> CreateUser([HttpTrigger(AuthorizationLevel.Function, "post", Route = "users")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            IEnumerable<string> Authorization;
            if (!req.Headers.TryGetValues("Authorization", out Authorization))
            {
                return req.CreateResponse(HttpStatusCode.Unauthorized);
            }

            if (Authorization.FirstOrDefault("") != "Bearer abc")
            {
                return req.CreateResponse(HttpStatusCode.Forbidden);
            }

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
