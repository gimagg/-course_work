using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.IO;

namespace WindowsFormsApp2
{
    public class User : Сlient, User_interface
    {

        public User() : base()
        {

        }
        new public int Regectration(string name,string password)
        {
            this.name = name;
            this.subject = "user";
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
                }
                conn.Close();
              

            }
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("INSERT INTO c.progress (user_id, progress) VALUES (@u, @p)", conn))
                {
                    command.Parameters.AddWithValue("u", id);
                    command.Parameters.AddWithValue("p", 1);
                 
                    var reader = command.ExecuteReader();
                }
                conn.Close();
       
            }

            return id;

        }
       public (int lec_id ,string subject, string text, string title, string username) GetLecture(string title)
        {

            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, user_id, subject, title, name FROM c.lecture WHERE title = @n ", conn))
                {
                    command.Parameters.AddWithValue("n", title);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    string path = "C:/Users/admin/source/repos/gimagg/-course_work/WindowsFormsApp2/lectures/" + title + ".txt";
                    string readText = File.ReadAllText(path);
                    return (Convert.ToInt32(reader["id"]) ,Convert.ToString(reader["subject"]), readText, Convert.ToString(reader["title"]), Convert.ToString(reader["name"]));

                    
                    conn.Close();

                }
            }

        }

        public int GetProgress(int user_id)
        {
            int progress;
            using (var conn = new NpgsqlConnection(db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT progress FROM c.progress WHERE user_id = @u ", conn))
                {
                    command.Parameters.AddWithValue("u", user_id);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    progress = Convert.ToInt32(reader["progress"]);
                    conn.Close();

                }
            }
            return progress;
        } 
    }
    
}
