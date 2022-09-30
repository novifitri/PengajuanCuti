using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PengajuanCuti.Middleware
{
    public class CheckAuth
    {
        private IHttpContextAccessor context;
        public CheckAuth(IHttpContextAccessor context)
        {
            this.context = context;
        }
        public bool checkLoggedIn()
        {
            if (context.HttpContext.Session.GetInt32("KaryawanId") == null)
                return false;
            return true;     
        }
        public bool IsAdmin()
        {
            if (context.HttpContext.Session.GetString("Role").Contains("Admin"))
                return true;
            return false;
        }
        public bool IsSuperAdmin()
        {
            if (context.HttpContext.Session.GetString("Role").ToLower() == "super admin")
                return true;
            return false;
        }
    }
}
