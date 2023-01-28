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
    public partial class Form1 : Form
    {
        List<string> studentDatas = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Opp Final;Integrated Security=SSPI;");
        public void addDataInList()
        {
            if(dataGridView1.Rows.Count > 1)
            {
                studentDatas.Clear();
                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    studentDatas.Add((string)dataGridView1["Id", i].Value);
                }
            }
        }
        public void removeDataInList(string data)
        {
            int counter = 0;
            foreach(string info in studentDatas)
            {
                if(info == data)
                {
                    studentDatas.RemoveAt(counter);
                }
                counter++;
            }
        }
        public void showDatas(string data)
        {
            SqlDataAdapter da = new SqlDataAdapter(data, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Öğrenci");
            dataGridView1.DataSource = ds.Tables["Öğrenci"];
            addDataInList();
            studentCount.Text = $"Toplam Öğrenci : {(studentDatas.Count() > 0 ? studentDatas.Count() - 1 : 0)}";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showDatas("select * from Öğrenci");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            showDatas("select * from Öğrenci");
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            showDatas("select * from Öğrenci");
            con.Close();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Öğrenci where Id = @id", con);
            cmd.Parameters.AddWithValue("@id", textBox4.Text);
            textBox4.Clear();
            removeDataInList(textBox4.Text);
            cmd.ExecuteNonQuery();
            Console.WriteLine(dataGridView1.RowCount);
            showDatas("select * from Öğrenci");
            if(dataGridView1.RowCount <= 1)
            {
                SqlCommand cmd1 = new SqlCommand("truncate table Öğrenci", con);
                cmd1.ExecuteNonQuery();
                studentDatas.Clear();
                showDatas("select * from Öğrenci");
            }
            con.Close();
            constId.Clear();
            id.Clear();
            bolum.Clear();
            isim.Clear();
            not.Clear();
            harf.Clear();
            devamsizlik.Clear();
        }

        private string autoNot(string point)
        {
            int convPoint = Convert.ToInt32(point);
            if (convPoint >= 90)
            {
                return "AA";
            }
            else if(convPoint >= 85)
            {
                return "BA";
            }
            else if(convPoint >= 80)
            {
                return "BB";
            }
            else if(convPoint >= 75)
            {
                return "CB";
            }
            else if(convPoint >= 70)
            {
                return "CC";
            }
            else if(convPoint >= 65)
            {
                return "DC";
            }
            else if(convPoint >=60)
            {
                return "DD";
            }
            else if(convPoint >= 50)
            {
                return "FD";
            }
            else if(convPoint > 0)
            {
                return "FF";
            }
            return "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int chosenCell = dataGridView1.SelectedCells[0].RowIndex;

            constId.Text = dataGridView1.Rows[chosenCell].Cells[0].Value.ToString();
            id.Text = dataGridView1.Rows[chosenCell].Cells[1].Value.ToString();
            bolum.Text = dataGridView1.Rows[chosenCell].Cells[2].Value.ToString();
            isim.Text = dataGridView1.Rows[chosenCell].Cells[3].Value.ToString();
            tarih.Text = dataGridView1.Rows[chosenCell].Cells[4].Value.ToString();
            not.Text = dataGridView1.Rows[chosenCell].Cells[5].Value.ToString();
            harf.Text = dataGridView1.Rows[chosenCell].Cells[6].Value.ToString();
            devamsizlik.Text = dataGridView1.Rows[chosenCell].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Öğrenci set Id = @id, Bölüm = @bolum, [Ad Soyad] = @isim, [Kayıt Tarihi] = @tarih, [Ortalama Not] = @not, [Notun Harf Karşılığı] = @harf, Devamsızlık = @devamsizlik where [*] = @constid", con);
            cmd.Parameters.AddWithValue("@id", id.Text);
            cmd.Parameters.AddWithValue("@bolum", bolum.Text);
            cmd.Parameters.AddWithValue("@isim", isim.Text);
            cmd.Parameters.AddWithValue("@tarih", tarih.Text);
            try
            {
                cmd.Parameters.AddWithValue("@not", Convert.ToInt32(not.Text));
            }
            catch(Exception x)
            {
                not.Text = "Geçerli bir veri giriniz.";
                Console.WriteLine(x);
            }
            cmd.Parameters.AddWithValue("@harf", harf.Text);
            try
            {
                cmd.Parameters.AddWithValue("@devamsizlik", Convert.ToInt32(devamsizlik.Text));
            }
            catch (Exception x)
            {
                devamsizlik.Text = "Geçerli bir veri giriniz.";
                Console.WriteLine(x);
            } 
            cmd.Parameters.AddWithValue("@constid", constId.Text);
            cmd.ExecuteNonQuery();
            showDatas("select * from Öğrenci");
            con.Close();
            id.Clear();
            bolum.Clear();
            isim.Clear();
            not.Clear();
            harf.Clear();
            devamsizlik.Clear();
        }

        private void not_TextChanged(object sender, EventArgs e)
        {
            if(not.Text != "" && not.Text != "-")
            {
                try
                {
                    Convert.ToInt32(not.Text);
                    if (not.Text != "-" && not.Text != "" && Convert.ToInt32(not.Text) > 0 && Convert.ToInt32(not.Text) <= 100)
                    {
                        harf.Text = autoNot(not.Text);
                    }
                }
                catch (Exception x)
                {
                    not.Text = "Geçerli bir veri giriniz.";
                    Console.WriteLine(x);
                }
            }
        }

        private void devamsizlik_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(devamsizlik.Text != "")
                {
                    Convert.ToInt32(devamsizlik.Text);
                }
            }
            catch(Exception x)
            {
                devamsizlik.Text = "Geçerli bir veri giriniz.";
                Console.WriteLine(x);
            }
        }
    }
}