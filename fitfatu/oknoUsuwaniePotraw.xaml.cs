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
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;


namespace fitfatu
{
    public partial class oknoUsuwaniePotraw : Window
    {
        MySqlConnection connect;
        List<Prod> Produkty;
        List<Przep> Przepisy;

        string server = "licznik-kalorii.cba.pl";
        string dataBase = "gymrun_project";
        string user = "czerwonysandal";
        string password = "NiebieskiK2losz";

        public oknoUsuwaniePotraw()
        {
            InitializeComponent();

            Produkty = new List<Prod>();
            Przepisy = new List<Przep>();

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
                reader2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Blad przy pobieraniu danych - produktow" + ex.ToString());
            }
            try
            {
                MySqlCommand uzy2 = new MySqlCommand("SELECT * FROM Przepisy", connect);
                MySqlDataReader reader4 = uzy2.ExecuteReader();
                while (reader4.Read())
                {
                    int id = (int)reader4["IdPrzepisu"];
                    string na = reader4["Nazwa"].ToString();
                    int po = (int)reader4["Porcje"];
                    int cz = (int)reader4["Czas"];
                    int le = (int)reader4["Level"];
                    float kc = (float)reader4["Kcal"];
                    float pr = (float)reader4["Protein"];
                    float fa = (float)reader4["Fats"];
                    float ca = (float)reader4["Carbs"];
                    string sk = reader4["Skladniki"].ToString();
                    string prz = reader4["Przygotowanie"].ToString();

                    Przep pp = new Przep(id, na, po, cz, le, kc, pr, fa, ca, sk, prz);

                    Przepisy.Add(pp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Blad przy pobieraniu danych - przepisow" + ex.ToString());
            }
            connect.Close();

            try
            {
                foreach (Prod xd in Produkty)
                {
                    cb1.Items.Add(xd.Nazwa);
                }
                foreach (Przep xd in Przepisy)
                {
                    cb2.Items.Add(xd.Nazwa);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("brak produktów lub przepisów" + ex.ToString());
            }
        }

        private void ComboBoxProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //PRODUKTY
            if(cb1.SelectedItem != null)
            {
                if (Potwierdzenie() == true)
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
                    cmd.CommandText = "DELETE FROM Produkty WHERE Nazwa ='"+cb1.SelectedItem.ToString()+"'";
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Blad przy usuwaniu " + ex.ToString());
                    }
                    connect.Close();

                    MessageBox.Show(cb1.SelectedItem.ToString() + " - usunieto ten produkt");
                    cb1.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Zdecyduj się");
                    cb1.SelectedItem = null;
                }
            }
        }

        private void ComboBoxPrzep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //PRZEPISY
            if(cb2.SelectedItem != null)
            {
                if (Potwierdzenie() == true)
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
                    cmd.CommandText = "DELETE FROM Przepisy WHERE Nazwa ='" + cb2.SelectedItem.ToString() + "'";
                    cmd.Prepare();

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Blad przy usuwaniu " + ex.ToString());
                    }
                    connect.Close();

                    MessageBox.Show(cb2.SelectedItem.ToString() + " - usunieto ten przepis");
                    cb2.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Zdecyduj się");
                    cb2.SelectedItem = null;
                }
            }
        }

        public Boolean Potwierdzenie()
        {
            string xd = Interaction.InputBox("Wpisz DELATE w celu potwierdzenia usuniecia", "Potwierdzenie Usunieci");
            if(xd == "DELATE")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
