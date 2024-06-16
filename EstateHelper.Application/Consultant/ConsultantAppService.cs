using AutoMapper;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Consultant
{
    public class ConsultantAppService : IConsultantAppService
    {
        private readonly IConsultantManager _consultantManager;
        private readonly IMapper _mapper;

        public ConsultantAppService(IConsultantManager consultantManager, IMapper mapper)
        {
            _consultantManager = consultantManager;
            _mapper = mapper;
        }
        public async Task<PagedResultDto<List<GetUserDto>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination)
        {
            var result = await _consultantManager.GetAllByFilter(Id, queryParam, pagination);   
            var mappedData = _mapper.Map<List<GetUserDto>>(result.Data);
            return new PagedResultDto<List<GetUserDto>>
            {
                TotalCount = result.TotalCount,
                Data = mappedData
            };
        }

        public async Task<PagedResultDto<List<GetUserDto>>> GetAllConsultants(PaginationParamaters paginationParamaters)
        {
            var result = await _consultantManager.GetAllConsultants(paginationParamaters);
            var mappedData = _mapper.Map<List<GetUserDto>>(result.Data);
            return new PagedResultDto<List<GetUserDto>>
            {
                TotalCount = result.TotalCount,
                Data = mappedData
            };
        }
    }
}
