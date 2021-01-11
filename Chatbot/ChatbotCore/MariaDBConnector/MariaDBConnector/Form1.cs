using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MariaDBConnector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySql.Data.MySqlClient.MySqlConnection connect;
        MySqlDataAdapter adapter;
        DataSet data;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            string myConnectString = "server=127.0.0.1;port=3306;Database=irpachatbot;uid=root;pwd=rhit2020;";
            
            try
            {
                connect = new MySql.Data.MySqlClient.MySqlConnection();
                connect.ConnectionString = myConnectString;
                connect.Open();
                if(connect.State == ConnectionState.Open)
                {
                    adapter = new MySqlDataAdapter("select * from info", connect);
                    data = new DataSet();

                    adapter.Fill(data, "info");
                    dataGridView1.DataSource = data.Tables["info"];
                    MessageBox.Show("Good");
                }
                connect.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
