using MonsterTradingCardGame_Wurnig.Battle;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig
{

    internal class Package
    {
        private readonly string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";

        public Package()
        {
            
        }

        public bool CreatePackage(List<CardJSON> jsoncardlist)
        {
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            //daten in die datenbank eintragen
            var sql = "INSERT INTO packages(cardid, cardid2, cardid3, cardid4, cardid5) VALUES(@cardid, @cardid2, @cardid3, @cardid4, @cardid5)";//@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("cardid", jsoncardlist[0].Id);
            cmd.Parameters.AddWithValue("cardid2", jsoncardlist[1].Id);
            cmd.Parameters.AddWithValue("cardid3", jsoncardlist[2].Id);
            cmd.Parameters.AddWithValue("cardid4", jsoncardlist[3].Id);
            cmd.Parameters.AddWithValue("cardid5", jsoncardlist[4].Id);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("creation failed!");
                return false;
            }

            Console.WriteLine("Package was succesfully created!");
            return true;
        }

        public string AcquirePackage(User user)
        {
            using var con = new NpgsqlConnection(_cs);
            con.Open();

            var sql = "SELECT * FROM packages LIMIT 1";
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            try
            {
                var card1 = rdr[1].ToString();
                var card2 = rdr[2].ToString();
                var card3 = rdr[3].ToString();
                var card4 = rdr[4].ToString();
                var card5 = rdr[5].ToString();
                var cards = new[] { card1, card2, card3, card4, card5 };


                rdr.Close();
                foreach (var card in cards)
                {
                    var sql2 = "INSERT INTO stack(cardid, player) VALUES(@cardid, @player)";//@ injection safe
                    using var cmd2 = new NpgsqlCommand(sql2, con);
                    cmd2.Parameters.AddWithValue("cardid", card);
                    cmd2.Parameters.AddWithValue("player", user.Username);
                    cmd2.ExecuteNonQuery();
                }

                var sql3 = "UPDATE users SET coins = (@coins) WHERE username = (@username);";
                using var cmd3 = new NpgsqlCommand(sql3,con);
                cmd3.Parameters.AddWithValue("username", user.Username);
                cmd3.Parameters.AddWithValue("coins", user.Coins - 5);
                cmd3.ExecuteNonQuery();

                var sql4 = "DELETE FROM packages WHERE cardid = @card1";
                using var cmd4 = new NpgsqlCommand(sql4,con);
                cmd4.Parameters.AddWithValue("card1", card1);
                cmd4.ExecuteNonQuery();
                StringBuilder sb = new StringBuilder();
                sb.Append(card1);
                sb.Append(card2);
                sb.Append(card3);
                sb.Append(card4);
                sb.Append(card5);
                return sb.ToString();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null!;
            }
            
        }


    }
}
