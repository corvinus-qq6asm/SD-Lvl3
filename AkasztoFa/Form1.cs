using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace AkasztoFa
{
    public partial class Form1 : Form
    {
        int hiba = 0;
        int jo = 0;
        string szo;
        Random rnd = new Random();

        void button1_Click(object sender, EventArgs e)//Lista beolvasása és abból kiválasztunk egy szót
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = "C:\\";// File helyének megadása
            file.Filter = "txt files|*.txt";//File kiterjesztése
            if (file.ShowDialog()==DialogResult.OK)
            {
                string[] szavak = File.ReadAllLines(file.FileName, Encoding.UTF8);//Egy tömbbe berakom a szavakat, melynek helye a file.FileName
                szo = szavak[rnd.Next(szavak.Length)];//Véletlen szó kiválasztása+véletlen szám genrálása melynek max értéke a listában szereplő számok
            }
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Text = hiba.ToString()+"/10 db";
            label4.Visible = true;

            StreamReader sr = new StreamReader("bill.txt"); //Billentyűzet beolvasása

            while (!sr.EndOfStream)
            {
                Button gomb = new Button();

                gomb.Left = int.Parse(sr.ReadLine());
                gomb.Top = int.Parse(sr.ReadLine());
                gomb.Height = int.Parse(sr.ReadLine());
                gomb.Width = int.Parse(sr.ReadLine());

                gomb.Text = sr.ReadLine();

                Controls.Add(gomb);

                gomb.Click += Gomb_Click;
            }
            sr.Close();

            for (int i = 0; i < szo.Length; i++) //A kiválasztott szó betűinek "helye"
            {
                Label hely = new Label();
                hely.Width = 40;
                hely.Height = 20;
                hely.Top = 300;
                hely.Left = 100 + (i * hely.Width);
                hely.Text = "__";

                Controls.Add(hely);
            }
            button1.Enabled = false;
        }     
        void Gomb_Click(object sender, EventArgs e)
        {
            Button LenyomottBetu = (Button)sender;

            if (szo.IndexOf(char.Parse(LenyomottBetu.Text)) >= 0)//Megnézem, hogy a lenyomtt betű szerepel-e a kiválasztott szóban
            {
                LenyomottBetu.BackColor = Color.Green; //Ha benne van a betű zöldre festem 
                LenyomottBetu.Enabled = false;// és nem lehet többet rákattintani a gombra

                for (int i = 0; i < szo.Length; i++)
                {
                    if (szo[i]==char.Parse(LenyomottBetu.Text)) //Megnézem, hgy hányadik helyen van benne a betű és megjelenítem
                    {
                        Label SzoBetu = new Label();
                        SzoBetu.Width = 40;
                        SzoBetu.Height = 30;
                        SzoBetu.Top = 285;
                        SzoBetu.Left = 100 + (i * SzoBetu.Width);
                        SzoBetu.Text = LenyomottBetu.Text;
                        jo++;//Léptetem ezt a változót, amikor egy olyan betűre kattintunk ami benne van a szóban

                        Controls.Add(SzoBetu);

                        if (jo==szo.Length)//Ha a változó értéke eléri a szó betűinek számát, akkor kiírja egy msgbox, hogy nyertünk 
                        {
                            MessageBox.Show("Nyertél");
                        }
                    } 
                }
            }
            else
            {
                LenyomottBetu.BackColor = Color.Red;//Ha a betű nincsen benne, akkor pirosra festem
                LenyomottBetu.Enabled = false;//és nem lehet többet rákattintani a gombra

                Label ElhasznaltBetu = new Label();//Kirakom az elhasznált betűket
                ElhasznaltBetu.Width = 20;
                ElhasznaltBetu.Height = 20;
                ElhasznaltBetu.Top = 330;
                ElhasznaltBetu.Left = 100 + (hiba * ElhasznaltBetu.Width);
                ElhasznaltBetu.BackColor = Color.Fuchsia;
                ElhasznaltBetu.Text = LenyomottBetu.Text;

                Controls.Add(ElhasznaltBetu);

                hiba++;//Léptetem ezt a változót, amikor egy olyan betűre kattintunk ami nincs benne a szóban
                label4.Text = hiba.ToString()+"/10 db";
                rajzolas(hiba);//A hiba számától függően "rajzol" a programunk
                if (hiba==10)//Ha a hibák száma eléri a tizet, akkor kiírja egy msgbox, hogy veszítettünk
                {
                    MessageBox.Show("Veszítettél \r\nA megoldás "+szo+" volt");               
                }               
            }  
        }
        public void rajzolas(int hiba)//Ez a program rész felel a rajzolásért
        {
            Graphics g = this.CreateGraphics();
            Pen toll = new Pen(Color.Black, 5);
            
            if (hiba==1)//Oszlop rajzolása, majd return
            {
                g.DrawLine(toll,600,50,600,300); return;
                
            }
            if (hiba == 2)//Talp rajzolása, majd return
            {
                g.DrawLine(toll,525,300,675,300); return;
                
            }
            if (hiba == 3)//Vízszintes gerenda rajzolása, majd return
            {
                g.DrawLine(toll, 600, 50, 650, 50); return;
            }
            if (hiba == 4)//Kötél rajzolása, majd return
            {
                g.DrawLine(toll, 650, 50, 650, 75); return;
            }
            if (hiba == 5)//Fej rajzolása, majd return
            {
                Point point1 = new Point(650, 75);
                Point point2 = new Point(675, 100);
                Point point3 = new Point(650, 125);
                Point point4 = new Point(625, 100);
                Point[] curvePoints = { point1, point2, point3, point4, };
                g.DrawClosedCurve(toll, curvePoints); return;
            }
            if (hiba == 6)//Test rajzolása, majd return
            {
                g.DrawLine(toll, 650, 125, 650, 195); return;
            }
            if (hiba == 7)//Bal láb rajzolása, majd return
            {
                g.DrawLine(toll, 650, 195, 600, 240); return;
            }
            if (hiba == 8)//Jobb láb rajzolása, majd return
            {
                g.DrawLine(toll, 650, 195, 700, 240); return;
            }
            if (hiba == 9)//Bal kar rajzolása, majd return
            {
                g.DrawLine(toll, 650, 135, 610, 200); return;
            }
            if (hiba==10)//Jobb kar rajzolása, majd return
            {
                g.DrawLine(toll, 650, 135, 690, 200); return;
            } 
        }
        public Form1()
        {
            InitializeComponent();
        }  
        private void button2_Click(object sender, EventArgs e)//Új játékot kezdhetünk ezzel a  gombbal
        {
            Form1 NewForm = new Form1();
            NewForm.Show();
            this.Dispose(false);
        }
    }
}