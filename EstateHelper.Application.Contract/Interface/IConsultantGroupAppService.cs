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
        Task<List<GetConsultantGroupDto>> GetAllConsultantGroup(string? Id, string? Name, string? Email, PaginationParamaters pagination);
        Task<GetConsultantGroupDto> EditConsultantGroup(EditConsultantGroupDto input);
        
    }
}
