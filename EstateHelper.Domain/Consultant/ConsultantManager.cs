using EstateHelper.Application.Contract;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Consultant
{
    public class ConsultantManager : IConsultantManager
    {
        private readonly IConsultantRepository _consultantRepository;

        public ConsultantManager(IConsultantRepository consultantRepository)
        {
            _consultantRepository = consultantRepository;
        }
        public async Task<PagedResultDto<List<AppUser>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination)
        {
            var result = await _consultantRepository.GetAllByFilter(Id, queryParam, pagination);    
            return result;  
        }

        public async Task<PagedResultDto<List<AppUser>>> GetAllConsultants(PaginationParamaters paginationParamaters)
        {
            var result = await _consultantRepository.GetAllAsync(paginationParamaters);
            return result;  
        }
    }
}
