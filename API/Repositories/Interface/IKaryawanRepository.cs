using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IKaryawanRepository
    {
        List<Karyawan> Get();
        Karyawan Get(int? id);

        int Post(Karyawan karyawan);
        int Put(Karyawan karyawan);
        int Delete(int id);
    }
}
