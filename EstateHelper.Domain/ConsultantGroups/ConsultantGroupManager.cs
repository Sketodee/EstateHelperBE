using AutoMapper;
using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using EstateHelper.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.ConsultantGroups
{
    public class ConsultantGroupManager : IConsultantGroupManager
    {
        private readonly IConsultantGroupRepository _consultantGroupRepository;
        private readonly Helpers _helpers;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _appUserManager;
        private readonly IUserRepository _userRepository;

        public ConsultantGroupManager(IConsultantGroupRepository consultantGroupRepository, Helpers helpers, IMapper mapper, UserManager<AppUser> appUserManager, IUserRepository userRepository)
        {
            _consultantGroupRepository = consultantGroupRepository;
            _helpers = helpers;
            _mapper = mapper;
            _appUserManager = appUserManager;
            _userRepository = userRepository;
        }

        public async Task<ConsultantGroup> AddOrRemoveMembersToGroup (AddMembersToConsultantGroupDto input)
        {
            //get logged in user 
            var user = await _helpers.ReturnLoggedInUser();

            //check if the consultant group already exists 
            var consultant = await _consultantGroupRepository.SingleOrDefaultAsync(x => x.Id == input.Id) ?? throw new Exception("Consultant Group not found");
            //check if all consultants exists
            var allCheck = new List<bool>();

            foreach (var id in input.MembersId)
            {
                //check if the member exist
                allCheck.Add(await _userRepository.SingleOrDefaultAsync(x => x.Id == id && x.isActive) != null);
            }

            if (!allCheck.All(b => b)) throw new Exception("One or more member not found");

            var existingList = consultant.MembersId.ToList();
            var newList = input.addMembers ? existingList.Union(input.MembersId).ToList() : existingList.Except(input.MembersId).ToList();

            consultant.MembersId = newList;
            consultant.LastUpdatedBy = user.Id;
            consultant.LastUpdatedOn = DateTime.Now; 
            var result = await _consultantGroupRepository.UpdateAsync(consultant);  
            return result;  

        }

        public async Task<ConsultantGroup> CreateAsync(CreateConsultantGroupDto input)
        {
            //get logged in user 
            var user = await _helpers.ReturnLoggedInUser();

            input.ReferrerId ??= 600;
            //validation if name and email exists 
            bool nameExist = await _consultantGroupRepository.SingleOrDefaultAsync(x => x.Name == input.Name) == null ? true : throw new Exception("Name is taken");
            bool emailExist = await _consultantGroupRepository.SingleOrDefaultAsync(x => x.Email == input.Email) == null ? true : throw new Exception("Email exists");
            //find if referrer is in the system
            var referrerExist = await _userRepository.SingleOrDefaultAsync(x => x.Link == input.ReferrerId && x.isActive) ?? throw new Exception("Referrer not found");
            //find if accoutnManager exists 
            var accountManagerExist = await _userRepository.SingleOrDefaultAsync(x => x.Id == input.AccountManagerId && x.isActive) ?? throw new Exception("Account Manager not found");
            //check if account manager had admin role 
            bool _ = await _appUserManager.IsInRoleAsync(accountManagerExist, EstateHelperEnums.EstateHelperRoles.Admin.ToString()) ? true : throw new Exception("Account Manager has no admin right");
            //generate alphanumeric code 
            var code = $"CG-{_helpers.GenerateAlphanumericID(10)}";
            var allCheck = new List<bool>();

            foreach (var id in input.MembersId)
            {
                //check if the member exist
                allCheck.Add(await _userRepository.SingleOrDefaultAsync(x => x.Id == id && x.isActive) != null);
            }

            if (!allCheck.All(b => b)) throw new Exception("One or more member not found");

            var newGroup = _mapper.Map<ConsultantGroup>(input);
            newGroup.Code = code;
            newGroup.CreatedBy = user.Id;

            var result = await _consultantGroupRepository.CreateAsync(newGroup);
            return result;
        }

        public async Task<bool> DeleteConsultantGroup(string Id)
        {
            //get logged in user 
            var user = await _helpers.ReturnLoggedInUser();

            //check if the consultant group already exists 
            var consultant = await _consultantGroupRepository.SingleOrDefaultAsync(x => x.Id == Id) ?? throw new Exception("Consultant Group not found");

            consultant.isDeleted = true;
            consultant.DeletedBy = user.Id;
            consultant.DeletedOn = DateTime.Now; 

            var result = await _consultantGroupRepository.DeleteAsync(consultant);
            return result;  
            
        }

        public async Task<ConsultantGroup> EditConsultantGroup(EditConsultantGroupDto input)
        {
            //get logged in user 
            var user = await _helpers.ReturnLoggedInUser();
            //check if name and email exist
            bool nameExist = await _consultantGroupRepository.SingleOrDefaultAsync(x => x.Name == input.Name) == null ? true : throw new Exception("Name is taken");
            bool emailExist = await _consultantGroupRepository.SingleOrDefaultAsync(x => x.Email == input.Email) == null ? true : throw new Exception("Email exists");
            //check if the consultant group already exists 
            var consultant = await _consultantGroupRepository.SingleOrDefaultAsync(x => x.Id == input.Id) ?? throw new Exception("Consultant Group not found");
            //find if accoutnManager exists 
            var accountManagerExist = await _userRepository.SingleOrDefaultAsync(x => x.Id == input.AccountManagerId && x.isActive) ?? throw new Exception("Account Manager not found");
            //check if account manager had admin role 
            bool _ = await _appUserManager.IsInRoleAsync(accountManagerExist, EstateHelperEnums.EstateHelperRoles.Admin.ToString()) ? true : throw new Exception("Account Manager has no admin right");
            //var newDetails = _mapper.Map<ConsultantGroup>(input);           
            var newDetails = _mapper.Map(input, consultant);
            newDetails.LastUpdatedBy = user.Id;
            newDetails.LastUpdatedOn = DateTime.Now;
            var result = await _consultantGroupRepository.UpdateAsync(newDetails); 
            return result;  
        }

        public async Task<List<ConsultantGroup>> GetAllConsultantGroup(string? Id, string? Name, string? Email, PaginationParamaters pagination)
        {
            var result = await _consultantGroupRepository.GetAllAsync(Id, Name, Email, pagination) ?? throw new Exception("No Consultant Group found"); 
            return result; 
        }

    }
}
