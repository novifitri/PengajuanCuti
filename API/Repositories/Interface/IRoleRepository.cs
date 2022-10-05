using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IRoleRepository
    {
        List<Role> Get();
        Role Get(int id);
        int Post(Role role);
        int Put(Role role);
        int Delete(int id);
    }
}
