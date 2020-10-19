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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.Drawing;
using System.IO;

namespace RPGMaps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Serializable]
    public class GameState
    {
        public List<Players> pl;
        public String Simg;
        public int kolumny;
        public int wiersze;
    };

    public partial class MainWindow : Window
    {
        Line[] lk=new Line[2048];
        Line[] lw = new Line[2048];
        public OpenFileDialog ofdPicture = new OpenFileDialog();
        public static GameState gs = new GameState();
        ImageBrush ib = new ImageBrush();
        BitmapImage img;
        static double val=1;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MenuItem_Click(object sender, RoutedEventArgs e)//Wczytaj mapę
        {
            canvas1.Height = this.ActualHeight;
            canvas1.Width = this.ActualWidth;
            ofdPicture.Filter = "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;
            if (ofdPicture.ShowDialog() == true)
            {
                img = new BitmapImage(new Uri(ofdPicture.FileName));
                ib.ImageSource = img;
                canvas1.Background = ib;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)//Rysuj kratę
        {
            
            Krata krata = new Krata();
            krata.ShowDialog();
            CrateOp(Krata.kol, Krata.wier,Krata.ru);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)//Gracz
        {
            Gracze gracze = new Gracze();
            gracze.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)//Zamknij
        {
            System.Environment.Exit(1);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)// O programie
        {
            AboutBox1 aboutbox = new AboutBox1();
            aboutbox.ShowDialog();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)//Zapisz grę
        {
            Krata k=new Krata();
            gs.pl = Gracze.players;
            gs.Simg = ib.ImageSource.ToString();
            gs.kolumny = Krata.kol;
            gs.wiersze = Krata.wier;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "RPGMaps file (*.rpgm)|*.rpgm|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                Stream str;
                if ((str = sfd.OpenFile()) != null)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(str, gs);
                    str.Close();
                }
            }
        }
        private void MenuItem_Click_6(object sender, RoutedEventArgs e)//Wczytaj grę
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Filter = "RPGMaps file (*.rpgm)|*.rpgm|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                Stream str;
                if ((str = sfd.OpenFile()) != null)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    gs = (GameState)bf.Deserialize(str);
                    str.Close();
                    Gracze.players = gs.pl;
                    canvas1.Height = this.ActualHeight;
                    canvas1.Width = this.ActualWidth;
                    img = new BitmapImage(new Uri(gs.Simg));
                    ib.ImageSource = img;
                    canvas1.Background = ib;
                    Krata.wier = gs.wiersze;
                    Krata.kol = gs.kolumny;
                    CrateOp(gs.kolumny, gs.wiersze,true);
                }
            }
        }
        private void MenuItem_Click_7(object sender, RoutedEventArgs e)//Kostki
        {
            Kostki kostki = new Kostki();
            kostki.ShowDialog();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)//Zmiana rozmiaru okna
        {
            canvas1.Height = this.ActualHeight;
            canvas1.Width = this.ActualWidth;
            st.ScaleX = val;
            st.ScaleY = val;
            CrateOp(Krata.kol, Krata.wier, false);
            CrateOp(Krata.kol, Krata.wier, Krata.ru);
        }
        System.Windows.Point point;
        bool activated;
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas1.CaptureMouse();
            point = e.GetPosition(canvas1);
            activated = true;
        }
        
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
                if (activated)
                {
                    translate.X += e.GetPosition(canvas1).X - point.X; 
                    translate.Y += e.GetPosition(canvas1).Y - point.Y;
                    
                }
                
            
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            canvas1.ReleaseMouseCapture();
            activated = false;
        }

        private void slider1_ValueChanged(object sender, RoutedEventArgs e)// Obsługa slidera
        {
            double val = slider1.Value;
            st.CenterX = (canvas1.Width / 2) -translate.X;
            st.CenterY=(canvas1.Height/2)-translate.Y;
            st.ScaleX= val;
            st.ScaleY=val;
            CrateOp(Krata.kol, Krata.wier, false);
            CrateOp(Krata.kol, Krata.wier, Krata.ru);
        }

        public void CrateOp(int kol, int wier, bool rysuj)//Metoda rysuj kratkę
        {
            int heigth = (int)canvas1.Height;
            int width = (int)canvas1.Width;
            if (rysuj == true)
            {
            for (int i = 0; i < kol; i++)
            {
                lk[i] = new Line();
                lk[i].Stroke = System.Windows.Media.Brushes.Black;
                lk[i].Fill = System.Windows.Media.Brushes.Black;
                lk[i].X1 = (int)(width * i) / kol;
                lk[i].X2 = (int)(width * i) / kol;
                lk[i].Y1 = 0;
                lk[i].Y2 = heigth;
                    canvas1.Children.Add(lk[i]);
            }
            }
                else
                {
                    for(int i=0; i<kol; i++)
                    canvas1.Children.Remove(lk[i]);
                }
            if(rysuj==true)
              {
            for (int i = 0; i < wier; i++)
                {
                lw[i] = new Line();
                lw[i].Stroke = System.Windows.Media.Brushes.Black;
                lw[i].Fill = System.Windows.Media.Brushes.Black;
                lw[i].X1 = 0;
                lw[i].X2 = width;
                lw[i].Y1 = (int)(heigth * i) / wier;
                lw[i].Y2 = (int)(heigth * i) / wier;
                canvas1.Children.Add(lw[i]);
                }
              }
                else
                  {
                for(int i=0; i<wier; i++)
                canvas1.Children.Remove(lw[i]);
                  }
            }
       
    }
}
