using RESTfulProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Repositories.ExpenditureCategoryRepository
{
    public class ExpenditureCategoryRepository : GenericRepository<ExpenditureCategory>, IExpenditureCategoryRepository
    {
        private readonly AppDBContext _context;
        public ExpenditureCategoryRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<ExpenditureCategory> GetUserCategories(int id)
        {
            return _context.ExpenditureCategories.Where(e => e.UserId == -1 || e.UserId == id);
        }
        public bool Exists(int id)
        {
            return _context.ExpenditureCategories.Any(e => e.ExpenditureCategoryId == id);
        }
    }
}
