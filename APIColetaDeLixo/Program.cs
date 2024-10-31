using APIColetaDeLixo.DataContext;
using APIColetaDeLixo.Interfaces.Repository;
using APIColetaDeLixo.Interfaces.Services;
using APIColetaDeLixo.Repository;
using APIColetaDeLixo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Inicializando Banco de dados
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<Context>(
  opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Services e Repositorys
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IColetaService, ColetaService>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IColetaRepository, ColetaRepository>();
#endregion

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
