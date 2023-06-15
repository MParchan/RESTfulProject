using RESTfulProject.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.ExpenditureCategoryService
{
    public interface IExpenditureCategoryService
    {
        public ExpenditureCategoryDto GetExpenditureCategoryById(int id);
        public List<ExpenditureCategoryDto> GetExpenditureCategories();
        public List<ExpenditureCategoryDto> GetUserExpenditureCategories(string email);
        public void AddExpenditureCategory(string email, ExpenditureCategoryDto expenditureCategory);
        public void RemoveExpenditureCategory(int id);
        public void UpdateExpenditureCategory(ExpenditureCategoryDto expenditureCategory);
        public bool ExpenditureCategoryExists(int id);
    }
}
