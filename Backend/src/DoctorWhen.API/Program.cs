using DoctorWhen.Persistence;
using DoctorWhen.API.Filters;
using DoctorWhen.Application.Utilities.AutoMapper;
using DoctorWhen.Application;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Define que todas as rotas serão escritas em minúsculas
builder.Services.AddRouting(option => option.LowercaseUrls = true);

// Adiciona os controladores de endpoints
builder.Services.AddControllers(
    options => options.Filters.Add<ExceptionFilter>())
    .AddJsonOptions(options =>
    {
        // Ignora erro de referência cíclica
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Ao invés de retornar o número do Enum, retorna o nome dele
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "DoctorWhen.API", Version = "1.0" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando Bearer.
                                    Entre com 'Bearer ' [espaço] então coloque seu token.
                                    Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// Adicionando Filro de Exceções customizadas
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

// Adicionando a camada de persistência por meio do Persistence.Bootstrapper.cs
builder.Services.AddPersistenceLayer(builder.Configuration);

// Adicionando a camada de application por meio do Application.Bootstrapper.cs
builder.Services.AddApplicationLayer(builder.Configuration);

// Adicionando AutoMapper
builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperConfiguration());
}).CreateMapper());

var app = builder.Build();

// HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
