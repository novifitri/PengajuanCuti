using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        [HttpPost]
        public IActionResult LoginAPI(LoginVM login)
        {
            var result = accountRepository.Login(login);
            if (result == null)
                return NotFound(new { message = "gagal login user tidak ditemukan", statusCode = 404 });
            return Ok(new { message = "berhasil login", statusCode = 200, data = result });

        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterAPI(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                var result = accountRepository.Register(register);
                if (result > 0)
                    return Ok(new { message = "berhasil register", statusCode = 200 });
            }
            return BadRequest(new { message = "gagal register", statusCode = 400 });
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordVM changePassword)
        {
            if (ModelState.IsValid)
            {
                var result = accountRepository.ChangePassword(changePassword);
                if (result > 0)
                    return Ok(new { message = "berhasil ganti password", statusCode = 200 });
            }
            return BadRequest(new { message = "gagal ganti password", statusCode = 400 });
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPWVM forget)
        {
            if (ModelState.IsValid)
            {
                var result = accountRepository.ForgetPassword(forget);
                if (result > 0)
                    return Ok(new { message = "berhasil ganti password", statusCode = 200 });
            }
            return BadRequest(new { message = "gagal ganti password", statusCode = 400 });
        }
    }
}
