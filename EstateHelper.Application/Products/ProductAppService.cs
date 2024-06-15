using AutoMapper;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Products;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Products
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductManager _productManager;
        private readonly IMapper _mapper;

        public ProductAppService(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }
        public async Task<GetProductDto> Create(CreateProductDto input)
        {
            var result = await _productManager.Create(input);  
            return _mapper.Map<GetProductDto>(result); 
        }

        public async Task<bool> Delete(string Id)
        {
            var result = await _productManager.Delete(Id);
            return result; 
        }

        public async Task<List<GetProductDto>> GetAllProducts(string? Id, string? Name, PaginationParamaters pagination)
        {
            var result = await _productManager.GetAllProducts(Id, Name, pagination);    
            return _mapper.Map<List<GetProductDto>>(result);
        }

        public async Task<GetProductDto> Update(EditProductDto input)
        {
            var result = await _productManager.Update(input);
            return _mapper.Map<GetProductDto>(result); 
        }
    }
}
