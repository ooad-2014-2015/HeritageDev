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
using MySql.Data.MySqlClient;

namespace it_shop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            poruka.Content = "admin";
            string username = "root";
            string password = "root";
            string db = "IT_SHOP";
            //Konekcija na bazu
            string connectionString = "server=192.168.1.11;user=" + username + ";pwd=" + password
           + ";database=" + db;
            MySqlConnection con = new MySqlConnection(connectionString);
            //con.Open();
            MySqlCommand upit = new MySqlCommand();
            upit.Connection = con;
            upit.CommandText = "SELECT * FROM ZAPOSLENICI;";
            txt.Text = null;
            try
            {
                //open the connection
                con.Open();
                //use a DataReader to process each record
                MySqlDataReader msqlReader = upit.ExecuteReader();
                while (msqlReader.Read())
                {
                    txt.Text += msqlReader.GetString(0) + '\t' + msqlReader.GetString(1) + '\t' + msqlReader.GetString(2) + '\t' + msqlReader.GetString(3) + '\t' + msqlReader.GetString(4) + '\n';
                }
            }
            catch (Exception er)
            {
                txt.Text = er.ToString();
            }
            finally
            {
                //always close the connection
                con.Close();
            }
        }

     
    }
}
