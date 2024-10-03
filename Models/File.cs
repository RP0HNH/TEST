using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CloudFile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название файла обязательно.")]
        [StringLength(100, ErrorMessage = "Название файла не должно превышать 100 символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Автор файла обязателен.")]
        public string Author { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow; // Устанавливаем значение по умолчанию на текущую дату

        [Range(1, long.MaxValue, ErrorMessage = "Размер файла должен быть положительным.")]
        public long Size { get; set; }

        [Required(ErrorMessage = "Формат файла обязателен.")]
        public string Format { get; set; }
    }
}
