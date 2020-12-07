using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace WindowsFormsApp2
{
    class Sessions
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "postgres";
        private static string Password = "1234";
        private static string Port = "5432";
        private static string db_string = "";
        public int user_id;
        public Sessions(int user_id)
        {
           this.user_id = user_id;
            db_string =
              String.Format(
                  "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                  Host,
                  User,
                  DBname,
                  Port,
                  Password);
        }
        public void Start()
        {
            using (var conn = new NpgsqlConnection(db_string))
            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();
                using (var command = new NpgsqlCommand("INSERT INTO c.sessions (user_id, status, type, event_id) VALUES (@u, @s, @t, 0)", conn))
                {
                    command.Parameters.AddWithValue("u", this.user_id);
                    command.Parameters.AddWithValue("s", true);
                    command.Parameters.AddWithValue("t", "start");
                    int nRows = command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public bool Check()
        {
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, user_id, event_id, status FROM c.sessions WHERE user_id = @u LIMIT 1 ", conn))
                {
                    command.Parameters.AddWithValue("n", this.user_id);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    try
                    {
                        Console.Out.WriteLine(String.Format(reader["id"].ToString()));
                        if (Convert.ToBoolean(reader["status"].ToString()))
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        Console.Out.WriteLine("not");
                    }
                }
                conn.Close();

            }
            return false;
        }
        public void Stop()
        {
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("UPDATE c.sessions SET status = false WHERE user_id = @u; ", conn))
                {
                    command.Parameters.AddWithValue("u", this.user_id);
                    int nRows = command.ExecuteNonQuery();
                }
                conn.Close();

            }
        }
    }
}
