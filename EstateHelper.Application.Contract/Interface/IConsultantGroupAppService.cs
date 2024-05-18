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
    }
}
