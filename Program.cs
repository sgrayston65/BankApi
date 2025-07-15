using BankApi.Applications.Services;
using BankApi.Repositories;
using Couchbase.Core.IO.Serializers;
using Couchbase.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog before building the app
Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.Debug()
    //.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Optional: file logs
    .Enrich.FromLogContext()         // Include useful context info
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog instead of default logging

builder.Services.AddCouchbase(options =>
{
    options.ConnectionString = "http://localhost";  // or your container IP
    options.UserName = "Admin";
    options.Password = "password";
}).AddSingleton<ITypeSerializer>(sp =>
    new DefaultSerializer(
        new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        },
        new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        }
));

builder.Services.AddCouchbaseBucket<INamedBucketProvider>("bank");

builder.Services.AddSingleton<IAccountRepository, AccountRepositoryCB>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepositoryCB>();
builder.Services.AddSingleton<ICustomerService, CustomerServiceCB>();
builder.Services.AddSingleton<IAccountService, AccountServiceCB>();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
        options.SerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
    });

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
