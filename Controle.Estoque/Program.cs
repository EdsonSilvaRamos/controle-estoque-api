using Controle.Estoque.Application.DTOs;
using Controle.Estoque.Application.Models;
using Controle.Estoque.Data.Context;
using Controle.Estoque.Data.Repository;
using Controle.Estoque.Data.Repository.Dapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;

//https://www.macoratti.net/20/07/aspc_mediatr1.htm
//https://www.treinaweb.com.br/blog/mediator-pattern-com-mediatr-no-asp-net-core
//https://medium.com/tableless/mediatr-com-asp-net-core-7b98ba0ca640


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<EstoqueContext>(opt => opt.UseMySql(conectionString, ServerVersion.AutoDetect(conectionString)));

builder.Services.AddSingleton<IDbConnection>(provider =>
{
    var connection = new MySqlConnection(conectionString);
    connection.Open();
    return connection;
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddScoped<IRepository<Produto>, ProdutoRepository>();
builder.Services.AddScoped<IRepository<RetiradaProduto>, RetiradaProdutoRepository>();
builder.Services.AddScoped<IRepository<LogErro>, LogErroRepository>();

builder.Services.AddScoped<IDapperRepository<Produto>, ProdutoDapperRepository>();
builder.Services.AddScoped<IDapperRepository<RetiradaProduto>, RetiradaProdutoDapperRepository>();
builder.Services.AddScoped<IDapperRepository<LogErro>, LogErroDapperRepository>();
builder.Services.AddScoped<IRelatorioDapperRepository, RelatorioDapperRepository>();

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