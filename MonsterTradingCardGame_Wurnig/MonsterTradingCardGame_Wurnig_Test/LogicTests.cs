using MonsterTradingCardGame_Wurnig;
using MonsterTradingCardGame_Wurnig.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterTradingCardGame_Wurnig_Test
{
    internal class LogicTests
    {


        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void KnightVSWaterSpell_WaterSpellWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Knight";
            card1.Damage = 20;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "WaterSpell";
            card2.Damage = 20;
            card2.Element = "Water";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck2.Owner);

        }
        [Test]
        public void GoblinVSDragon_DragonWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Dragon";
            card1.Damage = 30;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "Goblin";
            card2.Damage = 20;
            card2.Element = "Water";
            card2.Type = "Monster";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);

        }
        [Test]
        public void WizzardVSOrk_WizzardWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Wizzard";
            card1.Damage = 20;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "Ork";
            card2.Damage = 20;
            card2.Element = "Normal";
            card2.Type = "Monster";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void KrakenVSFireSpell_KrakenWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Kraken";
            card1.Damage = 20;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "FireSpell";
            card2.Damage = 20;
            card2.Element = "Fire";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void KrakenVSWaterSpell_KrakenWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Kraken";
            card1.Damage = 20;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "WaterSpell";
            card2.Damage = 20;
            card2.Element = "Water";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void KrakenVSRegularSpell_KrakenWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Kraken";
            card1.Damage = 20;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "RegularSpell";
            card2.Damage = 20;
            card2.Element = "Normal";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void FireElfVSDragon_FireElfWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Elf";
            card1.Damage = 20;
            card1.Element = "Fire";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "Dragon";
            card2.Damage = 20;
            card2.Element = "Normal";
            card2.Type = "Monster";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void FireIsEffectiveOnNormal_FireWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Ork";
            card1.Damage = 20;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "FireSpell";
            card2.Damage = 10;
            card2.Element = "Fire";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck2.Owner);
        }
        [Test]
        public void FireIsEffectiveOnNormal_NormalWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Ork";
            card1.Damage = 50;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "FireSpell";
            card2.Damage = 10;
            card2.Element = "Fire";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void FireIsEffectiveOnNormal_Draw()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Ork";
            card1.Damage = 40;
            card1.Element = "Normal";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "FireSpell";
            card2.Damage = 10;
            card2.Element = "Fire";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == "Draw");
        }
        [Test]
        public void WaterIsEffectiveOnFire_WaterWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "FireElf";
            card1.Damage = 20;
            card1.Element = "Fire";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "WaterSpell";
            card2.Damage = 10;
            card2.Element = "Water";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck2.Owner);
        }
        [Test]
        public void WaterIsEffectiveOnFire_FireWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "FireElf";
            card1.Damage = 50;
            card1.Element = "Fire";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "WaterSpell";
            card2.Damage = 10;
            card2.Element = "Water";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void WaterIsEffectiveOnFire_Draw()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "FireElf";
            card1.Damage = 40;
            card1.Element = "Fire";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "WaterSpell";
            card2.Damage = 10;
            card2.Element = "Water";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == "Draw");
        }
        [Test]
        public void NormalIsEffectiveOnWater_NormalWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Goblin";
            card1.Damage = 20;
            card1.Element = "Water";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "RegularSpell";
            card2.Damage = 10;
            card2.Element = "Normal";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck2.Owner);
        }
        [Test]
        public void NormalIsEffectiveOnWater_WaterWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Goblin";
            card1.Damage = 50;
            card1.Element = "Water";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "RegularSpell";
            card2.Damage = 10;
            card2.Element = "Normal";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        [Test]
        public void NormalIsEffectiveOnWater_Draw()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Goblin";
            card1.Damage = 40;
            card1.Element = "Water";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "RegularSpell";
            card2.Damage = 10;
            card2.Element = "Normal";
            card2.Type = "Spell";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == "Draw");
        }
        [Test]
        public void WaterMonsterVSFireMonster_Draw()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Goblin";
            card1.Damage = 20;
            card1.Element = "Water";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "Goblin";
            card2.Damage = 20;
            card2.Element = "Fire";
            card2.Type = "Monster";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == "Draw");
        }
        [Test]
        public void WaterMonsterVSFireMonster_FireMonsterWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Goblin";
            card1.Damage = 20;
            card1.Element = "Water";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "Goblin";
            card2.Damage = 21;
            card2.Element = "Fire";
            card2.Type = "Monster";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck2.Owner);
        }
        [Test]
        public void WaterMonsterVSFireMonster_WaterMonsterWins()
        {
            //Arrange
            Card card1 = new();
            card1.Name = "Goblin";
            card1.Damage = 20;
            card1.Element = "Water";
            card1.Type = "Monster";

            Card card2 = new();
            card2.Name = "FireElf";
            card2.Damage = 10;
            card2.Element = "Fire";
            card2.Type = "Monster";

            BattleDeck deck1 = new BattleDeck()
            {
                Cards = new List<Card>() { card1 },
                Owner = "Player 1"
            };
            BattleDeck deck2 = new BattleDeck()
            {
                Cards = new List<Card> { card2 },
                Owner = "Player 2"
            };

            var logic = new Logic(deck1, deck2);

            //act
            logic.Battle();

            //Asset
            Assert.That(logic.Winner == deck1.Owner);
        }
        
    }
}

