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
    }
}
