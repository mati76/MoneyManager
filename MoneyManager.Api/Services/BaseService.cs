using AutoMapper;
using System;

namespace MoneyManager.Api.Services
{
    public class BaseService
    {
        protected readonly IMapper _mapperService;

        public BaseService(IMapper mapperService)
        {
            if(mapperService == null)
            {
                throw new ArgumentNullException(nameof(mapperService));
            }
            _mapperService = mapperService;
        }
    }
}