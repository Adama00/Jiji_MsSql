using Jiji_Api.Data;
using Jiji_Api.Service.Interface;
using Jiji_Api.Service.Provider;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<JijiDbContext>(o =>o.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IProductsService, ProductsPgDbService>();    

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NextApp", policyBuider =>
    {
        policyBuider.WithOrigins("http://localhost:3000");
        policyBuider.AllowAnyHeader();
        policyBuider.AllowAnyMethod();
        policyBuider.AllowCredentials();

    });
    options.AddPolicy("BlazorApp", policyBuider =>
    {
        policyBuider.WithOrigins("http://localhost:3000");
        policyBuider.AllowAnyHeader();
        policyBuider.AllowAnyMethod();
        policyBuider.AllowCredentials();

    });
});

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
