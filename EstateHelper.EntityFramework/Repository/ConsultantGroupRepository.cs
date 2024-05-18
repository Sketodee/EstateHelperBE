using AutoMapper;
using Azure.Core;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Domain.ConsultantGroups;
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

namespace EstateHelper.EntityFramework.Repository
{
    public class ConsultantGroupRepository : IConsultantGroupRepository
    {
        private readonly AppDbContext _context;
        private readonly Helpers _helpers;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _appUserManager;
        private readonly IUserRepository _userRepository;

        public ConsultantGroupRepository(AppDbContext context, Helpers helpers, IMapper mapper, UserManager<AppUser> appUserManager, IUserRepository userRepository)
        {
            _context = context;
            _helpers = helpers;
            _mapper = mapper;
            _appUserManager = appUserManager;
            _userRepository = userRepository;
        }
        public async Task<ConsultantGroup> CreateAsync(CreateConsultantGroupDto input)
        {
            //get logged in user 
            var user = await _helpers.ReturnLoggedInUser();

            input.ReferrerId ??= 600;

            //find if referrer is in the system
            var referrerExist = (await _userRepository.GetAllAsync()).Where(x => x.Link == input.ReferrerId && x.isActive).FirstOrDefault() ?? throw new Exception("Referrer not found");
            //find if accoutnManager exists 
            var accountManagerExist = (await _userRepository.GetAllAsync()).Where(x => x.Id == input.AccountManagerId && x.isActive).FirstOrDefault() ?? throw new Exception("Account Manager not found");
            //check if account manager had admin role 
            bool _ = await _appUserManager.IsInRoleAsync(accountManagerExist, EstateHelperEnums.EstateHelperRoles.Admin.ToString()) ? true : throw new Exception("Account Manager has no admin right");
            //generate alphanumeric code 
            var code = $"CG-{_helpers.GenerateAlphanumericID(10)}"; 
            var allCheck = new List<bool>();    

            foreach(var id in input.MembersId)
            {
                //check if the member exist
                allCheck.Add((await _userRepository.GetAllAsync()).FirstOrDefault(x=> x.Id == id && x.isActive) != null);
            }

            if (!allCheck.All(b => b)) throw new Exception("One or more member not found");

            var newGroup = _mapper.Map<ConsultantGroup>(input); 
            newGroup.Code = code;
            newGroup.CreatedBy = user.Id;
             await _context.ConsultantGroups.AddAsync(newGroup);
            await _context.SaveChangesAsync();  
            return newGroup; 
        }
    }
}
