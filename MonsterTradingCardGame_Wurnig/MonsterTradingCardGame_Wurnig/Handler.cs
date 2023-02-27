using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MonsterTradingCardGame_Wurnig.Battle;
using System.Collections.Concurrent;

namespace MonsterTradingCardGame_Wurnig
{
    public class Handler
    {
        public HttpMessage Message{ get; set; }

        public List<BattleDeck> Lobby { get; set; }
        public Handler(HttpMessage msg, List<BattleDeck> lobby)
        {
            Message = msg;
            Lobby = lobby;
        }

        public bool Register()
        {
            var user = JsonConvert.DeserializeObject<User>(Message.Body);//haut username und pw in die klasse user 
            var registration = new Registration();
            return registration.doRegister(user.Username, user.Password);
        }

        public bool EditUserData()
        {
            var user = JsonConvert.DeserializeObject<User>(Message.Body);//haut den Body in die klasse user
            var edit = new Registration();
            var checkedToken = Message.Path.Split('/')[2];
            if(Message.AuthToken != checkedToken + "-mtcgToken")
            {
                return false;
            }
            
            return edit.DoEdit(Message.AuthToken, user.Name, user.Bio, user.Image);
        }

        public string ShowUserData()
        {
            var user = new User();
            var checkedToken = Message.Path.Split('/')[2];
            if (Message.AuthToken != checkedToken + "-mtcgToken")
            {
                return "failed to show UserData";
            }
                return user.ShowData(Message.AuthToken);
        }

        public string Login()
        {
            var user = JsonConvert.DeserializeObject<User>(Message.Body);
            var login = new Login();
            return login.Logmein(user.Username, user.Password);
        }

        public void CreatePackages()//package erstellen
        {
            //databasecard
            var jsoncardlist = JsonConvert.DeserializeObject<List<CardJSON>>(Message.Body);//JSONCardList bekommt ihre Werte zugewiesen
            var package = new Package();
            package.CreatePackage(jsoncardlist);

            foreach (var jsoncard in jsoncardlist!)
            {
                jsoncard.GenerateDatabaseCard();
            }
        }

        public string? AcquirePackages(User user)//package kaufen
        {
            var package = new Package();
            if (user.Coins >= 5)
            {
                
                package.AcquirePackage(user);
                return package.ToString();

            }

            Console.WriteLine("keine Kohle");
            return "keine Kohle";
            
        }

        public string ShowCards()//karten anzeigen
        {
           var cards = new CardJSON();
            return cards.ShowJsonCards(Message.AuthToken);
        }

        public bool ConfigureDecks()//deck mit 4 karten erstellen
        {
            var jsoncardlist = JsonConvert.DeserializeObject<List<string>>(Message.Body);//haut den Body in die klasse user

            if (jsoncardlist.Count == 4)
            {
                var player = Message.AuthToken.Split('-')[0];
                var deck = new JSONDeck();

                foreach (var card in jsoncardlist)
                {
                    deck.AddToDeck(card, player);
                }
                return true;
            }
            return false;            
            
        }

        internal string ShowDeck()
        {
            StringBuilder sb = new StringBuilder();
            var deck = new JSONDeck();
            var player = Message.AuthToken.Split('-')[0];
            
            return deck.ShowJsonDeck(player);
        }
    }
}
