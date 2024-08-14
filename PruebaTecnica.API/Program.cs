using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PruebaTecnica.Model;
using PruebaTecnica.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PruebaTecnicaDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InyectarDependencia(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("https://localhost:7179", "http://localhost:5007");
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
