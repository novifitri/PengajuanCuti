using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nama { get; set; }
    }
}
