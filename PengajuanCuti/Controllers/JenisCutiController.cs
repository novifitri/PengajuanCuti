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
    public class JenisCutiController : Controller
    {
        JenisCutiRepository jenisCutiRepository;
        CheckAuth checkAuth;
        public JenisCutiController(JenisCutiRepository jenisCutiRepository, CheckAuth checkAuth)
        {
            this.jenisCutiRepository = jenisCutiRepository;
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
            var data = jenisCutiRepository.Get();
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
            var data = jenisCutiRepository.Get(id);
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
        public IActionResult Create(JenisCuti jenisCuti)
        {
            if (ModelState.IsValid)
            {
                var result = jenisCutiRepository.Post(jenisCuti);
                if (result > 0)
                    return RedirectToAction("Index");

            }
            ModelState.AddModelError(string.Empty, "Jenis Cuti gagal ditambah");
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
            var data = jenisCutiRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JenisCuti jenisCuti)
        {
            if (ModelState.IsValid)
            {
                var result = jenisCutiRepository.Put(jenisCuti);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Jenis Cuti gagal diupdate");
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
            var data = jenisCutiRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(JenisCuti jenisCuti)
        {
            var result = jenisCutiRepository.Delete(jenisCuti.Id);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Jenis Cuti gagal dihapus");
            return View();
        }
    }
}
