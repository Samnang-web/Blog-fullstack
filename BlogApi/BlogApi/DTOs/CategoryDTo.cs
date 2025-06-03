using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs
{
    public class CategoryDTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CategoryCreateDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
