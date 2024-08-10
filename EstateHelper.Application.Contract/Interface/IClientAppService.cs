using EstateHelper.Application.Contract.Dtos.Client;
using EstateHelper.Application.Contract.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Interface
{
    public interface IClientAppService
    {
        Task<GetClientDto> Create(CreateClientDto input);

        Task<GetClientDto> Update(EditClientDto input);
        Task<bool> Delete(string Id);
        Task<PagedResultDto<List<GetClientDto>>> GetAllClients(PaginationParamaters pagination);

        Task<PagedResultDto<List<GetClientDto>>> GetAllClientsByFilter(string? Id, string? queryParam, PaginationParamaters pagination);
    }
}
