using PengajuanCuti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Repositories.Interface
{
    interface ICutiRepository
    {
        List<Cuti> Get();
        List<Cuti> GetSpesifik(int id);
        Cuti Get(int id);
        int Post(Cuti cuti);
        int Put(Cuti c);
        int Delete(int id);
    }
}
