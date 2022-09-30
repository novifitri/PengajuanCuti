using API.Context;
using API.Hash;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class AccountRepository : IAccountRepository
    {
        MyContext myContext;
        Hashing hash;
        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
            this.hash = new Hashing();
        }

        /* Login
        * Register
        * Change Password
        * Forget Password
       */

        public ResponseLogin Login(LoginVM login)
        {
            var data = myContext.UserRole
                        .Include(x => x.Role)
                        .Include(x => x.User)
                        .Include(x => x.User.Karyawan)
                        .FirstOrDefault(x =>
                         x.User.Karyawan.Email.Equals(login.Email)
                        );
            if (data == null)
                return null;
            //  verify password
            if (hash.ValidatePassword(login.Password, data.User.Password) == false)
            {
                // authentication failed
                return null;
            }
            ResponseLogin result = new ResponseLogin()
            {
                Id = data.User.Id,
                Fullname = data.User.Karyawan.Nama,
                Email = data.User.Karyawan.Email,
                Role = data.Role.Nama
            };
            return result;
       
        }

        public int Register(RegisterVM register)
        {
            //saving to Karyawan table
            Karyawan employee = new Karyawan();
            employee.Nama = register.Nama;
            employee.NIK = register.NIK;
            employee.Jenis_Kelamin = register.Jenis_Kelamin;
            employee.Tanggal_Lahir = register.Tanggal_Lahir;
            employee.Alamat = register.Alamat;
            employee.Nomor_Telp = register.Nomor_Telp;
            employee.Divisi_Id = register.Divisi_Id;
            employee.Email = register.Email;
            myContext.Karyawan.Add(employee);
            myContext.SaveChanges();

            //saving to User table
            User user = new User();
            user.Id = employee.Id;
            user.Password = hash.HashPassword(register.Password);
            myContext.User.Add(user);
            myContext.SaveChanges();

            //saving to UserRole table
            UserRole userRole = new UserRole();
            userRole.User_Id = user.Id;
            userRole.Role_Id = register.Role_Id;
            myContext.UserRole.Add(userRole);
            var result = myContext.SaveChanges();

            return result;
        }


        public int ChangePassword(ChangePasswordVM changePassword)
        {
            var data = myContext.User
                       .Include(x => x.Karyawan)
                       .FirstOrDefault(x =>
                       x.Karyawan.Email == changePassword.Email
                       );
            // check account and verify password
            if (data == null || hash.ValidatePassword(changePassword.OldPassword, data.Password) == false)
            {
                // authentication failed
                return -1;
            }
            if (!changePassword.NewPassword.Equals(changePassword.ConfirmNewPassword))
            {
                return -1;
            }
            data.Password = hash.HashPassword(changePassword.NewPassword);
            myContext.User.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public int ForgetPassword(ForgetPWVM forget)
        {
            var data = myContext.User
                    .Include(x => x.Karyawan)
                    .FirstOrDefault(x =>
                    x.Karyawan.Email == forget.Email
                    );
            if (data == null || !forget.NewPassword.Equals(forget.ConfirmNewPassword))
            {
                return -1;
            }
            data.Password = hash.HashPassword(forget.NewPassword);
            myContext.User.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
