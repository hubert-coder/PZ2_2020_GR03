using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySql.Data.MySqlClient;
namespace FitFatu
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Guest_ClickedAsync(object sender, EventArgs e)
        {
            await DisplayAlert("Informacja", "Nastąpi teraz zalogowanie do bazy", "OK");

            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            Polaczenie.Open();
            await DisplayAlert("Powiadomienie", "Połączony", "OK");
            await Navigation.PushModalAsync(new ZalogowanyGosc());


        }

        private async void User_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}
