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
        List<Recipts> Recipt;
        public YourRecipts()
        {
            InitializeComponent();
            Recipt = new List<Recipts>();

            try
            {



                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "SELECT * FROM Przepisy INNER JOIN Przepisy_Uzytkownika ON Przepisy.IdPrzepisu = Przepisy_Uzytkownika.Id_Przepisu";
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
                        YourReciptsPicker.Items.Add(xd.Nazwa); // ProductsName to Picker
                    }
                }
                catch (Exception ex)
                {

                }




                dataReader.Close();

                //close Connection
                Polaczenie.Close();

            }
            catch (Exception ex)
            {
                DisplayAlert("Powiadomienie", "Sprawdź połączenie", "OK");
            }


        }

        private async void GoBack_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoggedPage());
        }

        private async void RemoveFromList_Clicked(object sender, EventArgs e)
        {
            try
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
            catch(Exception ex)
            {
               await DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }
        }

        private void YourReciptsPicker_Focused(object sender, FocusEventArgs e)
        {
            
        }

        private void YourReciptsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int loading_products = 0;

                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "SELECT * FROM Przepisy WHERE Nazwa=@SelectedRecipt";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
                myCommand.Parameters.AddWithValue("@SelectedRecipt", YourReciptsPicker.SelectedItem.ToString());

                if (loading_products == 0)
                {
                    Polaczenie.Open();
                    loading_products = 1;
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




                    dataReader.Close();

                    //close Connection
                    Polaczenie.Close();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }
        }
    }
}