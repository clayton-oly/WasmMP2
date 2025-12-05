namespace WasmMP2.Client.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }
    }
}
