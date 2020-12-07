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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //регестрация препода
            RegesT regesPr = new RegesT(5);
            regesPr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e) 
            //войти
        {
            string name = textBox2.Text;
            string password = textBox1.Text;
            User user = new User();
            int user_id;
            int status;   
            (user_id, status)= user.LogIn(name, password);
            if(user_id!= 0)
            {
                if (status == 0)
                {
                    User_Form form = new User_Form(user_id);
                    form.Show();
                    this.Hide();
                }
                else
                {
                    PageTeach form = new PageTeach(user_id);
                    form.Show();
                    this.Hide();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
              //регесьрация ученик
        {
            RegesUser form = new RegesUser();
            form.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
