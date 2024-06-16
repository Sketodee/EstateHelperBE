using EstateHelper.Application.Contract;
using EstateHelper.Domain.Consultant;
using EstateHelper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.EntityFramework.Repository
{
    public class ConsultantRepository : IConsultantRepository
    {
        private readonly AppDbContext _context;

        public ConsultantRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResultDto<List<AppUser>>> GetAllAsync(PaginationParamaters pagination)
        {
            var result = await _context.Users.OrderByDescending(x => x.CreatedOn).ToListAsync();
            if (result.Count == 0) throw new Exception("No Consultant Found");
            var query = result.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return new PagedResultDto<List<AppUser>>
            {
                TotalCount = result.Count,
                Data = query
            };
        }

        public async Task<PagedResultDto<List<AppUser>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination)
        {
            var total = await _context.Users.OrderByDescending(x=> x.CreatedOn).ToListAsync();
            if (total.Count == 0) throw new Exception("No Consultant found");
            if (!string.IsNullOrEmpty(Id))
            {
                total = total.Where(x => x.Id == Id).ToList();
            }
            if (!string.IsNullOrEmpty(queryParam))
            {
                total = total.Where(x => x.FirstName.ToLower().Contains(queryParam.ToLower()) || x.Email.ToLower().Contains(queryParam.ToLower()) || x.Surname.Contains(queryParam)).ToList();
            }
            if (total.Count == 0) throw new Exception("No Consultant found");
            var query = total.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return new PagedResultDto<List<AppUser>>
            {
                TotalCount = total.Count,
                Data = query
            };
        }
    }
}
