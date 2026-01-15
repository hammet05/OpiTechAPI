using FluentValidation;
using MediatR;
using TrafficAccidentsAPI.Application.Commands;
using TrafficAccidentsAPI.Application.Commands.Validators;
using TrafficAccidentsAPI.Application.Common.Mappings;
using TrafficAccidentsAPI.Common.Middleware;

//using TrafficAccidentsAPI.Common.Middleware;
using TrafficAccidentsAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Siniestros Viales API",
        Version = "v1",
        Description = "API para gestión de siniestros viales - Clean Architecture + DDD + CQRS"
    });
});
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegistrarSiniestroCommand).Assembly));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssembly(typeof(RegistrarSiniestroCommandValidator).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
