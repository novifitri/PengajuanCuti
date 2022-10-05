using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IUserRoleRepository
    {
        List<UserRole> Get();
        UserRole Get(int id);
        int Post(UserRole userRole);
        int Put(UserRole userRole);
        int Delete(int id);
    }
}
