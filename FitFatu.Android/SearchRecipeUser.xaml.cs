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
    public partial class SearchRecipeUser : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        List<Recipts> Recipt;

        public SearchRecipeUser()
        {
         
            InitializeComponent();

            Recipt = new List<Recipts>();

            try
            {

                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "SELECT * FROM Przepisy";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);


                Polaczenie.Open();

                MySqlDataReader dataReader = myCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    int id = (int)dataReader["IdPrzepisu"];
                    string name = dataReader["Nazwa"].ToString();
                    int portion = (int)dataReader["Porcje"];
                    int time = (int)dataReader["Czas"];
                    int level = (int)dataReader["Level"];
                    float kcal = (float)dataReader["Kcal"];
                    float protein = (float)dataReader["Protein"];
                    float fats = (float)dataReader["Fats"];
                    float carbs = (float)dataReader["Carbs"];
                    string ingredients = dataReader["Skladniki"].ToString();
                    string preparation = dataReader["Przygotowanie"].ToString();

                    Recipts p = new Recipts(id, name, portion, time, level, kcal, protein, fats, carbs, ingredients, preparation);

                    Recipt.Add(p);
                }

                try
                {
                    foreach (Recipts xd in Recipt)
                    {
                        ReciptsListUser.Items.Add(xd.Nazwa); // ProductsName to Picker
                    }
                }
                catch (Exception ex)
                {

                }

                Polaczenie.Close();
            }
            catch (Exception ex)
            {
                DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }

        }

        private async void AddToList_Clicked(object sender, EventArgs e)

        {
            try
            {
                HiddenUser.Text = identity.ToString();
                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "INSERT INTO Przepisy_Uzytkownika(Id_User, Id_Przepisu) VALUES ('" + this.HiddenUser.Text + "','" + this.HiddenID.Text + "');";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
                Polaczenie.Open();
                myCommand.ExecuteNonQuery();
                Polaczenie.Close();
                await DisplayAlert("Informacja", "Rekord dodany do Twojego konta", "OK");
            }
            catch(Exception ex)
            {
              await DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }
        }

        private void ReciptsListUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string SelectedProduct = ReciptsListUser.SelectedItem.ToString();
                string query = "SELECT * FROM Przepisy WHERE Nazwa=@SelectedRecipt";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
                myCommand.Parameters.AddWithValue("@SelectedRecipt", ReciptsListUser.SelectedItem.ToString());

                Polaczenie.Open();

                MySqlDataReader dataReader = myCommand.ExecuteReader();


                while (dataReader.Read())
                {
                    int id = (int)dataReader["IdPrzepisu"];
                    string name = dataReader["Nazwa"].ToString();
                    int portion = (int)dataReader["Porcje"];
                    int time = (int)dataReader["Czas"];
                    int level = (int)dataReader["Level"];
                    float kcal = (float)dataReader["Kcal"];
                    float protein = (float)dataReader["Protein"];
                    float fats = (float)dataReader["Fats"];
                    float carbs = (float)dataReader["Carbs"];
                    string ingredients = dataReader["Skladniki"].ToString();
                    string preparation = dataReader["Przygotowanie"].ToString();

                    Recipts p = new Recipts(id, name, portion, time, level, kcal, protein, fats, carbs, ingredients, preparation);

                    Recipt.Add(p);
                }

                try
                {
                    foreach (Recipts recipt2 in Recipt)
                    {

                        NameReciptUser.Text = "Nazwa przepisu : " + recipt2.Nazwa;
                        PortionReciptUser.Text = "Porcje : " + recipt2.Porcje.ToString();
                        TimeReciptUser.Text = "Czas : " + recipt2.Czas.ToString();
                        LevelReciptUser.Text = "Poziom trudności : " + recipt2.Level.ToString();
                        KcalReciptUser.Text = "Wartość kaloryczna : " + recipt2.Kcal.ToString();
                        ProteinReciptUser.Text = "Białko : " + recipt2.Protein.ToString();
                        FatsReciptUser.Text = "Tłuszcz : " + recipt2.Fats.ToString();
                        CarbsReciptUser.Text = "Węglowodany : " + recipt2.Carbs.ToString();
                        IngredientsReciptUser.Text = "Nazwa przepisu : " + recipt2.Skladniki.ToString();
                        PreparationReciptUser.Text = "Wartość kaloryczna : " + recipt2.Przygotowanie.ToString();


                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }
        }

        private void ReciptsListUser_Focused(object sender, FocusEventArgs e)
        {
           
        }

        private void GoBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoggedPage());
        }
    }
}