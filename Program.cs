using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibretaContactosAPI.Data;
using LibretaContactosAPI.Handlers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibretaContactosAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibretaContactosAPIContext") ?? throw new InvalidOperationException("Connection string 'LibretaContactosAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    //Se comenta la condicion para que Swagger este disponible en la publicacion del hosting
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseMiddleware<BasicAuthHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
