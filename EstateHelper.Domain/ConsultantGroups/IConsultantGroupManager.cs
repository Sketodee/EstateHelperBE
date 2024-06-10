using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.ConsultantGroups
{
    public interface IConsultantGroupManager
    {
        Task<ConsultantGroup> CreateAsync(CreateConsultantGroupDto input);
        Task<ConsultantGroup> AddOrRemoveMembersToGroup(AddMembersToConsultantGroupDto input);
        Task<bool> DeleteConsultantGroup(string Id);
        Task<List<ConsultantGroup>> GetAllConsultantGroup(string? Id, string? Name, string? Email, PaginationParamaters pagination);
        Task<ConsultantGroup> EditConsultantGroup(EditConsultantGroupDto input);
      
    }
}
