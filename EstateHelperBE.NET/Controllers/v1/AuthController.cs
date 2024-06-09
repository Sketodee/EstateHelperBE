using EstateHelper.Application.Contract.Dtos.Login;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using EstateHelper.Domain.Shared;
using EstateHelper.Domain.User;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("Register")]
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

        [HttpPost("RegisterAdmin")]
        public async Task<ActionResult<ServiceResponse<CreateUserDto>>> SignUpGeneralAdmin(CreateUserDto request)
        {
            ServiceResponse<CreateUserDto> response = new();
            try
            {
                var result = await _authService.SignUpGeneralAdmin(request);
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


        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<LoginResponseDto>>> Login(LoginRequestDto request)
        {
            ServiceResponse<LoginResponseDto> response = new();
            try
            {
                var result = await _authService.Login(request);
                response.Data = result;
                response.Success = true;
                response.Message = "Login successful";
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
        [HttpGet("GetloggedInUser")]
        public async Task<ActionResult<ServiceResponse<GetLoggedInUserDto>>> GetLoggedInUser()
        {
            ServiceResponse<GetLoggedInUserDto> response = new();
            try
            {
                var result = await _authService.GetLoggedInUser();
                response.Data = result;
                response.Success = true;
                response.Message = "User fetched";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetRefreshToken")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            ServiceResponse<string> response = new();
            try
            {
                var result = await _authService.GetRefreshToken();
                response.Data = result;
                response.Success = true;
                response.Message = "Refresh token generated";
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
        [HttpPost("Logout")]
        public async Task<ActionResult<string>> Logout()
        {
            ServiceResponse<string> response = new();
            try
            {
                var result = await _authService.Logout();
                response.Data = result;
                response.Success = true;
                response.Message = "Successfully logged out";
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
        [HttpPost("AddUserToRole")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddUserToRole(string roleName, string Id)
        {
            ServiceResponse<bool> response = new();
            try
            {
                var result = await _authService.AddUserToRole(roleName, Id);
                response.Data = result;
                response.Success = true;
                response.Message = "User role successfully updated";
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
