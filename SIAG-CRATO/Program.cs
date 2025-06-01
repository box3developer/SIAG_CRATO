using SIAG_CRATO;
using SIAG_CRATO.Repositories.Implementations;
using SIAG_CRATO.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IChamadaRepository, ChamadaRepository>();

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

Global.Conexao = builder.Configuration.GetConnectionString("siagDB") ?? "";
Global.NodeRedAPI = builder.Configuration.GetConnectionString("nodeAPI") ?? "";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
