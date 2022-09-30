using Microsoft.AspNetCore.Mvc;
using PengajuanCuti.Middleware;
using PengajuanCuti.Models;
using PengajuanCuti.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Controllers
{
    public class RoleController : Controller
    {
        RoleRepository roleRepository;
        CheckAuth checkAuth;
        public RoleController(RoleRepository roleRepository, CheckAuth checkAuth)
        {
            this.roleRepository = roleRepository;
            this.checkAuth = checkAuth;
        }
        public IActionResult Index()
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = roleRepository.Get();
            return View(data);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = roleRepository.Get(id);
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                var result = roleRepository.Post(role);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Role gagal ditambah");
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var divisi = roleRepository.Get(id);
            if (divisi == null)
            {
                return NotFound();
            }
            return View(divisi);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var result = roleRepository.Put(role);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Role gagal diupdate");
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = roleRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Role role)
        {
            var result = roleRepository.Delete(role.Id);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Role gagal dihapus");
            return View();
        }
    }
}
