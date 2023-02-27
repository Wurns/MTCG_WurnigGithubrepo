using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterTradingCardGame_Wurnig;

namespace MonsterTradingCardGame_Wurnig_Test
{
    internal class HandlerTests
    {
        private Handler handler = new Handler(new HttpMessage(), new List<MonsterTradingCardGame_Wurnig.Battle.BattleDeck>());

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void AquireCardsWithNoMoney()
        {
            var user = new User();
            user.Coins = 0;
            var result = handler.AcquirePackages(user);

            Assert.That(result, Is.EqualTo("keine Kohle"));
        }
    }
}
