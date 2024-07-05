using Microsoft.EntityFrameworkCore;
using WebAPIEstatusAlumnos.Models.Context;

var builder = WebApplication.CreateBuilder(args);

//Agregar permisos de Cors
var MyAllowSpecificOrigin = "_myAllowSpecificOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigin,
        builder =>
        {
            builder.WithOrigins("https://localhost.52318").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
        });
});


//Agregar referencia para la cadena de conexión
string connectionString = builder.Configuration.GetConnectionString("InstitutoTich");
builder.Services.AddDbContext<EstatusContext>(x => x.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(MyAllowSpecificOrigin);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
