﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp
{
    public interface IAccountDatabase
    {
        Task<Account> GetOrCreateAccountAsync(string id);

        Task<Account> GetOrCreateAccountAsync(long id);

        Task<Account> FindByUserNameAsync(string userName);

        List<Account> GetAccountsOnce();

        Task ResetAsync();
    }
}