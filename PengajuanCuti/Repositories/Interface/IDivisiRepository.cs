using PengajuanCuti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Repositories.Interface
{
    public interface IDivisiRepository
    {
        List<Divisi> Get();
        Divisi Get(int id);
        int Post(Divisi divisi);
        int Put(Divisi divisi);
        int Delete(int id);
    }
}
