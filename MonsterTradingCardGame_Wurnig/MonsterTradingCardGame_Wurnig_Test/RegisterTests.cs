using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterTradingCardGame_Wurnig;

namespace MonsterTradingCardGame_Wurnig_Test
{
    internal class RegisterTests
    {
        private Mock<IRegistration> mock;
        [SetUp]
        public void Setup() 
        {
            //erstellt ein fake Registration object, gibt true zurück bei doRegister
            mock = new Mock<IRegistration>();
            mock.Setup(x => x.doRegister(It.IsAny<string>(),It.IsAny<string>())).Returns(true);
        }

    }
}
