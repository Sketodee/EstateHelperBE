using Azure.Core;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Client;
using EstateHelper.Application.Contract.Dtos.Products;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Application.Products;
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
    public class ClientController : ControllerBase
    {
        private readonly IClientAppService _clientAppService;

        public ClientController(IClientAppService clientAppService)
        {
            _clientAppService = clientAppService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse<GetClientDto>>> Create (CreateClientDto request)
        {
            ServiceResponse<GetClientDto> response = new();
            try
            {
                var result = await _clientAppService.Create(request);
                response.Data = result;
                response.Success = true;
                response.Message = "Client successfully created";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAllClients")]
        public async Task<ActionResult<ServiceResponse<PagedResultDto<List<GetClientDto>>>>> GetAllProducts([FromQuery] PaginationParamaters pagination)
        {
            ServiceResponse<PagedResultDto<List<GetClientDto>>> response = new();
            try
            {
                var result = await _clientAppService.GetAllClients(pagination);
                response.Data = result;
                response.Success = true;
                response.Message = "Clients successfully fetched";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAllClientsByFilter")]
        public async Task<ActionResult<ServiceResponse<PagedResultDto<List<GetClientDto>>>>> GetAllProductsByFilter(string? Id, string? queryParam, [FromQuery] PaginationParamaters pagination)
        {
            ServiceResponse<PagedResultDto<List<GetClientDto>>> response = new();
            try
            {
                var result = await _clientAppService.GetAllClientsByFilter(Id, queryParam, pagination);
                response.Data = result;
                response.Success = true;
                response.Message = "Clients successfully fetched";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<ServiceResponse<GetClientDto>>> Delete(string Id)
        {
            ServiceResponse<bool> response = new();
            try
            {
                var result = await _clientAppService.Delete(Id);
                response.Data = result;
                response.Success = true;
                response.Message = "Client successfully deleted";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpPost("Update")]
        public async Task<ActionResult<ServiceResponse<GetClientDto>>> Update(EditClientDto request)
        {
            ServiceResponse<GetClientDto> response = new();
            try
            {
                var result = await _clientAppService.Update(request);
                response.Data = result;
                response.Success = true;
                response.Message = "Clientsuccessfully updated";
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
