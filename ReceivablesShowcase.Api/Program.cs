using Microsoft.EntityFrameworkCore;
using ReceivablesShowcase.Application;
using ReceivablesShowcase.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSqlite<ReceivablesContext>(builder.Configuration.GetConnectionString("ReceivablesContext"));
builder.Services.AddReceivablesApplication();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    scope.ServiceProvider.GetRequiredService<ReceivablesContext>().Database.EnsureCreated();
}

app.Run();
