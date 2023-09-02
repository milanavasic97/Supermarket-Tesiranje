using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket_Tesiranje
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Milana vasic\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void fillcombo()
        {
            //ovaj metod je za combobox sa bazom podataka,jeste da se ukategoriji povezu sve one koje su napravljenje kat u bazi vec kao padajuci meni
            Con.Open();
            SqlCommand cmd = new SqlCommand("select KategorijaIme from KategorijaTb", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("KategorijaIme", typeof(string));
            dt.Load(rdr);
            KatCombo.ValueMember = "KategorijaIme";
            KatCombo.DataSource = dt;
            Con.Close();
          
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();
            nalepiti();
        }

        private void button2_Click(object sender, EventArgs e) // jeste kada smo u prodaju da se vratimo na kategorije
        {
            KategorijeForm kat = new KategorijeForm();
            kat.Show();
            this.Hide();
        }
        private void nalepiti() //ovo je za to da bi se prikazale kategorije u kvadratu pored
        {
            Con.Open();
            string query = "select * from ProizvodTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder buider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into ProizvodTbl values(" + ProdId.Text + " , '" + ProdIme.Text + "', '" + ProdKolicina.Text + "', '" + prodCena.Text + "', '" + KatCombo.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Proizvod dodat uspesno");
                Con.Close();
                nalepiti();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            ProdIme.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            ProdKolicina.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            prodCena.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            KatCombo.SelectedValue = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "")
                {
                    MessageBox.Show("Selektuj proizvod koju zelis da obrises");
                }
                else
                {
                    Con.Open();
                    string query = "delete from ProizvodTbl where ProductId = " + ProdId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Proizvod uspesno obrisan");
                    Con.Close();
                    nalepiti();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdId.Text == "" || ProdIme.Text == "" || ProdKolicina.Text == "" || prodCena.Text == "" || KatCombo.Text == "")
                {
                    MessageBox.Show("Nedostaju informacije");
                }
                else
                {
                    Con.Open();
                    string query = "update ProizvodTbl set ProductIme='" + ProdIme.Text + "', ProductKolicina='" + ProdKolicina.Text + "', ProductCena = '" + prodCena.Text + "' where ProductId= " + ProdId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Proizvod uspesno izmenjen");
                    Con.Close();
                    nalepiti();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

            this.Hide();
            FormaLogin1 login = new FormaLogin1();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            ObrazacProdavca prodavci = new ObrazacProdavca();
            prodavci.Show();
        }

        
    }
}
