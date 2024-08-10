using AutoMapper;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.Client;
using EstateHelper.Domain.Models;
using EstateHelper.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Clients
{
    public class ClientManager : IClientManager
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ClientManager(IClientRepository clientRepository, IUserRepository userRepository ,IMapper mapper)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Client> Create(CreateClientDto input)
        {
            //check if consultant id exist 
            var consultant = await _userRepository.SingleOrDefaultAsync(x => x.Link ==  input.ConsultantId) ?? throw new Exception("ConsultantId not found");
            var client = _mapper.Map<Client>(input);
            var result = await _clientRepository.CreateAsync(client);
            return result;
        }

        public async Task<bool> Delete(string Id)
        {
            var product = await _clientRepository.SingleOrDefaultAsync(x => x.Id == Id) ?? throw new Exception("Client not found");
            return await _clientRepository.DeleteAsync(product);
        }

        public async Task<PagedResultDto<List<Client>>> GetAllClients(PaginationParamaters pagination)
        {
            var client = await _clientRepository.GetAllAsync(pagination) ?? throw new Exception("No client found");
            return client;
        }

        public async Task<Client> Update (EditClientDto input)
        {
            //check if client exist 
            var client = await _clientRepository.SingleOrDefaultAsync(x => x.Id == input.Id) ?? throw new Exception("Client not found");
            var newDetails = _mapper.Map(input, client);
            var result = await _clientRepository.UpdateAsync(newDetails);   
            return result;
        }

        public async Task<PagedResultDto<List<Client>>> GetAllClientsByFilter(string? Id, string? queryParam, PaginationParamaters pagination)
        {
            var result = await _clientRepository.GetAllByFilter(Id, queryParam, pagination);
            return result;
        }
    }
}
