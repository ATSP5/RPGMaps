using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPGMaps
{
    /// <summary>
    /// Interaction logic for Kostki.xaml
    /// </summary>
    public partial class Kostki : Window
    {
        public static List<int> wyniki = new List<int>();
        public Kostki()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            textBox3.Clear();
            int kostka = int.Parse(textBox2.Text);
            int ilosckostek = int.Parse(textBox1.Text);
            int wynik = 0;
            int suma = 0;
            wyniki.Clear();
            Random rand = new Random();
            for (int i = 0; i < ilosckostek; i++)
            {
                wynik=rand.Next(1, kostka);
                suma += wynik;
                listBox1.Items.Add(wynik.ToString());
                wyniki.Add(wynik);
            }
            textBox3.AppendText(suma.ToString());
            textBox2.Clear();
            textBox1.Clear();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            listBox1.Items.Clear();
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
            this.Close();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (wyniki.Count != 0)
            {
                listBox1.Items.Clear();
                textBox3.Clear();
                textBox2.Clear();
                textBox1.Clear();
                int suma = 0;
                for (int i = 0; i < wyniki.Count; i++)
                {
                    suma += wyniki[i];
                    listBox1.Items.Add(wyniki[i].ToString());
                }
                textBox3.AppendText(suma.ToString());
            }
        }
    }
}
