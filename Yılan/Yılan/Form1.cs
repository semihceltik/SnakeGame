using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Yılan
{
    public partial class Form1 : Form
    {
        #region DEĞİŞKENLER
        
        public int hız = 200;
        int skor = 0;
        int süre = 0;
        int süre2 = 1;
        public  bool durdurma;
        public bool oyun = true;


       
        #endregion
        #region YÖNBİLGİLERİ

        public enum direction
        {

            up,
            down,
            right,
            left
        }
        public direction yön = direction.right;
        public int x = 1, y = 1;

        #endregion
        #region KUYRUK OLUŞTURMA
        public List<Konum> kuyruk = new List<Konum>();
        #endregion

        #region KONUMYARATMA
        public class Konum
        {
            
            
            public int xxx = 0;
            public int yyy = 0;

            public Konum(int xs, int ys)
            {
                
                this.xxx = xs;
                this.yyy = ys;
            }
        }
        public Konum yemek = new Konum(-1, -1); // Yemek oluşturmak için kullanılan konum

        #endregion
        #region FORM1
        public Form1()
        {
            InitializeComponent();
            #region KUYRUK EKLEME
            kuyruk.Add(new Form1.Konum(0, 0));
            #endregion
        }
        #endregion
        #region HAREKET ETME
        public void oyunubaslat()
        {


            Thread thread = new Thread(new ThreadStart(new Action(() =>
            {
                while (oyun) // SINIRSIZ DONGÜ
                {
                    if (yön == direction.right)
                    {
                        x++;
                    }
                    if (yön == direction.left)
                    {
                        x--;
                    }
                    if (yön == direction.up)
                    {
                        y--;
                    }
                    if (yön == direction.down)
                    {
                        y++;
                    }

                    calculatetable();
                    Thread.Sleep(hız);
                }



            })));
            thread.Start();

        }
        #endregion
        #region OYUNBAŞLATMA
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode.ToString() == "B")
            {
                if (durdurma==true)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                   
                }
                timer1.Enabled = true;
                timer2.Enabled = true;
                label5.Visible = false; // B YE VE D YE BASMAYI GİZLİYORUM
                label6.Visible = false; // B YE VE D YE BASMAYI GİZLİYORUM
                panel1.Visible = false;
                oyunubaslat();
                
            }

            
            if (e.KeyCode.ToString() == "D")
            {
                if (durdurma==false)
                {
                    this.Focus();
                    durdurma = true;
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    oyun = false;
                    

                }
                else if(durdurma==true)
                {
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    oyun = true;
                    durdurma = false;
                    oyunubaslat();
                    
                }


            }
            if (e.KeyCode == Keys.Down)
            {
                yön = direction.down;
            }
            if (e.KeyCode == Keys.Up)
            {
                yön = direction.up;
            }
            if (e.KeyCode == Keys.Right)
            {
                yön = direction.right;
            }
            if (e.KeyCode == Keys.Left)
            {
                yön = direction.left;
            }
        }
        #endregion
        public void herseyisıfırla()
        {
           
            x = 1;
            y = 1;
            süre = 0;
            süre2 = 1;
            skor = 0;
            yön = direction.right;
            kuyruk = new List<Konum>();
            kuyruk.Add(new Form1.Konum(0, 0));
            calculatetable();
            oyunubaslat();
            
            
            oyun = true;

        }
        #region GEREKSİZLER
        
        private void Form1_Load(object sender, EventArgs e)
        {
         
           
            
        }

        private void game_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region OYUNSÜRESİ
        private void timer1_Tick(object sender, EventArgs e)
        {
            süre++;
            label3.Text = Convert.ToString(süre);
        }
        #endregion
        #region SKORSÜRESİ
        private void timer2_Tick(object sender, EventArgs e)
        {
            süre2++;
            label4.Text = Convert.ToString(skor);

        }
        #endregion
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void SA_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            if (kontrol.Text == "kolay")
            {
               
                panel1.Visible = false;
                button1.Enabled = false;
                textbox.Enabled = false;
                this.Focus();
            }
            else if (kontrol.Text == "zor")
            {
                panel1.Visible = false;
                button1.Enabled = false;
                textbox.Enabled = false;
                this.Focus();
            }
            else
            {
                MessageBox.Show("LÜTFEN SEVİYE SEÇİNİZ", "HATA");
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("yilan.txt");  // KAYIT TUTMAK İÇİN YAPIYORUZ
            sw.Write("KULLANICI:");
            sw.WriteLine(textbox.Text);
            sw.Write("SEVİYE:");
            sw.WriteLine(kontrol.Text);
            sw.Write("SÜRE:");
            sw.WriteLine(label3.Text);
            sw.Write("SKOR:");
            sw.WriteLine(skor);
            sw.Close();

            herseyisıfırla();
            oyun = true;
            panel2.Visible = false;
            this.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            hız = 200;
            kontrol.Text = "kolay";

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            hız = 80;
            kontrol.Text = "zor";
        }

        private void textbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textbox_Enter(object sender, EventArgs e)
        {
            string isim = textbox.Text;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            StreamReader oku = new StreamReader("yilan.txt");
            Console.Write(oku);
        }


        #region OYUN
        public void calculatetable()
        {
            if (kuyruk.Count !=1)
            {
                for (int i = 1; i < kuyruk.Count; i++)
                {
                    if (kuyruk[i].xxx == x && kuyruk[i].yyy == y)
                    {
                        Invoke(new Action(() => {

                            oyun = false;
                            panel2.Visible = true;
                        }));

                    }
                }
            }
            if (x == yemek.xxx && y == yemek.yyy)
            {
                if (yemek.xxx == 1 || yemek.xxx == 500 || yemek.yyy == 1 || yemek.yyy == 500)
                {
                    skor = skor + 10;
                }
                if (süre2 >=100)
                {
                    skor = skor + 0;
                }
                else
                {
                    skor = skor + 100 / süre2;
                }
                
                süre2 = 1; // 1 eşitledim çünkü art arda doğan yemler ZeroException hatası alıyordum.
                yemek = new Konum(-1, -1);
                kuyruk.Add(new Form1.Konum(yemek.xxx, yemek.yyy));

                
            }

            Random rnd = new Random();
            Bitmap bitmap = new Bitmap(500, 500);
            kuyruk[0] = new Konum(x, y);
            if (x <= 0 || y <= 0 || x == 51 || y == 51)
            {
                Invoke(new Action(() => {

                    oyun = false;
                    panel2.Visible = true;


                }));
              
              
                    
            }
            else
            {
                for (int i = kuyruk.Count - 1; i > 0; i--)
                {
                    kuyruk[i] = kuyruk[i - 1];
                }

                for (int k = 0; k < kuyruk.Count; k++)
                {
                    for (int i = (kuyruk[k].xxx - 1) * 10; i < kuyruk[k].xxx * 10; i++)
                    {
                        for (int j = (kuyruk[k].yyy - 1) * 10; j < kuyruk[k].yyy * 10; j++)
                        {
                            bitmap.SetPixel(i, j, Color.Red);
                        }
                    }

                }
            }
            if (yemek.xxx == -1)
            {
                yemek = new Konum(rnd.Next(1, 50), rnd.Next(1, 50));
            }
            for (int i = (yemek.xxx - 1) * 10; i < yemek.xxx * 10; i++)
            {
                for (int j = (yemek.yyy - 1) * 10; j < yemek.yyy * 10; j++)
                {
                    bitmap.SetPixel(i, j, Color.Blue);
                }

            }
            game.Image = bitmap;
            #endregion

        }
    }
}
