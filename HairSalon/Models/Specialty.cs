using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
    public class Specialty
    {
        private string _specialtyName;
        private int _id;
        private int _stylistId;

        public Specialty (string specialtyName, int id = 0)
        {

            _id = id;
            _specialtyName = specialtyName;
        }

        public int GetStylistId ()
        {
            return this._stylistId;
        }
        public void SetStylistId (int stylistId)
        {
            this._stylistId = stylistId;
        }

        public string GetSpecialtyName ()
        {
            return _specialtyName;
        }

        public int GetId ()
        {
            return _id;
        }

        public override bool Equals (System.Object otherSpecialty)
        {
            if (!(otherSpecialty is Specialty))
            {
                return false;
            } else {
                Specialty newSpecialty = (Specialty) otherSpecialty;
                return this.GetId ().Equals (newSpecialty.GetId ());
            }
        }

        public override int GetHashCode ()
        {
            return this.GetId ().GetHashCode ();
        }


        public static List<Specialty> GetAll()
        {
            List<Specialty> allSpecialtys = new List<Specialty> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read()) {
            int specialtyId = rdr.GetInt32 (0);
            string specialtyName = rdr.GetString (1);
            Specialty newSpecialty = new Specialty (specialtyName, specialtyId);
            allSpecialtys.Add (newSpecialty);

            }
            conn.Close();
            if (conn != null) {
                conn.Dispose();
            }
            return allSpecialtys;
        }

        public static Specialty Find (int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add (searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int specialtyId = 0;
            string specialtyName = "";

            while (rdr.Read())
            {
                specialtyId = rdr.GetInt32(0);
                specialtyName = rdr.GetString(1);
            }

            Specialty newSpecialty = new Specialty (specialtyName, specialtyId);
            conn.Close();
            if (conn != null) {
                conn.Dispose();
            }

            return newSpecialty;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO specialties (specialty_name) VALUES (@specialty_name);";

            MySqlParameter specialtyName = new MySqlParameter();
            specialtyName.ParameterName = "@specialty_name";
            specialtyName.Value = this._specialtyName;
            cmd.Parameters.Add (specialtyName);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddStylist(Stylist newStylist)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand () as MySqlCommand;
            cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

            MySqlParameter stylist_id = new MySqlParameter();
            stylist_id.ParameterName = "@StylistId";
            stylist_id.Value = newStylist.GetId();
            cmd.Parameters.Add (stylist_id);

            MySqlParameter specialty_id = new MySqlParameter();
            specialty_id.ParameterName = "@SpecialtyId";
            specialty_id.Value = _id;
            cmd.Parameters.Add (specialty_id);

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
            cmd.CommandText = @"DELETE FROM specialties;";
            cmd.ExecuteNonQuery();

            conn.Close();

            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
