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
        public static bool Login(string username, string password)
        {
            string result = "no";
            bool ok = false;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT GebruikerID, Gebruikersnaam, Wachtwoord FROM gebruiker WHERE Wachtwoord = :password AND Gebruikersnaam = :username";
                m_command.Parameters.Add("password", OracleDbType.Varchar2).Value = password;
                m_command.Parameters.Add("username", OracleDbType.Varchar2).Value = username;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
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
    }
}