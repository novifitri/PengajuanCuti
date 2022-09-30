using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hash
{
    public class Hashing
    {
        private  string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public  string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());

        }
        public  bool ValidatePassword(string password, string currentHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, currentHash);
        }
    }
}
