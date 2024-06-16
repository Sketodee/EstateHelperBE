using EstateHelper.Application.ConsultantGroups;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EstateHelper.Application.Contract.Dtos.User;

namespace EstateHelperBE.NET.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ConsultantController : ControllerBase
    {
        private readonly IConsultantAppService _consultantAppService;

        public ConsultantController(IConsultantAppService consultantAppService)
        {
            _consultantAppService = consultantAppService;
        }

        [HttpGet("GetAllConsultants")]
        public async Task<ActionResult<ServiceResponse<PagedResultDto<List<GetUserDto>>>>> GetAllConsultants([FromQuery] PaginationParamaters pagination)
        {
            ServiceResponse<PagedResultDto<List<GetUserDto>>> response = new();
            try
            {
                var result = await _consultantAppService.GetAllConsultants(pagination);
                response.Data = result;
                response.Success = true;
                response.Message = "Consultants successfully fetched";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAllConsultantsByFilter")]
        public async Task<ActionResult<ServiceResponse<PagedResultDto<List<GetUserDto>>>>> GetAllConsultantsByFilter(string? Id, string? queryParam, [FromQuery] PaginationParamaters pagination)
        {
            ServiceResponse<PagedResultDto<List<GetUserDto>>> response = new();
            try
            {
                var result = await _consultantAppService.GetAllByFilter(Id, queryParam, pagination);
                response.Data = result;
                response.Success = true;
                response.Message = "Consultants successfully fetched";
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
