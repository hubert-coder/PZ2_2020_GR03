using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;
using static FitFatu.LoggedUserMenu;

namespace FitFatu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourRecipts : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        public YourRecipts()
        {
            InitializeComponent();


        }

        private async void GoBack_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoggedPage());
        }

        private async void RemoveFromList_Clicked(object sender, EventArgs e)
        {
            HiddenUser.Text = identity.ToString();
            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            string query = "DELETE FROM Przepisy_Uzytkownika WHERE Id_User=@Id_User AND Id_Przepisu=@IdPrzepisu";
            MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
            myCommand.Parameters.AddWithValue("@Id_User", HiddenUser.Text);
            myCommand.Parameters.AddWithValue("@IdPrzepisu", HiddenID.Text);

            Polaczenie.Open();
            myCommand.ExecuteNonQuery();
            Polaczenie.Close();
            await DisplayAlert("Informacja", "Produkt usunięty z Twojej listy", "OK");
        }

        private void YourReciptsPicker_Focused(object sender, FocusEventArgs e)
        {

        }

        private void YourReciptsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}