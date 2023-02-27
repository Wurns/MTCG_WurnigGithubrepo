using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Npgsql; 
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

namespace MonsterTradingCardGame_Wurnig
{
    internal class Registration : IRegistration
    {
        private readonly string _cs =
            "Host=localhost;Username=postgres;Password=1123456789absdefgh4562;Database=MTCGWurnig";

        public Registration()
        {

        }

        public bool doRegister(string username, string password)
        {
            //------------------Connect to Database------------------------------
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            //daten in die datenbank eintragen
            var sql = "INSERT INTO users(username, password, token) VALUES(@username, @password, @token)"; //@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            cmd.Parameters.AddWithValue("token", username + "-mtcgToken");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("registration failed !");
                return false;
            }

            Console.WriteLine($"user {username} was succesfully registered!");
            return true;
        }

        public bool DoEdit(string token, string name, string bio, string image)
        {
            //------------------Connect to Database------------------------------
            using var con = new NpgsqlConnection(_cs);
            con.Open();
            //daten in die datenbank eintragen
            var sql = "UPDATE users SET name = @name, bio = @bio, image = @image WHERE token = @token"; //@ injection safe
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("bio", bio);
            cmd.Parameters.AddWithValue("image", image);
            cmd.Parameters.AddWithValue("token", token);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("update failed !");
                return false;
            }

            Console.WriteLine("user was succesfully updated!");
            return true;
        }

    }
}
