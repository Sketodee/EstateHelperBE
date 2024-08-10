using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Client;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Clients
{
    public interface IClientManager
    {
        Task<Client> Create(CreateClientDto input);
        Task<bool> Delete(string Id);
        Task<PagedResultDto<List<Client>>> GetAllClients(PaginationParamaters pagination);
        Task<Client> Update(EditClientDto input);
        Task<PagedResultDto<List<Client>>> GetAllClientsByFilter(string? Id, string? queryParam, PaginationParamaters pagination); 
    }
}
