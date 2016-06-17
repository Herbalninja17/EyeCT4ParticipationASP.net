namespace tester.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using Oracle.ManagedDataAccess.Client;

    public class Database
    {
        private static OracleConnection m_conn;
        private static OracleCommand m_command;
        private static string connectionString = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS=(PROTOCOL=TCP)(HOST=fhictora01.fhict.local)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=fhictora)));User ID=dbi338530;PASSWORD=Hoi;";

        public static string acnaam { get; set; }
        public static string ac { get; set; }
        public static int acid { get; set; }
        public static int ItemIDSelected { get; set; }
        public static User user { get; set; }
        public static User userBekijken { get; set; }

        public static bool online { get; set; }

        public static List<Review> reviewsProfile = new List<Review>();
        public static List<string> chathistory = new List<string>();
        public static List<string> reviewsListAdmin = new List<string>();
        public static List<string> chats = new List<string>();
        public static List<string> reportedReviews = new List<string>();
        public static List<string> reportedChats = new List<string>();
        public static List<string> reportedRequests = new List<string>();
        public static List<string> reviewsRequests = new List<string>();
        public static List<ChatUser> chatUser = new List<ChatUser>();

        /// Haalt het command-object op waarmee queries uitgevoerd kunnen worden.
        public static OracleCommand Command { get { return m_command; } }

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
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
            }
            return returnvalue;
        }

        public static void CloseConnection()
        {
            try
            { m_conn.Close(); }
            catch (Exception ex)
            { Console.WriteLine("Connection failed: " + ex.Message); }
        }

        //Rechard
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
                        acid = Convert.ToInt32(_Reader["GebruikerID"]);
                        if (result == username) { ok = true; }
                        Online online = new Online();
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

        // Register
        public static void RegesterUser(string username, string password, string acctype, string email, string fullname, string address, string city, int phone, string gender, string rfid, string yncar, string ynlicence, string aboutMe)
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
                m_command.CommandText = "INSERT INTO Gebruiker (GebruikerID, Gebruikersnaam, Wachtwoord, Naam, Geslacht, Adres, Woonplaats, Telefoonnummer, HeeftRijbewijs, HeeftAuto, Email, Gebruikerstype, Rfidcode, ABOUTME) VALUES (:GebruikerID, :Gebruikersnaam, :Wachtwoord, :Naam, :Geslacht, :Adres, :Woonplaats, :Telefoonnummer, :rij, :auto, :Email, :Gebruikerstype, :rfid, :AboutMe)";
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
                m_command.Parameters.Add("AboutMe", OracleDbType.Varchar2).Value = aboutMe;

                m_command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
        } //goodluck! </Rechard>  

        // CHATHALEN <RECHARD>
        public static string chatbox(int needy, int volunteer)
        {
            chathistory.Clear();
            string bericht = string.Empty;
            string hetzender = string.Empty;
            string chatstring = string.Empty;
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

        public static void chatboxlist(int id)
        {
            //chathistory.Clear();
            chatUser.Clear();
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT g.GebruikerID, g.Gebruikersnaam from Chat c JOIN Gebruiker g ON c.Zender = g.GebruikerID WHERE c.GebruikerID = :ID OR c.GebruikerID2 = :ID GROUP BY g.Gebruikersnaam, g.GebruikerID";
                m_command.Parameters.Add("ID", OracleDbType.Varchar2).Value = id;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        //hetzender = Convert.ToString(_Reader["Gebruikersnaam"]);
                        ChatUser cuser = new ChatUser(Convert.ToInt32(_Reader["GebruikerID"]), _Reader["Gebruikersnaam"].ToString());
                        chatUser.Add(cuser);
                        //chatlist.Add(hetzender);
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //public static List<string> openchats = new List<string>();
        //public static string chatlist(int id)
        //{
        //    chathistory.Clear();
        //    string bericht = "";
        //    string hetzender = "";
        //    string chatstring = "";
        //    try
        //    {
        //        OpenConnection();
        //        m_command = new OracleCommand();
        //        m_command.Connection = m_conn;
        //        m_command.CommandText = "SELECT c.Bericht, c.Zender, g.Gebruikersnaam from Chat c LEFT JOIN Gebruiker g ON c.Zender = g.GebruikerID WHERE c.GebruikerID = :needy OR c.GebruikerID2 = :volunteer ORDER BY ChatID ";
        //        m_command.Parameters.Add("needy", OracleDbType.Varchar2).Value = id;
        //        m_command.Parameters.Add("volunteer", OracleDbType.Varchar2).Value = id;
        //        m_command.ExecuteNonQuery();
        //        using (OracleDataReader _Reader = Database.Command.ExecuteReader())
        //        {
        //            while (_Reader.Read())
        //            {
        //                hetzender = Convert.ToString(_Reader["Gebruikersnaam"]);
        //                bericht = Convert.ToString(_Reader["Bericht"]);
        //                chatstring = hetzender + ": " + bericht;
        //                chathistory.Add(chatstring);
        //            }
        //        }
        //    }
        //    catch (OracleException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return bericht;
        //}

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

        // Profile <OLAF>
        public static void Profile(int acid, bool a)
        {

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT GebruikerID, Gebruikersnaam, Email, Woonplaats, Adres, Telefoonnummer, ABOUTME FROM gebruiker WHERE GebruikerID = :accid";
                m_command.Parameters.Add("accid", OracleDbType.Int32).Value = acid;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    try
                    {
                        while (_Reader.Read())
                        {
                            string naam = _Reader["Gebruikersnaam"].ToString();
                            string email = _Reader["Email"].ToString();
                            string woonplaats = _Reader["Woonplaats"].ToString();
                            string adres = _Reader["Adres"].ToString();
                            int telefoon = Convert.ToInt32(_Reader["Telefoonnummer"]);
                            string aboutme = _Reader["ABOUTME"].ToString();
                            if (a == true)
                            {
                                user = new User(naam, email, woonplaats, adres, telefoon, aboutme);
                            }
                            else if (a == false)
                            {
                                userBekijken = new User(naam, email, woonplaats, adres, telefoon, aboutme);
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        Database.CloseConnection();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
        }

        //Review voor profile door <OLAF>

        public static void getMyReviews(int acid)
        {
            try
            {
                reviewsProfile.Clear();
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT r.reviewID, r.beoordeling, r.opmerkingen, r.needyID, n.gebruikersNaam AS needyNaam, r.volunteerID, v.gebruikersNaam AS volunteerNaam FROM Review r, Gebruiker n, Gebruiker v WHERE r.IsVisible = 'Y' AND r.needyID = n.gebruikerID AND r.volunteerID = v.gebruikerID AND r.needyID = :acid OR r.IsVisible = 'Y' AND r.needyID = n.gebruikerID AND r.volunteerID = v.gebruikerID AND r.volunteerID = :acid";
                m_command.Parameters.Add("acid", OracleDbType.Int32).Value = acid;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    try
                    {
                        while (_Reader.Read())
                        {
                            Review review = new Review(Convert.ToInt32(_Reader["reviewID"]), _Reader["beoordeling"].ToString(), _Reader["opmerkingen"].ToString(), Convert.ToInt32(_Reader["needyID"]), _Reader["needyNaam"].ToString(), Convert.ToInt32(_Reader["volunteerID"]), _Reader["volunteerNaam"].ToString());
                            reviewsProfile.Add(review);
                        }
                    }
                    catch (OracleException ex)
                    {
                        Database.CloseConnection();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
        }
        public static List<Request> GetRequests(int ID)
        {
            List<Request> requests = new List<Request>();

            try
            {
                OpenConnection();                   // om connection open te maken
                m_command = new OracleCommand();    // hoef eingelijk niet doordat het all in OpenConnection() zit
                m_command.Connection = m_conn;      // een connection maken met het command
                m_command.CommandText = "SELECT * FROM HULPVRAAG WHERE GEBRUIKERID = :ID AND ISVISIBLE = 'Y' ORDER BY HULPVRAAGID DESC";
                m_command.Parameters.Add("ID", OracleDbType.Int32).Value = ID;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    if (_Reader.HasRows)
                    {
                        while (_Reader.Read())
                        {
                            CultureInfo provider = CultureInfo.InvariantCulture;
                            string start = Convert.ToString(_Reader["STARTDATUM"]);
                            string end = Convert.ToString(_Reader["EINDDATUM"]);
                            DateTime startdate = DateTime.ParseExact(start, "HH:mm", provider);
                            DateTime enddate = DateTime.ParseExact(end, "HH:mm", provider);
                            bool a = false;
                            if (Convert.ToString(_Reader["Urgent"]) == "Y")
                            {
                                a = true;
                            }
                            else if (Convert.ToString(_Reader["Urgent"]) == "N")
                            {
                                a = false;
                            }
                            Request request = new Request(Convert.ToInt32(_Reader["HULPVRAAGID"]), Convert.ToInt32(_Reader["GEBRUIKERID"]), _Reader["OMSCHRIJVING"].ToString(), a, _Reader["LOCATIE"].ToString(), Convert.ToInt32(_Reader["REISTIJD"]), _Reader["VERVOERTYPE"].ToString(), startdate, enddate, Convert.ToInt32(_Reader["AANTALVRIJWILLIGERS"]));
                            requests.Add(request);
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
            return requests;
        }

        public static void placeARequest(int accountid, string omschrijving, string locatie, int reistijd,
            string vervoerType, string startDatum, string eindDatum, string urgent, int aantalVrijwilligers)
        {
            int this_hulpvraagID = 0;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT COUNT(HulpvraagID) from Hulpvraag";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        this_hulpvraagID = Convert.ToInt32(_Reader["COUNT(HulpvraagID)"]) + 1;
                    }
                }

                m_command.CommandText =
                    "INSERT INTO Hulpvraag(HulpvraagID, Omschrijving, Locatie, Reistijd, VervoerType, Startdatum, Einddatum, Urgent, AantalVrijwilligers, GebruikerID) VALUES(:HulpvraagID, :Omschrijving, :Locatie, :Reistijd, :Vervoertype, :Startdatum, :Einddatum, :Urgent, :AantalVrijwilligers, :GebruikerID)";

                Command.Parameters.Add("HulpvraagID", OracleDbType.Int32).Value = this_hulpvraagID;
                Command.Parameters.Add("Omschrijving", OracleDbType.Varchar2).Value = omschrijving;
                Command.Parameters.Add("Locatie", OracleDbType.Varchar2).Value = locatie;
                Command.Parameters.Add("Reistijd", OracleDbType.Int32).Value = reistijd;
                Command.Parameters.Add("Vervoertype", OracleDbType.Varchar2).Value = vervoerType;
                Command.Parameters.Add("Startdatum", OracleDbType.Varchar2).Value = startDatum;
                Command.Parameters.Add("Einddatum", OracleDbType.Varchar2).Value = eindDatum;
                Command.Parameters.Add("Urgent", OracleDbType.Char).Value = urgent;
                Command.Parameters.Add("AantalVrijwilligers", OracleDbType.Int32).Value = aantalVrijwilligers;
                Command.Parameters.Add("GebruikerID", OracleDbType.Int32).Value = accountid;

                Command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        //=============================================================================================================

        // REVIEWID - OPMERKINGEN, CHATID - BERICHT, HULPVRAAGID - OMSCHRIJVING
        // Get ID from selected chat/review/request to change visibility/reported// Raphael
        public static bool getSelected(string column, string message, string IDFromWich, string nameOfMessage)
        {
            bool ok = false;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT " + IDFromWich + " FROM " + column + " WHERE " + nameOfMessage + " = :GEKOZENBERICHT";
                //Command.Parameters.Add("COLUMN", OracleDbType.Varchar2).Value = column;
                Command.Parameters.Add("GEKOZENBERICHT", OracleDbType.Varchar2).Value = message;
                //Command.Parameters.Add("ITEMID", OracleDbType.Varchar2).Value = IDFromWich;
                //Command.Parameters.Add("BERICHT", OracleDbType.Varchar2).Value = nameOfMessage;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {

                        ItemIDSelected = (Convert.ToInt32(_Reader[IDFromWich]));

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
        public static bool getReviewAdmin()
        {
            reviewsListAdmin.Clear();
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

                        reviewsListAdmin.Add(_Reader["OPMERKINGEN"].ToString());

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
        public static bool getChat()
        {
            chats.Clear();
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
                        chats.Add(_Reader["BERICHT"].ToString());

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
        public static bool getReportedReviews()
        {
            bool ok = false;
            reportedReviews.Clear();

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT * FROM REVIEW WHERE ISREPORTED = 'Y'";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {

                        reportedReviews.Add(_Reader["OPMERKINGEN"].ToString());

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
        public static bool getreportedChat()
        {
            bool ok = false;
            reportedChats.Clear();

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
                        reportedChats.Add(_Reader["BERICHT"].ToString());

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
        public static bool getReportedRequests()
        {
            bool ok = false;
            reportedRequests.Clear();

            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT * FROM HULPVRAAG WHERE ISREPORTED = 'Y'";
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
                        reportedRequests.Add(_Reader["OMSCHRIJVING"].ToString());
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
        public static bool getRequests()
        {
            bool ok = false;
            reviewsRequests.Clear();
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
                        // string acctype = Convert.ToString(_Reader["Gebruikerstype"]);
                        // ac = acctype;
                        // int accID = Convert.ToInt32(_Reader["GebruikerID"]);
                        // acID = accID;
                        // result = Convert.ToString(_Reader["Gebruikersnaam"]);
                        // if (result == username) { ok = true; }
                        reviewsRequests.Add(_Reader["OMSCHRIJVING"].ToString());

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

        //Melvin get All requests die visible zijn
        public static List<Request> GetAllVisibleRequests(int userID)
        {
            List<Request> requests = new List<Request>();

            try
            {
                OpenConnection();                   // om connection open te maken
                m_command = new OracleCommand();    // hoef eingelijk niet doordat het all in OpenConnection() zit
                m_command.Connection = m_conn;      // een connection maken met het command
                m_command.CommandText = "SELECT * FROM HULPVRAAG WHERE ISVISIBLE = 'Y' AND HULPVRAAGID NOT IN(SELECT HULPVRAAGID FROM INTRESSE WHERE GEBRUIKERID =:gebruikerid) ORDER BY HULPVRAAGID ";
                m_command.Parameters.Add("gebruikerid", OracleDbType.Int32).Value = userID;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    if (_Reader.HasRows)
                    {
                        while (_Reader.Read())
                        {
                            bool a = false;
                            if (Convert.ToString(_Reader["Urgent"]) == "Y")
                            {
                                a = true;
                            }
                            else if (Convert.ToString(_Reader["Urgent"]) == "N")
                            {
                                a = false;
                            }
                            CultureInfo provider = CultureInfo.InvariantCulture;
                            string start = Convert.ToString(_Reader["STARTDATUM"]);
                            string end = Convert.ToString(_Reader["EINDDATUM"]);
                            DateTime startdate = DateTime.ParseExact(start, "HH:mm", provider);
                            DateTime enddate = DateTime.ParseExact(end, "HH:mm", provider);
                            Request request = new Request(Convert.ToInt32(_Reader["HULPVRAAGID"]), Convert.ToInt32(_Reader["GEBRUIKERID"]), _Reader["OMSCHRIJVING"].ToString(), a, _Reader["LOCATIE"].ToString(), Convert.ToInt32(_Reader["REISTIJD"]), _Reader["VERVOERTYPE"].ToString(), startdate, enddate, Convert.ToInt32(_Reader["AANTALVRIJWILLIGERS"]));

                            if (requests.Contains(request) != true)
                            {
                                requests.Add(request);
                            }

                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
            return requests;
        }

        //melvin Add 1 user in tabel intresse toevoegen 
        public static void intresse(int RequestID, int accountID)
        {
            try
            {
                OpenConnection();                   // om connection open te maken
                m_command = new OracleCommand();    // hoef eingelijk niet doordat het all in OpenConnection() zit
                m_command.Connection = m_conn;      // een connection maken met het command
                m_command.CommandText = "INSERT INTO INTRESSE (HULPVRAAGID,GebruikerID) VALUES (:HULPVRAAGID,:GebruikerID)";
                m_command.Parameters.Add("HULPVRAAGID", OracleDbType.Int32).Value = RequestID;
                m_command.Parameters.Add("GebruikerID", OracleDbType.Int32).Value = accountID;
                m_command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
        }

        public static List<Volunteer> GetVolunteers(int HulpvraagID)
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                OpenConnection();                   // om connection open te maken
                m_command = new OracleCommand();    // hoef eingelijk niet doordat het all in OpenConnection() zit
                m_command.Connection = m_conn;      // een connection maken met het command
                m_command.CommandText = "SELECT G.GEBRUIKERSNAAM, G.EMAIL, G.ADRES, G.WOONPLAATS, G.TELEFOONNUMMER, G.ABOUTME, G.GEBRUIKERID FROM GEBRUIKER G, INTRESSE I, HULPVRAAG H WHERE H.HULPVRAAGID = :HULPVRAAGID AND H.HULPVRAAGID = I.HULPVRAAGID AND I.GEBRUIKERID = G.GEBRUIKERID";
                m_command.Parameters.Add("HULPVRAAGID", OracleDbType.Int32).Value = HulpvraagID;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Command.ExecuteReader())
                {
                    if (_Reader.HasRows)
                    {
                        while (_Reader.Read())
                        {
                            Volunteer volunteer = new Volunteer(_Reader["GEBRUIKERSNAAM"].ToString(), _Reader["EMAIL"].ToString(), _Reader["ADRES"].ToString(), _Reader["WOONPLAATS"].ToString(), Convert.ToInt32(_Reader["TELEFOONNUMMER"]), _Reader["ABOUTME"].ToString(), Convert.ToInt32(_Reader["GEBRUIKERID"]));
                            volunteers.Add(volunteer);
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
            return volunteers;
        }


        public static List<Request> GetAllVisibleInterestedRequests(int userID)
        {
            List<Request> requests = new List<Request>();

            try
            {
                OpenConnection();                   // om connection open te maken
                m_command = new OracleCommand();    // hoef eingelijk niet doordat het all in OpenConnection() zit
                m_command.Connection = m_conn;      // een connection maken met het command
                m_command.CommandText = "SELECT * FROM HULPVRAAG WHERE ISVISIBLE = 'Y' AND HULPVRAAGID IN(SELECT HULPVRAAGID FROM INTRESSE WHERE GEBRUIKERID =:gebruikerid) ORDER BY HULPVRAAGID ";
                m_command.Parameters.Add("gebruikerid", OracleDbType.Int32).Value = userID;
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    if (_Reader.HasRows)
                    {
                        while (_Reader.Read())
                        {
                            bool a = false;
                            if (Convert.ToString(_Reader["Urgent"]) == "Y")
                            {
                                a = true;
                            }
                            else if (Convert.ToString(_Reader["Urgent"]) == "N")
                            {
                                a = false;
                            }
                            CultureInfo provider = CultureInfo.InvariantCulture;
                            string start = Convert.ToString(_Reader["STARTDATUM"]);
                            string end = Convert.ToString(_Reader["EINDDATUM"]);
                            DateTime startdate = DateTime.ParseExact(start, "HH:mm", provider);
                            DateTime enddate = DateTime.ParseExact(end, "HH:mm", provider);
                            Request request = new Request(Convert.ToInt32(_Reader["HULPVRAAGID"]), Convert.ToInt32(_Reader["GEBRUIKERID"]), _Reader["OMSCHRIJVING"].ToString(), a, _Reader["LOCATIE"].ToString(), Convert.ToInt32(_Reader["REISTIJD"]), _Reader["VERVOERTYPE"].ToString(), startdate, enddate, Convert.ToInt32(_Reader["AANTALVRIJWILLIGERS"]));

                            if (requests.Contains(request) != true)
                            {
                                requests.Add(request);
                            }

                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Database.CloseConnection();
                Console.WriteLine(ex.Message);
            }
            return requests;
        }

        public static void placeReview(int beoordeling, string opmerkingen, int needyID, int volunteerID)
        {
            int this_reviewID = 0;
            try
            {
                OpenConnection();
                m_command = new OracleCommand();
                m_command.Connection = m_conn;
                m_command.CommandText = "SELECT COUNT(ReviewID) from Review";
                m_command.ExecuteNonQuery();
                using (OracleDataReader _Reader = Database.Command.ExecuteReader())
                {
                    while (_Reader.Read())
                    {
                        this_reviewID = Convert.ToInt32(_Reader["COUNT(ReviewID)"]) + 1;
                    }
                }

                m_command.CommandText = "INSERT INTO Review(ReviewID, Beoordeling, Opmerkingen, NeedyID, VolunteerID, ISREPORTED, ISVISIBLE) VALUES(:ReviewID, :Beoordeling, :Opmerkingen, :NeedyID, :VolunteerID, 'N', 'Y')";
                Command.Parameters.Add("ReviewID", OracleDbType.Int32).Value = this_reviewID;
                Command.Parameters.Add("Beoordeling", OracleDbType.Varchar2).Value = beoordeling.ToString();
                Command.Parameters.Add("Opmerkingen", OracleDbType.Varchar2).Value = opmerkingen;
                Command.Parameters.Add("NeedyID", OracleDbType.Int32).Value = acid;
                Command.Parameters.Add("VolunteerID", OracleDbType.Int32).Value = volunteerID;
                Command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
