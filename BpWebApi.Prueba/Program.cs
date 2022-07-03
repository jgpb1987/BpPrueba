using BpAccesoDatos.Prueba;
using BpWebApi.Prueba.Services.Ahorro;
using BpWebApi.Prueba.Services.Clientes;
using BpWebApi.Prueba.Services.Financiero;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PruebaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PruebaConnectionString"));
});

builder.Services.AddScoped<ClienteServices>();
builder.Services.AddScoped<CuentaServices>();
builder.Services.AddScoped<MovimientoServices>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
