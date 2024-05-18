using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.ConsultantGroups
{
    public class ConsultantGroupManager : IConsultantGroupManager
    {
        private readonly IConsultantGroupRepository _consultantGroupRepository;

        public ConsultantGroupManager(IConsultantGroupRepository consultantGroupRepository)
        {
            _consultantGroupRepository = consultantGroupRepository;
        }
        public async Task<ConsultantGroup> CreateAsync(CreateConsultantGroupDto input)
        {
            var result = await _consultantGroupRepository.CreateAsync(input);
            return result;
        }
    }
}
