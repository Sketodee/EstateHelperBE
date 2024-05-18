using AutoMapper;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.ConsultantGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.ConsultantGroups
{
    public class ConsultantGroupAppService : IConsultantGroupAppService
    {
        private readonly IConsultantGroupManager _consultantGroupManager;
        private readonly IMapper _mapper;

        public ConsultantGroupAppService(IConsultantGroupManager consultantGroupManager, IMapper mapper)
        {
            _consultantGroupManager = consultantGroupManager;
            _mapper = mapper;
        }
        public async Task<GetConsultantGroupDto> CreateAsync(CreateConsultantGroupDto input)
        {
            var result = await _consultantGroupManager.CreateAsync(input);  
            return _mapper.Map<GetConsultantGroupDto>(result);  
        }
    }
}
