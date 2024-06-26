﻿using AutoMapper;
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

        public async Task<GetConsultantGroupDto> AddOrRemoveMembersToGroup(AddMembersToConsultantGroupDto input)
        {
            var result = await _consultantGroupManager.AddOrRemoveMembersToGroup(input);
            return _mapper.Map<GetConsultantGroupDto>(result);
        }

        public async Task<GetConsultantGroupDto> CreateAsync(CreateConsultantGroupDto input)
        {
            var result = await _consultantGroupManager.CreateAsync(input);  
            return _mapper.Map<GetConsultantGroupDto>(result);  
        }

        public async Task<bool> DeleteConsultantGroup(string Id)
        {
            var result = await _consultantGroupManager.DeleteConsultantGroup(Id);
            return result;
        }

        public async Task<GetConsultantGroupDto> EditConsultantGroup(EditConsultantGroupDto input)
        {
            var result = await _consultantGroupManager.EditConsultantGroup(input);
            return _mapper.Map<GetConsultantGroupDto>(result); 
        }

        public async Task<List<GetConsultantGroupDto>> GetAllConsultantGroup()
        {
           var result= await _consultantGroupManager.GetAllConsultantGroup();
            return _mapper.Map<List<GetConsultantGroupDto>>(result);
        }

        public async Task<List<GetConsultantGroupDto>> GetConsultantGroupByFilter(string? Id, string? Name, string? Email)
        {
            var result = await _consultantGroupManager.GetConsultantGroupByFilter(Id, Name, Email);
            return _mapper.Map<List<GetConsultantGroupDto>>(result) ;   
        }
    }
}
