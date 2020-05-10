using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;

namespace FitFatu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZalogowanyGosc : ContentPage
    {
        public ZalogowanyGosc()
        {
            InitializeComponent();
        }

        private async void About_ClickedAsync(object sender, EventArgs e)
        {
            await DisplayAlert("O autorze : ", "Dymnicki Hubert, 2020 © ", "OK");
        }

        private async void LogoutGuest_ClickedAsync(object sender, EventArgs e)
        {
            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            Polaczenie.Close();
            await DisplayAlert("Powiadomienie", "Połączenie zostało zakończone", "OK");
            await Navigation.PushModalAsync(new MainPage());
        }

        private async void SearchRecipe_ClickedAsync(object sender, EventArgs e)
        {
            await DisplayAlert("Powiadomienie", "Funkcja w budowie", "OK");
        }

        private async void SearchProduct_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SearchProduct());
        }

        private void SearchRecipe_Clicked(object sender, EventArgs e)
        {

        }

        private void CalculateBMI_Clicked(object sender, EventArgs e)
        {

        }
    }
}