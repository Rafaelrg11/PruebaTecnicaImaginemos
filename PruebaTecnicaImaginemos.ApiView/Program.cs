using PruebaTecnicaImaginemos.ApiView.Extencions;
using PruebaTecnicaImaginemos.ApiView.Middleware;
using PruebaTecnicaImaginemos.Application;
using PruebaTecnicaImaginemos.Infraestructure;
using PruebaTecnicaImaginemos.Infraestructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

DbInitializer.ApplyMigrations(app);

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
