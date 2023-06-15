using AutoMapper;
using RESTfulProject.Repository.Entities;
using RESTfulProject.Repository.Repositories.IncomeRepository;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.IncomeService
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public IncomeService(IIncomeRepository incomeRepository, IAuthService authService, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public IncomeDto GetIncomeById(int id)
        {
            var results = _incomeRepository.GetById(id);
            return _mapper.Map<IncomeDto>(results);
        }
        public List<IncomeDto> GetIncomes()
        {
            var results = _incomeRepository.GetAll().ToList();
            return _mapper.Map<List<IncomeDto>>(results);
        }
        public List<IncomeDto> GetUserIncomes(string email)
        {
            int id = _authService.GetUserIdByEmail(email);
            var results = _incomeRepository.GetUserIncomes(id).ToList();
            return _mapper.Map<List<IncomeDto>>(results);
        }
        public void AddIncome(string email, IncomeDto income)
        {
            if (email == null)
            {
                income.UserId = -1;
            }
            else
            {
                income.UserId = _authService.GetUserIdByEmail(email);
            }
            income.Date = DateTime.Now;
            _incomeRepository.Add(_mapper.Map<Income>(income));
        }
        public void RemoveIncome(int id)
        {
            var results = _incomeRepository.GetById(id);
            _incomeRepository.Remove(_mapper.Map<Income>(results));
        }
        public void UpdateIncome(IncomeDto income)
        {
            _incomeRepository.Update(_mapper.Map<Income>(income));
        }
        public bool IncomeExists(int id)
        {
            return _incomeRepository.Exists(id);
        }
    }
}
