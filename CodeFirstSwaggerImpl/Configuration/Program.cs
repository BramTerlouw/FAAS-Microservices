using CodeFirstSwaggerImpl.Middleware;
using CodeFirstSwaggerImpl.Services;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
      .ConfigureAppConfiguration(configurationBuilder =>
      {
      })
      .ConfigureFunctionsWorkerDefaults(Worker => Worker.UseNewtonsoftJson().UseMiddleware<JwtMiddelware>())
      .ConfigureOpenApi()
      .ConfigureServices(services =>
      {
          services.AddSingleton<ITokenService, TokenService>();
      })
            .Build();

host.Run();
