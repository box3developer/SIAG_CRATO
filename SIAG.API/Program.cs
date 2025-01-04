using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SIAG.Application.Armazenagem.Cadastro.Services;
using SIAG.Application.Armazenagem.Cadastro.Shared.Mappings;
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
// Repositories
builder.Services.AddScoped<IAgrupadorAtivoRepository, AgrupadorAtivoRepository>();
builder.Services.AddScoped<IAreaArmazenagemRepository, AreaArmazenagemRepository>();
builder.Services.AddScoped<ICaixaRepository, CaixaRepository>();
builder.Services.AddScoped<IDepositoRepository, DepositoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddScoped<IPalletRepository, PalletRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IProgramaRepository, ProgramaRepository>();
builder.Services.AddScoped<IRegiaoTrabalhoRepository, RegiaoTrabalhoRepository>();
builder.Services.AddScoped<ISetorTrabalhoRepository, SetorTrabalhoRepository>();
builder.Services.AddScoped<ITipoAreaRepository, TipoAreaRepository>();
builder.Services.AddScoped<ITipoEnderecoRepository, TipoEnderecoRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
// Services
builder.Services.AddSingleton<ILogService, SerilogLogService>();
builder.Services.AddScoped<AgrupadorAtivoService>();
builder.Services.AddScoped<AreaArmazenagemService>();
builder.Services.AddScoped<CaixaService>();
builder.Services.AddScoped<DepositoService>();
builder.Services.AddScoped<EnderecoService>();
builder.Services.AddScoped<EquipamentoService>();
builder.Services.AddScoped<PalletService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<ProgramaService>();
builder.Services.AddScoped<RegiaoTrabalhoService>();
builder.Services.AddScoped<SetorTrabalhoService>();
builder.Services.AddScoped<TipoAreaService>();
builder.Services.AddScoped<TipoEnderecoService>();
builder.Services.AddScoped<TurnoService>();

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
