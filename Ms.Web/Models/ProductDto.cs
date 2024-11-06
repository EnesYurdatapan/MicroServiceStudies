namespace Ms.Web.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
		//[AllowedExtensions(new string[] { ".jpg", ".png" })]
		public IFormFile? Image { get; set; }
        public string? ImageLocalPath { get; set; }
    }
}
