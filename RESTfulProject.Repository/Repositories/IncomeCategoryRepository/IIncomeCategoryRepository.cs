using RESTfulProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Repositories.IncomeCategoryRepository
{
    public interface IIncomeCategoryRepository : IGenericRepository<IncomeCategory>
    {
        public IEnumerable<IncomeCategory> GetUserCategories(int id);
        public bool Exists(int id);
    }
}
