using Microsoft.EntityFrameworkCore;
using MyStreeTBackend.Data;
using MyStreeTBackend.Global;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//DBConnection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer
    (builder.
    Configuration.
    GetConnectionString
    ("DefaultConnection"))
);
builder.Services.AddRepositoriesService();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();

