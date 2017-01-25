using MoneyManager.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyManager.WebApi.Services
{
    public class BaseService
    {
        protected readonly IMapperService _mapperService;

        public BaseService(IMapperService mapperService)
        {
            if(mapperService == null)
            {
                throw new ArgumentNullException(nameof(mapperService));
            }
            _mapperService = mapperService;
        }
    }
}