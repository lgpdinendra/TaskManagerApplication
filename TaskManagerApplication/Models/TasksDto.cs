using System.ComponentModel.DataAnnotations;

namespace TaskManagerApplication.Models
{
    public class TasksDto
    {
        public int Id { get; set; }

        [MaxLength(5)]
        [Required]
        public String TaskId { get; set; } = "";

        [MaxLength(100)]
        [Required]
        public string Title { get; set; } = "";

        [MaxLength(200)]
        [Required]
        public string Description { get; set; } = "";
    }
}
