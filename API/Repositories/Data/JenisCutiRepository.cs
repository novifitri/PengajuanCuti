using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class JenisCutiRepository : IJenisCutiRepository
    {
        MyContext myContext;

        public JenisCutiRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            myContext.JenisCuti.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<JenisCuti> Get()
        {
            var data = myContext.JenisCuti.ToList();
            return data;
        }

        public JenisCuti Get(int id)
        {
            var data = myContext.JenisCuti.Find(id);
            return data;
        }

        public int Post(JenisCuti jenisCuti)
        {
            myContext.JenisCuti.Add(jenisCuti);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(JenisCuti jenisCuti)
        {
            var data = Get(jenisCuti.Id);
            if (data == null)
                return -1;
            data.Nama = jenisCuti.Nama;
            myContext.JenisCuti.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
