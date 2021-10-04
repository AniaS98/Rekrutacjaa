using Rekrutacjaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rekrutacjaa
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetUser(Guid id);
        User GetUserbyCreds(string login, string password);

    }
}
