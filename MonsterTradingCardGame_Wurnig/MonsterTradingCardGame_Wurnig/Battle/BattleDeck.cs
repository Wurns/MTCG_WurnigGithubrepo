using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig.Battle
{
    public class BattleDeck
    {
        private readonly string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";
        public List<Card> Cards { get; set; }
        public string? Owner { get; set; }
        public BattleDeck() 
        { 
            Cards = new List<Card>();
        }
        public void FetchCards(string player)
        {
            Owner = player;
            List<CardJSON> cards = new List<CardJSON>();
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            const string sql = "SELECT id, name, damage FROM deck JOIN cards ON cards.id=deck.cardid WHERE player = @player"; //@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("player", player);
            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            try
            {
                while (rdr.Read()) //jede karte in eine Art von liste schieben
                {
                    CardJSON card = new CardJSON();
                    card.Id = rdr[0].ToString();
                    card.Name = rdr[1].ToString();
                    card.Damage = (double)rdr[2];
                    cards.Add(card);
                }
                foreach (var c in cards)
                {
                    Cards.Add(new Card(c));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create BattleDeck!");
            }
        }
    }
}
