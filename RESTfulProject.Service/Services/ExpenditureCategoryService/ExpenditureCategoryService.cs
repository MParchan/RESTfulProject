using AutoMapper;
using RESTfulProject.Repository.Entities;
using RESTfulProject.Repository.Repositories.ExpenditureCategoryRepository;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.ExpenditureCategoryService
{
    public class ExpenditureCategoryService : IExpenditureCategoryService
    {
        private readonly IExpenditureCategoryRepository _expenditureCategoryRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public ExpenditureCategoryService(IExpenditureCategoryRepository expenditureCategoryRepository, IAuthService authService, IMapper mapper)
        {
            _expenditureCategoryRepository = expenditureCategoryRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public ExpenditureCategoryDto GetExpenditureCategoryById(int id)
        {
            var results = _expenditureCategoryRepository.GetById(id);
            return _mapper.Map<ExpenditureCategoryDto>(results);
        }
        public List<ExpenditureCategoryDto> GetExpenditureCategories()
        {
            var results = _expenditureCategoryRepository.GetAll().ToList();
            return _mapper.Map<List<ExpenditureCategoryDto>>(results);
        }
        public List<ExpenditureCategoryDto> GetUserExpenditureCategories(string email)
        {
            int id = _authService.GetUserIdByEmail(email);
            var results = _expenditureCategoryRepository.GetUserCategories(id).ToList();
            return _mapper.Map<List<ExpenditureCategoryDto>>(results);
        }
        public void AddExpenditureCategory(string email, ExpenditureCategoryDto expenditureCategory)
        {
            if (email == null)
            {
                expenditureCategory.UserId = -1;
            }
            else
            {
                expenditureCategory.UserId = _authService.GetUserIdByEmail(email);
            }
            _expenditureCategoryRepository.Add(_mapper.Map<ExpenditureCategory>(expenditureCategory));
        }
        public void RemoveExpenditureCategory(int id)
        {
            var results = _expenditureCategoryRepository.GetById(id);
            _expenditureCategoryRepository.Remove(_mapper.Map<ExpenditureCategory>(results));
        }
        public void UpdateExpenditureCategory(ExpenditureCategoryDto expenditureCategory)
        {
            _expenditureCategoryRepository.Update(_mapper.Map<ExpenditureCategory>(expenditureCategory));
        }
        public bool ExpenditureCategoryExists(int id)
        {
            return _expenditureCategoryRepository.Exists(id);
        }
    }
}
