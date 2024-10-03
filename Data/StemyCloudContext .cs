using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class StemyCloudContext : DbContext
{
    // Конструктор принимает DbContextOptions и передает их в базовый класс
    public StemyCloudContext(DbContextOptions<StemyCloudContext> options) : base(options) { }

    // Таблица для хранения файлов
    public DbSet<CloudFile> Files { get; set; }

    // Таблица для хранения пользователей
    public DbSet<User> Users { get; set; }

    // Метод для настройки модели данных
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройки для модели CloudFile
        modelBuilder.Entity<CloudFile>(entity =>
        {
            entity.HasKey(e => e.Id); // Задаем первичный ключ
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100); // Поле Name обязательно
            entity.Property(e => e.Author).IsRequired(); // Поле Author обязательно
            entity.Property(e => e.UploadDate).IsRequired(); // Поле UploadDate обязательно
            entity.Property(e => e.Size).IsRequired(); // Поле Size обязательно
            entity.Property(e => e.Format).IsRequired(); // Поле Format обязательно
        });

        // Настройки для модели User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id); // Задаем первичный ключ
            // Здесь можно добавить дополнительные настройки для модели User
        });

        base.OnModelCreating(modelBuilder); // Вызываем метод базового класса
    }
}
