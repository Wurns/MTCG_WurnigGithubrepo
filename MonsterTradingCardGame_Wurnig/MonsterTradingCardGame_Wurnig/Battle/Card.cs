using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig.Battle
{
    public class Card
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Element { get; set; }
        public double Damage { get; set; }

        public Card() { }
        public Card(CardJSON card)
        {
            //check what kind of Card it is
            Name = card.Name ?? "";
            var count = Name.Count(char.IsUpper);

            if (count != 2)
            {
                Name = card.Name!;
                Element = "Normal";
                Type = "Monster";
            }
            else
            {
                var nameWithSpace = SplitPascalCase(Name);
                var splittedName = nameWithSpace.Split(" ");
                
                if (!Name.Contains("Spell"))
                {
                    Element = splittedName[0];
                    Name = splittedName[1];
                    Type = "Monster";
                }
                else
                {
                    Element = splittedName[0];
                    Type = "Spell";
                }
            }
            Damage = card.Damage;
        }
        
        //name splitten in element und name
        public string SplitPascalCase(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            return Regex.Replace(text, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}
