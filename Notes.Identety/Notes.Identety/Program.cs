using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Notes.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");  

// 1. ��������� ������ IdentityServer � �����������
builder.Services.AddIdentityServer(options =>
{
    // ����� ����� ��������� ��������� IdentityServer
})
    // ��������� in-memory ������������ ��������, �������� � �������������
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential(); // ���������� ��������� ���� ��� ������� �������
var app = builder.Build();

// 2. ��������� middleware IdentityServer
app.UseIdentityServer();

app.Run();
