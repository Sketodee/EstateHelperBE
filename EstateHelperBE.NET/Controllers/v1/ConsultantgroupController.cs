using EstateHelper.Application.Auth;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstateHelperBE.NET.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ConsultantgroupController : ControllerBase
    {
        private readonly IConsultantGroupAppService _consultantGroupAppService;

        public ConsultantgroupController(IConsultantGroupAppService consultantGroupAppService)
        {
            _consultantGroupAppService = consultantGroupAppService;
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
    }
}
