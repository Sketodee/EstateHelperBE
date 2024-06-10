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
        Task<Product> UpdateAsync(EditProductDto input);
        Task<Product> CreateAsync(CreateProductDto input);
        Task<bool> DeleteAsync(string Id);
        Task<List<Product>> GetAllAsync(string? Id, string? Name, PaginationParamaters pagination);
        Task<Product> SingleOrDefaultAsync(Expression<Func<Product, bool>> predicate);
    }
}
