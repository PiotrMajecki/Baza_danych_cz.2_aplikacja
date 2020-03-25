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

    public partial class Form2 : Form
    {
        public Form FormToShowOnClosing { get; set; }
        private OracleConnection oracleConnection;
        private OracleCommand oracleCommand;
        private OracleDataAdapter dataAdapter;
        private OracleCommandBuilder commandBuilder;
        private DataSet dataSet;
        private DataView dataView;
        public Form2(OracleConnection orcl)
        {
            oracleConnection = orcl;
            InitializeComponent();
            tablesNames.Items.Add("adresy");
            tablesNames.Items.Add("atrakcje");
            tablesNames.Items.Add("bilety");
            tablesNames.Items.Add("dla_doroslych");
            tablesNames.Items.Add("dla_dzieci");
            tablesNames.Items.Add("dla_mlodziezy");
            tablesNames.Items.Add("kasy");
            tablesNames.Items.Add("klienci");
            tablesNames.Items.Add("parki_rozrywki");
            tablesNames.Items.Add("pracownicy");
            tablesNames.Items.Add("pracownicy_atrakcje");
            tablesNames.Items.Add("pracownicy_kasy");
            tablesNames.Items.Add("przekaski");
            tablesNames.Items.Add("stanowiska");
            tablesNames.Items.Add("wagoniki");
            tablesNames.Items.Add("wlasciciele");
            tablesNames.Items.Add("wynagrodzenia");
            button3.Enabled = false;
            button2.Enabled = false;
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormToShowOnClosing != null)
                FormToShowOnClosing.Show();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataAdapter.Update(dataSet.Tables[0]);

            }
            catch(OracleException ex)
            {
                if(ex.Number == 1400)
                MessageBox.Show("Wartość w następnej kolumnie nie może byc pusta");
                if (ex.Number == 2291)
                    MessageBox.Show("Wartość klucza w jednej z kolumn jest nieprawidłowa");
                if (ex.Number == 2290)
                    MessageBox.Show("Wartość w kolumnie: " + ex.Message.ToString() + " nie jest prawidłowa");
                if (ex.Number == 1)
                    MessageBox.Show("Wartość klucza musi być unikatowa");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Czy napewno chcesz usunąć ten wiersz?", "Form Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                // cancel the closure of the form.

            }
            else
            {

                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        dataGridView1.Rows.RemoveAt(row.Index);
                    dataAdapter.Update(dataSet.Tables[0]);
                }
                catch (OracleException ex)
                {
                    if (ex.Number == 2292)
                        MessageBox.Show("Usuń najpierw rekord, który korzysta z tego wpisu");
                    string sql = "select *from " + tablesNames.Text;
                    oracleCommand = new OracleCommand(sql, oracleConnection);
                    oracleCommand.CommandType = CommandType.Text;

                    OracleDataReader dr = oracleCommand.ExecuteReader();

                    dataAdapter = new OracleDataAdapter(oracleCommand);
                    commandBuilder = new OracleCommandBuilder(dataAdapter);
                    dataSet = new DataSet();

                    dataAdapter.Fill(dataSet);
                    dataGridView1.DataSource = dataSet.Tables[0];
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tablesNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select *from " + tablesNames.Text;
            oracleCommand = new OracleCommand(sql, oracleConnection);
            oracleCommand.CommandType = CommandType.Text;

            OracleDataReader dr = oracleCommand.ExecuteReader();

            dataAdapter = new OracleDataAdapter(oracleCommand);
            commandBuilder = new OracleCommandBuilder(dataAdapter);
            dataSet = new DataSet();

            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.DataSource = dataSet.Tables[0].DefaultView;

            button2.Enabled = true;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button3.Enabled = true;
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            button3.Enabled = false;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Podano nieprawidłową wartość");
        }
    }
}
