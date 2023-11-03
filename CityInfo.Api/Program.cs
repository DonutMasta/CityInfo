using System.Reflection;
using CityInfo.Api;
using CityInfo.Api.DbContexts;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Container = System.ComponentModel.Container;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var container = new SimpleInjector.Container();
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
container.Options.DefaultLifestyle = Lifestyle.Scoped;

builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter))).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<CityInfoContext>(dbContextOptions =>
    dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:CityInfoConnection"]));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(x => x.FullName?.Replace("+", "-")));
builder.Services.AddSimpleInjector(container, options =>
{
    options.AddAspNetCore().AddControllerActivation();
    var mediatorAssemblies = new[] { typeof(IMediator).Assembly, Assembly.GetExecutingAssembly() };

    container.RegisterInstance(new ServiceFactory(container.GetInstance));
    container.RegisterSingleton<IMediator, Mediator>();
    container.Register(typeof(IRequestHandler<,>), mediatorAssemblies);

    container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
    {
        typeof(RequestPreProcessorBehavior<,>),
        typeof(RequestPostProcessorBehavior<,>)
    });
    container.Collection.Register(typeof(IRequestPreProcessor<>), mediatorAssemblies);
    container.Collection.Register(typeof(IRequestPostProcessor<,>), mediatorAssemblies);

    container.RegisterInstance(new FileExtensionContentTypeProvider());

    options.AutoCrossWireFrameworkComponents = true;
});
var app = builder.Build();

SimpleInjectorUseOptionsAspNetCoreExtensions.UseSimpleInjector(app, container);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());
container.Verify();
app.Run();