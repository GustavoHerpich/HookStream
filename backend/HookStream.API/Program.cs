using HookStream.API.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceConfigurations(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowAll"); 
app.UseWebSocketManager();

app.MapControllers();

app.Run();
