using AutoMapper;
using MoneyManager.Business.Models;

namespace MoneyManager.Api.AutoMapper
{
	public class HelperProfile : Profile
	{
		public HelperProfile()
		{
			CreateMap<DTO.SearchCriteria, SearchCriteria>();
		}
	}
}
