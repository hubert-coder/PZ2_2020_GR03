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
    public partial class YourProducts : ContentPage
    {

        List<Produkty> Produkt;

        public YourProducts()
        {
            InitializeComponent();

            Produkt = new List<Produkty>();

        }

        private async void GoBack_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoggedPage());
        }

        private void YourProductsPicker_Focused(object sender, FocusEventArgs e)
        {
            int loading_products = 0;

            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            string query = "SELECT * FROM Produkty INNER JOIN Produkty_Uzytkownika ON Produkty.IdProduktu = Produkty_Uzytkownika.IdProduktu";
            MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);

            if (loading_products == 0)
            {
                Polaczenie.Open();
                loading_products = 1;
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
                    foreach (Produkty xd in Produkt)
                    {
                        YourProductsPicker.Items.Add(xd.Nazwa); // ProductsName to Picker
                    }
                }
                catch (Exception ex)
                {

                }




                dataReader.Close();

                //close Connection
                Polaczenie.Close();
            }
            //return list to be displayed
            // return list;
        }

        private void YourProductsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int loading_products = 0;
            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            string SelectedProduct = YourProductsPicker.SelectedItem.ToString();
            string query = "SELECT * FROM Produkty WHERE Nazwa=@SelectedProduct";
            MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
            myCommand.Parameters.AddWithValue("@SelectedProduct", YourProductsPicker.SelectedItem.ToString());

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

        private async void RemoveFromUser_Clicked(object sender, EventArgs e)
        {
            HiddenUser.Text = identity.ToString();
            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            string query = "DELETE FROM Produkty_Uzytkownika WHERE Id_User=@Id_User AND IdProduktu=@IdProduktu";
            MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
            myCommand.Parameters.AddWithValue("@Id_User", HiddenUser.Text);
            myCommand.Parameters.AddWithValue("@IdProduktu", HiddenID.Text);
            
            Polaczenie.Open();
            myCommand.ExecuteNonQuery();
            Polaczenie.Close();
            await DisplayAlert("Informacja", "Produkt usunięty z Twojej listy", "OK");
        }
    }
}