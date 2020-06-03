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
    public partial class AddReciptPage : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        public AddReciptPage()
        {
            InitializeComponent();
        }

        public  void ReciptText_CompletedAsync(object sender, EventArgs e)
        {
            ReciptText.Text = ((Entry)sender).Text;
            
        }

        private  void ReciptName_CompletedAsync(object sender, EventArgs e)
        {
            ReciptName.Text = ((Entry)sender).Text;
            
        }

        private async void SendRecipt_ClickedAsync(object sender, EventArgs e)
        {
            if (ReciptName.Text != null || ReciptText.Text != null)
            {

                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "INSERT INTO Przepisy(Nazwa, Przygotowanie) VALUES (@Nazwa, @Przygotowanie)";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
                myCommand.Parameters.AddWithValue("@Nazwa", ReciptName.Text);
                myCommand.Parameters.AddWithValue("@Przygotowanie", ReciptText.Text);
                Polaczenie.Open();
                myCommand.ExecuteNonQuery();
                Polaczenie.Close();
                await DisplayAlert("Informacja", "OK", "OK");
            }
            else
            {
                await DisplayAlert("Informacja", "Podaj dane do przepisu", "OK");
            }
        }

        private void GoBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoggedPage());
        }
    }
}