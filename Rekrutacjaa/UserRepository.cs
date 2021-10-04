using Rekrutacjaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rekrutacjaa
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(LibraryDBContext context) : base(context) { }

        public List<User> GetUser(Guid id)
        {
            Expression<Func<User, bool>> expressionPredicate = x => x.UserId == id;
            return (List<User>)this.Find(expressionPredicate);
        }

        public User GetUserbyCreds(string login, string password)
        {
            Expression<Func<User, bool>> expressionPredicate = x => x.Login == login && x.Password == password;
            return Find(expressionPredicate)[0];
        }

    }
}
