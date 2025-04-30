using System.ComponentModel.DataAnnotations;
using NewsPortal.Enums;

namespace NewsPortal.Entity
{
    public class NewsEntity
    {
        public int Id { get; set; }

        
        public string? Title { get; set; } 

       
        // This is foreign key
        public int? CategoryId { get; set; }

        // Navigation property
        public Category? Category { get; set; }

        public string? Description { get; set; }

        
        public string? ImageUrl { get; set; }    



    }
}
