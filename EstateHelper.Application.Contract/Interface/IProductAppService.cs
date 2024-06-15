using EstateHelper.Application.Contract.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Interface
{
    public interface IProductAppService
    {
        Task<GetProductDto> Create(CreateProductDto input);

        Task<GetProductDto> Update(EditProductDto input);
        Task<bool> Delete(string Id);
        Task<List<GetProductDto>> GetAllProducts(string? Id, string? Name, PaginationParamaters pagination);
    }
}
