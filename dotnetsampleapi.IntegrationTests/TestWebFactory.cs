using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sample_api;
using sample_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotnetsampleapi.IntegrationTests
{
    // Create necessary infra needed to Test logic with Dummy DB

    public class TestWebFactory<T> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Need Context Objects  :  knows how to connect to dB
              var descriptor=  services.SingleOrDefault(d => d.ServiceType ==
                typeof(DbContextOptions<TataDBContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Use In Memory Database
                var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                // Context =>  In Memory 
                services.AddDbContext<TataDBContext>(options =>
                {
                    // Configuration Object Reads ConnectionString Here
                    options.UseInMemoryDatabase("InMemoryProductDB");
                });
            });
        }
    }
}
