using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashControl.Context;
using CashControl.Helpers;
using CashControl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CashControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _authContext;

        public CompanyController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }

        [HttpPost("companyAuthenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Company companyObj)
        {
            if (companyObj == null)
                return BadRequest();

            var company = await _authContext.Companies
                .FirstOrDefaultAsync(x => x.Username == companyObj.Username);
            if (company == null)
                return NotFound(new { Message = "Company not found!" });

            if (!PasswordHasher.VerifyPassword(companyObj.Password, company.Password))
            {
                return BadRequest(new { Message = "Password is incorrect" });
            }

            return Ok(new
            {
                Message = "Login Success!"
            });
        }

        [HttpPost("companyRegister")]
        public async Task<IActionResult> RegisterCompany([FromBody] Company companyObj)
        {
            if (companyObj == null)
                return BadRequest();

            if (await CheckUsernameExistAsync(companyObj.Username))
                return BadRequest(new { Message = "Username already exists" });

            if (await CheckEmailExistAsync(companyObj.Email))
                return BadRequest(new { Message = "Email already exists" });

            var passMessage = CheckPasswordStrength(companyObj.Password);
            if (!string.IsNullOrEmpty(passMessage))
                return BadRequest(new { Message = passMessage.ToString() });

            companyObj.Password = PasswordHasher.HashPassword(companyObj.Password);
            companyObj.Token = "";

            await _authContext.Companies.AddAsync(companyObj);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Company Registered!"
            });
        }

        private Task<bool> CheckUsernameExistAsync(string username)
            => _authContext.Companies.AnyAsync(x => x.Username == username);

        private Task<bool> CheckEmailExistAsync(string email)
            => _authContext.Companies.AnyAsync(x => x.Email == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();

            if (password.Length < 9)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be alphanumeric" + Environment.NewLine);

            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special characters" + Environment.NewLine);

            return sb.ToString();
        }
    }
}
