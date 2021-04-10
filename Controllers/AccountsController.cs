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
using System.Web.Helpers;
using System.Net.Http;
using BenriShop.ApiRepository.Accounts;

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


        [HttpPost("AddModAccount")]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> AddModAccount(Account account)
        {
            var _account = await _accountRepository.GetAccount(account.Username);

            if (_account != null)
            {
                return Conflict();
            }
            try
            {
                account.Role = Role.Mod;
                await _accountRepository.AddAccount(account);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return CreatedAtAction("GetAccount", new { id = account.Username }, account);
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
       // [Authorize]
        [HttpPost("ChangeRoleOfAccount/{id}")]
        public async Task<IActionResult> ChangeRoleOfAccount(string id, Account account)
        {

            var username = account.Username;
            var role = account.Role;

            var _account = await _accountRepository.GetAccount(username);

            if (_account == null)
            {
                return NotFound();
            }

            if (id != username)
            {
                return BadRequest();
            }

            if (_account.Role == role || role == "" || role == null )
            {
                return BadRequest();
            }
            _account.Role = role;

            try
            {
                await _accountRepository.UpdateAccount(_account);
                return Ok("Change role successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Accounts/5
       // [Authorize]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Account>> DeleteAccount(string id)
        {
            var account = await _accountRepository.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }
            try
            {
                _accountRepository.DeleteAccount(id);
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return account;
        }

        #endregion

        #region User

        #region Authorize

        // GET: api/Accounts/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            var account = await _accountRepository.GetAccount(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }
        // PUT: api/Accounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(string id, Account account)
        {
            if (id != account.Username)
            {
                return BadRequest();
            }

            var _account = await _accountRepository.GetAccount(id);

            if (_account == null)
            {
                return NotFound();
            }

            try
            {
                await _accountRepository.UpdateAccount(account);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        #endregion



        #region AllowAnonymous

        // POST: api/Accounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("CreateAccount")]
        [AllowAnonymous]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            var _account = await _accountRepository.GetAccount(account.Username);

            if (_account != null)
            {
                return Conflict();
            }
            try
            {
                await _accountRepository.AddAccount(account);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return CreatedAtAction("GetAccount", new { id = account.Username }, account);
        }

        [HttpPost("CheckAccount")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckAccountAsync(Account account)
        {

            var _account = await _accountRepository.GetAccount(account.Username);

            if (_account != null)
            {
                return Conflict("Trùng tài khoản");
            }
            else
            {
                return Ok("Có thể tạo");
            }
        }

        #endregion

        #endregion

        #region Method

        private bool AccountExists(string id)
        {
            if (_accountRepository.GetAccount(id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }

        #endregion


    }
}
