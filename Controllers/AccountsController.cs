using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenriShop.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Http;
using BenriShop.ApiRepository.Accounts;
using System.Security.Claims;

namespace BenriShop.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;


        public AccountsController(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        #region Admin
        /// <summary>
        /// Lấy toàn bộ danh sach tài khoản của database
        /// </summary>
        /// <returns></returns>
        // GET: api/Accounts/GetAccounts 
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAccounts")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            try
            {
                return (await _accountRepository.GetAccounts()).ToList();
               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Thêm tài khoản nhân viên
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        // POST: api/Accounts/AddModAccount 
        [HttpPost("AddModAccount")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Account>> AddModAccount(Account account)
        {
            var _account = await _accountRepository.GetAccount(account.UserName);

            if (_account != null)
            {
                return Conflict("Parameter account is not null");
            }
            try
            {
                account.Role = Role.Mod;
                await _accountRepository.AddAccount(account);
                return Ok("Add mod account successful!");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Error when AddAccount");
            }
        }
        /// <summary>
        /// Lấy danh sách tài khoản Mod
        /// </summary>
        /// <returns></returns>
        // GET: api/Accounts/GetModAccounts
        [Authorize(Roles = "Admin")]
        [HttpGet("GetModAccounts")]
        public async Task<ActionResult<IEnumerable<Account>>> GetModAccounts()
        {
            try
            {
                return (await _accountRepository.GetAccountsByRole("Admin")).ToList();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        /// <summary>
        /// Thay đổi quyền của tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // PUT: api/Accounts/ChangeRoleOfAccount
        [Authorize (Roles = "Admin")]
        [HttpPut("ChangeRoleOfAccount")]
        public async Task<IActionResult> ChangeRoleOfAccount(Account account)
        {
            var username = account.UserName;
            var role = account.Role;

            var _account = await _accountRepository.GetAccount(username);

            if (_account == null)
            {
                return NotFound("Not found this account in database");
            }

            if (_account.Role == role || role == "" || role == null )
            {
                return BadRequest("Error of parameter role");
            }
            _account.Role = role;

            try
            {
                await _accountRepository.UpdateAccount(_account);
                return Ok("Change role successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Error when call UpdateAccount(_account)");         
            }
 
        }
        /// <summary>
        /// Xóa tài khoản bằng cách truyền vào username
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Accounts/DeleteAccount/userName
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAccount/{userName}")]
        public async Task<ActionResult<Account>> DeleteAccount(string userName)
        {
            var account = await _accountRepository.GetAccount(userName);
            if (account == null)
            {
                return NotFound("Can't found account with this username");
            }
            try
            {
               if (await _accountRepository.DeleteAccountAsync(userName))
                {
                    return Ok("Delete account successful");
                }
                else
                {
                    return BadRequest("Error when call DeleteAccountAsync(userName)");
                }
               
            }
            catch (DbUpdateException)
            {
                return BadRequest("DbUpdateException in DeleteAccount");
            }

        }

        #endregion

        #region User

        #region Authorize
        /// <summary>
        /// Lấy thông tin tài khoản bằng cách truyền vào username
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Accounts/GetAccountInformation/userName
        [Authorize]
        [HttpGet("GetAccountInformation/{userName}")]
        public async Task<ActionResult<Account>> GetAccountInformation(string userName)
        {
            var identity = User.Identity as ClaimsIdentity;
            var accountRole = await _accountRepository.GetAccount(identity.Name);
            if (accountRole.Role == "Admin")
            {
                var account = await _accountRepository.GetAccount(userName);
                if (account == null)
                {
                    return NotFound("Can't found the account with id: " + userName);
                }
                return account;
            }
            else
            {
                if(identity.Name == userName)
                {
                    var account = await _accountRepository.GetAccount(userName);
                    if (account == null)
                    {
                        return NotFound("Can't found the account with id: " + userName);
                    }
                    return account;
                }
                else
                {
                    BadRequest("Not authorized");
                }
            }
            return BadRequest("Can't access to database!");
        }
        /// <summary>
        /// Thay đổi thông tin tài khoản bằng cách truyền vào username và một đối tượng Account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        // PUT: api/Accounts/ChangeAccountInformation/{userName}
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("ChangeAccountInformation/{userName}")]
        public async Task<IActionResult> ChangeAccountInformation(string userName, Account account)
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                if (userName != account.UserName || identity.Name != account.UserName)
                {
                    return BadRequest("Parameter username is diffirent with acount's username");
                }

                var _account = await _accountRepository.GetAccount(userName);

                if (_account == null)
                {
                    return NotFound("Not found account with this username");
                }

                try
                {
                    account.Role = _account.Role;
                    await _accountRepository.UpdateAccount(account);
                    return Ok("Update account successfully");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return BadRequest(ex.ToString() + "Error in ChangeAccountInformation()");
                }
            }
            return NoContent();
        }

        #endregion



        #region AllowAnonymous
        /// <summary>
        /// Tạo tài khoản với role là Customer bằng cách truyền vào 1 đối tượng Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        // POST: api/Accounts/CreateAccount
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("CreateAccount")]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> CreateAccount(Account account)
        {
            var _account = await _accountRepository.GetAccount(account.UserName);

            if (_account != null)
            {
                return Conflict("This user name is existed");
            }
            try
            {
                //Khách chỉ tạo đươc tài khoản là Customer
                account.Role = Role.Customer;
                if (account.Address == null)
                {
                    account.Address = "";
                }
                if (account.FullName == null)
                {
                    account.Address = "";
                }
                if (account.PhoneNumber == null)
                {
                    account.PhoneNumber = "";
                }
                await _accountRepository.AddAccount(account);
                return Ok("Add account is successful");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.ToString() + "Error in CreateAccount()");
            }
        }
        /// <summary>
        /// Kiểm tra tài khoản đã tồn tại trong database chưa
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        // POST: api/Accounts/CheckAccountAsync
        [HttpPost("CheckAccountAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckAccountAsync(Account account)
        {

            var _account = await _accountRepository.GetAccount(account.UserName);
            
            if (_account != null)
            {
                return Conflict("This user name is existed");
            }
            else
            {
                return Ok("User name can be use");
            }
        }

        #endregion

        #endregion

        #region Test
        [Authorize]
        [HttpPost("getname2")]
        public Object GetName2()
        {
            ClaimsIdentity identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                return identity.Name;
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "FullName").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }
            return null;
        }
        #endregion



        #region Method

        #endregion


    }
}
