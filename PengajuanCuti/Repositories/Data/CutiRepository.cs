using PengajuanCuti.Context;
using PengajuanCuti.Models;
using Microsoft.EntityFrameworkCore;
using PengajuanCuti.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Repositories.Data
{
    public class CutiRepository : ICutiRepository
    {
        MyContext myContext;

        public CutiRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            myContext.Cuti.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Cuti> GetSpesifik(int id)
        {
            var data = myContext.Cuti
                .Include(x=>x.JenisCuti)
                .Include(x=>x.Karyawan).Where(x => x.Karyawan_Id == id).ToList();
            return data;
        }

        public Cuti Get(int id)
        {
            var data = myContext.Cuti
                .Include(x => x.JenisCuti)
                .Include(x => x.Karyawan).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public int Post(Cuti cuti)
        {
            cuti.Status = "menunggu persetujuan";
            myContext.Cuti.Add(cuti);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Cuti cuti)
        {
            var data = Get(cuti.Id);
            if (data == null)
                return -1;
            data.JenisCuti_Id = cuti.JenisCuti_Id;
            data.TanggalMulai = cuti.TanggalMulai;
            data.TanggalAkhir = cuti.TanggalAkhir;
            data.Keterangan = cuti.Keterangan;
      
            myContext.Cuti.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
        public int Put2(Cuti cuti)
        {
            var data = Get(cuti.Id);
            if (data == null)
                return -1;
            data.Status = cuti.Status;

            myContext.Cuti.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
        public List<Cuti> Get()
        {
            var data = myContext.Cuti
                .Include(x => x.JenisCuti)
                .Include(x => x.Karyawan)
                .ToList();
            return data;
        }
    }
}
