using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySql.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
namespace FitFatu
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public MainPage()
        {
            InitializeComponent();
            
           
        }

        private async void Guest_ClickedAsync(object sender, EventArgs e)
        {



            MySqlConnection connect;



            string server = "licznik-kalorii.cba.pl";
            string dataBase = "gymrun_project";
            string user = "czerwonysandal";
            string password = "NiebieskiK2losz";
            string ConnectionString;

            
            ConnectionString = string.Format("server = {0}; Database = {1}; Uid = {2}; Pwd = {3};", server, dataBase, user, password);
            connect = new MySqlConnection(ConnectionString);
            

            try
            {
                connect.Open();
                await Navigation.PushModalAsync(new ZalogowanyGosc());
            }
            catch (MySqlException ex)
            {
                await DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }
            
            


        }

        private async void User_ClickedAsync(object sender, EventArgs e)
        {

            try
            {
                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                Polaczenie.Open();
                await Navigation.PushModalAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");

            }
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
