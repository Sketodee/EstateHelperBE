using EstateHelper.Application.Contract.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Interface
{
    public interface IConsultantAppService
    {
        Task<PagedResultDto<List<GetUserDto>>> GetAllConsultants(PaginationParamaters paginationParamaters);
        Task<PagedResultDto<List<GetUserDto>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination);
    }
}
