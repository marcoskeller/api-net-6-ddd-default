using Domain.Interfaces.Generics;
using Domain.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Infrastructure.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPIs.Token;
using AutoMapper;
using WebAPIs.Models;

var builder = WebApplication.CreateBuilder(args);

/*Adicionando Serviços ao Contêiner.*/
builder.Services.AddControllers();


//Configurando Swagger na Aplicação - Saiba mais sobre como configurar Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configurando a string de conexão
builder.Services.AddDbContext<ContextBase>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Configurando a aplicação para usar os usuários do Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();

//Adcionando Controllers com Views
builder.Services.AddControllersWithViews();


//Adicionando Páginas Razors
builder.Services.AddRazorPages();



/*Configurando Interface e Repositório */
builder.Services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
builder.Services.AddSingleton<IMessage, RepositoryMessage>();


/*Configuração do Serviço de Domínio */
builder.Services.AddSingleton<IServiceMessage, ServiceMessage>();


/*Configurando JWT*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(option =>
      {
          option.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuer = false,
              ValidateAudience = false,
              ValidateLifetime = true,
              ValidateIssuerSigningKey = true,

              ValidIssuer = "Mundo DEV",
              ValidAudience = "Mundo DEV",
              IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
          };

          option.Events = new JwtBearerEvents
          {
              OnAuthenticationFailed = context =>
              {
                  Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                  return Task.CompletedTask;
              },
              OnTokenValidated = context =>
              {
                  Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                  return Task.CompletedTask;
              }
          };
      });


/*Configurando o AutoMapper do Projeto*/
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<MessageViewModel, Message>();
    cfg.CreateMap<Message, MessageViewModel>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


//Inicialização do build
var app = builder.Build();


/*Configuração do pipeline de solicitação HTTP */
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


/*Configurando o Cors - Que é liberação de acesso de quem poderá acessar nossa aplicação*/

//Configurando o Cors para Desenvolvimento
var devClient = "http://localhost:4200";
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader().WithOrigins(devClient));

//Configurando o Cors para Diversos Ambientes como DEV, HML ou PRD
//var urlDev = "https://dominiodocliente.com.br";
//var urlHML = "https://dominiodocliente2.com.br";
//var urlPROD = "https://dominiodocliente3.com.br";
//app.UseCors(b => b.WithOrigins(urlDev, urlHML, urlPROD));




app.UseHttpsRedirection();

//Configurando para usar a Autorização do JWT
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//Configurando para Usar o Swagger como documentação da nossa API
app.UseSwaggerUI();

app.Run();
