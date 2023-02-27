using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MonsterTradingCardGame_Wurnig
{
    internal class JSONDeck
    {
        private readonly string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";
        public List<CardJSON> CardJsons { get; set; }

        internal bool AddToDeck(string cardid, string player)
        {
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            const string sql = "INSERT INTO deck(cardid, player) VALUES(@cardid, @player)"; //@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("cardid", cardid);
            cmd.Parameters.AddWithValue("player", player);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("CAN NOT add to deck!");
                return false;
            }

            Console.WriteLine($"card was succesfully added do deck!");
            return true;
        }

        public string ShowJsonDeck(string player)
        {
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            const string sql = "SELECT id, name, damage FROM deck JOIN cards ON cards.id=deck.cardid WHERE player = @player"; //@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("player", player);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            try
            {
                var deck = new StringBuilder();

                while (rdr.Read())
                {
                    deck.Append(rdr[0]);
                    deck.Append("   ");
                    deck.Append(rdr[1]);
                    deck.Append("   ");
                    deck.Append(rdr[2]);
                    deck.Append("   ");
                    deck.AppendLine();
                }
                return deck.ToString();
            }
            catch (Exception) //wenn user nicht existiert
            {
                return "something went wrong with ShowJsonDeck";
            }

        }
    }
}
