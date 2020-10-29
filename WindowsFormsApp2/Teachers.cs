using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApp2
{
    class Teachers
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "postgres";
        private static string Password = "1234";
        private static string Port = "5432";
        private static string db_string = "";
        public string name;
        public string subject;
        private string password;
        public Teachers(string name, string subject, string password)
        {
            this.name = name;
            this.subject = subject;
            this.password = password;
            db_string =
              String.Format(
                  "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                  Host,
                  User,
                  DBname,
                  Port,
                  Password);
        }
        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
        public bool CheckReg()
        {
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, name, subject, password FROM c.teachers WHERE name = @n ", conn))
                {
                 command.Parameters.AddWithValue("n", this.name);
                 var reader = command.ExecuteReader();  
                 reader.Read();
                    try
                    {
                        Console.Out.WriteLine(String.Format(reader["id"].ToString()));
                        if (Convert.ToString(reader["name"].ToString()) == this.name)
                        {
                            return true;
                        }
                    } catch
                    {
                        Console.Out.WriteLine("not");
                    }
                }
                conn.Close();

            }
            return false;
        }
        public void Regectration()
        {
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("INSERT INTO c.teachers (name, subject, password) VALUES (@n, @s, @p)", conn))
                {
                    command.Parameters.AddWithValue("n", this.name);
                    command.Parameters.AddWithValue("s", this.subject);
                    command.Parameters.AddWithValue("p", this.GetHash(this.password));
                    int nRows = command.ExecuteNonQuery();
                    Console.Out.WriteLine(String.Format("Number of rows inserted={0}", nRows));
                }
                conn.Close();

            }


        }
        public bool LogIn()
        {
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, name, subject, password FROM c.teachers WHERE name = @n ", conn))
                {
                    command.Parameters.AddWithValue("n", this.name);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    try
                    {
                        Console.Out.WriteLine(String.Format(reader["id"].ToString()));
                        if (Convert.ToString(reader["password"].ToString())== this.GetHash(this.password))
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
    }
    
}
