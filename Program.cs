using Chatbot.Application.Interaction;
using Chatbot.Application.Interfaces;
using Chatbot.Application.MessageService.MessageAdministrativoService;
using Chatbot.Application.MessageService.MessageDocenteService;
using Chatbot.Application.MessageService.MessagePublicoExternoService;
using Chatbot.Application.Services;
using Chatbot.Application.WhatsappService;
using Chatbot.Infrastructure.Data;
using Chatbot.Infrastructure.Importers;
using Chatbot.Infrastructure.Services;
using Chatbot.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatbotDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ExcelReaderService>();

builder.Services.Configure<SpreadsheetSettings>
    (builder.Configuration.GetSection("SpreadsheetSettings"));

builder.Services.AddScoped<SpreadsheetImporter>();

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.Configure<TwilioSettings>(
    builder.Configuration.GetSection("Twilio"));

builder.Services.AddSingleton<IUserStateCache, InMemoryUserStateCache>();
builder.Services.AddSingleton<TwilioMessageSender>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UserInteractionRouter>();
builder.Services.AddScoped<BotMessageHandler>();
builder.Services.AddScoped<DocenteInteractionService>();
builder.Services.AddScoped<AdministrativoInteractionService>();
builder.Services.AddScoped<BolsaGraduacaoDocente>();
builder.Services.AddScoped<BolsaGraduacaoAdm>();
builder.Services.AddScoped<PublicoExternoInteractionService>();
builder.Services.AddScoped<InformacoesUteis>();



builder.Services.AddOpenApi();


var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();