using System.Security.Cryptography;
using System.Text;

namespace ImbizoFoundation.Classes
{
    public class Worker
    {
        public static string? courseIDs;
        public static string? lessonIDs;
        public static string? companyIDs;

        public static string hashPassword(string UsersPassword)
        {
            //The purpose of this method is to convert the string password to hash and 
            //compare the hashed password with the password from the database 
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] password_btyes = Encoding.ASCII.GetBytes(UsersPassword);
            byte[] encrypted_btyes = sha1.ComputeHash(password_btyes);

            return Convert.ToBase64String(encrypted_btyes);
        }

    }
}
