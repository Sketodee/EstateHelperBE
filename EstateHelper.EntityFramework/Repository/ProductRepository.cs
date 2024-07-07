using EstateHelper.Application.Contract;
using EstateHelper.Domain.HelperFunctions;
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
        private readonly Helpers _helpers;
        private AppUser _loggedInUser;

        public ProductRepository(AppDbContext context, Helpers helpers)
        {
            _context = context;
            _helpers = helpers;
            InitializeLoggedInUser().GetAwaiter().GetResult();
        }

        private async Task InitializeLoggedInUser()
        {
            _loggedInUser = await _helpers.ReturnLoggedInUser();
        }

        public async Task<Product> CreateAsync(Product input)
        {
            input.CreatedBy = _loggedInUser.Id; 
            input.CreatedOn = DateTime.Now;
            await _context.Products.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<bool> DeleteAsync(Product input)
        {
            input.isDeleted = true; 
            input.DeletedBy = _loggedInUser.Id; 
            input.DeletedOn = DateTime.Now;
            _context.Products.Update(input);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<PagedResultDto<List<Product>>> GetAllAsync(PaginationParamaters pagination)
        {
            var result = await _context.Products.Where(x => !x.isDeleted).Include(x => x.Pricing).OrderByDescending(x=> x.CreatedOn).ToListAsync();
            if (result.Count == 0) throw new Exception("No Product Found");
            var query = result.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return new PagedResultDto<List<Product>>
            {
                TotalCount = result.Count,
                Data = query
            };
        }

        public async Task<Product> SingleOrDefaultAsync(Expression<Func<Product, bool>> predicate)
        {
            var product = await _context.Set<Product>().Where(x => !x.isDeleted).Include(x=> x.Pricing).FirstOrDefaultAsync(predicate);
            return product;
        }

        public async Task<Product> UpdateAsync(Product input)
        {
            input.LastUpdatedBy = _loggedInUser.Id;
            input.LastUpdatedOn = DateTime.Now; 
            _context.Products.Update(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<PagedResultDto<List<Product>>> GetAllByFilter(string? Id, string? Name, PaginationParamaters pagination)
        {
            var total = await _context.Products.Where(x => !x.isDeleted).Include(x => x.Pricing).OrderByDescending(x => x.CreatedOn).ToListAsync();
            if (total.Count == 0) throw new Exception("No Product found");
            if (!string.IsNullOrEmpty(Id))
            {
                total = total.Where(x => x.Id == Id).ToList();
            }
            if (!string.IsNullOrEmpty(Name))
            {
                total = total.Where(x => x.Name.ToLower().Contains(Name.ToLower())).ToList();
            }
            if (total.Count == 0) throw new Exception("No Product found");
            var query = total.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return new PagedResultDto<List<Product>>
            {
                TotalCount = total.Count,
                Data = query
            };
        }
    }
}
