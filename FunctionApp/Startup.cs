using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FunctionApp.Data;
using FunctionApp.Services;
using FunctionApp.Models;

[assembly: FunctionsStartup(typeof(FunctionApp.Startup))]

namespace FunctionApp {
    public class Startup : FunctionsStartup {
        public override void Configure(IFunctionsHostBuilder builder) {
            // Register EF Core with SQLite
            builder.Services.AddDbContext<FunctionAppDbContext>(options =>
                options.UseSqlite("Data Source=ecommerce.db"));

            // Register your services
            builder.Services.AddScoped<IProductService,ProductServiceImpl>();
            builder.Services.AddScoped<IUserService, UserServiceImpl>();
            builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
        }
    }
}
