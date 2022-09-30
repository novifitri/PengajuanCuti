using PengajuanCuti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Repositories.Interface
{
    public interface IJenisCutiRepository
    {
        List<JenisCuti> Get();
        JenisCuti Get(int id);

        int Post(JenisCuti jenisCuti);
        int Put(JenisCuti jenisCuti);
        int Delete(int id);
    }
}
