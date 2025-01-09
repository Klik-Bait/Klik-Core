var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/hc", () => "Healthy!");
app.MapGet("/health-check", () => "Healthy!");

app.UseHttpsRedirection();

