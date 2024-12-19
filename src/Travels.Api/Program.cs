using Travels.Application;
using Travels.Application.Interfaces;
using Travels.Application.UseCases.RouteCases.AddRoute;
using Travels.Application.UseCases.RouteCases.DeleteRoute;
using Travels.Application.UseCases.RouteCases.GetRoute;
using Travels.Application.UseCases.RouteCases.GetRouteById;
using Travels.Application.UseCases.RouteCases.UpdateRoute;
using Travels.Application.UseCases.RouteCases.GetCheapestRoute;

using Travels.Application.UseCases.PersonCases.AddPerson;
using Travels.Application.UseCases.PersonCases.GetPerson;
using Travels.Application.UseCases.PersonCases.GetPersonById;
using Travels.Application.UseCases.PersonCases.UpdatePerson;
using Travels.Application.UseCases.PersonCases.DeletePerson;

using Travels.Application.Interfaces.Repositories;
using Travels.Infrastructure;
using Travels.Infrastructure.Seed;
using Travels.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Travels API",
        Version = "v1",
        Description = "API para gerenciamento de rotas de viagem, incluindo cálculo da rota mais barata."
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Custom services para atender ao Clean Arch
builder.Services.AddApplicationServices(); // Serviços de Application
builder.Services.AddInfrastructureServices(); // Serviços de Infrastructure

builder.Services.AddSingleton<IRouteRepository, RouteRepository>();
builder.Services.AddSingleton<IPhoneRepository, PhoneRepository>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();

builder.Services.AddTransient<AddRouteUseCase>();
builder.Services.AddTransient<UpdateRouteUseCase>();
builder.Services.AddTransient<AddRouteUseCase>();
builder.Services.AddTransient<GetRouteByIdUseCase>();
builder.Services.AddTransient<GetRouteUseCase>();
builder.Services.AddTransient<DeleteRouteUseCase>();
builder.Services.AddTransient<UpdateRouteUseCase>();
builder.Services.AddTransient<GetCheapestRouteUseCase>();
builder.Services.AddTransient<IGetCheapestRouteCommand, GetCheapestRouteCommand>();

builder.Services.AddTransient<AddPersonUseCase>();
builder.Services.AddTransient<GetPersonUseCase>();
builder.Services.AddTransient<GetPersonByIdUseCase>();
builder.Services.AddTransient<UpdatePersonUseCase>();
builder.Services.AddTransient<DeletePersonUseCase>();

builder.Services.AddTransient<RouteSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<RouteSeeder>();
    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Travels API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
