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

namespace TriggerKullanimi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=TriggerKullanimi;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TBLKITAP", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void sayac()
        {
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select COUNT(*) from TBLKITAP", baglanti);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    lblKitapAdet.Text = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolü
            if (string.IsNullOrEmpty(textBoxAD.Text) || string.IsNullOrEmpty(textBoxYAZAR.Text) ||
                string.IsNullOrEmpty(textBoxSAYFA.Text) || string.IsNullOrEmpty(textBoxYAYIN.Text) ||
                string.IsNullOrEmpty(textBoxTUR.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=TriggerKullanimi;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into TBLKITAP (AD,YAZAR,SAYFA,YAYIN,TUR) values (@P1,@P2,@P3,@P4,@P5)", baglanti);
                    komut.Parameters.AddWithValue("@P1", textBoxAD.Text);
                    komut.Parameters.AddWithValue("@P2", textBoxYAZAR.Text);
                    komut.Parameters.AddWithValue("@P3", textBoxSAYFA.Text);
                    komut.Parameters.AddWithValue("@P4", textBoxYAYIN.Text);
                    komut.Parameters.AddWithValue("@P5", textBoxTUR.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Ekleme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                listele();
                sayac();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Silme işlemi için onay mesajı
            DialogResult dialogResult = MessageBox.Show("Bu kitabı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-KIMUOA0\SQLEXPRESS;Initial Catalog=TriggerKullanimi;Integrated Security=True"))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("delete from TBLKITAP where ID=@P1", baglanti);
                    komut.Parameters.AddWithValue("@P1", textBoxID.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Silme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                listele();
                sayac();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sayac();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            textBoxID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBoxAD.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBoxYAZAR.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBoxSAYFA.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            textBoxYAYIN.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            textBoxTUR.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }
    }
}
