using RESTfulProject.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.IncomeCategoryService
{
    public interface IIncomeCategoryService
    {
        public IncomeCategoryDto GetIncomeCategoryById(int id);
        public List<IncomeCategoryDto> GetIncomeCategories();
        public List<IncomeCategoryDto> GetUserIncomeCategories(string email);
        public void AddIncomeCategory(string email, IncomeCategoryDto incomeCategory);
        public void RemoveIncomeCategory(int id);
        public void UpdateIncomeCategory(IncomeCategoryDto incomeCategory);
        public bool IncomeCategoryExists(int id);
    }
}
