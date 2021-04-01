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

namespace BusStation
{
    public partial class ChangeFavoriteWindow : Window
    {
        public ChangeFavoriteWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RouteNumberBox.Text = RouteNumberBox.Text.Replace(" ", string.Empty);
            BusNumberBox.Text = BusNumberBox.Text.Replace(" ", string.Empty);

            int deleted = 0;
            string[] routersNumbers = RouteNumberBox.Text.Split(',');
            string[] busesNumbers = BusNumberBox.Text.Split(',');

            List<string> admissibleRoutesNumbers = new List<string>();
            List<string> admissibleBusesNumbers = new List<string>();

            for (int i = 0; i < routersNumbers.Length; i++)
            {
                if (!(routersNumbers[i].Trim().Equals(string.Empty)))
                {
                    admissibleRoutesNumbers.Add(routersNumbers[i]);
                }
            }

            for (int i = 0; i < busesNumbers.Length; i++)
            {
                if (!(busesNumbers[i].Trim().Equals(string.Empty)))
                {
                    admissibleBusesNumbers.Add(busesNumbers[i]);
                }
            }

            for (int i = 0; i < admissibleRoutesNumbers.Count; i++)
            {
                string query = "delete from users_favorite where favorite_number='" + admissibleRoutesNumbers[i] + "' and user_id=" + Core.user_id + " and favorite_type='route';";
                deleted += Core.ExecuteNonQuery(query);
            }

            for (int i = 0; i < admissibleBusesNumbers.Count; i++)
            {
                string query = "delete from users_favorite where favorite_number='" + admissibleBusesNumbers[i] + "' and user_id=" + Core.user_id + " and favorite_type='bus';";
                deleted += Core.ExecuteNonQuery(query);
            }

            MessageBox.Show("Удалено записей: " + deleted.ToString(), "Удаление записей");
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RouteNumberBox.Text = RouteNumberBox.Text.Replace(" ", string.Empty);
            BusNumberBox.Text = BusNumberBox.Text.Replace(" ", string.Empty);

            int added = 0;
            int alreadyAdded = 0;
            string[] routersNumbers = RouteNumberBox.Text.Split(',');
            string[] busesNumbers = BusNumberBox.Text.Split(',');

            List<string> admissibleRoutesNumbers = new List<string>();
            List<string> admissibleBusesNumbers = new List<string>();

            for (int i = 0; i < routersNumbers.Length; i++)
            {
                if (!(routersNumbers[i].Trim().Equals(string.Empty)))
                {
                    admissibleRoutesNumbers.Add(routersNumbers[i]);
                }
            }

            for (int i = 0; i < busesNumbers.Length; i++)
            {
                if (!(busesNumbers[i].Trim().Equals(string.Empty)))
                {
                    admissibleBusesNumbers.Add(busesNumbers[i]);
                }
            }

            for (int i = 0; i < admissibleRoutesNumbers.Count; i++)
            {
                string query = "select id from users_favorite where favorite_number='" + admissibleRoutesNumbers[i] + "' and user_id=" + Core.user_id + " and favorite_type='route';"; 
                using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
                {
                    connection.Open();
                    SQLiteCommand Command = new SQLiteCommand(query, connection);
                    SQLiteDataReader sqlReader = Command.ExecuteReader();
                    if (sqlReader.HasRows)
                    {
                        alreadyAdded++;
                    } else
                    {
                        string addFavoriteQuery = "insert into users_favorite (favorite_number, user_id, favorite_type) values ('" + admissibleRoutesNumbers[i] + "'," + Core.user_id + ", 'route');";
                        Core.ExecuteNonQuery(addFavoriteQuery);
                        added++;
                    }
                }
            }

            for (int i = 0; i < admissibleBusesNumbers.Count; i++)
            {
                string query = "select id from users_favorite where favorite_number='" + admissibleBusesNumbers[i] + "' and user_id=" + Core.user_id + " and favorite_type='bus';";
                using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
                {
                    connection.Open();
                    SQLiteCommand Command = new SQLiteCommand(query, connection);
                    SQLiteDataReader sqlReader = Command.ExecuteReader();
                    if (sqlReader.HasRows)
                    {
                        alreadyAdded++;
                    }
                    else
                    {
                        string addFavoriteQuery = "insert into users_favorite (favorite_number, user_id, favorite_type) values ('" + admissibleBusesNumbers[i] + "'," + Core.user_id + ", 'bus');";
                        Core.ExecuteNonQuery(addFavoriteQuery);
                        added++;
                    }
                }
            }
            MessageBox.Show("Добавлено записей в избранное: " + added.ToString() +"\nУже есть в избранном: " + alreadyAdded.ToString(), "Добавление в избранное");
            this.Close();
        }
    }
}
