using RESTfulProject.Repository.Entities;
using RESTfulProject.Repository.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Repositories.IncomeCategoryRepository
{
    public class IncomeCategoryRepository : GenericRepository<IncomeCategory>, IIncomeCategoryRepository
    {
        private readonly AppDBContext _context;
        public IncomeCategoryRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<IncomeCategory> GetUserCategories(int id)
        {
            return _context.IncomeCategories.Where(i => i.UserId == -1 || i.UserId == id);
        }
        public bool Exists(int id)
        {
            return _context.IncomeCategories.Any(i => i.IncomeCategoryId == id);
        }
    }
}
