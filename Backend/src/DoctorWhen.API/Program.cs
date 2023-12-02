using DoctorWhen.Domain.Extensions;
using DoctorWhen.Persistence.Repository.RepositoryAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DoctorWhenContext>(
    context => context.UseSqlServer(builder.Configuration.GetDatabaseConnection()) 
    // builder.Configuration.GetConnectionString("nomeDaChave") encapsulado em Domain/Extensions (Pacote NuGet Microsoft.Extensions.Configuration)
);

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
