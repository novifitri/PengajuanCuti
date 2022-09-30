using Microsoft.EntityFrameworkCore;
using PengajuanCuti.Context;
using PengajuanCuti.Models;
using PengajuanCuti.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Repositories.Data
{
    public class KaryawanRepository : IKaryawanRepository
    {
        MyContext myContext;

        public KaryawanRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            myContext.Karyawan.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Karyawan> Get()
        {
            var data = myContext.Karyawan.Include(x => x.Divisi).ToList();
            return data;
        }

        public Karyawan Get(int? id)
        {
            var data = myContext.Karyawan.Include(x => x.Divisi).FirstOrDefault(x => x.Id == id);
            return data;
        }

        public int Post(Karyawan karyawan)
        {
            myContext.Karyawan.Add(karyawan);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Karyawan karyawan)
        {
            var data = Get(karyawan.Id);
            if (data == null)
                return -1;
            data.Nama = karyawan.Nama;
            data.NIK = karyawan.NIK;
            data.Tanggal_Lahir = karyawan.Tanggal_Lahir;
            data.Jenis_Kelamin = karyawan.Jenis_Kelamin;
            data.Alamat = karyawan.Alamat;
            data.Nomor_Telp = karyawan.Nomor_Telp;
            data.Email = karyawan.Email;
            data.Divisi_Id = karyawan.Divisi_Id;
            myContext.Karyawan.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
