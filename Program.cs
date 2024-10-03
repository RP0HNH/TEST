using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add the DbContext to the DI container
builder.Services.AddDbContext<StemyCloudContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("StemyCloudConnection"))
           .EnableSensitiveDataLogging() // �������� ������������ �������������� ������
           .LogTo(Console.WriteLine, LogLevel.Information); // ����������� ��������
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders(); // ������� ����������� ����������
builder.Logging.AddConsole(); // ��������� ������������ � �������
builder.Logging.AddDebug(); // ��������� ������������ ��� �������

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
