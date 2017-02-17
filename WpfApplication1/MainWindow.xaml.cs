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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string myConnection = "server=107.180.54.170;port=3306;uid=kliu10;" + "pwd=Saweqr1!;database=glycan;";
        MySqlConnection conn = null;
        MySqlCommand cmd = new MySqlCommand();
        public MainWindow()
        {
            try
            {
                conn = new MySqlConnection(myConnection);
                conn.Open();
                cmd.Connection = conn;
                InitializeComponent();
                dataGrid.ItemsSource = Load_Compound_View();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void View_Compounds(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Load_Compound_View();
        }

        private void Insert_Compounds(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }

        private List<Compound> Load_Compound_View()
        {
            cmd.CommandText = "SELECT id, Family, Series, Name, Keywords, Structure, Stock, Updated FROM compounds";
            List<Compound> compounds = new List<Compound>();
            MySqlDataReader rdr = null;
            
            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    compounds.Add(new Compound()
                    {
                        ID = rdr.GetInt32(0),
                        Family = rdr.GetString(1),
                        Series = rdr.GetString(2),
                        Name = rdr.GetString(3),
                        Keyword = rdr.GetString(4),
                        Structure = rdr.GetString(5),
                        Stock = rdr.GetDouble(6),
                        Updated = rdr.GetDateTime(7)
                    });
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            rdr.Close();
            return compounds;
        }

        private void Selection_View(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Invoice_Modal(object sender, RoutedEventArgs e)
        {
            Insert_Invoice();
            dataGrid.ItemsSource = Load_Invoice_View();
        }

        private void View_Invoices(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Load_Invoice_View();
        }

        private void Insert_Invoice()
        {
            try
            {
                cmd.CommandText = "INSERT INTO invoice VALUES (@ID, @Email, @Mail, @Ship, @PID, @Stock, @Price, @Date)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@ID", "test-1-102302");
                cmd.Parameters.AddWithValue("@Email", "test@test.com");
                cmd.Parameters.AddWithValue("@Mail", "101 test street, test city, test 28102");
                cmd.Parameters.AddWithValue("@Ship", "101 test street, test city, test 28102");
                cmd.Parameters.AddWithValue("@PID", 101);
                cmd.Parameters.AddWithValue("@Stock", 5);
                cmd.Parameters.AddWithValue("@Price", 99.99);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private List<Invoice> Load_Invoice_View()
        {
            cmd.CommandText = "SELECT id, email, mailAddress, shipAddress, pid, stock, price, updated FROM invoice";
            List<Invoice> compounds = new List<Invoice>();
            MySqlDataReader rdr = null;

            try
            {
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    compounds.Add(new Invoice()
                    {
                        ID = rdr.GetString(0),
                        Email = rdr.GetString(1),
                        MailAddress = rdr.GetString(2),
                        ShipAddress = rdr.GetString(3),
                        PID = rdr.GetInt32(4),
                        Stock = rdr.GetDouble(5),
                        Price = rdr.GetDecimal(6),
                        Updated = rdr.GetDateTime(7)
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            rdr.Close();
            return compounds;
        }
    }
}
