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
    public partial class ShowFavoriteWindow : Window
    {
        public ShowFavoriteWindow()
        {
            InitializeComponent();
            FillFavoriteRoutesDataGrid();
            FillFavoriteBusesDataGrid();
        }

        private void FillFavoriteRoutesDataGrid()
        {
            List<Route> favoriteRoutesList = new List<Route>();

            string query = "select route_number, departure_time, departure_place, destination_place from routes where route_number in (select favorite_number from users_favorite where user_id=" + Core.user_id + " and favorite_type='route');";
            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand getRoutesCommand = new SQLiteCommand(query, connection);
                SQLiteDataReader sqlRoutesReader = getRoutesCommand.ExecuteReader();
                while (sqlRoutesReader.Read())
                {
                    string routeNumber = sqlRoutesReader.GetString(0);
                    string departureTime = sqlRoutesReader.GetString(1);
                    string departurePlace = sqlRoutesReader.GetString(2);
                    string destinationPlace = sqlRoutesReader.GetString(3);

                    string busNumbers = "(нет автобусов)";
                    SQLiteCommand newCommand = new SQLiteCommand("select bus_number from buses where route_number=" + routeNumber + ";", connection);
                    SQLiteDataReader newsqlReader = newCommand.ExecuteReader();
                    if (newsqlReader.HasRows)
                    {
                        busNumbers = "";
                        while (newsqlReader.Read())
                        {
                            busNumbers += newsqlReader.GetString(0) + ",";
                        }
                        busNumbers = busNumbers.Substring(0, busNumbers.Length - 1); // Отрезаем последнюю запятую
                    }

                    favoriteRoutesList.Add(new Route(routeNumber, departureTime, departurePlace, destinationPlace, busNumbers));
                }
            }
            FavoriteRoutesDataGrid.ItemsSource = favoriteRoutesList;
        }

        private void FillFavoriteBusesDataGrid()
        {
            List<Bus> favoriteBusesList = new List<Bus>();

            string query = "select bus_number, route_number, capaсity from buses where bus_number in (select favorite_number from users_favorite where user_id=" + Core.user_id + " and favorite_type='bus');";
            using (SQLiteConnection connection = new SQLiteConnection(Core.connectionString))
            {
                connection.Open();
                SQLiteCommand getRoutesCommand = new SQLiteCommand(query, connection);
                SQLiteDataReader sqlRoutesReader = getRoutesCommand.ExecuteReader();
                while (sqlRoutesReader.Read())
                {
                    string busNumber = sqlRoutesReader.GetString(0);
                    string routeNumber = sqlRoutesReader.GetString(1);
                    int capacity = sqlRoutesReader.GetInt32(2);

                    favoriteBusesList.Add(new Bus(busNumber, routeNumber, capacity));
                }
            }
            FavoriteBusesDataGrid.ItemsSource = favoriteBusesList;
        }
    }
}
