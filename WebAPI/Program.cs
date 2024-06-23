using Application;
using Application.DataServices;
using Application.Interfaces;
using Application.Services;
using DataAccess.Persistence;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using WebAPI.CompiledModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRestaurantLogic, RestaurantLogic>();
builder.Services.AddScoped<IRestaurantDS, RestaurantDS>();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseModel(DatabaseContextModel.Instance);
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();