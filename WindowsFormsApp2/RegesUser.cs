using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class RegesUser : Form
    {
        public RegesUser()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;
            string password = textBox2.Text;
            string password_r = textBox1.Text;
            if (password == password_r && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
            {
                var user = new User();
                if (!user.CheckReg(name))
                {
                    try
                    {
                        int id = user.Regectration(name, password);
                        User_Form site = new User_Form(id);
                        site.Show();
                        this.Hide();

                    }
                    catch
                    {
                        string message = "Проблема с базой данных";
                        string title = "Ошибка";
                        MessageBox.Show(message, title);
                        Console.Out.WriteLine("notrttt");
                    }

                }
                else
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

