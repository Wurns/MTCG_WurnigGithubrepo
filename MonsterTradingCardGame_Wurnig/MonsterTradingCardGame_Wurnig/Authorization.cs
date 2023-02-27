using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig
{
    internal class Authorization
    {
        private readonly string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";
        public User? Authorize(string token)
        {
            var fullusername = token.Split("-");
            var username = fullusername[0];
            //------------------Connect to Database------------------------------
            using var con = new NpgsqlConnection(_cs);
            con.Open();

            var sql = "SELECT * FROM users WHERE username = @username";//@ sql injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("username", username );
            var user = new User();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                user.Username = rdr[0].ToString()!;
                user.Password = rdr[1].ToString()!;
                user.Coins = (int)rdr[2];
                return user;

            }
            catch (Exception)//wenn user nicht existiert
            {
                Console.WriteLine("login failed!");
                return null;
            }

        }
    }
}
