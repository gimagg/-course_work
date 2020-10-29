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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 first = new Form1();
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
                var teachers = new Teachers(name, subject, password);
                if (!teachers.CheckReg()){
                    teachers.Regectration();
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
                Console.Out.WriteLine("notrttt");
            }
        }

    }
}
