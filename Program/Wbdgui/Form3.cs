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
    public partial class Form3 : Form
    {
        List<string> nazwiska = new List<string>();
        List<int> id_atrak = new List<int>();
        List<int> id_kasy = new List<int>();

        public Form FormToShowOnClosing { get; set; }
        private OracleConnection oracleConnection;
        private OracleCommand oracleCommand;
        private OracleDataAdapter dataAdapter;
        private OracleCommandBuilder commandBuilder;
        private DataSet dataSet;
        private DataView dataView;
        int Id=0;
        public Form3(OracleConnection orcl)
        {
            oracleConnection = orcl;
            InitializeComponent();
            jobs.Items.Add("pracownicy");
            jobs.Items.Add("pracownicy_atrakcje");
            jobs.Items.Add("pracownicy_kasy");
            jobs.Items.Add("atrakcje");
            jobs.Items.Add("kasy");
            jobs.Items.Add("wynagrodzenia");
            jobs.Enabled = false;
            button1.Enabled = false;
        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormToShowOnClosing != null)
                FormToShowOnClosing.Show();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //string sql = "select *from pracownicy where NAZWISKO_PRACOWNIKA = " +nazwisko;
            //oracleCommand = new OracleCommand(sql, oracleConnection);
            //oracleCommand.CommandType = CommandType.Text;

            //OracleDataReader dr = oracleCommand.ExecuteReader();

            //dataAdapter = new OracleDataAdapter(oracleCommand);
            //commandBuilder = new OracleCommandBuilder(dataAdapter);
            //dataSet = new DataSet();

            //dataAdapter.Fill(dataSet);

            //dataGridView1.DataSource = dataSet.Tables[0];
            //dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
        }

        private void jobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nazwiska.Contains(nazwisko.Text))
                {
                string sql = "select ID_PRACOWNIKA from pracownicy where NAZWISKO_PRACOWNIKA = '" + nazwisko.Text + "'";
                getTable(sql);
                Id = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);

                sql = "select ID_ATRAKCJI from pracownicy_atrakcje where ID_PRACOWNIKA = " +Id;
                getTable(sql);
                int ile = Convert.ToInt32(dataSet.Tables[0].Rows.Count);

                id_atrak.Clear();
                for(int i=0;i<ile;i++)
                id_atrak.Add(Convert.ToInt32(dataSet.Tables[0].Rows[i][0]));

                sql = "select ID_KASY from pracownicy_kasy where ID_PRACOWNIKA = " + Id;
                getTable(sql);
                int ile2 = Convert.ToInt32(dataSet.Tables[0].Rows.Count);

                id_kasy.Clear();
                for (int i = 0; i < ile2; i++)
                    id_kasy.Add(Convert.ToInt32(dataSet.Tables[0].Rows[i][0]));

                if (jobs.Text == "pracownicy_atrakcje")
                {
                    sql = "select * from pracownicy_atrakcje where ID_PRACOWNIKA = " + Id;
                    getTable(sql);
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                }
                else if (jobs.Text == "pracownicy_kasy")
                {
                    sql = "select * from pracownicy_kasy where ID_PRACOWNIKA = " + Id;
                    getTable(sql);
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                }
                else if(jobs.Text == "pracownicy")
                {
                    sql = "select *from pracownicy where NAZWISKO_PRACOWNIKA = '" + nazwisko.Text + "'";
                    getTable(sql);
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                }
                else if(jobs.Text == "atrakcje")
                {
                    string atrakcje;
                    if (id_atrak.Count > 0)
                    {
                        atrakcje = id_atrak[0].ToString();
                        for (int i = 0; i < ile; i++)
                        {
                            atrakcje += ", " + id_atrak[i];
                        }
                        sql = "select *from atrakcje where ID_ATRAKCJI IN(" + atrakcje + ")";
                        getTable(sql);
                    }
                    else
                    {
                        sql = "select *from atrakcje where ID_ATRAKCJI = 100";
                        getTable(sql);
                    }
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                }
                else if(jobs.Text == "kasy")
                {
                    string kasy;
                    if (id_kasy.Count > 0)
                    {
                        kasy = id_kasy[0].ToString();

                        for (int i = 0; i < ile2; i++)
                        {
                            sql = "select *from kasy where ID_KASY IN(" + kasy + ")";
                            
                            getTable(sql);
                        }
                    }
                    else
                    {
                        sql = "select *from kasy where ID_KASY = 100";
                             getTable(sql);
                    }
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                }
                else
                {
                    sql = "select *from wynagrodzenia where ID_PRACOWNIKA = "+Id;
                    getTable(sql);
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            nazwiska.Clear();
            string sql = "select NAZWISKO_PRACOWNIKA from pracownicy";
            getTable(sql);

            int ile = Convert.ToInt32(dataSet.Tables[0].Rows.Count);

            for (int i=0;i<ile;i++)
            {
                nazwiska.Add(dataSet.Tables[0].Rows[i][0].ToString());
            }

            if(nazwiska.Contains(nazwisko.Text))
            {
                sql = "select *from pracownicy where NAZWISKO_PRACOWNIKA = '" + nazwisko.Text+"'";
                getTable(sql);
                dataGridView1.DataSource = dataSet.Tables[0];
                dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                jobs.Enabled = true;
                jobs.Text = "pracownicy";
            }
            else
            {
                jobs.Enabled = false;
                MessageBox.Show("Podano nazwisko, które nie występuje w bazie pracowników");

            }
            
        }

        private void nazwisko_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
            jobs.Enabled = false;
        }

        private void setButtonVisibility()
        {
            if ( nazwisko.Text != String.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void getTable(string sql)
        {
            oracleCommand = new OracleCommand(sql, oracleConnection);
            oracleCommand.CommandType = CommandType.Text;

            OracleDataReader dr = oracleCommand.ExecuteReader();

            dataAdapter = new OracleDataAdapter(oracleCommand);
            commandBuilder = new OracleCommandBuilder(dataAdapter);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            
        }

        private void splitContainer1_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
