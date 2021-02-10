using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace MovieDatabaseApp
{
    /// <summary>
    /// Interaction logic for MovieDatabaseHome.xaml
    /// </summary>
    public partial class MovieDatabaseHome : Window
    {
        public MovieDatabaseHome()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-AU4JA7HP; Initial Catalog=MovieDB; Integrated Security=True;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM tblMovie WHERE Name=@Name";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Name", txtMovieName.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count == 1){

                    MainWindow movieDetails = new MainWindow();
                    movieDetails.Show();
                    this.Close();
                }
                else{

                    MessageBox.Show("Movie you searched was not found in my database.");
                }
            }
            catch (Exception ex){

                MessageBox.Show(ex.Message);
            }
            finally{

                sqlCon.Close();
            }
        }
    }
}
