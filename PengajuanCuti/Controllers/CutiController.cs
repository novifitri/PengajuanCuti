using PengajuanCuti.Models;
using Microsoft.AspNetCore.Mvc;
using PengajuanCuti.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PengajuanCuti.Controllers
{
    public class CutiController : Controller
    {
        CutiRepository cutiRepository;
        KaryawanRepository karyawanRepository;
        JenisCutiRepository jenisCutiRepository;
        public CutiController(CutiRepository cutiRepository, KaryawanRepository karyawanRepository, JenisCutiRepository jenisCutiRepository)
        {
           this.cutiRepository = cutiRepository;
            this.karyawanRepository = karyawanRepository;
            this.jenisCutiRepository = jenisCutiRepository;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetInt32("KaryawanId");
            if (user == null)
                return Redirect("/Account/Login");

            //jika admin bisa lihat isi semua daftar cuti
            var Role = HttpContext.Session.GetString("Role");
            List<Cuti> data= new List<Cuti>();
            if (Role.Contains("Admin"))
            {
                data = cutiRepository.Get();
                return View(data);
            }

            //jika bukan maka hanya bisa lihat punya dia
            var karyawan = karyawanRepository.Get(user);
            data = cutiRepository.GetSpesifik(karyawan.Id);
            return View(data);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = cutiRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var user = HttpContext.Session.GetInt32("KaryawanId");
            if (user == null)
                return Redirect("/Account/Login");
            var karyawan = karyawanRepository.Get(user);
            List<JenisCuti> all = jenisCutiRepository.Get();
            foreach (JenisCuti kategori in all.ToList())
            {
                if (kategori.Nama.ToLower().Contains("hamil") && karyawan.Jenis_Kelamin.Equals("Laki - laki"))
                {
                    all.Remove(kategori);
                }
            }
            ViewData["Categories"] = all;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cuti cuti)
        {
            var user = HttpContext.Session.GetInt32("KaryawanId");
            if (user == null)
                return Redirect("/Account/Login");
            var karyawan = karyawanRepository.Get(user);
            cuti.Karyawan_Id = karyawan.Id;
            var result = cutiRepository.Post(cuti);
            if (result > 0)
                return RedirectToAction("Index");
            
            List<JenisCuti> all = jenisCutiRepository.Get();
            foreach (JenisCuti kategori in all.ToList())
            {
                if (kategori.Nama.ToLower().Contains("hamil") && karyawan.Jenis_Kelamin.Equals("Laki - laki"))
                {
                    all.Remove(kategori);
                }
            }
            ViewData["Categories"] = all;
            ModelState.AddModelError(string.Empty, "Cuti gagal ditambah");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = HttpContext.Session.GetInt32("KaryawanId");
            if (user == null)
                return Redirect("/Account/Login");

            var data = cutiRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
          
            var karyawan = karyawanRepository.Get(user);
            List<JenisCuti> all = jenisCutiRepository.Get();
            foreach (JenisCuti kategori in all.ToList())
            {
                if (kategori.Nama.ToLower().Contains("hamil") && karyawan.Jenis_Kelamin.Equals("Laki - laki"))
                {
                    all.Remove(kategori);
                }
            }
            ViewData["Categories"] = all;
            return View(data);
        }

        [HttpGet]
        public IActionResult EditAdmin(int id)
        {
            var data = cutiRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            List<JenisCuti> all = jenisCutiRepository.Get();
            ViewData["Categories"] = all;
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAdmin(Cuti cuti)
        {
            var result = cutiRepository.Put2(cuti);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Cuti gagal diupdate");
            List<JenisCuti> all = jenisCutiRepository.Get();
            ViewData["Categories"] = all;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cuti cuti)
        {
            var result = cutiRepository.Put(cuti);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
    
            ModelState.AddModelError(string.Empty, "Cuti gagal diupdate");
            List<JenisCuti> all = jenisCutiRepository.Get();
            ViewData["Categories"] = all;
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = cutiRepository.Get(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Cuti cuti)
        {
            var result = cutiRepository.Delete(cuti.Id);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Cuti gagal dihapus");
            return View();
        }
    }
}
