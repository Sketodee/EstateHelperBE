using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstateHelperBE.NET.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sgnupuser")]
        public async Task<ActionResult<ServiceResponse<CreateUserDto>>> SignUpUser(CreateUserDto request)
        {
            ServiceResponse<CreateUserDto> response = new();
            try
            {
                var result = await _authService.SignUpUser(request);
                response.Data = result; 
                response.Success = true;
                response.Message = "User successfully created";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false; 
                response.Message = ex.Message;
                return StatusCode(500, response); 
            }
        }

        [HttpPost("sgnupadmin")]
        public async Task<ActionResult<ServiceResponse<CreateUserDto>>> SignUpAdmin(CreateUserDto request)
        {
            ServiceResponse<CreateUserDto> response = new();
            try
            {
                var result = await _authService.SignUpAdmin(request);
                response.Data = result;
                response.Success = true;
                response.Message = "User successfully created";
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
