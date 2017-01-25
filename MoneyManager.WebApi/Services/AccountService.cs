using MoneyManager.Business;
using MoneyManager.Business.Interfaces;
using MoneyManager.Business.Models;
using MoneyManager.WebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MoneyManager.WebApi.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAuthBusiness _authBusiness;

        public AccountService(IAuthBusiness authBusiness, IMapperService mapperService) : base(mapperService)
        {
            if(authBusiness == null)
            {
                throw new ArgumentNullException(nameof(authBusiness));
            }

            _authBusiness = authBusiness;
        }

        public ActionResult Register(AccountDTO account)
        {
            return _authBusiness.RegisterUser(_mapperService.Map<AppUser>(account));
        }

        
    }
}