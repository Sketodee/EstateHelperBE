using EstateHelper.Application.Contract;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product> UpdateAsync(Product input);
        Task<Product> CreateAsync(Product input);
        Task<bool> DeleteAsync(Product input);
        Task<List<Product>> GetAllAsync(string? Id, string? Name, PaginationParamaters pagination);
        Task<Product> SingleOrDefaultAsync(Expression<Func<Product, bool>> predicate);
        
    }
}
