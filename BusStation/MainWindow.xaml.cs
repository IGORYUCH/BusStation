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

namespace BusStation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowAllRoutersButton_Click(object sender, RoutedEventArgs e)
        {
            ShowRoutesWindow showRoutersWindow = new ShowRoutesWindow();
            showRoutersWindow.Show();

        }

        private void FilterSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            FilterSettingsWindow filterSettingsWindow = new FilterSettingsWindow();
            filterSettingsWindow.Show();
        }

        private void ShowFavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            ShowFavoriteWindow showFavoriteWindow = new ShowFavoriteWindow();
            showFavoriteWindow.Show();

        }

        private void ChangeFavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeFavoriteWindow changeFavoriteWindow = new ChangeFavoriteWindow();
            changeFavoriteWindow.Show();

        }
    }
}
