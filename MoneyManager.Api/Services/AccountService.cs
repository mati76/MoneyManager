using MoneyManager.Business.Interfaces;
using BLL = MoneyManager.Business.Models;
using MoneyManager.Api.DTO;
using System;
using AutoMapper;

namespace MoneyManager.Api.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAuthBusiness _authBusiness;

        public AccountService(IAuthBusiness authBusiness, IMapper mapperService) : base(mapperService)
        {
            if(authBusiness == null)
            {
                throw new ArgumentNullException(nameof(authBusiness));
            }

            _authBusiness = authBusiness;
        }

        public BLL.ActionResult Register(AccountDTO account)
        {
            return _authBusiness.RegisterUser(_mapperService.Map<BLL.AppUser>(account));
        }

        
    }
}