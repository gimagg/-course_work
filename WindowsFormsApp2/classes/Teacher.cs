using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Npgsql;

namespace WindowsFormsApp2
{

    class Teacher : Сlient, Teacher_interface
    {
        public Teacher() :base()
        { 

        }
        public int Regectration(string name, string password, string subject)
        {
            this.name = name;
            this.password = password;
            int id = 0;
            using (var conn = new NpgsqlConnection(base.db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("INSERT INTO c.teachers (name, subject, password, role) VALUES (@n, @s, @p, @r)  RETURNING id", conn))
                {
                    command.Parameters.AddWithValue("n", this.name);
                    command.Parameters.AddWithValue("s", subject);
                    command.Parameters.AddWithValue("p", base.GetHash(this.password));
                    command.Parameters.AddWithValue("r", 1);
                    this.subject = subject;
                    var reader = command.ExecuteReader();
                    reader.Read();
                    id = Convert.ToInt32(reader["id"]);
                }
                conn.Close();
                return id;
            }

        }
    public void AddLecture(string title, string text)
        {
            int id = 0;
            using (var conn = new NpgsqlConnection(base.db_string))

            {
                Random rand = new Random();
                Console.Out.WriteLine("Opening connection");
                string path = "./lectures/" + title;
                int role;
                (user_id, role) = this.LogIn(name, password);
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(text);
                }
                conn.Open();
                using (var command = new NpgsqlCommand("INSERT INTO c.lecture (user_id, subject, title, name) VALUES (@u, @s, @t, @l)  RETURNING id", conn))
                {
                    command.Parameters.AddWithValue("u", user_id);
                    command.Parameters.AddWithValue("s", subject);
                    command.Parameters.AddWithValue("t", base.GetHash(this.password));
                    command.Parameters.AddWithValue("l", 1);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    id = Convert.ToInt32(reader["id"]);
                }
                conn.Close();
            }

        }
    }
   
}
