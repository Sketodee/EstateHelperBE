using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Products;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Products
{
    public interface IProductManager
    {
        Task<Product> Update(EditProductDto input);
        Task<Product> Create(CreateProductDto input);
        Task<bool> Delete(string Id);
        Task<List<Product>> GetAllProducts(string? Id, string? Name, PaginationParamaters pagination);
    }
}
