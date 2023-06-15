using AutoMapper;
using RESTfulProject.Repository.Entities;
using RESTfulProject.Service.DTOs;

namespace RESTfulProject.API.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserViewModel, UserDto>().ReverseMap();

            CreateMap<IncomeCategory, IncomeCategoryDto>().ReverseMap();
            CreateMap<IncomeCategoryViewModel, IncomeCategoryDto>().ReverseMap();

            CreateMap<ExpenditureCategory, ExpenditureCategoryDto>().ReverseMap();
            CreateMap<ExpenditureCategoryViewModel, ExpenditureCategoryDto>().ReverseMap();

            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<IncomeViewModel, IncomeDto>().ReverseMap();

            CreateMap<Expenditure, ExpenditureDto>().ReverseMap();
            CreateMap<ExpenditureViewModel, ExpenditureDto>().ReverseMap();
        }
    }
}
