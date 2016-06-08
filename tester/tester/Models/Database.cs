using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace tester.Models
{
    public class Database
    {
        static OracleConnection m_conn;
        static OracleCommand m_command;
        static string connectionString = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS=(PROTOCOL=TCP)(HOST=fhictora01.fhict.local)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=fhictora)));User ID=dbi338530;PASSWORD=Hoi;";

        // Open de verbinding met de database
        public static bool OpenConnection()
        {
            bool returnvalue = false;
            m_conn = new OracleConnection();
            try
            {
                m_conn.ConnectionString = connectionString;
                m_conn.Open();
                // Controleer of de verbinding niet al open is
                if (m_conn.State != System.Data.ConnectionState.Open)
                { return true; }
            }
            catch (Exception ex) { Console.WriteLine("Connection failed: " + ex.Message); }
            return returnvalue;
        }

        public static void CloseConnection()
        {
            try
            { m_conn.Close(); }
            catch (Exception ex)
            { Console.WriteLine("Connection failed: " + ex.Message); }
        }

        /// Haalt het command-object op waarmee queries uitgevoerd kunnen worden.
        public static OracleCommand Command { get { return m_command; } }

        //Rechard
        public static string acnaam = "";
        public static string ac;
        public static bool Login(string username, string password)
        {
            string result = "no";
            bool ok = false;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT GebruikerID, Gebruikersnaam, Wachtwoord, Gebruikerstype FROM gebruiker WHERE Wachtwoord = :password AND Gebruikersnaam = :username";
                m_command.Parameters.Add("password", OracleDbType.Varchar2).Value = password;
                m_command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        ac = acctype;
                        result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        acnaam = result;
                        if (result == username) { ok = true; }
                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
            return ok;
        }

        public static void RegesterUser(string username, string password, string acctype, string email, string fullname, string address, string city, int phone, string gender, string rfid, string yncar, string ynlicence)
        {
            int AutoID = 0;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT COUNT(GebruikerID) from Gebruiker";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        AutoID = Convert.ToInt32(_Reader["COUNT(GebruikerID)"]) + 1;
                    }
                }
                m_command.CommandText = "INSERT INTO Gebruiker (GebruikerID, Gebruikersnaam, Wachtwoord, Naam, Geslacht, Adres, Woonplaats, Telefoonnummer, HeeftRijbewijs, HeeftAuto, Email, Gebruikerstype, Rfidcode) VALUES (:GebruikerID, :Gebruikersnaam, :Wachtwoord, :Naam, :Geslacht, :Adres, :Woonplaats, :Telefoonnummer, :rij, :auto, :Email, :Gebruikerstype, :rfid)";
                m_command.Parameters.Add("GebruikerID", OracleDbType.Int32).Value = AutoID;
                m_command.Parameters.Add("Gebruikersnaam", OracleDbType.Varchar2).Value = username;
                m_command.Parameters.Add("Wachtwoord", OracleDbType.Varchar2).Value = password;
                m_command.Parameters.Add("Naam", OracleDbType.Varchar2).Value = fullname;
                m_command.Parameters.Add("Geslacht", OracleDbType.Varchar2).Value = gender;
                m_command.Parameters.Add("Adres", OracleDbType.Varchar2).Value = address;
                m_command.Parameters.Add("Woonplaats", OracleDbType.Varchar2).Value = city;
                m_command.Parameters.Add("Telefoonnummer", OracleDbType.Int32).Value = phone;
                m_command.Parameters.Add("rij", OracleDbType.Varchar2).Value = ynlicence;
                m_command.Parameters.Add("auto", OracleDbType.Varchar2).Value = yncar;
                m_command.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;
                m_command.Parameters.Add("Gebruikerstype", OracleDbType.Varchar2).Value = acctype;
                m_command.Parameters.Add("rfid", OracleDbType.Varchar2).Value = rfid;

                m_command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
        } //goodluck! </Rechard>  


        // CHATHALEN <RECHARD>
        public static List<string> chathistory = new List<string>();
        public static string chatbox(int needy, int volunteer)
        {
            chathistory.Clear();
            string bericht = "";
            string hetzender = "";
            string chatstring = "";
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT c.Bericht, c.Zender, g.Gebruikersnaam from Chat c LEFT JOIN Gebruiker g ON c.Zender = g.GebruikerID WHERE c.GebruikerID = :needy AND c.GebruikerID2 = :volunteer ORDER BY ChatID ";
                m_command.Parameters.Add("needy", OracleDbType.Varchar2).Value = needy;
                m_command.Parameters.Add("volunteer", OracleDbType.Varchar2).Value = volunteer;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        hetzender = Convert.ToString(_Reader["Gebruikersnaam"]);
                        bericht = Convert.ToString(_Reader["Bericht"]);
                        chatstring = hetzender + ": " + bericht;
                        chathistory.Add(chatstring);
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return bericht;

        }

        // CHAT INSERTS <RECHARD>
        public static void chatsend(int needy, int volunteer, string bericht, int zender)
        {
            int AutoID = 0;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT COUNT(ChatID) from Chat";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        AutoID = Convert.ToInt32(_Reader["COUNT(ChatID)"]) + 1;
                    }
                }
                m_command.CommandText = "INSERT INTO Chat (ChatID, Zender, GebruikerID, GebruikerID2, Bericht) VALUES (:ChatID, :Zender, :GebruikerID, :GebruikerID2, :Bericht)";
                m_command.Parameters.Add("ChatID", OracleDbType.Int32).Value = AutoID;
                m_command.Parameters.Add("Zender", OracleDbType.Int32).Value = zender;
                m_command.Parameters.Add("GebruikerID", OracleDbType.Int32).Value = needy;
                m_command.Parameters.Add("GebruikerID2", OracleDbType.Int32).Value = volunteer;
                m_command.Parameters.Add("Bericht", OracleDbType.Varchar2).Value = bericht;
                m_command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
        }
    }


}