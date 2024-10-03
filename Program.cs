using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add the DbContext to the DI container
builder.Services.AddDbContext<StemyCloudContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("StemyCloudConnection"))
           .EnableSensitiveDataLogging() // Включить логгирование чувствительных данных
           .LogTo(Console.WriteLine, LogLevel.Information); // Логирование запросов
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders(); // Удаляем стандартные провайдеры
builder.Logging.AddConsole(); // Добавляем логгирование в консоль
builder.Logging.AddDebug(); // Добавляем логгирование для отладки

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
