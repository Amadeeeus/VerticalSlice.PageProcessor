using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Npgsql;
using VerticalSlice.PageProcessor.Features.ProcessPage;
using VerticalSlice.PageProcessor.Features.ProcessPage.Constants;
using VerticalSlice.PageProcessor.Features.ProcessPage.Repositories.Implementations;
using VerticalSlice.PageProcessor.Features.ProcessPage.Repositories.Interfaces;
using VerticalSlice.PageProcessor.Features.ProcessPage.Services.Implementations;
using VerticalSlice.PageProcessor.Features.ProcessPage.Services.Interfaces;
using VerticalSlice.PageProcessor.Shared.Exceptions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("Postgres") ??
                       throw new InvalidOperationException("Строка подключения не найдена");

builder.Services.AddSingleton<NpgsqlDataSource>(_ => 
    NpgsqlDataSource.Create(connectionString));

builder.Services.AddFastEndpoints(options =>
    {
        options.Assemblies = [typeof(ProcessPageEndpoint).Assembly];
    })
    .SwaggerDocument(options =>
{
    options.DocumentSettings = settings =>
    {
        settings.Title = "Vertical Slice Page Processor API";
        settings.Version = "v1";
    };
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(ProcessPageMapper).Assembly);
});

builder.Services.AddScoped<IProcessPageService, ProcessPageService>();
builder.Services.AddScoped<IProcessPageRepository, ProcessPageRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.UseFastEndpoints(config =>
{
    config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    
    config.Serializer.Options.WriteIndented = true;

    config.Errors.ResponseBuilder = (failures, stx, statusCode) =>
    {
        return new ProcessPageResponse
        {
            IsError = 1,
            ErrorCode = ErrorCodes.ValidationError,
            ErrorMessage = string
                .Join("; ", failures
                    .Select(x=>x.ErrorMessage))
        };
    };

    config.Errors.ProducesMetadataType = typeof(ProcessPageResponse);
});

app.UseSwaggerGen(config =>
    {
        config.Path = "/api/swagger/{documentName}/swagger.json";
    },
    ui =>
    {
        ui.Path = "/api/swagger";
        ui.DocumentPath =
            "/api/swagger/{documentName}/swagger.json";
    });

app.Run();
