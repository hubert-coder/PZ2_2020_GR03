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
    public partial class RecipeLotteryUser : ContentPage
    {
        List<Recipts> Recipt;

        public RecipeLotteryUser()
        {
            InitializeComponent();

            Recipt = new List<Recipts>();

        }

        private void Lottery_Clicked(object sender, EventArgs e)
        {
            try
            {

                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "SELECT * FROM Przepisy ORDER BY RAND() LIMIT 1;";
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
                        Name.Text = "Nazwa przepisu : " + xd.Nazwa;
                        Portions.Text = "Porcje : " + xd.Porcje.ToString();
                        Time.Text = "Czas : " + xd.Czas.ToString();
                        Level.Text = "Poziom trudności : " + xd.Level.ToString();
                        Kcal.Text = "Wartość kaloryczna : " + xd.Kcal.ToString();
                        Protein.Text = "Białko : " + xd.Protein.ToString();
                        Fats.Text = "Tłuszcz : " + xd.Fats.ToString();
                        Carbs.Text = "Węglowodany : " + xd.Carbs.ToString();
                        Ingredients.Text = "Nazwa przepisu : " + xd.Skladniki.ToString();
                        Preparation.Text = "Wartość kaloryczna : " + xd.Przygotowanie.ToString();
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

        private void GoBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoggedPage());
        }


    }
}