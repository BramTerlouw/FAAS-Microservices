using HttpTrigger_EF_SQL.DAL;
using HttpTrigger_EF_SQL.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
      .ConfigureAppConfiguration(configurationBuilder =>
      {
      })
      .ConfigureFunctionsWorkerDefaults()
      .ConfigureServices(services =>
      {
          services.AddTransient<IOrderService, OrderService>();
          //services.AddDbContext<OrderDBContext>(options => 
          //{
          //    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase"));
          //});
      })
            .Build();

host.Run();
