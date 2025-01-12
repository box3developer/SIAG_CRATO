using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SIAG.Application.Armazenagem.Cadastro.Services;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Application.Armazenagem.Cadastro.Shared.Mappings;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.CrossCutting.Logging;
using SIAG.CrossCutting.Services;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios;
using SIAG.Infrastructure.Configuracao;
using System.Data;

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
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters(); ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependencies
builder.Services.AddSingleton<ILogService, SerilogLogService>();
builder.Services.AddScoped<StatusDynamicService>();
// Add Repositories
builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
// Add Services
builder.Services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
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

app.UseCors("AllowAllOrigins"); // Habilitar a política de CORS

app.MapControllers();

app.Run();
