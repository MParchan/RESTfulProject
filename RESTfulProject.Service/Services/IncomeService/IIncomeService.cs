using RESTfulProject.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.IncomeService
{
    public interface IIncomeService
    {
        public IncomeDto GetIncomeById(int id);
        public List<IncomeDto> GetIncomes();
        public List<IncomeDto> GetUserIncomes(string email);
        public void AddIncome(string email, IncomeDto income);
        public void RemoveIncome(int id);
        public void UpdateIncome(IncomeDto income);
        public bool IncomeExists(int id);
    }
}
