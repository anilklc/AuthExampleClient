using System.ComponentModel.DataAnnotations;

namespace AuthExampleClient.DTOs.Product
{
    public class CreateProduct
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero.")]
        public float? ProductPrice { get; set; }
        [Required(ErrorMessage = "Brand name is required.")]
        public string BrandId { get; set; }
    }
}
