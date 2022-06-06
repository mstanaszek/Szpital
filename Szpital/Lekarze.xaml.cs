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
using System.Data.SqlClient;
using System.Data;

namespace Szpital
{
    /// <summary>
    /// Logika interakcji dla klasy Lekarze.xaml
    /// </summary>
    public partial class Lekarze : Window
    {
        public Lekarze()
        {
            InitializeComponent();
            LoadGrid();
            
        }
        public void ClearData()
        {
            imie_txt.Clear();
            nazwisko_txt.Clear();
            tel_txt.Clear();
            spec_txt.Clear();
            id_txt.Clear();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-KRM9RBBT;Initial Catalog=OsrodekZdrowia;Integrated Security=True");

        public void LoadGrid()
        {
            SqlCommand sql = new SqlCommand("SELECT * FROM Lekarz", conn);
            DataTable data = new DataTable();
            conn.Open();
            SqlDataReader sqlDataReader = sql.ExecuteReader();
            data.Load(sqlDataReader);
            conn.Close();
            datagrid.ItemsSource = data.DefaultView;
        }

        public bool NotEmpty()
        {
            if (imie_txt.Text == String.Empty && nazwisko_txt.Text == String.Empty && tel_txt.Text == String.Empty && spec_txt.Text == String.Empty)
            {
                MessageBox.Show("Należy podać wszystkie dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        public bool NotEmpty2()
        {
            if (imie_txt.Text == String.Empty && nazwisko_txt.Text == String.Empty && tel_txt.Text == String.Empty && spec_txt.Text == String.Empty && id_txt.Text == String.Empty)
            {
                MessageBox.Show("Należy podać wszystkie dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void Insert(object sender, RoutedEventArgs e)
        {
            if (NotEmpty())
            {
                SqlCommand sql = new SqlCommand("INSERT INTO Lekarz VALUES (@Imie, @Nazwisko, @Tel, @Spec)", conn);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@Imie", imie_txt.Text);
                sql.Parameters.AddWithValue("@Nazwisko", nazwisko_txt.Text);
                sql.Parameters.AddWithValue("@Tel", tel_txt.Text);
                sql.Parameters.AddWithValue("@Spec", spec_txt.Text);
                conn.Open();
                sql.ExecuteNonQuery();
                conn.Close();
                LoadGrid();
                ClearData();
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {

            if (NotEmpty2())
            {
                SqlCommand sql = new SqlCommand("UPDATE Lekarz SET Imie = @Imie, Nazwisko = @Nazwisko, Telefon = @Tel, Specjalizacja = @Spec WHERE ID  = " + id_txt.Text + "", conn);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@Imie", imie_txt.Text);
                sql.Parameters.AddWithValue("@Nazwisko", nazwisko_txt.Text);
                sql.Parameters.AddWithValue("@Tel", tel_txt.Text);
                sql.Parameters.AddWithValue("@Spec", spec_txt.Text);
                conn.Open();
                sql.ExecuteNonQuery();
                conn.Close();
                LoadGrid();
                ClearData();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (NotEmpty2())
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("DELETE FROM Lekarz WHERE ID = " + id_txt.Text + "", conn);
                sql.ExecuteNonQuery();
                conn.Close();
                LoadGrid();
                ClearData();
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private void Menu(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
