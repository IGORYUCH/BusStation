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
using System.Configuration;
using System.Data.SQLite;

namespace BusStation
{
    public partial class ShowRoutesWindow : Window
    {
        public ShowRoutesWindow()
        {
            //string connectionString = ConfigurationManager.AppSettings["connectionString"];

            InitializeComponent();
            FillRoutesDataGrid();
        }

        private void FillRoutesDataGrid()
        {
            List<Route> routesList = new List<Route>();

            string sql = "select route_number, departure_time, departure_place, destination_place from routes;";

            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            { 
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(sql, connection);
                SQLiteDataReader sqlReader = Command.ExecuteReader();
                while (sqlReader.Read())
                {
                    string routeNumber = sqlReader.GetString(0);
                    string departureTime = sqlReader.GetString(1);
                    string departurePlace = sqlReader.GetString(2);
                    string destinationPlace = sqlReader.GetString(3);

                    string busNumbers = "(нет автобусов)";
                    SQLiteCommand newCommand = new SQLiteCommand("select bus_number from buses where route_number=" + routeNumber + ";", connection);
                    SQLiteDataReader newsqlReader = newCommand.ExecuteReader();
                    if (newsqlReader.HasRows) // Если у маршрута есть автобусы
                    { 
                        busNumbers = "";
                        while (newsqlReader.Read())
                        {
                            busNumbers += newsqlReader.GetString(0) + ",";
                        }
                        busNumbers = busNumbers.Substring(0, busNumbers.Length - 1); // Отрезаем последнюю запятую
                    }

                    routesList.Add(new Route(routeNumber, departureTime, departurePlace, destinationPlace, busNumbers));
                }
            }

            List<string> routeFiltersList = new List<string>();
            List<string> busFiltersList = new List<string>();

            string routeFilterQuery = "select filtering_number from users_filters where user_id=" + Core.user_id + " and filtering_type='route'";
            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(routeFilterQuery, connection);
                SQLiteDataReader sqlReader = Command.ExecuteReader();

                while (sqlReader.Read()) {
                    string routeFilter = sqlReader.GetString(0);
                    routeFiltersList.Add(routeFilter);
                }
            }

            string busFilterQuery = "select filtering_number from users_filters where user_id=" + Core.user_id + " and filtering_type='bus'";
            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand Command = new SQLiteCommand(busFilterQuery, connection);
                SQLiteDataReader sqlReader = Command.ExecuteReader();

                while (sqlReader.Read())
                {
                    string busFilter = sqlReader.GetString(0);
                    busFiltersList.Add(busFilter);
                }
            }


            List<Route> toDeleteItems = new List<Route>();
            foreach (Route route in routesList)
            {
                bool routeFilter = false;
                bool busFilter = false;




                if (routeFiltersList.Count == 0 || routeFiltersList.Contains(route.Number))
                {
                    routeFilter = true;
                }
                if (busFiltersList.Count != 0)
                {
                    List<string> routeBusNumbers = new List<string>(route.busNumbers.Split(','));
                    foreach (string routeBusNumber in routeBusNumbers)
                    {
                        if (busFiltersList.Contains(routeBusNumber))
                        {
                            busFilter = true;
                            break;
                        }
                    }
                } else
                {
                    busFilter = true;
                }
                if (routeFiltersList.Count != 0 && busFiltersList.Count != 0)
                {
                    if (!(routeFilter || busFilter))
                    {
                        toDeleteItems.Add(route);
                    }
                } else
                {
                    if (!(routeFilter && busFilter))
                    {
                        toDeleteItems.Add(route);
                    }
                }
            }

            foreach (Route deletingItem in toDeleteItems)
            {
                routesList.Remove(deletingItem);
            }

            RoutesDataGrid.ItemsSource = routesList;
        }
    }
}
