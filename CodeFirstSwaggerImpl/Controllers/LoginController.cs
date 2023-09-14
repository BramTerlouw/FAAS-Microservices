using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using CodeFirstSwaggerImpl.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace CodeFirstSwaggerImpl.Controllers
{
    [OpenApiExample(typeof(LoginRequestExample))]
    public class LoginRequest
    {
        [OpenApiProperty(Description = "Email for the user logging in.")]
        [JsonRequired]
        public string? Email { get; set; }

        [OpenApiProperty(Description = "Password for the user logging in.")]
        [JsonRequired]
        public string? Password { get; set; }
    }

    public class LoginRequestExample : OpenApiExample<LoginRequest>
    {
        public override IOpenApiExample<LoginRequest> Build(NamingStrategy NamingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Erwin",
                new LoginRequest()
                {
                    Email = "bramterlouw@outlook.com",
                    Password = "secret"
                },
                NamingStrategy));

            return this;
        }
    }

    public class LoginResult
    {
        private JwtSecurityToken Token { get; }

        [OpenApiProperty(Description = "The access token to be used in every subsequent operation for this user.")]
        [JsonRequired]
        public string AccessToken => new JwtSecurityTokenHandler().WriteToken(Token);

        [OpenApiProperty(Description = "The token type.")]
        [JsonRequired]
        public string TokenType => "Bearer";

        [OpenApiProperty(Description = "The amount of seconds until the token expires.")]
        [JsonRequired]
        public int ExpiresIn => (int)(Token.ValidTo - DateTime.UtcNow).TotalSeconds;

        public LoginResult(JwtSecurityToken Token)
        {
            this.Token = Token;
        }
    }
    public class LoginController
    {
        ILogger Logger { get; }
        ITokenService TokenService { get; }

        public LoginController(ITokenService TokenService, ILogger<LoginController> Logger)
        {
            this.TokenService = TokenService;
            this.Logger = Logger;
        }

        [Function(nameof(LoginController.Login))]
        [OpenApiOperation(operationId: "Login", tags: new[] { "Login" }, Summary = "Login for a user",
                            Description = "This method logs in the user, and retrieves a JWT bearer token.")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(LoginRequest), Required = true, Description = "The user credentials")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(LoginResult), Description = "Login success")]
        public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, FunctionContext executionContext)
        {
            LoginRequest login = JsonConvert.DeserializeObject<LoginRequest>(await new StreamReader(req.Body).ReadToEndAsync());

            LoginResult result = await TokenService.CreateToken(login);

            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(result);

            return response;
        }
    }

}
