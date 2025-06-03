using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs
{
    public class BlogDTo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class BlogCreateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string AuthorName { get; set; }  
        public int CategoryId { get; set; }
    }

    public class BlogUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorName { get; set; } 
        public DateTime? UpdatedAt {  get; set; }
        public int CategoryId { get; set; }
    }
}
