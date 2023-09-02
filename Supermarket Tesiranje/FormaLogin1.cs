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
    public partial class FormaLogin1 : Form
    {
        public FormaLogin1()
        {
            InitializeComponent();
        }
        public static string ProdavacIme = "";
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Milana vasic\Documents\marketdb.mdf;Integrated Security=True;Connect Timeout=30");
        private object webDriver;

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            UsernameTb.Text = "";
            LozinkaTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UsernameTb.Text == "" || LozinkaTb.Text == "")
            {
                MessageBox.Show("Unesi username i lozinku!");
            }
            else
            {
                if (SelektujUlogu.SelectedIndex > -1)
                {
                    if (SelektujUlogu.SelectedItem.ToString() == "ADMIN")
                    {
                        if (UsernameTb.Text == "Admin" && LozinkaTb.Text == "Admin")
                        {
                            ProductForm proizvodi = new ProductForm();
                            proizvodi.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Ako si ti Admin,unesi ispravan username i lozinku");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("U odeljku za prodavce");
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select count(8) from ProdavacTb where ProdavacIme='" + UsernameTb.Text + "' and ProdavacLozinka = '" + LozinkaTb.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            ProdavacIme = UsernameTb.Text;
                            ProdajnaForma prodati = new ProdajnaForma();
                            prodati.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Pogresan Username ili lozinka");
                        }
                        Con.Close();

                    }


                }
                else
                {
                    MessageBox.Show("Selektuj Ulogu");
                }

            }
        }


        private void SelektujUlogu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
