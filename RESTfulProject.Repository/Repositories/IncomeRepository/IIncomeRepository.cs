using RESTfulProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Repositories.IncomeRepository
{
    public interface IIncomeRepository : IGenericRepository<Income>
    {
        public IEnumerable<Income> GetUserIncomes(int id);
        public bool Exists(int id);
    }
}
