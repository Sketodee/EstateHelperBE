using EstateHelper.Application.ConsultantGroups;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Products;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> Create(CreateProductDto request)
        {
            ServiceResponse<GetProductDto> response = new();
            try
            {
                var result = await _productAppService.Create(request);
                response.Data = result;
                response.Success = true;
                response.Message = "Product successfully created";
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<ServiceResponse<List<GetProductDto>>>> GetAllProducts(string? Id, string? Name,[FromQuery] PaginationParamaters pagination)
        {
            ServiceResponse<List<GetProductDto>> response = new();
            try
            {
                var result = await _productAppService.GetAllProducts(Id, Name, pagination);
                response.Data = result;
                response.Success = true;
                response.Message = "Products successfully fetched";
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
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> Delete (string Id)
        {
            ServiceResponse<bool> response = new();
            try
            {
                var result = await _productAppService.Delete(Id);
                response.Data = result;
                response.Success = true;
                response.Message = "Product successfully deleted";
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
        public async Task<ActionResult<ServiceResponse<GetProductDto>>> Update(EditProductDto request)
        {
            ServiceResponse<GetProductDto> response = new();
            try
            {
                var result = await _productAppService.Update(request);
                response.Data = result;
                response.Success = true;
                response.Message = "Product successfully updated";
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
