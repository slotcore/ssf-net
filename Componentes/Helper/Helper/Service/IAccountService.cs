using Helper.Classes.Login;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Service
{
    public interface IAccountService
    {
        [Post("/api/account/login/")]
        Task<AccountToken> Login([Body] Login login);
    }
}
