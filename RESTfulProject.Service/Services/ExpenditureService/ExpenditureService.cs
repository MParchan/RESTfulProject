using AutoMapper;
using RESTfulProject.Repository.Entities;
using RESTfulProject.Repository.Repositories.ExpenditureRepository;
using RESTfulProject.Service.DTOs;
using RESTfulProject.Service.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.ExpenditureService
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly IExpenditureRepository _expenditureRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public ExpenditureService(IExpenditureRepository expenditureRepository, IAuthService authService, IMapper mapper)
        {
            _expenditureRepository = expenditureRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public ExpenditureDto GetExpenditureById(int id)
        {
            var results = _expenditureRepository.GetById(id);
            return _mapper.Map<ExpenditureDto>(results);
        }
        public List<ExpenditureDto> GetExpenditures()
        {
            var results = _expenditureRepository.GetAll().ToList();
            return _mapper.Map<List<ExpenditureDto>>(results);
        }
        public List<ExpenditureDto> GetUserExpenditures(string email)
        {
            int id = _authService.GetUserIdByEmail(email);
            var results = _expenditureRepository.GetUserExpenditures(id).ToList();
            return _mapper.Map<List<ExpenditureDto>>(results);
        }
        public void AddExpenditure(string email, ExpenditureDto expenditure)
        {
            if (email == null)
            {
                expenditure.UserId = -1;
            }
            else
            {
                expenditure.UserId = _authService.GetUserIdByEmail(email);
            }
            expenditure.Date = DateTime.Now;
            _expenditureRepository.Add(_mapper.Map<Expenditure>(expenditure));
        }
        public void RemoveExpenditure(int id)
        {
            var results = _expenditureRepository.GetById(id);
            _expenditureRepository.Remove(_mapper.Map<Expenditure>(results));
        }
        public void UpdateExpenditure(ExpenditureDto expenditure)
        {
            _expenditureRepository.Update(_mapper.Map<Expenditure>(expenditure));
        }
        public bool ExpenditureExists(int id)
        {
            return _expenditureRepository.Exists(id);
        }
    }
}
