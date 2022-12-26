using Polly;
using SampleService.Service;

namespace SampleService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            /*builder.Services.AddHttpClient("RootServiceClient", client =>
            {
                
            });*/

            builder.Services.AddHttpClient<IRootServiceClient, RootServiceClient>("RootServiceClient", client =>
            {

            }).AddTransientHttpErrorPolicy(configurePolicy => configurePolicy.WaitAndRetryAsync(retryCount: 3, sleepDurationProvider: (count) => TimeSpan.FromSeconds(2*count), onRetry: (respons, sleepDuration, count, context) => { 
                var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();

                logger.LogError(respons.Exception != null ? respons.Exception 
                    : new Exception($"\n{respons.Result.StatusCode}: {respons.Result.RequestMessage}"), 
                    $"Count {count} RootService request exception") ;
            }));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}