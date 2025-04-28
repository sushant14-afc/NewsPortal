using NewsPortal.Enums;

namespace NewsPortal.Model
{
    public class NewsModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public NewsCategoryEnum? Category { get; set; }

        public string? ImageUrl { get; set; }
    }
}
