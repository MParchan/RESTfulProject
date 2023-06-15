using RESTfulProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByEmail(string email);
        public int GetIdByEmail(string email);
        public bool Exists(string email);
    }
}
