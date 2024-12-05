var builder = WebApplication.CreateBuilder(args);

// ### Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// ### Configure the HTTP request pipeline.

// Use the strict-transport-security header in production
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseAuthorization();

app.MapControllers();

app.Run();