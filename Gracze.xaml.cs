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
    /// Interaction logic for Gracze.xaml
    /// </summary>
    [Serializable]
    public class Players
    {
        public string name;
        public int AC;
        public int HP;
        public int Ini;
        public Players(string imie, int klasapanc, int zycie, int inicj)
        {
            name = imie;
            AC = klasapanc;
            HP = zycie;
            Ini = inicj;
        }
    };
    public partial class Gracze : Window
    {
        public static List<Players> players = new List<Players>();
        ListBox lb = new ListBox();
        public Gracze()
        {
            InitializeComponent();
            for (int i = 0; i < players.Count; i++)
            {
                listBox1.Items.Add(players[i].name);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)// Anuluj
        {
            this.Close();
        }
        private void button2_Click(object sender, RoutedEventArgs e)//Dodaj
        {
            players.Add(new Players(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text)));
            listBox1.Items.Add(textBox1.Text);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button3_Click(object sender, RoutedEventArgs e)//Usuń
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                listBox1.Items.RemoveAt(i);
                players.RemoveAt(i);
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)//Edytuj
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                players[i].name = textBox1.Text;

                players[i].AC = int.Parse(textBox2.Text);
                players[i].HP = int.Parse(textBox3.Text);
                players[i].Ini = int.Parse(textBox4.Text);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
        }
        private int Comparer(Players plx, Players ply)
        {
            if (plx.Ini == ply.Ini)
            {
                return 0;
            }
            if (plx.Ini > ply.Ini)
            {
                return -1;
            }
            if (plx.Ini < ply.Ini)
            {
                return 1;
            }
            else return 0;
        }
        private void button5_Click(object sender, RoutedEventArgs e)//Posortuj
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            listBox1.Items.Clear();
            players.Sort(Comparer);
            for (int i = 0; i < players.Count; i++)
            {
                listBox1.Items.Add(players[i].name);
            }

        }
        private void SelChange(object sender, RoutedEventArgs e)//Selekcja z listy
        {
            int i = listBox1.SelectedIndex;
            if (i >= 0)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox1.AppendText(players[i].name);
                textBox2.AppendText(players[i].AC.ToString());
                textBox3.AppendText(players[i].HP.ToString());
                textBox4.AppendText(players[i].Ini.ToString());
            }
        }
        private void button6_Click(object sender, RoutedEventArgs e)//Zamień w górę
        {
            int i = listBox1.SelectedIndex;
            if (i > 0)
            {
                Players pl = new Players(players[i].name, players[i].AC, players[i].HP, players[i].Ini);
                players[i] = players[i - 1];
                players[i - 1] = pl;
                listBox1.Items.Clear();
                for (int q = 0; q < players.Count; q++)
                {
                    listBox1.Items.Add(players[q].name);
                }
            }
        }
        private void button7_Click(object sender, RoutedEventArgs e)//Zamień w dół
        {
            int i = listBox1.SelectedIndex;
            if (i < players.Count-1)
            {
                Players pl = new Players(players[i].name, players[i].AC, players[i].HP, players[i].Ini);
                players[i] = players[i+1];
                players[i+1] = pl;
                listBox1.Items.Clear();
                for (int q = 0; q < players.Count; q++)
                {
                    listBox1.Items.Add(players[q].name);
                }
            }
        }
    }
}
