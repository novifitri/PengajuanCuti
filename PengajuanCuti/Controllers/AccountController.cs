using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PengajuanCuti.Models;
using PengajuanCuti.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PengajuanCuti.Controllers
{
    public class AccountController : Controller
    {
        HttpClient HttpClient;
        string address;
        DivisiRepository divisiRepository;
        RoleRepository roleRepository;

        public AccountController(DivisiRepository divisiRepository, RoleRepository roleRepository)
        {
            this.address = "https://localhost:44318/api/Account/";
            this.HttpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
            this.divisiRepository = divisiRepository;
            this.roleRepository = roleRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address, content).Result;

            if (result.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseClient>(await result.Content.ReadAsStringAsync());
                HttpContext.Session.SetString("Role", data.data.Role);
                HttpContext.Session.SetInt32("KaryawanId", data.data.Id);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Email dan password salah");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            List<Divisi> allDivision = divisiRepository.Get();
            List<Role> roles = roleRepository.Get();
            ViewData["AllDivision"] = allDivision;
            ViewData["AllRoles"] = roles;
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
                var result = HttpClient.PostAsync(address + "Register", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Registrasi gagal");
                }
            }
            List<Divisi> allDivision = divisiRepository.Get();
            List<Role> roles = roleRepository.Get();
            ViewData["AllDivision"] = allDivision;
            ViewData["AllRoles"] = roles;
            return View();
        }

        [HttpGet]
        [Route("ChangePassword")]
        public IActionResult ChangePW(LoginVM login)
        {
            var user = HttpContext.Session.GetInt32("KaryawanId");
            if (user == null)
                return Redirect("Account/Login");

            return View();
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePW(ChangePasswordVM changePassword)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(changePassword), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address + "ChangePassword", content).Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Ganti Password gagal");
            return View();
        }

        [HttpGet]
        [Route("ForgetPassword")]
        public IActionResult ForgetPW(ForgetPWVM forgotPW)
        {
            return View();
        }
        
        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPw(ForgetPWVM forget)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(forget), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address + "ForgetPassword", content).Result;
            if (result.IsSuccessStatusCode)
            {
                return Redirect("Account/Login");
            }
            ModelState.AddModelError(string.Empty, "Ganti Password gagal");
            return View();
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
