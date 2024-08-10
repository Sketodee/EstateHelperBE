using AutoMapper;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Client;
using EstateHelper.Application.Contract.Dtos.Products;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Client
{
    public class ClientAppService : IClientAppService
    {
        private readonly IClientManager _clientManager;
        private readonly IMapper _mapper;

        public ClientAppService(IClientManager clientManager, IMapper mapper)
        {
            _clientManager = clientManager;
            _mapper = mapper;
        }
        public async Task<GetClientDto> Create(CreateClientDto input)
        {
            var result = await _clientManager.Create(input);
            return _mapper.Map<GetClientDto>(result);    
        }

        public async Task<bool> Delete(string Id)
        {
            var result = await _clientManager.Delete(Id);
            return result; 
        }

        public async Task<PagedResultDto<List<GetClientDto>>> GetAllClients(PaginationParamaters pagination)
        {
            var result = await _clientManager.GetAllClients(pagination);
            var mappedData = _mapper.Map<List<GetClientDto>>(result.Data);
            return new PagedResultDto<List<GetClientDto>>
            {
                TotalCount = result.TotalCount,
                Data = mappedData
            };
        }

        public async Task<PagedResultDto<List<GetClientDto>>> GetAllClientsByFilter(string? Id, string? queryParam, PaginationParamaters pagination)
        {
            var result = await _clientManager.GetAllClientsByFilter(Id, queryParam, pagination);
            var mappedData = _mapper.Map<List<GetClientDto>>(result.Data);
            return new PagedResultDto<List<GetClientDto>>
            {
                TotalCount = result.TotalCount,
                Data = mappedData
            }; 
        }

        public async Task<GetClientDto> Update(EditClientDto input)
        {
            var result = await _clientManager.Update(input);
            return _mapper.Map<GetClientDto>(result);  
        }
    }
}
