using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _name;
        private string _stylist;
        private int _id;

        public Client(string Name, string Stylist, int Id =0)
        {
            _name = Name;
            _stylist = Stylist;
            _id = Id;

        }

        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetStylist()
        {
            return _stylist;
        }

        public void SetId(int setId)
        {
            _id = setId;
        }
        public void SetName(string setName)
        {
            _name = setName;
        }
        public void SetStylist(string setStylist)
        {
            _stylist = setStylist;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO client (clientname, stylistid) VALUES (@clientname, @stylistid);";

            MySqlParameter clientname = new MySqlParameter();
            clientname.ParameterName = "@clientname";
            clientname.Value = _name;
            cmd.Parameters.Add(clientname);

            MySqlParameter stylistid = new MySqlParameter();
            stylistid.ParameterName = "@stylistid";
            stylistid.Value = _stylist;
            cmd.Parameters.Add(stylistid);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClient = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT clientname, stylistname FROM stylist INNER JOIN client ON stylist.stylistname = client.stylistid;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              string clientName = rdr.GetString(1);
              string stylistName = rdr.GetString(2);
              int clientId = rdr.GetInt32(0);

              Client newClient = new Client(clientName, stylistName, clientId);
              allClient.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClient;
        }

        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM client WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            string stylistName ="";

            while(rdr.Read())
            {
              clientId = rdr.GetInt32(0);
              clientName = rdr.GetString(1);
              stylistName = rdr.GetString(2);
            }

            Client newClient = new Client(clientName, stylistName, clientId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return newClient;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM client WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM client;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
