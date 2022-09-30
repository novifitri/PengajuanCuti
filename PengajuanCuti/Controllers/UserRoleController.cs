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
    public class UserRoleController : Controller
    {
        UserRoleRepository userRoleRepository;
        KaryawanRepository karyawanRepository;
        RoleRepository roleRepository;
        CheckAuth checkAuth;
        public UserRoleController(UserRoleRepository userRoleRepository, KaryawanRepository karyawanRepository, RoleRepository roleRepository, CheckAuth checkAuth)
        {
            this.userRoleRepository = userRoleRepository;
            this.karyawanRepository = karyawanRepository;
            this.roleRepository = roleRepository;
            this.checkAuth = checkAuth;
        }
        public IActionResult Index()
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = userRoleRepository.Get();
            return View(data);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = userRoleRepository.Get(id);
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            List<Karyawan> karyawans = karyawanRepository.Get();
            ViewData["AllKaryawan"] = karyawans;
            List<Role> roles = roleRepository.Get();
            ViewData["AllRoles"] = roles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                var result = userRoleRepository.Post(userRole);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            List<Karyawan> karyawans = karyawanRepository.Get();
            ViewData["AllKaryawan"] = karyawans;
            List<Role> roles = roleRepository.Get();
            ViewData["AllRoles"] = roles;
            ModelState.AddModelError(string.Empty, "User Role gagal ditambah");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = userRoleRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            List<Karyawan> karyawans = karyawanRepository.Get();
            ViewData["AllKaryawan"] = karyawans;
            List<Role> roles = roleRepository.Get();
            ViewData["AllRoles"] = roles;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                var result = userRoleRepository.Put(userRole);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            List<Karyawan> karyawans = karyawanRepository.Get();
            ViewData["AllKaryawan"] = karyawans;
            List<Role> roles = roleRepository.Get();
            ViewData["AllRoles"] = roles;
            ModelState.AddModelError(string.Empty, "User role gagal diupdate");
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = userRoleRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(UserRole userRole)
        {
            var result = userRoleRepository.Delete(userRole.Id);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
