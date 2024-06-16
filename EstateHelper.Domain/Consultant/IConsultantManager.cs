using EstateHelper.Application.Contract;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Consultant
{
    public interface IConsultantManager
    {
        Task<PagedResultDto<List<AppUser>>> GetAllConsultants(PaginationParamaters paginationParamaters);
        Task<PagedResultDto<List<AppUser>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination);
    }
}
