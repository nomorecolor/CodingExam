using System.Security.Cryptography;
using System.Text;

namespace CodingExam.Common
{
    public static class Helpers
    {
        public static string Encrypt(string word)
        {
            var data = Encoding.ASCII.GetBytes(word);
            data = new SHA256Managed().ComputeHash(data);

            return Encoding.ASCII.GetString(data);
        }
    }
}
