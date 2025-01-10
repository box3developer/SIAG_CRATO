using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SIAG.Application.Armazenagem.Cadastro.Shared.Mappings;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Interfaces;
using SIAG.CrossCutting.Logging;
using SIAG.CrossCutting.Services;
using SIAG.Infrastructure.Configuracao;
using System.Data;

using CadastroInterfaces = SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using CoreInterfaces = SIAG.Domain.Armazenagem.Core.Interfaces;
using CadastroRepos = SIAG.Infrastructure.Armazenagem.Cadastro.Repositorios;
using CoreRepos = SIAG.Infrastructure.Armazenagem.Core.Repositorios;
using CadastroServices = SIAG.Application.Armazenagem.Cadastro.Services;
using CoreServices = SIAG.Application.Armazenagem.Core.Services;

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
builder.Services.AddScoped<CadastroInterfaces.IAgrupadorAtivoRepository, CadastroRepos.AgrupadorAtivoRepository>();
builder.Services.AddScoped<CadastroInterfaces.IAreaArmazenagemRepository, CadastroRepos.AreaArmazenagemRepository>();
builder.Services.AddScoped<CadastroInterfaces.ICaixaRepository, CadastroRepos.CaixaRepository>();
builder.Services.AddScoped<CadastroInterfaces.IDepositoRepository, CadastroRepos.DepositoRepository>();
builder.Services.AddScoped<CadastroInterfaces.IEnderecoRepository, CadastroRepos.EnderecoRepository>();
builder.Services.AddScoped<CadastroInterfaces.IEquipamentoRepository, CadastroRepos.EquipamentoRepository>();
builder.Services.AddScoped<CadastroInterfaces.IPalletRepository, CadastroRepos.PalletRepository>();
builder.Services.AddScoped<CadastroInterfaces.IPedidoRepository, CadastroRepos.PedidoRepository>();
builder.Services.AddScoped<CadastroInterfaces.IProgramaRepository, CadastroRepos.ProgramaRepository>();
builder.Services.AddScoped<CadastroInterfaces.IRegiaoTrabalhoRepository, CadastroRepos.RegiaoTrabalhoRepository>();
builder.Services.AddScoped<CadastroInterfaces.ISetorTrabalhoRepository, CadastroRepos.SetorTrabalhoRepository>();
builder.Services.AddScoped<CadastroInterfaces.ITipoAreaRepository, CadastroRepos.TipoAreaRepository>();
builder.Services.AddScoped<CadastroInterfaces.ITipoEnderecoRepository, CadastroRepos.TipoEnderecoRepository>();
builder.Services.AddScoped<CadastroInterfaces.ITurnoRepository, CadastroRepos.TurnoRepository>();

builder.Services.AddScoped<CoreInterfaces.IAgrupadorAtivoRepository, CoreRepos.AgrupadorAtivoRepository>();
builder.Services.AddScoped<CoreInterfaces.IAreaArmazenagemRepository, CoreRepos.AreaArmazenagemRepository>();
builder.Services.AddScoped<CoreInterfaces.ICaixaRepository, CoreRepos.CaixaRepository>();
builder.Services.AddScoped<CoreInterfaces.IDepositoRepository, CoreRepos.DepositoRepository>();
builder.Services.AddScoped<CoreInterfaces.IEnderecoRepository, CoreRepos.EnderecoRepository>();
builder.Services.AddScoped<CoreInterfaces.IEquipamentoRepository, CoreRepos.EquipamentoRepository>();
builder.Services.AddScoped<CoreInterfaces.IPalletRepository, CoreRepos.PalletRepository>();
builder.Services.AddScoped<CoreInterfaces.IPedidoRepository, CoreRepos.PedidoRepository>();
builder.Services.AddScoped<CoreInterfaces.IProgramaRepository, CoreRepos.ProgramaRepository>();
builder.Services.AddScoped<CoreInterfaces.IRegiaoTrabalhoRepository, CoreRepos.RegiaoTrabalhoRepository>();
builder.Services.AddScoped<CoreInterfaces.ISetorTrabalhoRepository, CoreRepos.SetorTrabalhoRepository>();
builder.Services.AddScoped<CoreInterfaces.ITipoAreaRepository, CoreRepos.TipoAreaRepository>();
builder.Services.AddScoped<CoreInterfaces.ITipoEnderecoRepository, CoreRepos.TipoEnderecoRepository>();
builder.Services.AddScoped<CoreInterfaces.ITurnoRepository, CoreRepos.TurnoRepository>();
// Services
builder.Services.AddSingleton<ILogService, SerilogLogService>();
builder.Services.AddScoped<StatusDynamicService>();
builder.Services.AddScoped<CadastroServices.AgrupadorAtivoService>();
builder.Services.AddScoped<CadastroServices.AreaArmazenagemService>();
builder.Services.AddScoped<CadastroServices.CaixaService>();
builder.Services.AddScoped<CadastroServices.DepositoService>();
builder.Services.AddScoped<CadastroServices.EnderecoService>();
builder.Services.AddScoped<CadastroServices.EquipamentoService>();
builder.Services.AddScoped<CadastroServices.PalletService>();
builder.Services.AddScoped<CadastroServices.PedidoService>();
builder.Services.AddScoped<CadastroServices.ProgramaService>();
builder.Services.AddScoped<CadastroServices.RegiaoTrabalhoService>();
builder.Services.AddScoped<CadastroServices.SetorTrabalhoService>();
builder.Services.AddScoped<CadastroServices.TipoAreaService>();
builder.Services.AddScoped<CadastroServices.TipoEnderecoService>();
builder.Services.AddScoped<CadastroServices.TurnoService>();
builder.Services.AddScoped<CoreServices.AgrupadorAtivoService>();
builder.Services.AddScoped<CoreServices.AreaArmazenagemService>();
builder.Services.AddScoped<CoreServices.CaixaService>();
builder.Services.AddScoped<CoreServices.DepositoService>();
builder.Services.AddScoped<CoreServices.EnderecoService>();
builder.Services.AddScoped<CoreServices.EquipamentoService>();
builder.Services.AddScoped<CoreServices.PalletService>();
builder.Services.AddScoped<CoreServices.PedidoService>();
builder.Services.AddScoped<CoreServices.ProgramaService>();
builder.Services.AddScoped<CoreServices.RegiaoTrabalhoService>();
builder.Services.AddScoped<CoreServices.SetorTrabalhoService>();
builder.Services.AddScoped<CoreServices.TipoAreaService>();
builder.Services.AddScoped<CoreServices.TipoEnderecoService>();
builder.Services.AddScoped<CoreServices.TurnoService>();

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
