using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Oracle.DataAccess.Client;

namespace Wbdgui
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
          InitializeComponent();
        }

        private OracleConnection oracleConnection = new OracleConnection();
        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                oracleConnection.Close();
                oracleConnection.ConnectionString = "User Id=" + login.Text + ";Password=" + haslo.Text + ";Data Source=" + datasource.Text + ";";
                oracleConnection.Open();
               
                    if (privileges.Checked)
                    {
                        Form2 form2 = new Form2(oracleConnection);
                        this.Hide();
                        form2.FormToShowOnClosing = this;
                        form2.Show();
                    }
                    else
                    {
                        Form3 form3 = new Form3(oracleConnection);
                        this.Hide();
                        form3.FormToShowOnClosing = this;
                        form3.Show();
                    }

                login.Clear();
                haslo.Clear();
                
            }
            catch(OracleException ex)
            {
                if (ex.Number == 1017)
                    MessageBox.Show("Podano zły login lub hasło");
            }
            
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
    }
}
