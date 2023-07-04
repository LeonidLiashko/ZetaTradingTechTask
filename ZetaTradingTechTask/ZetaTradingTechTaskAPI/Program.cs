using Microsoft.AspNetCore.Server.Kestrel.Core;
using ZetaTradingTechTaskAPI;
using ZetaTradingTechTaskService.Interfaces;
using ZetaTradingTechTaskService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<ITreeService, TreeService>();
builder.Services.AddScoped<IExceptionService, ExceptionService>();

var app = builder.Build();

app.UseExceptionHandler("/api.user.journal/error");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();