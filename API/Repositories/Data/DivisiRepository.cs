using API.Context;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class DivisiRepository : IDivisiRepository
    {
            MyContext myContext;

            public DivisiRepository(MyContext myContext)
            {
                this.myContext = myContext;
            }
            public int Delete(int id)
            {
                var data = Get(id);
                if (data == null)
                    return -1;
                myContext.Divisi.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }

            public List<Divisi> Get()
            {
                var data = myContext.Divisi.ToList();
                return data;
            }

            public Divisi Get(int id)
            {
                var data = myContext.Divisi.Find(id);
                return data;
            }

            public int Post(Divisi divisi)
            {
                myContext.Divisi.Add(divisi);
                var result = myContext.SaveChanges();
                return result;
            }

            public int Put(Divisi divisi)
            {
                var divisiToUpdate = Get(divisi.Id);
                if (divisiToUpdate == null)
                    return -1;
                divisiToUpdate.Nama = divisi.Nama;
                myContext.Divisi.Update(divisiToUpdate);
                var result = myContext.SaveChanges();
                return result;
            }
        
    }
}
