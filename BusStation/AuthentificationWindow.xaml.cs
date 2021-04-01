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
using System.Data.SQLite;
using System.Configuration;

namespace BusStation
{
    public partial class Autentification : Window
    {
        public Autentification()
        {
            InitializeComponent();
            Core.connectionString = ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;
        }

        private void AuthorisationButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;
            string query = "select id from users where login='" + login + "' and password='" + password + "';";

            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(query, connection);
                SQLiteDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        Core.user_id = userId.ToString();

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                }  else
                {
                    MessageBox.Show("Логин или пароль не верные", "Ошибка авторизации");
                }          
            }
        }
    }
}