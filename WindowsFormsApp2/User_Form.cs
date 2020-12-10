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
    public partial class User_Form : Form
    {
        public int user_id;
        public int progress;
        public int lec_id;
        public User_Form(int user_id)
        {
            this.user_id = user_id;
            InitializeComponent();
            User user = new User();
            this.progress = user.GetProgress(this.user_id);
            var lec = new Lectures();
            Lectures[] arr = lec.arrayLecture(this.progress);
            for (int i = 0; i < arr.Length; i++)
            {
   
                listBox1.Items.Add(Convert.ToString(arr[i].Title));
            }
            label2.Text = Convert.ToString(user.GetProgress(this.user_id)) + " " + Convert.ToString(this.user_id);


  
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main first = new Main();
            first.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
            //  string message = listBox1.SelectedItem.ToString();
            //  string titlem = "Ошибка";
            //  MessageBox.Show(message, titlem);
            try  
            {
                var user = new User();
                (int lec_id, string subject, string text, string title, string username) = user.GetLecture(listBox1.SelectedItem.ToString());

                this.lec_id = lec_id;
                textBox1.Text = text;
                Checks checks = new Checks();
                Checks[] arr = checks.arrayCheck(this.lec_id);
                listBox2.Items.Clear();
                for (int i = 0; i < arr.Length; i++)
                {
                    listBox2.Items.Add(Convert.ToString(arr[i].id) + " " + Convert.ToString(arr[i].text));
                }
            }
            catch
            {

            }

            //  listBox1.SelectedItem();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {

                Lectures lec = new Lectures();
                listBox1.Items.Clear();
                if (lec.GetProgress(this.user_id) <= this.lec_id)
                {
                    this.progress = this.lec_id;
                    lec.AppProgress(this.user_id, this.lec_id);
                }
                //            this.progress = this.lec_id;
                //          lec.AppProgress(this.user_id, this.lec_id);
                Lectures[] arr = lec.arrayLecture(this.progress);
                for (int i = 0; i < arr.Length; i++)
                {
                    listBox1.Items.Add(Convert.ToString(arr[i].Title));
                }
                label2.Text = Convert.ToString(this.progress) + " " + Convert.ToString(this.user_id);
            } else
            {
                MessageBox.Show("ответь на все вопросы");

            }
        }

        private void User_Form_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            Console.WriteLine(sender);
            //  string message = listBox1.SelectedItem.ToString();
            //  string titlem = "Ошибка";
            //  MessageBox.Show(message, titlem);
            try
            {
                var user = new Checks();
                string[] word = listBox2.SelectedItem.ToString().Split(' ');
                int id_lec1 = Convert.ToInt32(word[0]);
                (int id, int lec_id, string text, int value) = user.SelectInfoCheck(id_lec1);

                //      this.lec_id = lec_id;
                textBox2.Text = text;
            }
            catch
            {

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int radio;
            var check = new Checks();
            try
            {
                string[] word = listBox2.SelectedItem.ToString().Split(' ');
                int id_lec1 = Convert.ToInt32(word[0]);
                (int id, int lec_id, string text, int value) = check.SelectInfoCheck(id_lec1);
                if (radioButton1.Checked == true)
                {
                    radio = 1;
                }
                else if (radioButton2.Checked == true)
                {
                    radio = 2;
                }
                else if (radioButton3.Checked == true)
                {
                    radio = 3;
                }
                else
                {
                    radio = 0;
                }
                if (value == radio)
                {

                    MessageBox.Show("ответ правильный");
                    //  listBox2.SelectedItem = listBox2.SelectedItem.ToString() + "готово"; 
                }
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("выбири вопрос");
            }
        }
    }  

}
