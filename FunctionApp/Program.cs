using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FunctionApp.Data;
using FunctionApp.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddDbContext<FunctionAppDbContext>(options =>
            options.UseSqlite("Data Source=ecommerce.db"));

        services.AddScoped<IProductService, ProductServiceImpl>();
        services.AddScoped<IUserService, UserServiceImpl>();
        services.AddScoped<IOrderService, OrderServiceImpl>();
    })
    .Build();

host.Run();
