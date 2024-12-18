using System.ComponentModel.DataAnnotations;

namespace HW_ASP_11._2.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название должно быть не более 100 символов")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Описание должно быть не более 500 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Дата выполнения обязательна")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Статус обязателен")]
        [RegularExpression("^(Not Started|In Progress|Completed)$", ErrorMessage = "Недопустимый статус")]
        public string Status { get; set; }
    }
}
