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

namespace Laba2
{
    public partial class Form1 : Form
    {
        class Car
        {
            public string code { get; set; }
            public string brand { get; set; }
            public string model { get; set; }
            public Car(string c, string b, string m)
            {
                code = c;
                brand = b;
                model = m;
            }
            public override string ToString()
            {
                return code + ": " + brand + " " + model;
            }
        }

        class Info
        {
            public string code { get; set; }
            public string bodywork { get; set; }
            public string engine_volume { get; set; }
            public string power { get; set; }
            public Info(string c, string b, string e, string p)
            {
                code = c;
                bodywork = b;
                engine_volume = e;
                power = p;
            }
            public override string ToString()
            {
                return code + ": " + bodywork + " " + engine_volume + " " + power;
            }
        }

        List<Car> lc;
        List<Info> li;

        public void ReadCar()
        {
            try
            {
                using (StreamReader sr = new StreamReader("brandAndModel.txt"))
                {
                    string line;
                    string[] fields;
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        fields = line.Split(';');
                        lc.Add(new Car(fields[0], fields[1], fields[2]));
                        listBox1.Items.Add(line);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(ex.Message);
            }
        }

        public void ReadInfo()
        {
            try
            {
                using (StreamReader sr = new StreamReader("characteristic.txt"))
                {
                    string line;
                    string[] fields;
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        fields = line.Split(';');
                        li.Add(new Info(fields[0], fields[1], fields[2], fields[3]));
                        listBox2.Items.Add(line);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                listBox2.Items.Clear();
                listBox2.Items.Add(ex.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            lc = new List<Car>();
            ReadCar();
            li = new List<Info>();
            ReadInfo();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Вывод автомобилей с кузовом типа "купе"
            listBox3.Items.Clear();
            var result = from car in lc
                         from info in li
                         where info.bodywork == " coupe" && info.code == car.code
                         select car;
            foreach (var item in result)
            {
                listBox3.Items.Add(item.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Вывод автомобилей с мощностью больше 100, но меньше 200 л.с.
            listBox3.Items.Clear();
            var result = from car in lc
                         from info in li
                         where int.Parse(info.power.Substring(info.power.IndexOf(" ") + 1, info.power.Length - info.power.IndexOf(" ") - 5)) > 100 &&
                               int.Parse(info.power.Substring(info.power.IndexOf(" ") + 1, info.power.Length - info.power.IndexOf(" ") - 5)) < 200 &&
                               info.code == car.code
                         select car;
            foreach (var item in result)
            {
                listBox3.Items.Add(item.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Вывод авто, сгруппированных по марке
            listBox3.Items.Clear();
            var result = from car in lc
                         group car by car.brand;
            foreach (IGrouping<string, Car> car in result)
            {
                listBox3.Items.Add(" " + car.Key);
                foreach (var item in car)
                {
                    listBox3.Items.Add(item.ToString());
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Вывод марки и модели авто, объём двигателя которых = 1.6 л
            listBox3.Items.Clear();
            var result = from car in lc
                         from info in li
                         where info.engine_volume == " 1.6 l" && car.code == info.code
                         select car;
            foreach (var item in result)
            {
                listBox3.Items.Add(item.brand + " " + item.model);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Вывод внедорожников с мощностью выше 120 л.с.
            listBox3.Items.Clear();
            var result = from car in lc
                         from info in li
                         where int.Parse(info.power.Substring(info.power.IndexOf(" ") + 1, info.power.Length - info.power.IndexOf(" ") - 5)) > 120 &&
                               info.bodywork == " suv" &&
                               info.code == car.code 
                         select car;
            foreach (var item in result)
            {
                listBox3.Items.Add(item.ToString());
            }
        }
    }
}
