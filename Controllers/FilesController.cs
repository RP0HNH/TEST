using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Добавлено для логгирования
using WebApplication1.Models;
using System.IO;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly StemyCloudContext _context;
    private readonly ILogger<FilesController> _logger; // Добавлено

    public FilesController(StemyCloudContext context, ILogger<FilesController> logger) // Добавлено
    {
        _context = context;
        _logger = logger; // Добавлено
    }

    [HttpPost]
    public async Task<ActionResult<CloudFile>> UploadFile([FromForm] IFormFile file, [FromForm] string author)
    {
        try
        {
            // Шаг 1: Проверка файла
            if (!IsValidFile(file, out var validationMessage))
            {
                _logger.LogWarning("Ошибка валидации файла: {Message}", validationMessage);
                return BadRequest(validationMessage);
            }

            // Шаг 2: Проверка на уникальность имени файла
            var existingFile = await _context.Files.FirstOrDefaultAsync(f => f.Name == file.FileName);
            if (existingFile != null)
            {
                _logger.LogWarning("Файл с именем {FileName} уже существует", file.FileName);
                return Conflict($"Файл с именем {file.FileName} уже существует.");
            }

            // Шаг 3: Определение информации о файле
            var cloudFile = GetFileInfo(file, author);
            _logger.LogInformation("Информация о файле определена: {FileName}, размер: {Size} байт", cloudFile.Name, cloudFile.Size);

            // Шаг 4: Сохранение файла физически
            await SaveFileToDiskAsync(file);
            _logger.LogInformation("Файл {FileName} успешно сохранён на диск", cloudFile.Name);

            // Шаг 5: Сохранение информации о файле в базе данных
            await SaveFileInfoToDatabaseAsync(cloudFile);
            _logger.LogInformation("Информация о файле {FileName} сохранена в базе данных", cloudFile.Name);

            return CreatedAtAction(nameof(GetFile), new { id = cloudFile.Id }, cloudFile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при загрузке файла");
            return StatusCode(500, "Произошла ошибка при загрузке файла.");
        }
    }


    // Метод для проверки файла
    private bool IsValidFile(IFormFile file, out string validationMessage)
    {
        if (file == null || file.Length == 0)
        {
            validationMessage = "Файл не выбран или пуст.";
            return false;
        }

        validationMessage = string.Empty;
        return true;
    }

    // Метод для определения информации о файле
    private CloudFile GetFileInfo(IFormFile file, string author)
    {
        return new CloudFile
        {
            Name = file.FileName,
            Author = author,
            UploadDate = DateTime.UtcNow, // Используем UTC вместо локального времени
            Size = file.Length,
            Format = Path.GetExtension(file.FileName)
        };
    }


    // Метод для сохранения файла на диск
    private async Task SaveFileToDiskAsync(IFormFile file)
    {
        var filePath = Path.Combine("files", file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
    }

    // Метод для сохранения информации о файле в базу данных
    private async Task SaveFileInfoToDatabaseAsync(CloudFile cloudFile)
    {
        _context.Files.Add(cloudFile);
        await _context.SaveChangesAsync();
    }


    [HttpGet]
    public async Task<ActionResult<List<CloudFile>>> GetFiles()
    {
        try
        {
            return await _context.Files.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении файлов"); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при получении файлов.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CloudFile>> GetFile(int id)
    {
        try
        {
            var cloudFile = await _context.Files.FindAsync(id);
            if (cloudFile == null)
            {
                return NotFound();
            }

            return cloudFile;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении файла с ID {Id}", id); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при получении файла.");
        }
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile(int id)
    {
        try
        {
            var cloudFile = await _context.Files.FindAsync(id);
            if (cloudFile == null)
            {
                return NotFound("Файл не найден.");
            }

            var filePath = Path.Combine("files", cloudFile.Name);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Физический файл не найден.");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, cloudFile.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при загрузке файла с ID {Id}", id); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при загрузке файла.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFile(int id, [FromBody] string newName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                return BadRequest("Новое имя не может быть пустым.");
            }

            // Проверяем, существует ли файл с таким же именем
            var existingFile = await _context.Files.FirstOrDefaultAsync(f => f.Name == newName);
            if (existingFile != null)
            {
                return Conflict($"Файл с именем {newName} уже существует.");
            }

            var cloudFile = await _context.Files.FindAsync(id);
            if (cloudFile == null)
            {
                return NotFound("Файл не найден.");
            }

            cloudFile.Name = newName;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении файла с ID {Id}", id); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при обновлении файла.");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(int id)
    {
        try
        {
            var cloudFile = await _context.Files.FindAsync(id);
            if (cloudFile == null)
            {
                return NotFound("Файл не найден.");
            }

            _context.Files.Remove(cloudFile);
            await _context.SaveChangesAsync();

            // Удаляем физический файл, если он существует
            var filePath = Path.Combine("files", cloudFile.Name);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении файла с ID {Id}", id); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при удалении файла.");
        }
    }
}
