using Microsoft.EntityFrameworkCore;

namespace Jiji_Api.Models
{
    public class ApiResponseCart<T>
    {
        public  string Code { get; set; }
        public string? Message { get; set; }
        public DbSet<Cart>? Data { get; set; }
    }
}
