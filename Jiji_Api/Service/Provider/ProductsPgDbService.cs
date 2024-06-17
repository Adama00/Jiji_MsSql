using Jiji_Api.Data;
using Jiji_Api.Models;
using Jiji_Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Jiji_Api.Service.Provider
{
    public class ProductsPgDbService : IProductsService

    {

        private readonly JijiDbContext _context;
        public ProductsPgDbService(JijiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseCart<Cart>> AddToCart(Cart cartItem)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseCart<Cart>> GetCart()
        {
            try
            {
                var product = _context.Cart;
                if (product == null)
                {
                    return new ApiResponseCart<Cart>
                    {
                        Code = $"{(int)HttpStatusCode.NotFound}",
                        Message = "No Records Found",

                    };
                }

                await Task.CompletedTask;
                return new ApiResponseCart<Cart>
                {
                    Code = $"{(int)HttpStatusCode.OK}",
                    Message = $"Query successful",
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseCart<Cart>
                {
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = $"Error getting Product records, {ex.Message}"

                };
            }
        }

        public async Task<ApiResponseProducts<Products>> GetProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return new ApiResponseProducts<Products>
                    {
                        Code = $"{(int)HttpStatusCode.NotFound}",
                        Message = "No Records Found",

                    };
                }

                await Task.CompletedTask;
                return new ApiResponseProducts<Products>
                {
                    Code = $"{(int)HttpStatusCode.OK}",
                    Message = $"Query with parameters Id: {id}",
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseProducts<Products>
                {
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = $"Error getting Product records, {ex.Message}"

                };
            }
        }

       

        //Get All Products
        public async Task<ApiResponseProducts<Products>> GetProducts([FromQuery] int? categoryId, [FromQuery] int? regionId, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            try 
            {
                var query = _context.Products.AsQueryable();
                if (categoryId != null)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                if (regionId != null) 
                {
                    query = query.Where(p=>p.RegionId == regionId);
                }
                if(minPrice != null)
                {
                    query = query.Where(p=>p.Price >= minPrice);
                }
                if(maxPrice != null)
                {
                    query = query.Where((p)=>p.Price <= maxPrice);
                }
                if(query == null)
                {
                    return new ApiResponseProducts<Products>
                    {
                        Code = $"{(int)HttpStatusCode.NotFound}",
                        Message = "No Records Found",

                    };
                }
                var products = query;
                await Task.CompletedTask;
                return new ApiResponseProducts<Products>
                {
                    Code = $"{(int)HttpStatusCode.OK}",
                    Message = $"Query with parameters regionId: {regionId}",
                    Data = (Products)query,
                };
                //var products = query.ToList();
            }catch (Exception ex) 
            {
                return new ApiResponseProducts<Products>
                {
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = $"Error getting Product records, {ex.Message}"

                };
            }
        }

        public async Task<ApiResponseCart<Cart>> RemoveFromCart(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponseCart<Cart>> UpdateCart(int id, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
