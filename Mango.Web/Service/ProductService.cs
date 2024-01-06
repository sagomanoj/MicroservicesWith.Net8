using Mango.Web.Models;
using Mango.Web.Models.Dto;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService) 
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto> CreateProductAsync(ProductDto dto)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.POST,
                Url = Utility.Utility.ProductAPIBase + "/api/product/",
                Data = dto
            });

            return result;
        }

        public async Task<ResponseDto> DeleteProductAsync(int id)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.DELETE,
                Url = Utility.Utility.ProductAPIBase + "/api/product/" + id
            });

            return result;
        }

        public async Task<ResponseDto> GetAllProductsAsync()
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.GET,
                Url = Utility.Utility.ProductAPIBase + "/api/product"
            });

            return result;
        }

        public async Task<ResponseDto> GetProductByIdAsync(int id)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.GET,
                Url = Utility.Utility.ProductAPIBase + "/api/product/" + id
            });

            return result;
        }

        public async Task<ResponseDto> UpdateProductAsync(ProductDto dto)
        {
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Utility.Utility.APIype.PUT,
                Url = Utility.Utility.ProductAPIBase + "/api/product/",
                Data = dto
            });

            return result;
        }
    }
}
