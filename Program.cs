using McpServerSample.Config;
using McpServerSample.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ApiKeyManager.Initialize(builder.Configuration);

builder.Host.UseSerilog((context, logger) =>
{
    var config = context.Configuration;
    logger.ReadFrom.Configuration(config);
    logger.WriteTo.Console();
});

builder.Services.AddHttpClient("weather");

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

var app = builder.Build();

app.UseApiKeyAuthentication();

app.MapMcp("/mcp");

app.Run();