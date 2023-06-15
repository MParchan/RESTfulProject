using RESTfulProject.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.ExpenditureService
{
    public interface IExpenditureService
    {
        public ExpenditureDto GetExpenditureById(int id);
        public List<ExpenditureDto> GetExpenditures();
        public List<ExpenditureDto> GetUserExpenditures(string email);
        public void AddExpenditure(string email, ExpenditureDto expenditure);
        public void RemoveExpenditure(int id);
        public void UpdateExpenditure(ExpenditureDto expenditure);
        public bool ExpenditureExists(int id);
    }
}
