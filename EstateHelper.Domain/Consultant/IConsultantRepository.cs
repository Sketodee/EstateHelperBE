using EstateHelper.Application.Contract;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Consultant
{
    public interface IConsultantRepository
    {
        Task<PagedResultDto<List<AppUser>>> GetAllAsync(PaginationParamaters pagination);
        Task<PagedResultDto<List<AppUser>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination);
    }
}
