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

namespace OPPFinal
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Opp Final;Integrated Security=SSPI;");
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Öğrenci", con);
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("insert into Öğrenci (Id, Bölüm, [Ad Soyad], [Kayıt Tarihi], [Ortalama Not], [Notun Harf Karşılığı], Devamsızlık)  values (@1, @2, @3, @4, @5, @6, @7)", con);
            
            cmd.Parameters.AddWithValue("@1", this.id.Text);
            cmd.Parameters.AddWithValue("@2", this.bolum.Text);
            cmd.Parameters.AddWithValue("@3", this.isim.Text);
            cmd.Parameters.AddWithValue("@4", this.tarih.Value);
            cmd.Parameters.AddWithValue("@5", DBNull.Value);
            cmd.Parameters.AddWithValue("@6", DBNull.Value);
            cmd.Parameters.AddWithValue("@7", DBNull.Value);
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
            this.Close();
            var form1 = new Form1();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            var form1 = new Form1();
            form1.Show();
        }
    }
}
