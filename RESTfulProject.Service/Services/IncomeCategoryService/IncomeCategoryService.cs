using AutoMapper;
using RESTfulProject.Repository.Entities;
using RESTfulProject.Repository.Repositories.IncomeCategoryRepository;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.IncomeCategoryService
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public IncomeCategoryService(IIncomeCategoryRepository incomeCategoryRepository, IAuthService authService, IMapper mapper)
        {
            _incomeCategoryRepository = incomeCategoryRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public IncomeCategoryDto GetIncomeCategoryById(int id)
        {
            var results = _incomeCategoryRepository.GetById(id);
            return _mapper.Map<IncomeCategoryDto>(results);
        }
        public List<IncomeCategoryDto> GetIncomeCategories()
        {
            var results = _incomeCategoryRepository.GetAll().ToList();
            return _mapper.Map<List<IncomeCategoryDto>>(results);
        }
        public List<IncomeCategoryDto> GetUserIncomeCategories(string email)
        {
            int id = _authService.GetUserIdByEmail(email);
            var results = _incomeCategoryRepository.GetUserCategories(id).ToList();
            return _mapper.Map<List<IncomeCategoryDto>>(results);
        }
        public void AddIncomeCategory(string email, IncomeCategoryDto incomeCategory)
        {
            if (email == null)
            {
                incomeCategory.UserId = -1;
            }
            else
            {
                incomeCategory.UserId = _authService.GetUserIdByEmail(email);
            }
            _incomeCategoryRepository.Add(_mapper.Map<IncomeCategory>(incomeCategory));
        }
        public void RemoveIncomeCategory(int id)
        {
            var results = _incomeCategoryRepository.GetById(id);
            _incomeCategoryRepository.Remove(_mapper.Map<IncomeCategory>(results));
        }
        public void UpdateIncomeCategory(IncomeCategoryDto incomeCategory)
        {
            _incomeCategoryRepository.Update(_mapper.Map<IncomeCategory>(incomeCategory));
        }
        public bool IncomeCategoryExists(int id)
        {
            return _incomeCategoryRepository.Exists(id);
        }
    }
}
