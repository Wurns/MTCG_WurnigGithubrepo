using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using Npgsql;
using MonsterTradingCardGame_Wurnig.Battle;
using System.Collections.Concurrent;
using Npgsql.Replication.PgOutput.Messages;

namespace MonsterTradingCardGame_Wurnig
{
    class MyTcpListener
    {
        public List<BattleDeck> Lobby { get; set; }

        public List<string> Winners { get; set; }
        
        public MyTcpListener() 
        { 
            Winners = new List<string>();
            Lobby = new List<BattleDeck>();
        }
        public void Server()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 10001.
                Int32 port = 10001;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    Thread t = new Thread(() => HandleClient(client));
                    t.Start();
                }
                
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        public void TruncateAll()
        {
            string _cs = "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";
            string[] tables = { "cards", "packages", "stack", "deck", "users" };
            foreach (var table in tables)
            {
                using var con = new NpgsqlConnection(_cs);
                con.Open();
                var sql = $"TRUNCATE table {table}";
                using var cmd = new NpgsqlCommand(sql, con);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Console.WriteLine("creation failed!");
                }
            }
        }

        public HttpMessage ParseRequest(byte[] bytes)
        {
            //bytes werden in string umgewandelt
            string data = Encoding.UTF8.GetString(bytes);

            var splitter = data.Split(Environment.NewLine + Environment.NewLine);
            var message = new HttpMessage(); //hier stehen Username und PW zb
            message.Header = splitter[0];
            message.Body = splitter[1];
            var datalines = message.Header.Split(Environment.NewLine);
            var datalineslines = datalines[0].Split(" ");
            message.Method = datalineslines[0];
            message.Path = datalineslines[1];
            message.AuthToken = "";
            foreach (var line in datalines)
            {
                if (line.StartsWith("Authorization: "))
                {
                    message.AuthToken = line.Split(" ")[2];
                }
            }

            return message;
        }
        //read/receive the message
        public byte[] Receive(TcpClient client)
        {
            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();
            // Buffer for reading data
            var bytes = new byte[client.ReceiveBufferSize];
            //reads all bytes in the message
            stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }
        //handle the 
        public void SendSuccess(TcpClient client,string content)
        {
            //baut den string
            var message = new StringBuilder();
            message.Append("HTTP/1.1 200 OK");
            message.Append(Environment.NewLine);
            message.Append("Content-Length: ");
            message.Append(content.Length); //content länge berechnen
            message.Append(Environment.NewLine);
            message.Append("Content-Type: text/plain");
            message.Append(Environment.NewLine);
            message.Append(Environment.NewLine);
            message.Append(content);
            message.Append(Environment.NewLine);
            message.Append(Environment.NewLine);

            //schickt das dann an den client
            var stream = client.GetStream();
            var bytes = Encoding.UTF8.GetBytes(message.ToString());
            stream.Write(bytes,0,bytes.Length);
        }

        public void SendFailure(TcpClient client, string content)
        {
            //build the message 
            var message = new StringBuilder();
            message.Append("HTTP/1.1 400 Error");
            message.Append(Environment.NewLine);
            message.Append("Content-Length: ");
            message.Append(content.Length); //content länge berechnen
            message.Append(Environment.NewLine);
            message.Append("Content-Type: text/plain");
            message.Append(Environment.NewLine);
            message.Append(Environment.NewLine);
            message.Append(content);
            message.Append(Environment.NewLine);
            message.Append(Environment.NewLine);

            var stream = client.GetStream();
            var bytes = Encoding.UTF8.GetBytes(message.ToString());
            stream.Write(bytes, 0, bytes.Length);
        }
        public void Beep(string winner, string player1, string player2) //unique feature 
        {  
            if(winner == player1)
            {
                Console.Beep(659, 125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(659, 125);
                Thread.Sleep(167);
                Console.Beep(523, 125);
                Console.Beep(659, 125);
                Thread.Sleep(125);
                Console.Beep(784, 125);
                Thread.Sleep(375);
                Console.Beep(392, 125);
            }
            else if(winner == player2)
            {
                Console.Beep(1320, 500);
                Console.Beep(990, 250);
                Console.Beep(1056, 250);
                Console.Beep(1188, 250);
                Console.Beep(1320, 125);
                Console.Beep(1188, 125);
                Console.Beep(1056, 250);
                Console.Beep(990, 250);
                Console.Beep(880, 500);
                Console.Beep(880, 250);
                Console.Beep(1056, 250);
                Console.Beep(1320, 500);
                Console.Beep(1188, 250);
                Console.Beep(1056, 250);
                Console.Beep(990, 750);
                Console.Beep(1056, 250);
                Console.Beep(1188, 500);
                Console.Beep(1320, 500);
                Console.Beep(1056, 500);
                Console.Beep(880, 500);
                Console.Beep(880, 500);
            }
            else
            {
                Console.Beep(200, 600);
                Console.Beep(150, 600);
                Console.Beep(100, 1200);
            }

        }
        
        public void HandleClient(TcpClient client)
        {
            var bytes = Receive(client);

            var message = ParseRequest(bytes);

            //-----------------HANDLER------------------
            var handler = new Handler(message, Lobby);

            //1-----------------Registration-------------
            if (message.Path == "/users")
            {
                if (handler.Register())
                {
                    SendSuccess(client, "Successfully registered!");
                }
                else
                {
                    SendFailure(client, "Failed to register!");
                }
            }
            //14-----------------editUserData-----------
            else if (message.Path.StartsWith("/users/"))
            {
                if (message.Method == "PUT")
                {
                    if (handler.EditUserData())
                    {
                        SendSuccess(client, "Sucesfullly edited!");
                    }
                    else
                    {
                        SendFailure(client, "Failed to edit!");
                    }
                }
                else if(message.Method == "GET")
                {
                    SendSuccess(client, handler.ShowUserData());
                }
                else
                {
                    SendFailure(client, "Failed to edit!");
                }                               
            }
            //2------------------Login------------------
            else if (message.Path == "/sessions")
            {
                var token = handler.Login();
                if (token == "")
                {
                    SendFailure(client,"Login failed!");
                }
                else
                {
                    SendSuccess(client, "Login successful\n" + token);
                }
                
            }
            //3,6-----------------create packages---------
            else if (message.Path == "/packages")
            {
                if (message.AuthToken != "admin-mtcgToken")
                {
                    Console.WriteLine("Package creation failed (You are not an admin!)");
                    SendFailure(client,"Package creation problem!");
                }
                //create packages
                handler.CreatePackages();
                SendSuccess(client, "Package created!");

            }
            //4,5,7---------------acquire Packages--------------
            else if (message.Path == "/transactions/packages")
            {
                //authorization kriegt einen token und returned user und coins
                var authorization = new Authorization();
                var user = authorization.Authorize(message.AuthToken);
                if (user != null)
                {
                    handler.AcquirePackages(user);
                    SendSuccess(client,"Package acquired!");
                }
                else
                {
                    Console.WriteLine("An error occured!");
                    SendFailure(client, "Acquire cards problem!");
                }
            }
            //8,9 show all aquired cards
            else if (message.Path == "/cards")
            {
                //show cards
                if (message.AuthToken != "")
                {
                    var cards = handler.ShowCards();
                    SendSuccess(client, cards);
                }
                SendFailure(client,"show cards problem!");

            }
            //11--------------configure deck-------------------------
            else if (message.Path == "/deck")
            {
                if (message.Method == "PUT")
                {
                    if (handler.ConfigureDecks())
                    {
                        SendSuccess(client, "Sucesfullly configured deck!");
                    }
                    else
                    {
                        SendFailure(client, "FAILED to configure deck!");
                    }
                }
                else if (message.Method == "GET")//show deck
                {
                    SendSuccess(client, handler.ShowDeck());
                }
                else
                {
                    SendFailure(client, "Failed to edit!");
                }
            }
            else if (message.Path == "/battles")
            {
                var deck = new BattleDeck();
                Console.WriteLine(message.AuthToken.Split("-")[0]);
                deck.FetchCards(message.AuthToken.Split("-")[0]);
                Lobby.Add(deck);
                 
                if(Lobby.Count >= 2) //2. player füllt lobby auf 2 damit ein battle startet
                {
                    lock (Lobby)
                    {
                        var deck1 = new BattleDeck();
                        var deck2 = new BattleDeck();
                                            
                        deck1 = Lobby[0];
                        deck2 = Lobby[1];
                        

                        var battle = new Logic(deck1, deck2);
                        battle.Battle();
                        Winners.Add(battle.Winner);
                        Lobby.Remove(deck1);
                        Lobby.Remove(deck2);
                        //hier beepen
                        Beep(battle.Winner, deck1.Owner, deck2.Owner);
                        SendSuccess(client, battle.Winner);
                    }
                }
                else
                {
                   while(Lobby.Count != 0)//wartet auf Battle outcome
                   {

                   }
                   SendSuccess(client, Winners[0]);
                   
                }
                Console.WriteLine("Hat geklappt");
            }
        }

    }
}
   
