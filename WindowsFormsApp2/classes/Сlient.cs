using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using Npgsql;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp2
{
    public class Сlient : Client_Interface
    {
        public string db_string = "";
        public int user_id;
        public string name;
        public string subject = "user";
        public string password;
        public Сlient()
        {
            string Host = "localhost";
            string User_r = "postgres";
            string DBname = "fork";
            string Password_r = "12345";
            string Port = "5432";
            this.db_string =
              String.Format(
                  "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                  Host,
                  User_r,
                  DBname,
                  Port,
                  Password_r);
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Db_string
        {
            get { return db_string; }
            set { db_string = value; }
        }

        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        public bool CheckReg(string name)
        {
            this.name = name;

            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, name, subject, password, role FROM c.teachers WHERE name = @n ", conn))
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
        public (int, int) LogIn(string name, string password)
        {
            this.name = name;
            this.password = password;
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, name, subject, password, role FROM c.teachers WHERE name = @n ", conn))
                {
                    command.Parameters.AddWithValue("n", this.name);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    try
                    {
                        Console.Out.WriteLine(String.Format(reader["id"].ToString()));
                        if (Convert.ToString(reader["password"].ToString()) == this.GetHash(this.password))
                        {
                            return (Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["role"]));
                        }
                    }
                    catch
                    {
                        Console.Out.WriteLine("not");
                    }
                }
                conn.Close();

            }
            return (0, 0);
        }
        public (int, string, string, string) SelectInfoUser(int user_id)
        {
            using (var conn = new NpgsqlConnection(Db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, name, subject, password FROM c.teachers WHERE id = @i ", conn))
                {
                    command.Parameters.AddWithValue("i", user_id);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    return (Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"]), Convert.ToString(reader["subject"]), Convert.ToString(reader["password"]));
                    conn.Close();
                }
            }

        }
        public int Regectration(string name, string password)
        {
            this.name = name;
            this.subject = "client";
            this.password = password;
            int id = 0;
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("INSERT INTO c.teachers (name, subject, password, role) VALUES (@n, @s, @p, @r)  RETURNING id", conn))
                {
                    command.Parameters.AddWithValue("n", this.name);
                    command.Parameters.AddWithValue("s", subject);
                    command.Parameters.AddWithValue("p", this.GetHash(this.password));
                    command.Parameters.AddWithValue("r", 0);

                    var reader = command.ExecuteReader();
                    reader.Read();
                    id = Convert.ToInt32(reader["id"]);
                    conn.Close();
                    return id;
                }
                


            }
        }
    }
    
}
