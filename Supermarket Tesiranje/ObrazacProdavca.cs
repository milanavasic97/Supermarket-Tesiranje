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
    public partial class ObrazacProdavca : Form
    {
        public ObrazacProdavca()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Milana vasic\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void nalepiti() //ovo je za to da bi se prikazale kategorije u kvadratu pored
        {
            Con.Open();
            string query = "select * from ProdavacTb";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder buider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdavacDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ObrazacProdavca_Load(object sender, EventArgs e)
        {
            nalepiti();
        }

        private void ProdavacDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PId.Text = ProdavacDGV.SelectedRows[0].Cells[0].Value.ToString();
            PIme.Text = ProdavacDGV.SelectedRows[0].Cells[1].Value.ToString();
            PGodine.Text = ProdavacDGV.SelectedRows[0].Cells[2].Value.ToString();
            PTelefon.Text = ProdavacDGV.SelectedRows[0].Cells[3].Value.ToString();
            PLozinka.Text = ProdavacDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (PId.Text == "" || PIme.Text == "" || PTelefon.Text == "" || PGodine.Text == "" || PLozinka.Text == "")
                {
                    MessageBox.Show("Unesite sve podatke");
                }
                else
                {
                    Con.Open();
                    string query = "insert into ProdavacTb values(" + PId.Text + " , '" + PIme.Text + "', '" + PTelefon.Text + "', '" + PLozinka.Text + "', '" + PGodine.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prodavac dodat uspesno");
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
                if (PId.Text == "" || PIme.Text == "" || PTelefon.Text == "" || PGodine.Text == "" || PLozinka.Text == "")
                {
                    MessageBox.Show("Nedostaju informacije");
                }
                else
                {
                    Con.Open();
                    string query = "update ProdavacTb set ProdavacGodine=" + PGodine.Text + ", ProdavacIme= '" + PIme.Text + "', ProdavacTelefon = '" + PTelefon.Text + "', ProdavacLozinka = '" + PLozinka.Text + "' where ProdavacId= " + PId.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prodavac uspesno izmenjen");
                    Con.Close();
                    nalepiti();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (PId.Text == "")
                {
                    MessageBox.Show("Selektuj prodavca kojeg zelis da obrises");
                }
                else
                {
                    Con.Open();
                    string query = "delete from ProdavacTb where ProdavacId ="+PId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prodavac uspesno obrisan");
                    Con.Close();
                    nalepiti();
                  


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm proizvodi = new ProductForm();
            proizvodi.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            KategorijeForm kategorije = new KategorijeForm();
            kategorije.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormaLogin1 login = new FormaLogin1();
            login.Show();
        }
    }
}
