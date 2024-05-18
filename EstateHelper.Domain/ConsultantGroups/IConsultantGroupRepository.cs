using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;

namespace EstateHelper.Domain.ConsultantGroups
{
    public interface IConsultantGroupRepository
    {
        Task<ConsultantGroup> CreateAsync(CreateConsultantGroupDto input);
    }
}
