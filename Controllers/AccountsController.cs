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
        // GET: api/Accounts 
        [Authorize(Roles = "Admin")]
        [HttpGet]
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
        [HttpPost]
        [AllowAnonymous]
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
            catch (DbUpdateException ex)
            {
                return BadRequest("Error when AddAccount");
            }
        }



        /// <summary>
        /// Lấy danh sách tài khoản Mod
        /// </summary>
        /// <returns></returns>
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
        [Authorize (Roles = "Admin")]
        [HttpPut("ChangeRoleOfAccount/{id}")]
        public async Task<IActionResult> ChangeRoleOfAccount(string id, Account account)
        {
            var username = account.UserName;
            var role = account.Role;

            var _account = await _accountRepository.GetAccount(username);

            if (_account == null)
            {
                return NotFound("Not found this account in database");
            }

            if (id != username)
            {
                return BadRequest("Parameter username is diffirent with account's username in database");
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
        // DELETE: api/Accounts/5
       // [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(string id)
        {
            var account = await _accountRepository.GetAccount(id);
            if (account == null)
            {
                return NotFound("Can't found account with this username");
            }
            try
            {
               if (await _accountRepository.DeleteAccountAsync(id))
                {
                    return Ok("Delete account successful");
                }
                else
                {
                    return BadRequest("Error when call DeleteAccountAsync(id)");
                }
               
            }
            catch (DbUpdateException)
            {
                return account;
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
        // GET: api/Accounts/5
        [Authorize]
        [HttpGet("GetAccountInformation/{id}")]
        public async Task<ActionResult<Account>> GetAccountInformation(string id)
        {
            var identity = User.Identity as ClaimsIdentity;
            var accountRole = await _accountRepository.GetAccount(identity.Name);
            if (accountRole.Role == "Admin")
            {
                var account = await _accountRepository.GetAccount(id);
                if (account == null)
                {
                    return NotFound("Can't found the account with id: " + id);
                }
                return account;
            }
            else
            {
                if(identity.Name == id)
                {
                    var account = await _accountRepository.GetAccount(id);
                    if (account == null)
                    {
                        return NotFound("Can't found the account with id: " + id);
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
        // PUT: api/Accounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("ChangeAccountInformation/{id}")]
        public async Task<IActionResult> ChangeAccountInformation(string id, Account account)
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                if (id != account.UserName || identity.Name != account.UserName)
                {
                    return BadRequest("Parameter username is diffirent with acount's username");
                }

                var _account = await _accountRepository.GetAccount(id);

                if (_account == null)
                {
                    return NotFound("Not found account with this username");
                }

                try
                {
                    await _accountRepository.UpdateAccount(account);
                    return Ok("Update account successfully");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
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
        // POST: api/Accounts
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
                await _accountRepository.AddAccount(account);
                return Ok("Add account is successful");
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

            return CreatedAtAction("GetAccount", new { id = account.UserName }, account);
        }
        /// <summary>
        /// Kiểm tra tài khoản đã tồn tại trong database chưa
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost("CheckAccount")]
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
            var identity = User.Identity as ClaimsIdentity;
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
