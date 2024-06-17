using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jiji_Api.Models
{
    [Table("products")]
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Categories? Category { get; set; }

        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public Regions? Region { get; set; }
    }
}
