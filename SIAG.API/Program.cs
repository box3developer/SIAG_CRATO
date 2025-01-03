using SIAG.CrossCutting.Logging;
using Serilog;
using SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios;
using SIAG.Application.Armazenagem.Cadastro.Services;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Infrastructure.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using SIAG.Application.Armazenagem.Cadastro.Shared.Mappings;
using SIAG.CrossCutting.Interfaces;
using SIAG.CrossCutting.Services;

var builder = WebApplication.CreateBuilder(args);

// Config DbContext
builder.Services.AddDbContext<SiagDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerSIAGCrato")));

// Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IMappingService, MappingService>(); ;

// Config Dapper
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("SqlServerSIAGCrato")));

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Config Serilog
SerilogConfig.ConfigureSerilog();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependencies
// Repositories
builder.Services.AddScoped<IAreaArmazenagemRepository, AreaArmazenagemRepository>();
// Services
builder.Services.AddSingleton<ILogService, SerilogLogService>();
builder.Services.AddScoped<AreaArmazenagemService>();

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
