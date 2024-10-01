using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.IO;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly StemyCloudContext _context;

    public FilesController(StemyCloudContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<CloudFile>> UploadFile([FromForm] IFormFile file, [FromForm] string author)
    {
        // Проверяем, есть ли файл
        if (file == null || file.Length == 0)
        {
            return BadRequest("Файл не выбран.");
        }

        // Сохраняем файл физически
        var filePath = Path.Combine("files", file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Сохраняем информацию о файле в БД
        var cloudFile = new CloudFile
        {
            Name = file.FileName,
            Author = author,
            UploadDate = DateTime.Now,
            Size = file.Length,
            Format = Path.GetExtension(file.FileName)
        };

        _context.Files.Add(cloudFile);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFile), new { id = cloudFile.Id }, cloudFile);
    }

    [HttpGet]
    public async Task<ActionResult<List<CloudFile>>> GetFiles()
    {
        return await _context.Files.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CloudFile>> GetFile(int id)
    {
        var cloudFile = await _context.Files.FindAsync(id);
        if (cloudFile == null)
        {
            return NotFound();
        }

        return cloudFile;
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile(int id)
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFile(int id, [FromBody] string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            return BadRequest("Новое имя не может быть пустым.");
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(int id)
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
}
