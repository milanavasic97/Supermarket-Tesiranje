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
    public partial class ProdajnaForma : Form
    {
        public ProdajnaForma()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Milana vasic\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void nalepiti() //ovo je za to da bi se prikazale kategorije u kvadratu pored
        {
            Con.Open();
            string query = "select ProductIme,ProductKolicina from ProizvodTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder buider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProizDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void nalepitiRacun() 
        {
            Con.Open();
            string query = "select * from  RacunT";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder buider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            racuniDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ProdajnaForma_Load(object sender, EventArgs e)
        {
            nalepiti();
            nalepitiRacun();
            fillcombo();
            ImeProdavca.Text = FormaLogin1.ProdavacIme;
        }

        private void ProizDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdIme.Text = ProizDGV.SelectedRows[0].Cells[0].Value.ToString();
            prodCena.Text = ProizDGV.SelectedRows[0].Cells[1].Value.ToString();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Datum.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
        int Grdtotal = 0, n = 0; //da ne bi moglo da se unese proi bez cene da iskoci poruka

        private void button7_Click(object sender, EventArgs e)
        {
            if (RacunId.Text == "")
            {
                MessageBox.Show("Nedostaje racun ID");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into RacunT values(" + RacunId.Text + " , '" + ImeProdavca.Text + "', '" + Datum.Text + "', '" + iznos.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Narudzbina dodata uspesno");
                    Con.Close();
                    nalepitiRacun();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nalepiti();
        }

        private void KatCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select ProductIme, ProductKolicina from ProizvodTbl  where ProductKategorija = '" + KatCombo.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder buider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProizDGV.DataSource = ds.Tables[0];
            Con.Close();
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

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormaLogin1 login = new FormaLogin1();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (RacunId.Text == "")
                {
                    MessageBox.Show("Selektuj racun koji zelis da obrises");
                }
                else
                {
                    Con.Open();
                    string query = "delete from RacunT where RacunIdTb = " + RacunId.Text + "";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Prodaja uspesno obrisana");
                    Con.Close();
                    nalepitiRacun();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ProdIme.Text == "" || ProdKolicina.Text == "")
            {
                MessageBox.Show("Nedostaju podaci");
            }
            else
            {
                int total = Convert.ToInt32(prodCena.Text) * Convert.ToInt32(ProdKolicina.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(NarudzbineDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdIme.Text;
                newRow.Cells[2].Value = prodCena.Text;
                newRow.Cells[3].Value = ProdKolicina.Text;
                newRow.Cells[4].Value = Convert.ToInt32(prodCena.Text) * Convert.ToInt32(ProdKolicina.Text);
                NarudzbineDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + total;
                iznos.Text = ""+ Grdtotal;
            }
        }
    }
}
