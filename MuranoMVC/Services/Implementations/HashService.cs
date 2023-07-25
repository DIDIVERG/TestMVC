using System.Security.Cryptography;
using System.Text;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services.Implementations;

public class HashService : IHashService
{
    public string GetHash(List<string> keywords)
    {
        keywords.Sort();
        string combinedWords = string.Join("", keywords).ToLower();
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(combinedWords));
            StringBuilder hashBuilder = new StringBuilder();
            foreach (byte b in data)
            {
                hashBuilder.Append(b.ToString("x2"));
            }
            string hash = hashBuilder.ToString();

            return hash;
        }
    }
}