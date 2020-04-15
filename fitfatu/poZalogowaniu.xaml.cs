using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace fitfatu
{
    /// <summary>
    /// Logika interakcji dla klasy poZalogowaniu.xaml
    /// </summary>
    public partial class poZalogowaniu : Window
    {
        bool czyWylogowac = false; 
        static string connectionString = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project";
        MySqlConnection connection = new MySqlConnection(connectionString);
        public poZalogowaniu()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (Zmienne.CzyGosc)
            {
                WitajLogin.Content = "Witaj, zalogowano jako: Gość";
                dodajPotrawe.Visibility = Visibility.Hidden;
                usunPotrawę.Visibility = Visibility.Hidden;
                logoutButton.Content = "Zaloguj";
            }
            else WitajLogin.Content = "Witaj, zalogowano jako: " + Zmienne.Login.ToString();
        }

        private void dodajPotrawe_Click(object sender, RoutedEventArgs e)
        {
            oknoDodaniaPotraw oknoDodaniaPotrawy = new oknoDodaniaPotraw();
            oknoDodaniaPotrawy.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(!czyWylogowac) System.Windows.Application.Current.Shutdown();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            czyWylogowac = true;
            MainWindow przedZalogowaniem = new MainWindow();
            przedZalogowaniem.Show();
            this.Close();
        }
    }
}
