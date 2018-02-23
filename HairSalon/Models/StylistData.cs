using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _clients;

        public Stylist(string clients, int Id = 0)
        {
            _id = Id;
            _clients = clients;
        }

        // Getters

        public int GetId()
        {
            return _id;
        }
        public string GetClients()
        {
            return _clients;
        }
        // Setters
        public void SetId(int setId)
        {
            _id = setId;
        }
        public void SetClients(string setClients)
        {
            _clients = setClients;
        }

        public static List<Stylist> GetAll()
        {
           List<Stylist> allStylist = new List<Stylist> {};

           MySqlConnection conn = DB.Connection();
           conn.Open();

           MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"SELECT * FROM stylist;";

           MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
           while(rdr.Read())
           {
             int stylistId = rdr.GetInt32(0);
             string clients = rdr.GetString(1);
             Stylist newStylist = new Stylist(clients, stylistId);
             allStylist.Add(newStylist);
           }
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return allStylist;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylist (clients) VALUES (@clients);";

            MySqlParameter clients = new MySqlParameter();
            clients.ParameterName = "@clients";
            clients.Value = this._clients;
            cmd.Parameters.Add(clients);

            // Code to declare, set, and add values to a categoryId SQL parameters has also been removed.

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

        }
        // public void StylistSave()
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"CREATE TABLE INTO victor_felix (clients VARCHAR (255))";
        //
        //     MySqlParameter clients = new MySqlParameter();
        //     clients.ParameterName = "@clients";
        //     clients.Value = this._clients;
        //     cmd.Parameters.Add(clients);
        //
        //     // Code to declare, set, and add values to a categoryId SQL parameters has also been removed.
        //
        //     cmd.ExecuteNonQuery();
        //     _id = (int) cmd.LastInsertedId;
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
    }
}
