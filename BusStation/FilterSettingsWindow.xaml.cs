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
    public partial class FilterSettingsWindow : Window
    {
        public FilterSettingsWindow()
        {
            InitializeComponent();
            FillFiltersBoxes();
        }

        private void FillFiltersBoxes()
        {
            string select_routes_filters = "select filtering_number from users_filters where user_id=" + Core.user_id + " and filtering_type='route';";
            string select_buses_filters = "select filtering_number from users_filters where user_id=" + Core.user_id + " and filtering_type='bus';";
            
            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(select_routes_filters, connection);
                SQLiteDataReader sqlReader = Command.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        RouteNumberBox.Text = RouteNumberBox.Text + sqlReader.GetString(0) + ",";
                    }
                    RouteNumberBox.Text = RouteNumberBox.Text.Substring(0, RouteNumberBox.Text.Length - 1);
                }
            }

            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(select_buses_filters, connection);
                SQLiteDataReader  sqlReader = Command.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        BusNumberBox.Text = BusNumberBox.Text + sqlReader.GetString(0) + ",";
                    }
                    BusNumberBox.Text = BusNumberBox.Text.Substring(0, BusNumberBox.Text.Length - 1);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            RouteNumberBox.Clear();
            BusNumberBox.Clear();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            RouteNumberBox.Text = RouteNumberBox.Text.Replace(" ", string.Empty);
            BusNumberBox.Text = BusNumberBox.Text.Replace(" ", string.Empty);

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

            Core.ExecuteNonQuery("delete from users_filters where user_id=" + Core.user_id); // удаляем все прыдыдущие фильтры

            for (int i = 0; i < admissibleRoutesNumbers.Count; i++)
            {
                Core.ExecuteNonQuery("insert into users_filters (user_id, filtering_number, filtering_type) values (" + Core.user_id +",'" + admissibleRoutesNumbers[i] + "'," + "'route'" + ")");
            }

            for (int i = 0; i < admissibleBusesNumbers.Count; i++)
            {
                Core.ExecuteNonQuery("insert into users_filters (user_id, filtering_number, filtering_type) values (" + Core.user_id + ",'" + admissibleBusesNumbers[i] + "'," + "'bus'" + ")");
            }

            this.Close();
        }
    }
}
