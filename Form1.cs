using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;//Acces ıcın kutuphanemızı eklıyoruz..

namespace Hasta_kayıt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection banlantim = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=hasta_kayit.mdb");
        OleDbDataAdapter oku;
        DataTable tablo;
        OleDbCommand komut;

        void Kayitgoster()
        {
            banlantim.Open();
            oku = new OleDbDataAdapter("select *from hasta_bilgiler", banlantim);
            tablo = new DataTable();
            oku.Fill(tablo);

            dataGridView1.DataSource = tablo;
            banlantim.Close();
        }

        void Sil()
        {

            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

            pictureBox1.ImageLocation = "";

        }


        private void button1Goster_Click(object sender, EventArgs e)
        {
            Kayitgoster();
        }

        private void button2Kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                banlantim.Open();
                oku = new OleDbDataAdapter("insert into hasta_bilgiler(Tc_No,Ad,Soyad,Telefon,Adres,posta,ytarih,ctarih,ilac,Teshis,ucret,resim)  values('" + txttc.Text + "','" + txtad.Text + "','" + txtsoyad.Text + "','" + txttelefon.Text + "','" + txtadres.Text + "','" + txteposta.Text + "','" + dateTimePicker1.Value + "','" + dateTimePicker1.Value + "','" + txtilac.Text + "','" + txtteshis.Text + "','" + txtucret.Text + "','" + txtresim.Text + "')", banlantim);
                tablo = new DataTable();
                oku.Fill(tablo);
                dataGridView1.DataSource = tablo;
                banlantim.Close();
                Kayitgoster();
                Sil();

                label15durum.Text = "Hasta Kayıt edildi";
            }
            catch (Exception)
            {

                MessageBox.Show("Bilgileri dogru  eksiksiz girin.!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void button5ResimEkle_Click(object sender, EventArgs e)
        {
            OpenFileDialog resim = new OpenFileDialog();
            resim.Filter = "jpg Dosyası |*.jpg|png Dosyası |*.png";

            if (resim.ShowDialog() == DialogResult.OK)
            {
                txtresim.Text = resim.FileName;
                pictureBox1.ImageLocation = resim.FileName;//FileName Dosya yolu Demek

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Kayitgoster();
        }

        private void button3Ara_Click(object sender, EventArgs e)
        {
            try
            {
                banlantim.Open();

                tablo = new DataTable();
                oku = new OleDbDataAdapter("select *from hasta_bilgiler where Ad like '" + textBox7.Text + "' or Telefon like '" + textBox7.Text + "'", banlantim); //aramayı krıter cogalt (2-3)
                oku.Fill(tablo);
                dataGridView1.DataSource = tablo;

                banlantim.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Aradıgınız kayıt bulunamadı..", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void button6Sil_Click(object sender, EventArgs e)
        {
            DialogResult cvp = MessageBox.Show("Silmek istediginizden eminisiniz..", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (cvp == DialogResult.Yes)
            {
                banlantim.Open();

                tablo = new DataTable();
                oku = new OleDbDataAdapter("delete  *from hasta_bilgiler where Ad='" + textBox8.Text + "'", banlantim);
                oku.Fill(tablo);
                dataGridView1.DataSource = tablo;
                banlantim.Close();
                Kayitgoster();

                label15durum.Text = "Hasta Kayıt silindi..";

            }
            else
            {
                Kayitgoster();
                banlantim.Close();
            }



        }

        private void button4Guncelle_Click(object sender, EventArgs e)//Tc_No='"+txttc.Text+ "',
        {
            banlantim.Open();
            komut = new OleDbCommand("update hasta_bilgiler set  Ad='" + txtad.Text + "',Soyad='" + txtsoyad.Text + "',Telefon='" + txttelefon.Text + "',Adres='" + txtadres.Text + "',posta='" + txteposta.Text + "',ytarih='" + dateTimePicker1.Text + "',ctarih='" + dateTimePicker2.Text + "',ilac='" + txtilac.Text + "', Teshis='" + txtteshis.Text + "',ucret='" + txtucret.Text + "',resim='" + txtresim.Text + "' where  Tc_No='" + dataGridView1.CurrentRow.Cells[1].Value + "'", banlantim);
            komut.ExecuteNonQuery();
            banlantim.Close();
            Kayitgoster();

            MessageBox.Show("Güncelleme işlemi başarılı bravo :xD ", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //kod yazılacak..?
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txttc.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txttelefon.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtadres.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txteposta.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
            dateTimePicker2.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            txtilac.Text = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
            txtteshis.Text = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
            txtucret.Text = dataGridView1.Rows[secilen].Cells[11].Value.ToString();
            txtresim.Text = dataGridView1.Rows[secilen].Cells[12].Value.ToString();

            pictureBox1.ImageLocation = dataGridView1.Rows[secilen].Cells[12].Value.ToString();

        }
    }
}


