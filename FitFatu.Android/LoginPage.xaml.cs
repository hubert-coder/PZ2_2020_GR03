using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySql.Data.MySqlClient;
using Java.Security;


using System.Data;
using Mzsoft.BCrypt;
using System.Security.Policy;
using FitFatu.Droid;
using static FitFatu.LoggedUserMenu;

namespace FitFatu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        List<LoggedUser>User;
        List<IntegerUser> Id_Users;

        public LoginPage()
        {
            InitializeComponent();

            User = new List<LoggedUser>();
            Id_Users = new List<IntegerUser>();

        }

        private async void Test_ClickedAsync(object sender, EventArgs e)
        {
            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            string query = "INSERT INTO Produkty(Nazwa, Kcal, Protein, Fats, Carbs) VALUES ('Kaszanka', 1000, 24.5, 246.1, 21)";

            MySqlCommand myCommand = new MySqlCommand(query, Polaczenie); 
            Polaczenie.Open();
            myCommand.ExecuteNonQuery();
            Polaczenie.Close();
            await DisplayAlert("Informacja", "OK", "OK");
            
        }

       

        private async void TryLogin_ClickedAsync(object sender, EventArgs e)
        {
            String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
            MySqlConnection Polaczenie = new MySqlConnection(cos);
            Polaczenie.Open();
            await Navigation.PushModalAsync(new LoggedPage());
        }

        public  void Login_User_Clicked(object sender, EventArgs e)
        {

            try
            {



                string hashed_password = "";
                int local_id = 0;
                String cos = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project;";
                MySqlConnection Polaczenie = new MySqlConnection(cos);
                string query = "SELECT Password FROM Users WHERE Login=@LogUser";
                MySqlCommand myCommand = new MySqlCommand(query, Polaczenie);
                myCommand.Parameters.AddWithValue("@LogUser", Entry_Login.Text);

                Polaczenie.Open();
                MySqlDataReader dataReader = myCommand.ExecuteReader();

                try
                {
                    while (dataReader.Read())
                    {
                        string password = dataReader["Password"].ToString();

                        LoggedUser p = new LoggedUser(password);

                        User.Add(p);

                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Informacja", "Wystąpił błąd", "OK");
                    Polaczenie.Close();


                }



                try
                {
                    foreach (LoggedUser hashed in User)
                    {

                        hashed_password = hashed.User_Password;

                    }

                }
                catch (Exception ex)
                {
                    Polaczenie.Close();


                }

                if (Entry_Password.Text == null)
                {
                    DisplayAlert("Informacja", "Podaj hasło", "OK");


                }
                if (Entry_Login.Text == null)
                {
                    DisplayAlert("Informacja", "Podaj login", "OK");


                }

                string hash_pass = hashed_password;
                Polaczenie.Close();
                try
                {
                    if (BCrypt.CheckPassword(Entry_Password.Text, hash_pass))
                    {
                        string query_2 = "SELECT IdUser FROM Users WHERE Login=@LogUser";
                        MySqlCommand myCommand_2 = new MySqlCommand(query_2, Polaczenie);
                        myCommand_2.Parameters.AddWithValue("@LogUser", Entry_Login.Text);
                        Polaczenie.Open();

                        MySqlDataReader dataReader_2 = myCommand_2.ExecuteReader();

                        try
                        {
                            while (dataReader_2.Read())
                            {
                                int id_user = (int)dataReader_2["IdUser"];

                                IntegerUser abc = new IntegerUser(id_user);

                                Id_Users.Add(abc);

                            }
                        }
                        catch (Exception ex)
                        {
                            DisplayAlert("Informacja", "Wystąpił błąd", "OK");

                        }

                        try
                        {
                            foreach (IntegerUser identify in Id_Users)
                            {
                                local_id = identify.id_user;

                            }

                        }
                        catch (Exception ex)
                        {
                            Polaczenie.Close();

                        }
                        LoggedUserMenu.identity = local_id;



                        Navigation.PushModalAsync(new LoggedPage());


                    }
                    else
                    {

                        Polaczenie.Close();
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Informacja", ex.ToString(), "OK");
                    Polaczenie.Close();

                }







                Polaczenie.Close();

            }
            catch (Exception ex)
            {
                DisplayAlert("Powiadomienie", "Sprawdź połączenie z internetem", "OK");

            }
        }

        private void Entry_Login_Completed(object sender, EventArgs e)
        {
            Entry_Login.Text = ((Entry)sender).Text;
        }

        private void Entry_Password_Completed(object sender, EventArgs e)
        {
            Entry_Password.Text = ((Entry)sender).Text;
        }

        private void GoBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }
    }
}