using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Stylist
    {
        private int _id;
        private string _client;

        public Stylist(string client, int Id = 0)
        {
            _id = Id;
            _client = client;
        }

        // Getters

        public int GetId()
        {
            return _id;
        }
        public string GetClient()
        {
            return _client;
        }
        // Setters
        public void SetId(int setId)
        {
            _id = setId;
        }
        public void SetClient(string setClient)
        {
            _client = setClient;
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
             string client = rdr.GetString(1);
             Stylist newStylist = new Stylist(client, stylistId);
             allStylist.Add(newStylist);
           }
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
           return allStylist;
        }

    }
}
