namespace Learn.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string? ProductColor { get; set; }
        public bool IsAvailable { get; set; }
        public int ProductTypeId { get; set; }
        public int SpecialTagId { get; set; }
    }
}
