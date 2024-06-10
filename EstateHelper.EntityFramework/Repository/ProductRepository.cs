using EstateHelper.Application.Contract;
using EstateHelper.Domain.Models;
using EstateHelper.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EstateHelper.EntityFramework.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product input)
        {
            await _context.Products.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<bool> DeleteAsync(Product input)
        {
            _context.Products.Update(input);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Product>> GetAllAsync(string? Id, string? Name, PaginationParamaters pagination)
        {
            var query = await _context.Products.Where(x => !x.isDeleted).ToListAsync();
            if (query.Count == 0) throw new Exception("No Product found");
            if (!string.IsNullOrEmpty(Id))
            {
                query = query.Where(x => x.Id == Id).ToList();
            }
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.Name == Name).ToList();
            }
            if (query.Count == 0) throw new Exception("No Consultant Group found");
            query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return query;
        }

        public async Task<Product> SingleOrDefaultAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await _context.Set<Product>().Where(x => !x.isDeleted).FirstOrDefaultAsync(predicate) ?? throw new Exception("Product not found");
            return product;
        }

        public async Task<Product> UpdateAsync(Product input)
        {
            _context.Products.Update(input);
            await _context.SaveChangesAsync();
            return input;
        }
    }
}
