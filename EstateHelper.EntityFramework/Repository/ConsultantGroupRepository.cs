using AutoMapper;
using Azure.Core;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Domain.ConsultantGroups;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using EstateHelper.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.EntityFramework.Repository
{
    public class ConsultantGroupRepository : IConsultantGroupRepository
    {
        private readonly AppDbContext _context;

        public ConsultantGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ConsultantGroup> CreateAsync(ConsultantGroup input)
        {
            await _context.ConsultantGroups.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<bool> DeleteAsync(ConsultantGroup input)
        {
            _context.ConsultantGroups.Update(input);
            int result = await _context.SaveChangesAsync();
            return result>0;
        }

        public async Task<List<ConsultantGroup>> GetAllAsync(string? Id, string? Name, string? Email, PaginationParamaters pagination)
        {
            var query = await _context.ConsultantGroups.Where(x => !x.isDeleted).ToListAsync(); 
            if(query.Count== 0) throw new Exception("No Consultant Group found");
            if (!string.IsNullOrEmpty(Id))
            {
                query = query.Where(x => x.Id == Id).ToList();
            }
            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.Name == Name).ToList();
            }
            if (!string.IsNullOrEmpty(Email))
            {
                query = query.Where(x => x.Email == Email).ToList();
            }
            if (query.Count == 0) throw new Exception("No Consultant Group found");
            query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).OrderByDescending(x => x.CreatedOn).ToList();
            return query; 
        }

        public async Task<ConsultantGroup> SingleOrDefaultAsync(Expression<Func<ConsultantGroup, bool>> predicate)
        {
            var consultantGroup = await _context.Set<ConsultantGroup>().Where(x=> !x.isDeleted).FirstOrDefaultAsync(predicate);
            return consultantGroup;
        }

        public async Task<ConsultantGroup> UpdateAsync(ConsultantGroup input)
        {
             _context.ConsultantGroups.Update(input);
            await _context.SaveChangesAsync();
            return input;
        }
    }
}
