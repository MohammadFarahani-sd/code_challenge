using Autofac.Core;
using Mc2.CrudTest.Application.Command.Customers;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Infrastructure.Domain.Customers;
using Mc2.CrudTest.Infrastructure.Persistence;
using Mc2.CrudTest.Presentation.Server.Seed;
using Mc2.CrudTest.Query;
using Mc2.CrudTest.Query.Handlers.Customers;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Presentation.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<CustomerQueryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
            });

            builder.Services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection"));
            });


            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerCommandHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCustomerQueryHandler).Assembly));

            builder.Services.AddDbContext<CustomerQueryDbContext>();
            builder.Services.AddDbContext<CustomerDbContext>();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<CustomerQueryDbContext>();
                    var seedClass = new ContextSeed();
                    seedClass.SeedMigrationAsync(dbContext).Wait();
                }
                catch (Exception exception)
                {
                    Console.Write(exception.Message);
                }
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();


                app.UseSwagger();
                app.UseSwaggerUI();

            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}