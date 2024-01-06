using Mango.Web.Models;
using Mango.Web.Models.Dto;

namespace Mango.Web.Service
{
    public interface IProductService
    {
        Task<ResponseDto> GetAllProductsAsync();
        Task<ResponseDto> GetProductByIdAsync(int id);
        Task<ResponseDto> UpdateProductAsync(ProductDto dto);
        Task<ResponseDto> CreateProductAsync(ProductDto dto);
        Task<ResponseDto> DeleteProductAsync(int id);
    }
}
