using BenriShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.ApiRepository.Accounts
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Lấy tất cả đối tượng Account được lưu trong database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Account>> GetAccounts();
        /// <summary>
        /// Lấy 1 đối tượng Account bằng cách truyền vào username
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<Account> GetAccount(string accountId);
        /// <summary>
        /// Thêm tài khoản bằng 1 đối tượng Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<Account> AddAccount(Account account);
        /// <summary>
        /// Cập nhật thông tin của 1 đối tượng Account bằng cách truyền vào 1 đối tượng với value mới.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<Account> UpdateAccount(Account account);
        /// <summary>
        /// Xóa một tài khoản bằng cách truyền vào username
        /// </summary>
        /// <param username="accountId"></param>
        /// <returns></returns>
        Task<bool> DeleteAccountAsync(string accountId);
        /// <summary>
        /// Lấy ra tất cả các Account có role trùng với role được truyền vào
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<Account>> GetAccountsByRole(string role);
    }
}
