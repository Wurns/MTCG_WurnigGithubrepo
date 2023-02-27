// See https://aka.ms/new-console-template for more information

using System.Dynamic;using System.Net.Security;
using System.Net.Sockets;
using MonsterTradingCardGame_Wurnig;

Console.WriteLine("Welcome to the Monster Trading Card Game!");
var listener = new MyTcpListener();
//listener.TruncateAll(); //hier kann man wählen ob man persistierende DB haben will oder nicht.
listener.Server();
