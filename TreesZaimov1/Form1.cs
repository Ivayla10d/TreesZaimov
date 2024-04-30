using Microsoft.VisualBasic;
using MySqlConnector;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TreesZaimov1
{
    public partial class Form1 : Form
    {
        string connstr = "server=10.6.0.33;" +
            "port=3306;" +
            "user=PC1;" +
            "password = 1111;" +
            "database=trees_zaimov";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0)
                connect.Open();
            MessageBox.Show("Conection NOW opened");


            CmbFillIn(cmbOtdel, connect, "otdel");
            CmbFillIn(cmbKlas, connect, "class");
            CmbFillIn(cmbRazred, connect, "razred");
            CmbFillIn(cmbSemeistvo, connect, "family");
            CmbFillIn(cmbRod, connect, "rod");
            CmbFillIn(cmbVid, connect, "type");

            connect.Close();
        }
        public void CmbFillIn(ComboBox cm, MySqlConnection conn, string table)
        {

            MySqlCommand query = new MySqlCommand($"Select * from {table}", conn);

            MySqlDataReader readerCombo = query.ExecuteReader();

            List<ComboBoxItem> items = new List<ComboBoxItem>();
            while (readerCombo.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = readerCombo[1].ToString();
                item.Value = (int)readerCombo[0];

                items.Add(item);
            }
            readerCombo.Close();
            cm.DataSource = items;
            cm.DisplayMember = "Text";
            cm.ValueMember = "Value";
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            string sql = "INSERT INTO `trees_zaimov`.`tree`" +
                "(`name`,`imageUrl`," +
                "`otdel_id`, `class_id`, `razred_id`," +
                "`family_id`, `rod_id`, `vid_id`," +
                "description, DateLastUpdate)" +
                "VALUES(@name, @img, @otdel, @class," +
                "@razred, @family, @rod, @vid, @info, @dateReg)";

            MySqlConnection connect = new MySqlConnection(connstr);
            if (connect.State == 0) connect.Open();
            MySqlCommand query = new MySqlCommand(sql, connect);

            query.Parameters.AddWithValue("@name", txtNaimenovanie.Text);
            query.Parameters.AddWithValue("@img", txtURL.Text);
            query.Parameters.AddWithValue("@otdel", cmbOtdel.SelectedValue);
            query.Parameters.AddWithValue("@class", cmbKlas.SelectedValue);
            query.Parameters.AddWithValue("@razred", cmbRazred.SelectedValue);
            query.Parameters.AddWithValue("@family", cmbSemeistvo.SelectedValue);
            query.Parameters.AddWithValue("@rod", cmbRod.SelectedValue);
            query.Parameters.AddWithValue("@vid", cmbVid.SelectedValue);
            query.Parameters.AddWithValue("@info", txtDescription.Text);
            query.Parameters.AddWithValue("@DateReg", DateAndTime.Now);

            query.ExecuteNonQuery();
            MessageBox.Show("Input is OK");

            connect.Close();

            

            
        }
    }
}
