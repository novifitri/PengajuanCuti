using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Nama { get; set; }
        [Required]

        public string NIK { get; set; }
        [Required]

        public string Jenis_Kelamin { get; set; }
        [Required]
        public DateTime Tanggal_Lahir { get; set; }
        public string Alamat { get; set; }
        [Required]

        public string Nomor_Telp { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Password minimal 8 karakter", MinimumLength = 8)]
        public string Password { get; set; }
        [Required]

        public int Divisi_Id { get; set; }
        [Required]

        public int Role_Id { get; set; }
    }
}
