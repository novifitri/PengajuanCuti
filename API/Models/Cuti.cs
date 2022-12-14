using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Cuti
    {
        [Key]
        public int Id { get; set; }

        public Karyawan Karyawan;
        [ForeignKey("Karyawan")]
        public int Karyawan_Id { get; set; }


        public JenisCuti JenisCuti;
        [ForeignKey("JenisCuti")]
        public int JenisCuti_Id { get; set; }

        [Required]
        public DateTime TanggalMulai { get; set; }
        [Required]
        public DateTime TanggalAkhir { get; set; }
        [Required]
        public string Keterangan { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
