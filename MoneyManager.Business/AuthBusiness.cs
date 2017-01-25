using MoneyManager.Business.Interfaces;
using System;
using MoneyManager.Business.Models;
using MoneyManager.Business.Repository;

namespace MoneyManager.Business
{
    public class AuthBusiness : BaseBusiness, IAuthBusiness
    {
        private readonly IAuthRepository _authRepository;

        public AuthBusiness(IUnitOfWorkFactory unitOfWorkFactory, IAuthRepository authRepository) : base(unitOfWorkFactory)
        {
            if(authRepository == null)
            {
                throw new ArgumentNullException(nameof(authRepository));
            }
            _authRepository = authRepository;
        }
        
        public ActionResult RegisterUser(AppUser user)
        {
            var result = _authRepository.RegisterUser(user);
            _authRepository.Dispose();
            return result;
        }

        public AppUser ValidateUser(string userName, string password)
        {
            var result = _authRepository.ValidateUser(userName, password);
            _authRepository.Dispose();
            return result;
        }
    }
}
