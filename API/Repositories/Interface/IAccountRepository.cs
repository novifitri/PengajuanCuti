using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    interface IAccountRepository
    {
        ResponseLogin Login(LoginVM login);
    }
}
