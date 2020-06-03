using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;
using System.Xml.Xsl;

namespace FitFatu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchProduct : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }



        List<Produkty> Produkt;


        public SearchProduct()
        {
            InitializeComponent();

            Produkt = new List<Produkty>();


        }



        private async void PowrotMenuGosc_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ZalogowanyGosc());
        }

        private void SelectSQL_Clicked(object sender, EventArgs e)
        {
            
        }

        

        private void ProductsName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int loading_products = 0;
            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
           string SelectedProduct =  ProductsName.SelectedItem.ToString();
            string query = "SELECT * FROM Produkty WHERE Nazwa=@SelectedProduct";
            MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
            myCommand.Parameters.AddWithValue("@SelectedProduct", ProductsName.SelectedItem.ToString());

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
                foreach (Produkty xd in Produkt)
                {
                    
                    NameProduct.Text = "Nazwa produktu : " + xd.Nazwa;
                    KcalProduct.Text = "Wartość kaloryczna : " + xd.Kcal.ToString();
                    ProteinProduct.Text = "Białko : " + xd.Protein.ToString();
                    FatsProduct.Text = "Tłuszcz : " + xd.Fats.ToString();
                    CarbsProduct.Text = "Węglowodany : " + xd.Carbs.ToString();

                    
                }
            }
            catch (Exception ex)
            {

            }
            Polaczenie.Close();
        }

        private void ProductsName_Focused(object sender, FocusEventArgs e)
        {
            int loading_products = 0;

            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            string query = "SELECT * FROM Produkty";
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
                        ProductsName.Items.Add(xd.Nazwa); // ProductsName to Picker
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
    }
}