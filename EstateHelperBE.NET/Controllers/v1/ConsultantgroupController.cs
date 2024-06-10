﻿using EstateHelper.Application.Auth;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstateHelperBE.NET.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.GeneralAdmin}")]
    public class ConsultantgroupController : ControllerBase
    {
        private readonly IConsultantGroupAppService _consultantGroupAppService;

        public ConsultantgroupController(IConsultantGroupAppService consultantGroupAppService)
        {
            _consultantGroupAppService = consultantGroupAppService;
        }

        [HttpGet("GetAllConsultantGroup")]
        public async Task<ActionResult<ServiceResponse<List<GetConsultantGroupDto>>>> GetAllConsultantGroup(string? Id, string? Name, string? Email,[FromQuery] PaginationParamaters pagination)
        {
            ServiceResponse<List<GetConsultantGroupDto>> response = new();
            try
            {
                var result = await _consultantGroupAppService.GetAllConsultantGroup(Id, Name, Email, pagination); 
                response.Data = result; 
                response.Success = true ;
                response.Message = "Groups successfully fetched";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse<GetConsultantGroupDto>>> Create(CreateConsultantGroupDto request)
        {
            ServiceResponse<GetConsultantGroupDto> response = new();
            try
            {
                var result = await _consultantGroupAppService.CreateAsync(request);
                response.Data = result;
                response.Success = true;
                response.Message = "Group successfully created";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("EditConsultantGroup")]
        public async Task<ActionResult<ServiceResponse<GetConsultantGroupDto>>> EditConsultantGroup(EditConsultantGroupDto input)
        {
            ServiceResponse<GetConsultantGroupDto> response = new();
            try
            {
                var result = await _consultantGroupAppService.EditConsultantGroup(input);
                response.Data = result;
                response.Success = true;
                response.Message = "Group members successfully updated";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }


        [Authorize]
        [HttpPost("AddOrRemoveMembersToGroup")]
        public async Task<ActionResult<ServiceResponse<GetConsultantGroupDto>>> AddOrRemoveMembersToGroup(AddMembersToConsultantGroupDto input)
        {
            ServiceResponse<GetConsultantGroupDto> response = new();
            try
            {
                var result = await _consultantGroupAppService.AddOrRemoveMembersToGroup(input);
                response.Data = result;
                response.Success = true;
                response.Message = "Group members successfully updated";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [Authorize(Roles = RoleNames.GeneralAdmin)]
        [HttpPost("DeleteConsultantGroup")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteConsultantGroup(string Id)
        {
            ServiceResponse<bool> response = new();
            try
            {
                var result = await _consultantGroupAppService.DeleteConsultantGroup(Id);
                response.Data = result;
                response.Success = true;
                response.Message = "Group successfully deleted";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

    }
}
