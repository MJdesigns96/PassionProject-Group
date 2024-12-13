using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class ViewerDto
    {
        public required string ViewerId { get; set; }
        public string? ViewerName { get; set; }

    }
}
