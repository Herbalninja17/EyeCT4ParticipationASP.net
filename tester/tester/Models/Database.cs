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


        // Update table IsVisible/IsReported <Raphael>
        public static bool alterYorN(string COLUMN, int ID, string IDFromWich, string visibleOrReported, string YorN)
        {
            bool ok = false;

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "UPDATE " + COLUMN + " SET " + visibleOrReported + " = '" + YorN + "' WHERE " + IDFromWich + "=" + ID;
                //Command.Parameters.Add("Y", OracleDbType.Varchar2).Value = YorN;
                //Command.Parameters.Add("IDFromWich", OracleDbType.Varchar2).Value = IDFromWich;
                //Command.Parameters.Add("1", OracleDbType.Int32).Value = Convert.ToString(ID);
                //Command.Parameters.Add("COLUMN", OracleDbType.Varchar2).Value = COLUMN;
                m_command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
            return ok;
        }

        // GetReviews admin <Raphael>
        public static List<string> reviewsListAdmin = new List<string>();
        public static bool getReviewAdmin()
        {
            bool ok = false;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT * FROM REVIEW";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        //string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        //ac = acctype;
                        //int accID = Convert.ToInt32(_Reader["GebruikerID"]);
                        //acID = accID;
                        //result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        //if (result == username) { ok = true; }
                        reviewsListAdmin.Add(Convert.ToString(_Reader["OPMERKINGEN"]));

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

        // GetChat admin <Raphael>
        public static List<string> chats = new List<string>();
        public static bool getChat(long UserID1, long UserID2)
        {
            bool ok = false;

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT * FROM CHAT";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        //string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        //ac = acctype;
                        //int accID = Convert.ToInt32(_Reader["GebruikerID"]);
                        //acID = accID;
                        //result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        //if (result == username) { ok = true; }
                        chats.Add(Convert.ToString(_Reader["BERICHT"]));

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

        // GetReported reviews admin <Raphael>
        public static List<string> reportedReviews = new List<string>();
        public static bool getReportedReviews(string query)
        {
            bool ok = false;

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = query;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        ////string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        ////ac = acctype;
                        ////int accID = Convert.ToInt32(_Reader["GebruikerID"]);
                        ////acID = accID;
                        ////result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        ////if (result == username) { ok = true; }
                        //if (query == "SELECT OPMERKINGEN FROM REVIEW WHERE ISREPORTED = 'N'")
                        //{
                        //reported.Add(Convert.ToString(_Reader["OPMERKINGEN"]));
                        //}
                        //if (query == "SELECT BERICHT FROM CHAT WHERE ISREPORTED = 'N'")
                        //{
                        //reported.Add(Convert.ToString(_Reader["BERICHT"]));
                        //}
                        //if (query == "SELECT OMSCHRIJVING FROM HULPVRAAG WHERE ISREPORTED = 'N'")
                        //{
                        reportedReviews.Add(Convert.ToString(_Reader["OPMERKINGEN"]));
                        //}
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

        // GetreportedChat admin <Raphael>
        public static List<string> reportedChats = new List<string>();
        public static bool getreportedChat()
        {
            bool ok = false;

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT BERICHT FROM CHAT WHERE ISREPORTED = 'Y'";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        //string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        //ac = acctype;
                        //int accID = Convert.ToInt32(_Reader["GebruikerID"]);
                        //acID = accID;
                        //result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        //if (result == username) { ok = true; }
                        reportedChats.Add(Convert.ToString(_Reader["BERICHT"]));

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

        // GetReported requests admin <Raphael>
        public static List<string> reportedRequests = new List<string>();
        public static bool getReportedRequests(string query)
        {
            bool ok = false;

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = query;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        ////string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        ////ac = acctype;
                        ////int accID = Convert.ToInt32(_Reader["GebruikerID"]);
                        ////acID = accID;
                        ////result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        ////if (result == username) { ok = true; }
                        //if (query == "SELECT OPMERKINGEN FROM REVIEW WHERE ISREPORTED = 'N'")
                        //{
                        //reported.Add(Convert.ToString(_Reader["OPMERKINGEN"]));
                        //}
                        //if (query == "SELECT BERICHT FROM CHAT WHERE ISREPORTED = 'N'")
                        //{
                        //reported.Add(Convert.ToString(_Reader["BERICHT"]));
                        //}
                        //if (query == "SELECT OMSCHRIJVING FROM HULPVRAAG WHERE ISREPORTED = 'N'")
                        //{
                        reportedRequests.Add(Convert.ToString(_Reader["OMSCHRIJVING"]));
                        //}
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

        // GetRequests admin <Raphael>
        public static List<string> reviewsRequests = new List<string>();
        public static bool getRequests()
        {
            bool ok = false;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT * FROM HULPVRAAG";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        //string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        //ac = acctype;
                        //int accID = Convert.ToInt32(_Reader["GebruikerID"]);
                        //acID = accID;
                        //result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        //if (result == username) { ok = true; }
                        reviewsRequests.Add(Convert.ToString(_Reader["OMSCHRIJVING"]));

                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }


            //    try
            //    {
            //        OpenConnection();
            //        m_command = new OracleCommand();
            //        m_command.Connection = m_conn;
            //        m_command.CommandText = "SELECT * FROM REVIEW";
            //        m_command.ExecuteNonQuery();
            //        using (OracleDataReader _Reader = Database.Command.ExecuteReader())
            //        {
            //            while (_Reader.Read())
            //            {
            //                //string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
            //                //ac = acctype;
            //                //int accID = Convert.ToInt32(_Reader["GebruikerID"]);
            //                //acID = accID;
            //                //result = Convert.ToString(_Reader["Gebruikersnaam"]);
            //                //if (result == username) { ok = true; }
            //                reviewsRequests.Add(Convert.ToString(_Reader["OPMERKINGEN"]));

            //            }
            //        }
            //    }
            //    catch (OracleException ex)
            //    {
            //        Database.CloseConnection();
            //        Console.WriteLine(ex.Message);
            //    }
            return ok;

        }
    }
}