using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Stylist
    {
        private string _name;
        private int _id;

        public Stylist(string name, int id = 0)
        {
            _name = name;
            _id = id;
        }

        public override int GetHashCode()
        {
          return this.GetId().GetHashCode();
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public override bool Equals(System.Object otherStylist)
        {
            if (!(otherStylist is Stylist))
            {
                return false;
            }
            else
            {
                Stylist newStylist = (Stylist) otherStylist;
                return this.GetId().Equals(newStylist.GetId());
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Client> GetClients()
        {
            List<Client> allMyClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._id;
            cmd.Parameters.Add(stylistId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientStylistId = rdr.GetInt32(2);
                Client newClient = new Client(clientName, clientStylistId, clientId);

                allMyClients.Add(newClient);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allMyClients;

   }

        public static List<Stylist> GetAll()
        {
             List<Stylist> allStylists = new List<Stylist> {};
             MySqlConnection conn = DB.Connection();

             conn.Open();
             var cmd = conn.CreateCommand() as MySqlCommand;
             cmd.CommandText = @"SELECT * FROM stylists;";
             var rdr = cmd.ExecuteReader() as MySqlDataReader;
             while(rdr.Read())
             {
               int StylistId = rdr.GetInt32(0);
               string StylistName = rdr.GetString(1);
               Stylist newStylist = new Stylist(StylistName, StylistId);
               allStylists.Add(newStylist);
             }
             conn.Close();
             if (conn != null)
             {
               conn.Dispose();
             }
             return allStylists;
        }


        public static Stylist Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM stylists WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            int stylistId = 0;
            string stylistName = "";

            while (rdr.Read())
            {
                stylistId = rdr.GetInt32(0);
                stylistName = rdr.GetString(1);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            Stylist myStylist = new Stylist(stylistName, id);
            return myStylist;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@thisId";
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
            cmd.CommandText = @"DELETE FROM stylists;";
            cmd.ExecuteNonQuery();

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Specialty> GetSpecialties ()
        {
            MySqlConnection conn = DB.Connection ();
            conn.Open ();
            MySqlCommand cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"SELECT specialties.* FROM stylists
            JOIN stylists_specialties ON (stylists.id = stylists_specialties.stylist_id)
            JOIN specialties ON (stylists_specialties.specialty_id = specialties.id)
            WHERE stylists.id = @StylistId;";

            MySqlParameter stylistIdParameter = new MySqlParameter ();
            stylistIdParameter.ParameterName = "@StylistId";
            stylistIdParameter.Value = _id;
            cmd.Parameters.Add (stylistIdParameter);

            MySqlDataReader rdr = cmd.ExecuteReader () as MySqlDataReader;
            List<Specialty> specialties = new List<Specialty> { };

            while (rdr.Read ())
        {
            int specialtyId = rdr.GetInt32 (0);
            string specialtyDescription = rdr.GetString (1);
            Specialty newSpecialty = new Specialty (specialtyDescription, specialtyId);
            specialties.Add (newSpecialty);
        }
            conn.Close ();
            if (conn != null) {
            conn.Dispose ();
        }
            return specialties;
        }

        public void AddSpecialty (Specialty newSpecialty)
        {
          MySqlConnection conn = DB.Connection ();
          conn.Open ();
          var cmd = conn.CreateCommand () as MySqlCommand;
          cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

          MySqlParameter stylist_id = new MySqlParameter ();
          stylist_id.ParameterName = "@StylistId";
          stylist_id.Value = _id;
          cmd.Parameters.Add (stylist_id);

          MySqlParameter specialty_id = new MySqlParameter ();
          specialty_id.ParameterName = "@SpecialtyId";
          specialty_id.Value = newSpecialty.GetId ();
          cmd.Parameters.Add (specialty_id);

          cmd.ExecuteNonQuery ();
          conn.Close ();
          if (conn != null) {
            conn.Dispose ();
          }
        }
        public void Edit(string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @id;";

            MySqlParameter name = new MySqlParameter("@newName", newName);
            MySqlParameter id = new MySqlParameter("@id", _id);
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(id);

            cmd.ExecuteNonQuery();
            _name = newName;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
