using BenriShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Accounts
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<Account> GetAccount(string accountId);
        Task<Account> AddAccount(Account account);
        Task<Account> UpdateAccount(Account account);
        void DeleteAccount(string accountId);

        Task<IEnumerable<Account>> GetAccountsByRole(string role);




    }
}
