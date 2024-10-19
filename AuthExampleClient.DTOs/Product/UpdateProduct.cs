namespace AuthExampleClient.DTOs.Product
{
    public class UpdateProduct
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public float ProductPrice { get; set; }
        public string BrandId { get; set; }
    }
}
