using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Interface
{
    public interface IConsultantGroupAppService
    {
        Task<GetConsultantGroupDto> CreateAsync(CreateConsultantGroupDto input);
        Task<GetConsultantGroupDto> AddOrRemoveMembersToGroup(AddMembersToConsultantGroupDto input);
        Task<bool> DeleteConsultantGroup(string Id);
        Task<PagedResultDto<List<GetConsultantGroupDto>>> GetAllConsultantGroup(PaginationParamaters pagination);
        Task<PagedResultDto<List<GetConsultantGroupDto>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination);
        Task<GetConsultantGroupDto> EditConsultantGroup(EditConsultantGroupDto input);
        
    }
}
