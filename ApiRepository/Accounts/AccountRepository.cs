using BenriShop.ApiRepository.Accounts;
using BenriShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Accounts
{
    /// <summary>
    /// Lớp này dùng để viết các hàm cần sử dụng cho Account 
    /// </summary>
    public class AccountRepository : IAccountRepository
    {

        private readonly BenriShopContext _context;

        public AccountRepository(BenriShopContext context)
        {
            this._context = context;
        }
        
        public async Task<Account> AddAccount(Account account)
        {
            var result = await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        
        public async Task<bool> DeleteAccountAsync(string accountId)
        {

            var account = await _context.Accounts.FindAsync(accountId);
            if (account != null)
            {
                try
                {
                    _context.Accounts.Remove(account);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
                return true;
            }
            return false;
        }
        
        public async Task<Account> GetAccount(string accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(e => e.UserName == accountId);
        }

        
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }
        
        public async Task<Account> UpdateAccount(Account account)
        {
            var result = await _context.Accounts.FirstOrDefaultAsync(e => e.UserName == account.UserName);

            if (result != null)
            {
                result.FullName = account.FullName;
                result.Address = account.Address;
                result.PhoneNumber = account.PhoneNumber;
                result.Password = account.Password;
                result.Role = account.Role;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
        
        public async Task<IEnumerable<Account>> GetAccountsByRole(string role)
        {
            IQueryable<Account> query = _context.Accounts;
            query = query.Where(e => e.Role.Contains(role));
            if (query != null)
            {
                List<Account> accounts = await query.ToListAsync();
                return accounts;
            }
            else
            {
                return null;
            }
        }
    }
}
