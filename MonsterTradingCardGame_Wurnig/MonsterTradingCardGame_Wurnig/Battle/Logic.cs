using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig.Battle
{
    public class Logic
    {
        private readonly BattleDeck _deck1;
        private readonly BattleDeck _deck2;
        private Random randi1 = new();
        private Random randi2 = new();

        public string? Winner { get; set; }

        public Logic(BattleDeck deck1, BattleDeck deck2)
        {
            _deck1 = deck1;
            _deck2 = deck2;
        }
        public void Battle()
        {
            var roundcount = 0;


            while (_deck1.Cards.Count != 0 && _deck2.Cards.Count != 0 && roundcount < 100)
            {
                var randi01 = randi1.Next(_deck1.Cards.Count);
                var randi02 = randi2.Next(_deck2.Cards.Count);
                var card1 = _deck1.Cards.ElementAt(randi01);
                var card2 = _deck2.Cards.ElementAt(randi02);

                if (card2.Type == "Monster" && card1.Type == "Monster")
                {
                    MonsterFight(card1, card2);
                    
                }
                else
                {
                    SpellFight(card1, card2);
                    
                }
                roundcount++;
            }
            if(roundcount == 100)
            {
                Winner = "Draw";
            }
            else if(_deck1.Cards.Count == 0)
            {
                Winner = _deck2.Owner;

            }
            else
            {
                Winner = _deck1.Owner;
            }
        }

        public void MonsterFight(Card card1, Card card2)
        {
            
            //goblins are too afraid of dragons!
            if (card1.Name == "Goblin" && card2.Name == "Dragon" || card1.Name == "Dragon" && card2.Name == "Goblin")
            {
                Console.WriteLine("Goblins are too afraid of Dragons!");
                switch (card2.Name)
                {
                    case "Dragon": //player 2 wins
                        _deck2.Cards.Add(card1);
                        _deck1.Cards.Remove(card1);
                        Console.WriteLine("Player 1 Goblin is too afraid, and runs away!");
                        Console.WriteLine("");
                        return;
                    case "Goblin"://player 1 wins
                        _deck1.Cards.Add(card2);
                        _deck2.Cards.Remove(card2);
                        Console.WriteLine("Player 2 Goblin is too afraid, and runs away!");
                        Console.WriteLine("");
                        return;
                    default:
                        return;
                }
            }
            //wizzards can control orks
            if (card1.Name == "Wizzard" && card2.Name == "Ork" || card1.Name == "Ork" && card2.Name == "Wizard")
            {
                Console.WriteLine($"Wizzards can control Orks!");

                switch (card2.Name)
                {
                    case "Wizzard": //player 2 wins
                        _deck2.Cards.Add(card1);
                        _deck1.Cards.Remove(card1);
                        Console.WriteLine("Player 2 Wizzard is controlling the Ork!");
                        Console.WriteLine("");
                        return;
                    case "Ork"://player 1 wins
                        _deck1.Cards.Add(card2);
                        _deck2.Cards.Remove(card2);
                        Console.WriteLine("Player 1 Wizzard is controlling the Ork!");
                        Console.WriteLine("");
                        return;
                    default:
                        return;
                }
            }
            //fire elves evade dragon attacks
            if ((card1.Element == "Fire" && card1.Name == "Elf" && card2.Name == "Dragon") || (card1.Name == "Dragon" && card2.Element == "Fire" && card2.Name == "Elf"))
            {
                Console.WriteLine($"FireElves evade Dragons attacks!");

                switch (card2.Name)
                {
                    case "Elf": //player 2 wins
                        _deck2.Cards.Add(card1);
                        _deck1.Cards.Remove(card1);
                        Console.WriteLine("Player 2 FireElf evaded the Attack!");
                        Console.WriteLine("");
                        return;
                    case "Dragon"://player 1 wins
                        _deck1.Cards.Add(card2);
                        _deck2.Cards.Remove(card2);
                        Console.WriteLine("Player 1 FireElf evaded the Attack!");
                        Console.WriteLine("");
                        return;
                    default:
                        return;
                }
            }


            if (card1.Damage > card2.Damage)
            {
                Console.WriteLine($"Player 1:{card1.Name} {card1.Damage} vs. Player 2: {card2.Name} {card2.Damage} => ");
                Console.WriteLine($"{card1.Name} wins!");
                Console.WriteLine("");
                _deck1.Cards.Add(card2);
                _deck2.Cards.Remove(card2);
                return;
            }

            else if(card1.Damage < card2.Damage)
            {
                Console.WriteLine($"Player 1:{card1.Name} {card1.Damage} vs. Player 2: {card2.Name} {card2.Damage} => ");
                Console.WriteLine($"{card2.Name} wins!");
                Console.WriteLine("");
                _deck2.Cards.Add(card1);
                _deck1.Cards.Remove(card1);
                return;
            }
            else
            {
                Console.WriteLine($"Player 1:{card1.Name} {card1.Damage} vs. Player 2: {card2.Name} {card2.Damage} => ");
                Console.WriteLine("It´s a Draw!");
                Console.WriteLine("");
                return;
            }
        
        }

        public void SpellFight(Card card1, Card card2)
        {
            //kraken is immune to spells
            if (card1.Name == "Kraken")
            {
                _deck1.Cards.Add(card2);
                _deck2.Cards.Remove(card2);
                Console.WriteLine("Player 1 Kraken is immune to Spells!");
                Console.WriteLine("");
                return;
            }
            if (card2.Name == "Kraken")
            {
                _deck2.Cards.Add(card1);
                _deck1.Cards.Remove(card1);
                Console.WriteLine("Player 2 Kraken is immune to Spells!");
                Console.WriteLine("");
                return;
            }
            //WaterSpell makes Knight drown
            if(card1.Name == "WaterSpell" && card2.Name == "Knight")
            {
                _deck1.Cards.Add(card2);
                _deck2.Cards.Remove(card2);
                Console.WriteLine("Player 2 Knight drowned because of the Flood!");
                Console.WriteLine("");
                return;
            }
            if(card1.Name == "Knight" && card2.Name == "WaterSpell")
            {
                _deck2.Cards.Add(card1);
                _deck1.Cards.Remove(card1);
                Console.WriteLine("Player 1 Knight drowned because of the Flood!");
                Console.WriteLine("");
                return;
            }



            //effective fights
            if (card1.Element == "Water" && card2.Element == "Fire" || card1.Element == "Fire" && card2.Element == "Normal" || card1.Element == "Normal" && card2.Element == "Water")
            {
                
                var doubleDamage = card1.Damage * 2;
                var halfedDamage = card2.Damage / 2;
                Console.WriteLine($"{card1.Element} is effective on {card2.Element}!");
                Console.WriteLine("");
                if (doubleDamage > halfedDamage) //effective trait wins
                {
                    _deck1.Cards.Add(card2);
                    _deck2.Cards.Remove(card2);
                    Console.WriteLine($"Player 2: {card2.Name} ({card2.Damage}) vs Player 1: {card1.Name} ({card1.Damage}) => {card2.Damage} VS {card1.Damage} -> {doubleDamage} VS {halfedDamage} => {card1.Name} wins!");
                    Console.WriteLine("");
                    return;
                }
                else if (doubleDamage < halfedDamage)//uneffective trait wins
                {
                    _deck2.Cards.Add(card1);
                    _deck1.Cards.Remove(card1);
                    Console.WriteLine($"Player 2: {card2.Name} ({card2.Damage}) vs Player 1: {card1.Name} ({card1.Damage}) => {card2.Damage} VS {card1.Damage} -> {doubleDamage} VS {halfedDamage} => {card2.Name} wins!");
                    Console.WriteLine("");
                    return;
                }
                else
                {
                    Console.WriteLine("It´s a draw! nothing happened.");
                    Console.WriteLine("");
                    return;
                }
            }
            if (card1.Element == "Fire" && card2.Element == "Water" || card1.Element == "Normal" && card2.Element == "Fire" || card1.Element == "Water" && card2.Element == "Normal")
            {

                var doubleDamage = card2.Damage * 2;
                var halfedDamage = card1.Damage / 2;
                Console.WriteLine($"{card2.Element} is effective on {card1.Element}!");
                Console.WriteLine("");
                if (doubleDamage > halfedDamage) //effective trait wins
                {
                    _deck2.Cards.Add(card1);
                    _deck1.Cards.Remove(card1);
                    Console.WriteLine($"Player 2: {card2.Name} ({card2.Damage}) vs Player 1: {card1.Name} ({card1.Damage}) => {card2.Damage} VS {card1.Damage} -> {doubleDamage} VS {halfedDamage} => {card2.Name} wins!");
                    Console.WriteLine("");
                    return;
                }
                else if (doubleDamage < halfedDamage)//uneffective trait wins
                {
                    _deck1.Cards.Add(card2);
                    _deck2.Cards.Remove(card2);
                    Console.WriteLine($"Player 2: {card2.Name} ({card2.Damage}) vs Player 1: {card1.Name} ({card1.Damage}) => {card2.Damage} VS {card1.Damage} -> {doubleDamage} VS {halfedDamage} => {card1.Name} wins!");
                    Console.WriteLine("");
                    return;
                }
                else
                {
                    Console.WriteLine("It´s a draw! nothing happened.");
                    Console.WriteLine("");
                    return;
                }
            }


        }


    }
}
