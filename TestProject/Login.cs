using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Login : Form
    {
        MainForm form2;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Id, pw;
            Id = textBox1.Text;
            pw = textBox2.Text;
            form2 = (MainForm)Owner;
            form2.ID = Id;
            form2.PW = pw;

            //form2.Show();
            this.Close();

        }

    }
}
