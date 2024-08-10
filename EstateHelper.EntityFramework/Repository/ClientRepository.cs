using EstateHelper.Application.Contract;
using EstateHelper.Domain.Clients;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.EntityFramework.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        private readonly Helpers _helpers;
        private AppUser _loggedInUser;

        public ClientRepository(AppDbContext context, Helpers helpers)
        {
            _context = context;
            _helpers = helpers;
            InitializeLoggedInUser().GetAwaiter().GetResult();
        }

        private async Task InitializeLoggedInUser()
        {
            _loggedInUser = await _helpers.ReturnLoggedInUser();
        }

        public async Task<Client> CreateAsync(Client input)
        {
            input.CreatedBy = _loggedInUser.Id;
            input.CreatedOn = DateTime.Now;
            await _context.Clients.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<bool> DeleteAsync(Client input)
        {
            input.isDeleted = true;
            input.DeletedBy = _loggedInUser.Id;
            input.DeletedOn = DateTime.Now;
            _context.Clients.Update(input);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<PagedResultDto<List<Client>>> GetAllAsync(PaginationParamaters pagination)
        {
            var result = await _context.Clients.Where(x => !x.isDeleted).OrderByDescending(x => x.CreatedOn).ToListAsync();
            if (result.Count == 0) throw new Exception("No ClientFound");
            var query = result.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return new PagedResultDto<List<Client>>
            {
                TotalCount = result.Count,
                Data = query
            };
        }

        public async Task<Client> SingleOrDefaultAsync(Expression<Func<Client, bool>> predicate)
        {
            var client = await _context.Set<Client>().Where(x => !x.isDeleted).FirstOrDefaultAsync(predicate);
            return client;
        }

        public async Task<Client> UpdateAsync(Client input)
        {
            input.LastUpdatedBy = _loggedInUser.Id;
            input.LastUpdatedOn = DateTime.Now;
            _context.Clients.Update(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<PagedResultDto<List<Client>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination)
        {
            var total = await _context.Clients.OrderByDescending(x => x.CreatedOn).ToListAsync();
            if (total.Count == 0) throw new Exception("No Client found");
            if (!string.IsNullOrEmpty(Id))
            {
                total = total.Where(x => x.Id == Id).ToList();
            }
            if (!string.IsNullOrEmpty(queryParam))
            {
                total = total.Where(x => x.FirstName.ToLower().Contains(queryParam.ToLower())
                || x.Email.ToLower().Contains(queryParam.ToLower())
                || x.Surname.ToLower().Contains(queryParam.ToLower())).ToList();
            }
            if (total.Count == 0) throw new Exception("No Client found");
            var query = total.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return new PagedResultDto<List<Client>>
            {
                TotalCount = total.Count,
                Data = query
            };
        }
    }
}
