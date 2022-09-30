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
    public class UserRoleRepository : IUserRoleRepository
    {
        MyContext myContext;

        public UserRoleRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(int id)
        {
            var data = Get(id);
            if (data == null)
                return -1;
            myContext.UserRole.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<UserRole> Get()
        {
            var data = myContext.UserRole
                        .Include(x=>x.Role)        
                        .Include(x=>x.User)
                        .Include(x=>x.User.Karyawan)
                        .ToList();
            return data;
        }

        public UserRole Get(int id)
        {
            var data = myContext.UserRole
                        .Include(x => x.Role)
                        .Include(x => x.User)
                        .Include(x => x.User.Karyawan)
                        .FirstOrDefault(x => x.Id == id);
            return data;
        }
        public int Post(UserRole userRole)
        {
            myContext.UserRole.Add(userRole);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(UserRole userRole)
        {
            var data = Get(userRole.Id);
            if (data == null)
                return -1;
            data.User_Id = userRole.User_Id;
            data.Role_Id = userRole.Role_Id;
            myContext.UserRole.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
