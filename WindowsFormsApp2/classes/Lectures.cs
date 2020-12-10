using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Npgsql;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class Lectures : Lectures_interface
    {   
        private string db_string;
        public int id { get; set; }
        public string Title { get; set; }
        public string Before { get; set; }
        public Lectures()
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
        public string Db_string
        {
            get { return db_string; }
            set { db_string = value; }
        }

       

        public int GetLecId(string before)
        {
            int before_id =0;
            using (var conn = new NpgsqlConnection(db_string))
            {
                conn.Open();
                try
                {
                    using (var command1 = new NpgsqlCommand("SELECT id FROM c.lecture WHERE title = @b", conn))
                    {
                        command1.Parameters.AddWithValue("b", before);
                        var reader1 = command1.ExecuteReader();
                        reader1.Read();
                        before_id = Convert.ToInt32(reader1["id"]);
                    }
                    conn.Close();
                }
                catch
                {
                    before_id = 0;
                }
            }
           
            return before_id;
        }
        public void AddLecture(string text, int id_user, string title, string before)
        {
            int id = 0;

            using (var conn = new NpgsqlConnection(db_string))
            {
                Random rand = new Random();
                Console.Out.WriteLine("Opening connection");
                string path = "C:/Users/admin/source/repos/gimagg/-course_work/WindowsFormsApp2/lectures/" + title + ".txt";
                int user_id;
                string name;
                string subject;
                string password;
                if (!File.Exists(path))
                {
                    User user = new User();
                    (user_id, name, subject, password) = user.SelectInfoUser(id_user);
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(text);
                        conn.Open();
                        int id_before = GetLecId(before);
                      
                    using (var command = new NpgsqlCommand("INSERT INTO c.lecture (user_id, subject, title, name, before) VALUES (@u, @s, @t, @l, @b)  RETURNING id", conn))
                        {
                            command.Parameters.AddWithValue("u", user_id);
                            command.Parameters.AddWithValue("s", subject);
                            command.Parameters.AddWithValue("t", title);
                            command.Parameters.AddWithValue("l", name);
                            command.Parameters.AddWithValue("b", id_before);
                            var reader = command.ExecuteReader();
                            reader.Read();
                            id = Convert.ToInt32(reader["id"]);
                        }
                        conn.Close();
                    }
                }
                else
                {
                    string message = "Такая лекция уже есть";
                    string titlem = "Ошибка";
                    MessageBox.Show(message, titlem);
                }
            }
        }

        private Lectures[] add_obj_to_arr(Lectures[] array,int id, string before, string title)
        {
            Lectures[] newarray = new Lectures[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newarray[i] = array[i];
            }
            Lectures elem = new Lectures();
            elem.id = id;
            elem.Title = title;
            elem.Before = before;
            newarray[array.Length] = elem;
            return newarray;
        }

        public Lectures[] arrayLecture()
        {
            using (var conn = new NpgsqlConnection(db_string))

            {
                conn.Open();
                Lectures[] arr = new Lectures[0];
                //arr[0].title = 0;
                using (var command = new NpgsqlCommand("SELECT id ,title, before FROM c.lecture", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        arr = add_obj_to_arr(arr, Convert.ToInt32(reader["id"]) ,Convert.ToString(reader["before"]), Convert.ToString(reader["title"]));
                
                    }
                    conn.Close();
                    return arr;

                }
            }
        }
        public Lectures[] arrayLecture(int before)
        {
            using (var conn = new NpgsqlConnection(db_string))
            {
                conn.Open();
                Lectures[] arr = new Lectures[0];
                //arr[0].title = 0;
                using (var command = new NpgsqlCommand("SELECT id ,title, before FROM c.lecture WHERE before <= @b", conn))
                {
                    command.Parameters.AddWithValue("b", before);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        arr = add_obj_to_arr(arr, Convert.ToInt32(reader["id"]), Convert.ToString(reader["before"]), Convert.ToString(reader["title"]));

                    }
                    conn.Close();
                    return arr;

                }
            }
        }
        public void AppProgress(int user_id, int lecture)
        {

            using (var conn = new NpgsqlConnection(db_string))

            {
                conn.Open();
                Lectures[] arr = new  Lectures[0];
                //arr[0].title = 0;
                using (var command = new NpgsqlCommand("UPDATE c.progress SET progress = @l WHERE user_id = @u", conn))
                {

                    command.Parameters.AddWithValue("u", user_id);
                    command.Parameters.AddWithValue("l", lecture);
                    var reader = command.ExecuteReader();
                }
                conn.Close();
            }
        }
        public int GetProgress(int user_id)
        {
            int id;
            using (var conn = new NpgsqlConnection(db_string))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT progress FROM c.progress WHERE user_id = @u", conn))
                {
                    command.Parameters.AddWithValue("u", user_id);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    id = Convert.ToInt32(reader["progress"]);
                    conn.Close();
        
                }
            }
            return id;
        }
        
    }
}
