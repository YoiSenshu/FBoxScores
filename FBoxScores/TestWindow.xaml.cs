using FBox.Entities;
using FBoxScores.Util;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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
using System.Windows.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FBoxScores
{
    /*
     * TODO:
     * Dodać możliwość zmiany czcionki.
     * Popracować nad wyglądem aplikacji.
     * Przenieść właściwości do styli.
     * Uzupełnić xaml dla 2 zakładek.
     * Pobierać style z konfiguracji.
     * Podpiąć kod do pól (działanie aplikacji)
     */

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private TrenazerpilkarskiContext context;
        private DispatcherTimer timer = new DispatcherTimer();

        public List<Place> miejsca;

        public TestWindow()
        {
            InitializeComponent();
            this.context = new TrenazerpilkarskiContext();

            miejsca = new List<Place>()
            {
                new Place(1, "Jan", "Kowalski", "KSG", 7, 70),
                new Place(2, "Andrzej", "Nowak", "FBI", 5, 50),
                new Place(3, "Ewa", "Adamczyk", "CSI", 4, 40),
                new Place(4, "Elżbieta", "Jakaśtam", "", 3, 30),
                new Place(5, "Karol", "Kwiatkowski", null, 1, 10)
            };

            Lista.ItemsSource = miejsca;
        }

        private void MainMenuTogglerButton_Click(object sender, RoutedEventArgs e)
        {
            if(MenuGrid.Visibility != Visibility.Visible)
            {
                LastGameGrid.Visibility = Visibility.Collapsed;
                GameRecordGrid.Visibility = Visibility.Collapsed;
                PlayerScoreGrid.Visibility = Visibility.Collapsed;
                MenuGrid.Visibility = Visibility.Visible;
            }
        }

        private void Button_Game1(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            LastGameGrid.Visibility = Visibility.Visible;
        }

        private void Button_Game2(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            GameRecordGrid.Visibility = Visibility.Visible;
        }

        private void Button_Game3(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            PlayerScoreGrid.Visibility = Visibility.Visible;
        }
    }
}
