using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Добавлено для логгирования
using WebApplication1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly StemyCloudContext _context;
    private readonly ILogger<UsersController> _logger; // Добавлено

    public UsersController(StemyCloudContext context, ILogger<UsersController> logger) // Добавлено
    {
        _context = context;
        _logger = logger; // Добавлено
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromBody] User user)
    {
        try
        {
            // Проверка на валидацию входных данных
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                return BadRequest("Имя и фамилия пользователя не могут быть пустыми.");
            }

            // Проверка на существование пользователя с таким же именем
            var existingUser = await _context.Users
                .AnyAsync(u => u.FirstName == user.FirstName && u.LastName == user.LastName);
            if (existingUser)
            {
                return Conflict("Пользователь с таким именем уже существует.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании пользователя"); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при создании пользователя.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении пользователей"); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при получении пользователей.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении пользователя с ID {Id}", id); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при получении пользователя.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        try
        {
            // Проверка на валидацию входных данных
            if (string.IsNullOrWhiteSpace(updatedUser.FirstName) || string.IsNullOrWhiteSpace(updatedUser.LastName))
            {
                return BadRequest("Имя и фамилия пользователя не могут быть пустыми.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Проверяем, существует ли пользователь с такими же именем и фамилией
            var existingUser = await _context.Users
                .AnyAsync(u => u.FirstName == updatedUser.FirstName && u.LastName == updatedUser.LastName && u.Id != id);

            if (existingUser)
            {
                return Conflict("Пользователь с такими именем и фамилией уже существует.");
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Biography = updatedUser.Biography;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении пользователя с ID {Id}", id); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при обновлении пользователя.");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении пользователя с ID {Id}", id); // Логгирование ошибки
            return StatusCode(500, "Произошла ошибка при удалении пользователя.");
        }
    }
}
