namespace MonsterTradingCardGame_Wurnig
{
    public interface IRegistration
    {
        bool DoEdit(string token, string name, string bio, string image);
        bool doRegister(string username, string password);
    }
}