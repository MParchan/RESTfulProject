using RESTfulProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Repositories.ExpenditureCategoryRepository
{
    public interface IExpenditureCategoryRepository : IGenericRepository<ExpenditureCategory>
    {
        public IEnumerable<ExpenditureCategory> GetUserCategories(int id);
        public bool Exists(int id);
    }
}
