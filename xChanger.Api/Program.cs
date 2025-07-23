//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Queues;
using xChanger.Api.Brokers.Sheets;
using xChanger.Api.Brokers.Storages;
using xChanger.Api.Services.Coordinations;
using xChanger.Api.Services.Foundations.ExternalPersons;
using xChanger.Api.Services.Foundations.Persons;
using xChanger.Api.Services.Foundations.Pets;
using xChanger.Api.Services.Orchestrations.ExternalPersons;
using xChanger.Api.Services.Orchestrations.PersonPets;
using xChanger.Api.Services.Orchestrations.Persons;
using xChanger.Api.Services.Processings.ExternalPersons;
using xChanger.Api.Services.Processings.Persons;
using xChanger.Api.Services.Processings.Pets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StorageBroker>();
AddBrokers(builder.Services);
AddFoundationServices(builder.Services);
AddProcessingServices(builder.Services);
AddOrchestrationServices(builder.Services);
AddCoordinationServices(builder.Services);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
            url: "/swagger/v1/swagger.json",
            name: "xChanger.Api v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

static void AddBrokers(IServiceCollection services)
{
    services.AddTransient<IStorageBroker, StorageBroker>();
    services.AddTransient<ILoggingBroker, LoggingBroker>();
    services.AddTransient<ISheetBroker, SheetBroker>();
    services.AddTransient<IQueueBroker, QueueBroker>();
}

static void AddFoundationServices(IServiceCollection services)
{
    services.AddTransient<IExternalPersonService, ExternalPersonService>();
    services.AddTransient<IExternalPersonEventService, ExternalPersonEventService>();
    services.AddTransient<IExternalPersonInputService, ExternalPersonInputService>();
    services.AddTransient<IPersonService, PersonService>();
    services.AddTransient<IPersonXMLService, PersonXMLService>();
    services.AddTransient<IPetService, PetService>();
}

static void AddProcessingServices(IServiceCollection services)
{
    services.AddTransient<IExternalPersonProcessingService, ExternalPersonProcessingService>();
    services.AddTransient<IExternalPersonEventProcessingService, ExternalPersonEventProcessingService>();
    services.AddTransient<IExternalPersonInputProcessingService, ExternalPersonInputProcessingService>();

    services.AddTransient<IPersonProcessingService, PersonProcessingService>();
    services.AddTransient<IPersonXMLProcessingService, PersonXMLProcessingService>();
    services.AddTransient<IPetProcessingService, PetProcessingService>();
}

static void AddOrchestrationServices(IServiceCollection services)
{
    services.AddTransient<IExternalPersonOrchestrationService, ExternalPersonOrchestrationService>();
    services.AddTransient<IExternalPersonPetEventOrchestrationService, ExternalPersonPetEventOrchestrationService>();
    services.AddTransient<IPersonPetOrchestrationService, PersonPetOrchestrationService>();
    services.AddTransient<IPersonOrchestrationService, PersonOrchestrationService>();
}

static void AddCoordinationServices(IServiceCollection services) =>
    services.AddTransient<IExternalPersonWithPetsCoordinationService, ExternalPersonWithPetsCoordinationService>();