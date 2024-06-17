using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jiji_Api.Models
{
    [Table("cart")]
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products? Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
