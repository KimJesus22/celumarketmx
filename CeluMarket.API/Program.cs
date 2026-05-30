using CeluMarket.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// NUEVO: 1. Configurar CORS para permitir que Astro se conecte
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAstroFrontEnd", policy =>
    {
        policy.WithOrigins("http://localhost:4321", "https://celumarketmx.vercel.app") // El puerto por defecto de Astro y el dominio de Vercel
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// NUEVO: 2. Activar CORS en el pipeline (Debe ir ANTES de UseAuthorization y MapControllers)
app.UseCors("AllowAstroFrontEnd");

app.UseAuthorization();

app.MapControllers();

app.Run();
