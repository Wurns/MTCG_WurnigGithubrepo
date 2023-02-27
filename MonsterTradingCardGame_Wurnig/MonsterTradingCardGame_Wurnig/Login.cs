using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig
{
    internal class Login
    {
        private readonly string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";
        public string Logmein(string username, string password)
        {
            //------------------Connect to Database------------------------------
            using var con = new NpgsqlConnection(_cs);
            con.Open();

            var sql = "SELECT * FROM users WHERE username = @username";//@ sql injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("username", username);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            try
            {
                var dbpassword = rdr[1].ToString();

                if (password != dbpassword)
                {
                    Console.WriteLine("login failed!");
                    return "";
                }
            }
            catch (Exception)//wenn user nicht existiert
            {
                Console.WriteLine("login failed!");
                return "";
            }
            
            Console.WriteLine($"{username} succesfully logged in");
            return $"{username}-mtcgToken";
        }
    }
}
