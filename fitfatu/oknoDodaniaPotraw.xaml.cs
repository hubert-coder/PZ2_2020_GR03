using System;
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
using MySql.Data;
using MySql.Data.MySqlClient;


namespace fitfatu
{
    public partial class oknoDodaniaPotraw : Window
    {
        MySqlConnection connect;
        List<Prod> Produkty;

        string server = "licznik-kalorii.cba.pl";
        string dataBase = "gymrun_project";
        string user = "czerwonysandal";
        string password = "NiebieskiK2losz";

        string skladniki = null;

        public oknoDodaniaPotraw()
        {
            InitializeComponent();

            Produkty = new List<Prod>();

            connect = new MySqlConnection();
            connect.ConnectionString = string.Format("server = {0}; Database = {1}; Uid = {2}; Pwd = {3};", server, dataBase, user, password);

            try
            {
                connect.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("nie udalo sie poloczyc z baza" + ex.ToString());
            }

            try
            {
                MySqlCommand uzy = new MySqlCommand("SELECT * FROM Produkty", connect);
                MySqlDataReader reader2 = uzy.ExecuteReader();
                while (reader2.Read())
                {
                    int id = (int)reader2["IdProduktu"];
                    string na = reader2["Nazwa"].ToString();
                    float kc = (float)reader2["Kcal"];
                    float pr = (float)reader2["Protein"];
                    float fa = (float)reader2["Fats"];
                    float ca = (float)reader2["Carbs"];

                    Prod p = new Prod(id, na, kc, pr, fa, ca);

                    Produkty.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Blad przy pobieraniu danych" + ex.ToString());
            }
            connect.Close();
            try
            {
                foreach (Prod xd in Produkty)
                {
                    cb1.Items.Add(xd.Nazwa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("brak produktów" + ex.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (t1.Text != null && t2.Text != null && t3.Text != null && t4.Text != null && t5.Text != null && t6.Text != null)
            {
                connect = new MySqlConnection();
                connect.ConnectionString = string.Format("server = {0}; Database = {1}; Uid = {2}; Pwd = {3};", server, dataBase, user, password);

                try
                {
                    connect.Open();
                    MessageBox.Show("U are in");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("nie udalo sie poloczyc z baza" + ex.ToString());
                }

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandText = "INSERT INTO Produkty(IdProduktu, Nazwa, Kcal, Protein, Fats, Carbs) VALUES(@IdProduktu, @Nazwa, @Kcal, @Protein, @Fats, @Carbs)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@IdProduktu", t1.Text);
                cmd.Parameters.AddWithValue("@Nazwa", t2.Text);
                cmd.Parameters.AddWithValue("@Kcal", t3.Text);
                cmd.Parameters.AddWithValue("@Protein", t4.Text);
                cmd.Parameters.AddWithValue("@Fats", t5.Text);
                cmd.Parameters.AddWithValue("@Carbs", t6.Text);

                try
                {
                    cmd.ExecuteNonQuery();
                    t1.Text = null;
                    t2.Text = null;
                    t3.Text = "0";
                    t4.Text = "0";
                    t5.Text = "0";
                    t6.Text = "0";
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Blad przy dodawaniu " + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij wszystkie miejsca");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_4(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_5(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_6(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_7(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_8(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_9(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_10(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_11(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_12(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_13(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_14(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(t16.Text.ToString()) >= 1)
            {
                lv.Items.Add(cb1.SelectedItem.ToString() + " - " + t16.Text + " Gram");
                skladniki += cb1.SelectedItem.ToString() + " - " + t16.Text + " Gram";
                foreach (Prod xd in Produkty)
                {
                    if (xd.Nazwa == cb1.SelectedItem.ToString())
                    {
                        t12.Text = Convert.ToString(Convert.ToDouble(t12.Text.ToString()) + xd.Kcal);
                        t13.Text = Convert.ToString(Convert.ToDouble(t13.Text.ToString()) + xd.Protein);
                        t14.Text = Convert.ToString(Convert.ToDouble(t14.Text.ToString()) + xd.Fats);
                        t15.Text = Convert.ToString(Convert.ToDouble(t15.Text.ToString()) + xd.Carbs);
                    }
                }
            }
        }

        private void TextBox_TextChanged_15(object sender, TextChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_16(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (t7.Text != null && t8.Text != null && t9.Text != null && t10.Text != null && t11.Text != null && t12.Text != null && t13.Text != null && t14.Text != null && t15.Text != null && t17.Text != null)
            {
                connect = new MySqlConnection();
                connect.ConnectionString = string.Format("server = {0}; Database = {1}; Uid = {2}; Pwd = {3};", server, dataBase, user, password);

                try
                {
                    connect.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("nie udalo sie poloczyc z baza" + ex.ToString());
                }

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connect;
                cmd.CommandText = "INSERT INTO Przepisy(IdPrzepisu, Nazwa, Porcje, Czas, Level, Kcal, Protein, Fats, Carbs, Skladniki, Przygotowanie) VALUES(@IdPrzepisu, @Nazwa, @Porcje, @Czas, @Level, @Kcal, @Protein, @Fats, @Carbs, @Skladniki, @Przygotowanie)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@IdPrzepisu", t7.Text);
                cmd.Parameters.AddWithValue("@Nazwa", t8.Text);
                cmd.Parameters.AddWithValue("@Porcje", t9.Text);
                cmd.Parameters.AddWithValue("@Czas", t10.Text);
                cmd.Parameters.AddWithValue("@Level", t11.Text);
                cmd.Parameters.AddWithValue("@Kcal", t12.Text);
                cmd.Parameters.AddWithValue("@Protein", t13.Text);
                cmd.Parameters.AddWithValue("@Fats", t14.Text);
                cmd.Parameters.AddWithValue("@Carbs", t15.Text);
                cmd.Parameters.AddWithValue("@Skladniki", skladniki);
                cmd.Parameters.AddWithValue("@Przygotowanie", t17.Text);

                try
                {
                    cmd.ExecuteNonQuery();
                    t7.Text = null;
                    t8.Text = null;
                    t9.Text = null;
                    t10.Text = null;
                    t11.Text = null;
                    t12.Text = "0";
                    t13.Text = "0";
                    t14.Text = "0";
                    t15.Text = "0";
                    t17.Text = null;
                    connect.Close();
                    lv.Items.Clear();
                    t16.Text = "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Blad przy dodawaniu " + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij wszystkie miejsca");
            }
        }
    }
}
