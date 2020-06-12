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
    public partial class SearchRecipt : ContentPage
    {
        List<Recipts> Recipt;

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        public SearchRecipt()
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
                        ReciptsList.Items.Add(xd.Nazwa); // ProductsName to Picker
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }

        }

        private async void PowrotMenuGosc_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ZalogowanyGosc());
        }

        private void ReciptsList_Focused(object sender, FocusEventArgs e)
        {


           
        }

        private void ReciptsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string SelectedProduct = ReciptsList.SelectedItem.ToString();
                string query = "SELECT * FROM Przepisy WHERE Nazwa=@SelectedRecipt";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
                myCommand.Parameters.AddWithValue("@SelectedRecipt", ReciptsList.SelectedItem.ToString());

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

                        NameRecipt.Text = "Nazwa przepisu : " + xd.Nazwa;
                        PortionRecipt.Text = "Porcje : " + xd.Porcje.ToString();
                        TimeRecipt.Text = "Czas : " + xd.Czas.ToString();
                        LevelRecipt.Text = "Poziom trudności : " + xd.Level.ToString();
                        KcalRecipt.Text = "Wartość kaloryczna : " + xd.Kcal.ToString();
                        ProteinRecipt.Text = "Białko : " + xd.Protein.ToString();
                        FatsRecipt.Text = "Tłuszcz : " + xd.Fats.ToString();
                        CarbsRecipt.Text = "Węglowodany : " + xd.Carbs.ToString();
                        IngredientsRecipt.Text = "Nazwa przepisu : " + xd.Skladniki.ToString();
                        PreparationRecipt.Text = "Wartość kaloryczna : " + xd.Przygotowanie.ToString();


                    }
                }
                catch (Exception ex)
                {

                }
                Polaczenie.Close();
            }
            catch(Exception ex)
            {

                DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }
        }
    }
}