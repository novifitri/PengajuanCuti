using PengajuanCuti.Context;
using PengajuanCuti.Models;
using PengajuanCuti.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Repositories.Data
{
    public class RoleRepository : IRoleRepository
    {
        MyContext myContext;

        public RoleRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            myContext.Role.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Role> Get()
        {
            var roles = myContext.Role.ToList();
            return roles;
        }

        public Role Get(int id)
        {
            var role = myContext.Role.Find(id);
            return role;
        }

        public int Post(Role role)
        {
            myContext.Role.Add(role);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Role role)
        {
            var data = Get(role.Id);
            if (data == null)
                return -1;
            data.Nama = role.Nama;
            myContext.Role.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
