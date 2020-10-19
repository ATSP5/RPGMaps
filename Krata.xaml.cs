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
using System.Drawing;
namespace RPGMaps
{
    /// <summary>
    /// Interaction logic for Krata.xaml
    /// </summary>
    public partial class Krata : Window
    {
        public static int kol=0;
        public static int wier=0;
        public static bool ru=true;
        public Krata()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)//Anuluj
        {
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)//Rysuj kratę
        {
            kol = int.Parse(textBox2.Text);
            wier = int.Parse(textBox1.Text);
            ru = true;
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)//Usuń kratę
        {
            ru = false;
            this.Close();
        }
    }
}
