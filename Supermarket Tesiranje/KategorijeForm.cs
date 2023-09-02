using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Supermarket_Tesiranje
{
    public partial class KategorijeForm : Form
    {
        public KategorijeForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Milana vasic\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string query = "insert into KategorijaTb values(" + CatIdTb.Text + " , '" + CatImeTb.Text + "','" + CatOpisTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kategorija dodata uspesno");
                Con.Close();
                nalepiti();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void nalepiti() //ovo je za to da bi se prikazale kategorije u kvadratu pored
        {
            Con.Open();
            string query = "select * from KategorijaTb";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder buider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            KatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void KategorijeForm_Load(object sender, EventArgs e)
        {
            nalepiti();
        }

        private void KatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = KatDGV.SelectedRows[0].Cells[0].Value.ToString();
            CatImeTb.Text = KatDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatOpisTb.Text = KatDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (CatIdTb.Text == "")
                {
                    MessageBox.Show("Selektuj kategoriju koju zelis da obrises");
                }
                else
                {
                    Con.Open();
                    string query = "delete from KategorijaTb where KategorijaId = " + CatIdTb.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kategorija uspesno obrisana");
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
                if (CatIdTb.Text == "" || CatImeTb.Text == "" || CatOpisTb.Text == "")
                {
                    MessageBox.Show("Nedostaju informacije");
                }
                else
                {
                    Con.Open();
                    string query = "update KategorijaTb set KategorijaIme='" + CatImeTb.Text + "', KategorijaOpis='" + CatOpisTb.Text + "' where KategorijaId= " + CatIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kategorija uspesno izmenjena");
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
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            ProdajnaForma prodavci = new ProdajnaForma();
            prodavci.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormaLogin1 login = new FormaLogin1();
            login.Show();
        }
    }
}
