using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MoneyManager.Business.Models;

namespace MoneyManager.Api.AutoMapper
{
	public class AuthProfile : Profile
	{
		public AuthProfile()
		{
            CreateMap<DTO.AccountDTO, AppUser>();
            CreateMap<IdentityResult, ActionResult>();
            CreateMap<AppUser, IdentityUser>();
        }
	}
}
