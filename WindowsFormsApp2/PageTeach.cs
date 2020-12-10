using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public partial class PageTeach : Form
    {
        public int user_id;
        public int lec_id;
        public PageTeach(int user_id)
        {
            this.user_id = user_id;
            InitializeComponent();
            textBox1.Text = Convert.ToString(this.user_id);
            var lec = new Lectures();
            Lectures[] arr = lec.arrayLecture();
            for (int i = 0; i < arr.Length; i++)
            {
                listBox1.Items.Add(Convert.ToString(arr[i].Title));
            }
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string title = textBox2.Text;
            var lec = new Lectures();
            string before;
            try
            {
                before = listBox1.SelectedItem.ToString();
            }
            catch
            {
                before = "0";
            }
            lec.AddLecture(text, this.user_id, title, before);
            var arr = lec.arrayLecture();
            listBox1.Items.Clear();
            for (int i = 0; i < arr.Length; i++)
            {
                listBox1.Items.Add(Convert.ToString(arr[i].Title));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var user = new User();
                (int lec_id, string subject, string text, string title, string username) = user.GetLecture(listBox1.SelectedItem.ToString());
                this.lec_id = lec_id;
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

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
        }

  

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
              
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int radio;
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
            string text = textBox3.Text;

            Checks checks = new Checks();
            Lectures l = new Lectures();
            checks.CreateQwe(l.GetLecId(listBox1.SelectedItem.ToString()), radio, text);


        }   

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
