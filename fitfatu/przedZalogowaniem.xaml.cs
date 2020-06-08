using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static fitfatu.Zmienne;
namespace fitfatu
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connectionString = "server=licznik-kalorii.cba.pl;uid=czerwonysandal;pwd=NiebieskiK2losz;database=gymrun_project";
        MySqlConnection connection = new MySqlConnection(connectionString);
        bool typedLogin = false;
        bool typedPassword = false;

        public MainWindow()
        {
            this.Visibility = Visibility.Collapsed;
            this.Visibility = Visibility.Visible;
            InitializeComponent();
        }

        private void zalogujButton_Click(object sender, RoutedEventArgs e)
        {
            Zmienne.CzyGosc = false;
            Zmienne.Login = loginBox.Text;
            if (checkPassword(hasloBox.Password.ToString(), loginBox.Text))
            {
                otworzOknoPoZalogowaniu();
                this.Close();

            }
        }

        private bool checkPassword(string userPassword, string userLogin)
        {
            connection.Open();
            int zmiennaxd = 0;
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Password from Users where Login='" + loginBox.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            try
            {
                Console.WriteLine(dt.Rows[0].Field<string>(0));
                string DBpasswd = (dt.Rows[0].Field<string>(0));
                if (BCrypt.Net.BCrypt.Verify(hasloBox.Password.ToString(), DBpasswd))
                {
                    zmiennaxd = 1;
                }
                else
                {
                    MessageBox.Show("Błędny login lub hasło");
                }
            }
            catch
            {
                MessageBox.Show("Błędny login lub hasło");
            }
            connection.Close();
            if (zmiennaxd == 1) return true;
            else return false;

        }

        private void zalogujJakoGoscButton_Click(object sender, RoutedEventArgs e)
        {
            Zmienne.CzyGosc = true;
            otworzOknoPoZalogowaniu();
            this.Close();
        }

        private static void otworzOknoPoZalogowaniu()
        {
            poZalogowaniu oknoPoZalogowaniu = new poZalogowaniu();
            oknoPoZalogowaniu.Show();
        }

        private void loginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!typedLogin)
            {
                loginBox.Text = "";
                typedLogin = true;
                loginBox.Opacity = 100;
            }
        }

        private void hasloBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!typedPassword)
            {
                hasloBox.Password = "";
                typedPassword = true;
                hasloBox.Opacity = 100;
            }
        }
    }
}
