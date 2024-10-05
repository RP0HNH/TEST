using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add the DbContext to the DI container
builder.Services.AddDbContext<StemyCloudContext>(options =>
{
    // Используем PostgreSQL и подключаем логирование запросов
    options.UseNpgsql(builder.Configuration.GetConnectionString("StemyCloudConnection"))
           .EnableSensitiveDataLogging() // Включить логгирование чувствительных данных (например, параметры SQL-запросов)
           .LogTo(Console.WriteLine, LogLevel.Information); // Логирование запросов в консоль
});

// Удаляем конфигурацию Swagger/OpenAPI
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders(); // Очищаем стандартные логгеры
builder.Logging.AddConsole(); // Логирование в консоль
builder.Logging.AddDebug(); // Логирование для отладки

// Настройка Kestrel, если требуется увеличить лимит на размер тела запроса
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 104857600; // 100 MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Удаляем включение Swagger в режиме разработки
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

// Включаем редирект на HTTPS
app.UseHttpsRedirection();

// Подключаем авторизацию
app.UseAuthorization();

// Настраиваем маршрутизацию контроллеров
app.MapControllers();

// Запуск приложения
app.Run();
