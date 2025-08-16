using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Notes.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");  

// 1. Добавляем сервис IdentityServer с настройками
builder.Services.AddIdentityServer(options =>
{
    // Здесь можно настроить параметры IdentityServer
})
    // Добавляем in-memory конфигурацию клиентов, ресурсов и пользователей
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential(); // Используем временный ключ для подписи токенов
var app = builder.Build();

// 2. Добавляем middleware IdentityServer
app.UseIdentityServer();

app.Run();
