using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Karyawan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nama { get; set; }
        [Required]
        public string NIK { get; set; }
        [Required]
        public string Jenis_Kelamin { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Tanggal_Lahir { get; set; }
        public string Alamat { get; set; }
        [Required]
        public string Nomor_Telp { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Divisi Divisi { get; set; }

        [ForeignKey("Divisi")]
        public int Divisi_Id { get; set; }
    }
}
