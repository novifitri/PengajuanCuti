using API.Models;
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
    public class DivisiController : ControllerBase
    {
        DivisiRepository divisiRepository;

        public DivisiController(DivisiRepository divisiRepository)
        {
            this.divisiRepository = divisiRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = divisiRepository.Get();
            return Ok(new { statusCode = 200, message = "List semua divisi", data = data });
        }
        [HttpPost]
        public IActionResult Post(Divisi divisi)
        {
            if (ModelState.IsValid)
            {
                var result = divisiRepository.Post(divisi);
                if (result > 0)
                    return Ok(new { statusCode = 200, message = "Divisi berhasil ditambah"});
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public IActionResult Put(Divisi divisi)
        {
            var result = divisiRepository.Put(divisi);
            if (result == -1)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            else if (result > 0)
            {
                return Ok(new { message = "Divisi berhasil diubah", statusCode = 200 });
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Destroy(int id)
        {
            var result = divisiRepository.Delete(id);
            if (result == -1)
                return NotFound(new { statusCode = 404, message = "Data tidak ditemukan" });
            else if (result > 0)
            {
                return Ok(new { message = "Divisi berhasil dihapus", statusCode = 200 });
            }
            return BadRequest();
       
        }
    }
}
