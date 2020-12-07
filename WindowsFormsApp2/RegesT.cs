using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    
    public partial class RegesT : Form
    {
        public int user_id;
        public RegesT(int user_id)
        {
            this.user_id = user_id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main first = new Main();
            first.Show();
            this.Hide();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string subject = textBox2.Text;
            string password = textBox3.Text;
            string password_r = textBox4.Text;
            if (password == password_r && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
            {
                var teachers = new Teacher();
                if (!teachers.CheckReg(name)){
                    try {
                        int id = teachers.Regectration(name, password, subject);
                        PageTeach site = new PageTeach(id);
                        site.Show();
                        this.Hide();

                    } catch
                    {
                        string message = "Проблема с базой данных";
                        string title = "Ошибка";
                        MessageBox.Show(message, title);
                        Console.Out.WriteLine("notrttt");
                    }
                        
                } else
                {
                    string message = "Препод с таким именем уже есть";
                    string title = "Ошибка";
                    MessageBox.Show(message, title);
                    Console.Out.WriteLine("notrttt");
                }
            }
            else
            {
                string message = "Заполните все поля";
                string title = "Ошибка";
                MessageBox.Show(message, title);
            }
        }

    }
}
