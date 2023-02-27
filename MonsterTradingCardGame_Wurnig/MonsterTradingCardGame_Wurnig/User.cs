using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig
{
    public class User
    {
        private readonly string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";

        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }

        public string? Name { get; set; }
        public int Coins { get; set; }
        public int Elo { get; set; }

        public string ShowData(string token)
        {
            //------------Connect to Database-----------------
            using var con = new NpgsqlConnection(_cs);
            con.Open();

            var sql = "SELECT * FROM users WHERE token = @token";//@ sql injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("token", token);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            var content = new StringBuilder();
            try
            {
                content.Append("Name: ");
                content.Append(rdr.GetString(6));
                content.Append("\nBio: ");
                content.Append(rdr.GetString(3));
                content.Append("\nImage: ");
                content.Append(rdr.GetString(4));
                Console.WriteLine($"Name: {rdr[6]}");
                Console.WriteLine($"Bio: {rdr[3]}");
                Console.WriteLine($"Image: {rdr[4]}");
                
            }
            catch (Exception)//wenn user nicht existiert
            {
                Console.WriteLine("failed to show data!");
            }
            return content.ToString();
        }
    }
}
