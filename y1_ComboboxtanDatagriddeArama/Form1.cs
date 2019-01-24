using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Data;

namespace y1_ComboboxtanDatagriddeArama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server = YASEMINGOKTAS;  Database = NORTHWND; Trusted_Connection=true;");
        SqlCommand cmd;
        private void Form1_Load(object sender, EventArgs e)
        {
            ComboDoldur();
        }
        private void ComboDoldur()
        {
            cmd = new SqlCommand("Select CategoryID, CategoryName from Categories", conn);
            if (conn.State == ConnectionState.Closed)   // eğer connection kapalıysa aç.
            {
                conn.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())   // okuyacak bir şey kalmayana kadar verileri okuyarak 1 veya 0 döndürür. okuyacak bir şey yoksa 0 döner.
            {
                comboBoxCateName.Items.Add(dr[1].ToString());  // categoryname'leri combobox a dolduruyoruz.
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void comboBoxCateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrunListele();
            DataGridUrunListele();
        }
        private void UrunListele()
        {
            cmd = new SqlCommand("Select c.CategoryName, p.ProductName, p.UnitPrice, p.UnitsInStock From Products p inner join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = @CatName",conn);
            cmd.Parameters.AddWithValue("@CatName", comboBoxCateName.SelectedItem.ToString());
            // cmd.Parameters.AddWithValue("@CatName", comboBoxCateName.Text);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            listView1.Items.Clear();

            while (dr.Read())
            {
                ListViewItem kayit = new ListViewItem();
                kayit.Text = dr["CategoryName"].ToString(); // veya->  kayit.Text = dr[0].ToString();
                kayit.SubItems.Add(dr["ProductName"].ToString());
                kayit.SubItems.Add(dr["UnitPrice"].ToString());
                kayit.SubItems.Add(dr["UnitsInStock"].ToString());

                listView1.Items.Add(kayit);

             }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void DataGridUrunListele()
        {
            cmd = new SqlCommand("Select c.CategoryName, p.ProductName, p.UnitPrice, p.UnitsInStock From Products p inner join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = @CatName", conn);
            cmd.Parameters.AddWithValue("@CatName", comboBoxCateName.SelectedItem.ToString());
            // cmd.Parameters.AddWithValue("@CatName", comboBoxCateName.Text);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            int satir = 0;
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {
                satir = dataGridView1.Rows.Add();   // add otomatik olarak row eklediği için satir'i 1 arttırmaya gerek kalmıyor. add() int değer döndürür.
                // satir ++;
                dataGridView1.Rows[satir].Cells[0].Value = dr["CategoryName"].ToString();
                dataGridView1.Rows[satir].Cells[1].Value = dr["ProductName"].ToString();
                dataGridView1.Rows[satir].Cells[2].Value = dr["UnitPrice"].ToString();
                dataGridView1.Rows[satir].Cells[3].Value = dr["UnitsInStock"].ToString();
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

       
    }
}
