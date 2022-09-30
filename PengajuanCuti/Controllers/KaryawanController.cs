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
    public class KaryawanController : Controller
    {
        KaryawanRepository karyawanRepository;
        DivisiRepository divisiRepository;
        CheckAuth checkAuth;
        public KaryawanController(KaryawanRepository karyawanRepository, DivisiRepository divisiRepository, CheckAuth checkAuth)
        {
            this.checkAuth = checkAuth;
            this.karyawanRepository = karyawanRepository;
            this.divisiRepository = divisiRepository;
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
            var data = karyawanRepository.Get();
            return View(data);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = karyawanRepository.Get(id);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            List<Divisi> allDivision = divisiRepository.Get();
            ViewData["AllDivision"] = allDivision;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Karyawan karyawan)
        {
            if (ModelState.IsValid)
            {
                var result = karyawanRepository.Post(karyawan);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Karyawan gagal ditambah");
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data= karyawanRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            List<Divisi> allDivision = divisiRepository.Get();
            ViewData["AllDivision"] = allDivision;
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Karyawan karyawan)
        {
            if (ModelState.IsValid)
            {
                var result = karyawanRepository.Put(karyawan);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }      
            ModelState.AddModelError(string.Empty, "Karyawan gagal diupdate");
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");
            //check admin  
            if (checkAuth.IsSuperAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = karyawanRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Karyawan karyawan)
        {
            var result = karyawanRepository.Delete(karyawan.Id);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Karyawan gagal dihapus");
            return View();
        }
    }
}
