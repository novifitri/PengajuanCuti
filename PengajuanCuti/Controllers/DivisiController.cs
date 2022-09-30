using Microsoft.AspNetCore.Http;
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
    public class DivisiController : Controller
    {
        DivisiRepository divisiRepository;
        CheckAuth checkAuth;
        public DivisiController(DivisiRepository divisiRepository, CheckAuth checkAuth)
        {
            this.divisiRepository = divisiRepository;
            this.checkAuth = checkAuth;
        }
        public IActionResult Index()
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("Account/Login");

            //check admin  
            if (checkAuth.IsAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var data = divisiRepository.Get();
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
            var data = divisiRepository.Get(id);
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

        public IActionResult Create(Divisi divisi)
        {
            if (ModelState.IsValid)
            {
                var result = divisiRepository.Post(divisi);
                if (result > 0)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Divisi gagal ditambah");
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!checkAuth.checkLoggedIn())
                return Redirect("/Account/Login");

            //check admin  
            if (checkAuth.IsAdmin() == false)
            {
                return RedirectToAction("Index", "Unauthorized");
            }
            var divisi = divisiRepository.Get(id);
            if (divisi == null)
            {
                return NotFound();
            }
            return View(divisi);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Divisi divisi)
        {
            if (ModelState.IsValid)
            {
                var result = divisiRepository.Put(divisi);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Divisi gagal diupdate");
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
            var divisi = divisiRepository.Get(id);
            if (divisi == null)
            {
                return NotFound();
            }
            return View(divisi);
        }

        [HttpPost]
        public IActionResult Delete(Divisi divisi)
        {
            var result = divisiRepository.Delete(divisi.Id);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Divisi gagal dihapus");
            return View();
        }
    }
}
