using MonsterTradingCardGame_Wurnig.Battle;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NpgsqlTypes;

namespace MonsterTradingCardGame_Wurnig
{

    public class CardJSON
    {
        private readonly string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";

        public string? Id { get; set; }
        public string? Name { get; set; }
        public double Damage { get; set; }

        //database eintrag
        public bool GenerateDatabaseCard()
        {
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            const string sql = "INSERT INTO cards(id, name, damage) VALUES(@id, @name, @damage)"; //@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("id", Id);
            cmd.Parameters.AddWithValue("name", Name);
            cmd.Parameters.AddWithValue("damage", Damage);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("generation failed !");
                return false;
            }

            Console.WriteLine($"Card {Name} was succesfully added do database!");
            return true;
        }

        public string ShowJsonCards(string player)
        {
            var splittedPlayer = player.Split("-");
            var name = splittedPlayer[0];
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            const string sql = "SELECT id, name, damage FROM stack JOIN cards ON cards.id=stack.cardid WHERE player = @player"; //@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("player", name);
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
                Console.WriteLine("something went wrong");
                return "";
            }

        }

    }
}