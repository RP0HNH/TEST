using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add the DbContext to the DI container
builder.Services.AddDbContext<StemyCloudContext>(options =>
{
    // ���������� PostgreSQL � ���������� ����������� ��������
    options.UseNpgsql(builder.Configuration.GetConnectionString("StemyCloudConnection"))
           .EnableSensitiveDataLogging() // �������� ������������ �������������� ������ (��������, ��������� SQL-��������)
           .LogTo(Console.WriteLine, LogLevel.Information); // ����������� �������� � �������
});

// ������� ������������ Swagger/OpenAPI
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders(); // ������� ����������� �������
builder.Logging.AddConsole(); // ����������� � �������
builder.Logging.AddDebug(); // ����������� ��� �������

// ��������� Kestrel, ���� ��������� ��������� ����� �� ������ ���� �������
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 104857600; // 100 MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // ������� ��������� Swagger � ������ ����������
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

// �������� �������� �� HTTPS
app.UseHttpsRedirection();

// ���������� �����������
app.UseAuthorization();

// ����������� ������������� ������������
app.MapControllers();

// ������ ����������
app.Run();
