using Jiji_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jiji_Api.Service.Interface
{
    public interface IProductsService
    {
        public Task<ApiResponseProducts<Products>> GetProducts([FromQuery] int? categoryId, [FromQuery] int? regionId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice);
        public Task<ApiResponseProducts<Products>> GetProduct(int id);        
        public Task<ApiResponseCart<Cart>> AddToCart(Cart cartItem);
        public Task<ApiResponseCart<Cart>> GetCart();
        public Task<ApiResponseCart<Cart>> UpdateCart(int id, int quantity);
        public Task<ApiResponseCart<Cart>> RemoveFromCart(int id);
       
        

    }
}
