using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Load secrets from secrets.json
builder.Services.Configure<GitHubOptions>(builder.Configuration.GetSection("GitHub"));

// Add caching
builder.Services.AddMemoryCache();

// Register GitHub service
builder.Services.AddSingleton<GitHubApiService>();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();
