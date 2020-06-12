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
    public partial class SearchProductUser : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        List<Produkty> Produkt;

        public SearchProductUser()
        {
            InitializeComponent();

            Produkt = new List<Produkty>();


            try
            {


                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "SELECT * FROM Produkty";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);


                Polaczenie.Open();

                MySqlDataReader dataReader = myCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    int id = (int)dataReader["IdProduktu"];
                    string name = dataReader["Nazwa"].ToString();
                    float kcal = (float)dataReader["Kcal"];
                    float protein = (float)dataReader["Protein"];
                    float fats = (float)dataReader["Fats"];
                    float carbs = (float)dataReader["Carbs"];

                    Produkty p = new Produkty(id, name, kcal, protein, fats, carbs);

                    Produkt.Add(p);
                }

                try
                {
                    foreach (Produkty products in Produkt)
                    {
                        ProductsNameUser.Items.Add(products.Nazwa); // ProductsName to Picker
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
                DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");
            }

        }

        private void ProductsNameUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int loading_products = 0;
                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string SelectedProduct = ProductsNameUser.SelectedItem.ToString();
                string query = "SELECT * FROM Produkty WHERE Nazwa=@SelectedProduct";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
                myCommand.Parameters.AddWithValue("@SelectedProduct", ProductsNameUser.SelectedItem.ToString());

                if (loading_products == 0)
                {
                    loading_products = 1;
                    Polaczenie.Open();

                    MySqlDataReader dataReader = myCommand.ExecuteReader();


                    while (dataReader.Read())
                    {
                        int id = (int)dataReader["IdProduktu"];
                        string name = dataReader["Nazwa"].ToString();
                        float kcal = (float)dataReader["Kcal"];
                        float protein = (float)dataReader["Protein"];
                        float fats = (float)dataReader["Fats"];
                        float carbs = (float)dataReader["Carbs"];

                        Produkty p = new Produkty(id, name, kcal, protein, fats, carbs);

                        Produkt.Add(p);
                    }
                }
                try
                {
                    foreach (Produkty products in Produkt)
                    {

                        NameProductUser.Text = "Nazwa produktu : " + products.Nazwa;
                        KcalProductUser.Text = "Wartość kaloryczna : " + products.Kcal.ToString();
                        ProteinProductUser.Text = "Białko : " + products.Protein.ToString();
                        FatsProductUser.Text = "Tłuszcz : " + products.Fats.ToString();
                        CarbsProductUser.Text = "Węglowodany : " + products.Carbs.ToString();
                        HiddenID.Text = products.IdProduktu.ToString();
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

        private void ProductsNameUser_Focused(object sender, FocusEventArgs e)
        {
            
        }

       

        private void PowrotMenuUser_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoggedPage());
        }

        private async void AddToUser_Clicked(object sender, EventArgs e)
        {
            try
            {
                HiddenUser.Text = identity.ToString();
                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "INSERT INTO Produkty_Uzytkownika(Id_User, IdProduktu) VALUES ('" + this.HiddenUser.Text + "','" + this.HiddenID.Text + "');";
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
    }
}