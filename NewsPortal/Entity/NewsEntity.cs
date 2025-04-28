using System.ComponentModel.DataAnnotations;
using NewsPortal.Enums;

namespace NewsPortal.Entity
{
    public class NewsEntity
    {
        public int Id { get; set; }

        
        public string? Title { get; set; } 

        
        public NewsCategoryEnum? Category { get; set; }

        
        public string? Description { get; set; }

        
        public string? ImageUrl { get; set; }    



    }
}
