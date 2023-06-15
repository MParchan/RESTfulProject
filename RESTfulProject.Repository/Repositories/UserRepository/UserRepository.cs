using Microsoft.EntityFrameworkCore;
using RESTfulProject.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Repository.Repositories.UserRepository
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email.Equals(email));
        }
        public int GetIdByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email.Equals(email));
            return user.UserId;
        }
        public bool Exists(string email)
        {
            return _context.Users.Any(p => p.Email.Equals(email));
        }
    }
}
