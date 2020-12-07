using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace WindowsFormsApp2
{
    public class Che
    {
        public int id { get; set; }
        public int lect_id { get; set; }
        public string text { get; set; }
        public int value { get; set; }
    }
    class Checks : Lectures
    {
        public Checks() : base()
        {

        }
        public int CreateQwe(int lect_id, int value, string text)
        {
            int before_id = 0;
            using (var conn = new NpgsqlConnection(base.Db_string))
            {
                conn.Open();
                try
                {
                    using (var command1 = new NpgsqlCommand("INSERT INTO c.checks (lect_id, text, value) VALUES (@l, @t, @v)  RETURNING id", conn))
                    {
                        command1.Parameters.AddWithValue("l", lect_id);
                        command1.Parameters.AddWithValue("t", text);
                        command1.Parameters.AddWithValue("v", value);
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
        private Che[] add_obj_to_arr(Che[] array, int id,int lect_id,string text,int value)
        {
            Che[] newarray = new Che[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newarray[i] = array[i];
            }
            Che elem = new Che();
            elem.id = id;
            elem.lect_id = lect_id;
            elem.text = text;
            elem.value = value;
            newarray[array.Length] = elem;
            return newarray;
        }
        public Che[] arrayCheck(int lect_id)
        {
            using (var conn = new NpgsqlConnection(base.Db_string))

            {
                conn.Open();
                Che[] arr = new Che[0];
                //arr[0].title = 0;
                using (var command = new NpgsqlCommand("SELECT id, lect_id, text, value FROM c.checks WHERE lect_id=@a ", conn))
                {
                    command.Parameters.AddWithValue("a", lect_id);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        arr = add_obj_to_arr(arr, Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["lect_id"]), Convert.ToString(reader["text"]), Convert.ToInt32(reader["value"]));

                    }
                    conn.Close();
                    return arr;

                }
            }
        }
        public (int, int, string, int) SelectInfoCheck(int id)
        {
            using (var conn = new NpgsqlConnection(base.Db_string))

            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT id, lect_id, text, value FROM c.checks WHERE id = @i ", conn))
                {
                    command.Parameters.AddWithValue("i", id);
                    var reader = command.ExecuteReader();
                    reader.Read();
                  //  conn.Close();
                    return (Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["lect_id"]), Convert.ToString(reader["text"]), Convert.ToInt32(reader["value"]));
                }
            }

        }

    }
}
