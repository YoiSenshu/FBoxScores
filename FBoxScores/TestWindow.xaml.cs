using FBox.Entities;
using FBoxScores.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
        private KeyValueConfigurationCollection settings;
        private TrenazerpilkarskiContext context;
        private DispatcherTimer timer = new DispatcherTimer();

        public List<Place> miejsca;

        public TestWindow()
        {
            InitializeComponent();

            settings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;
            context = new TrenazerpilkarskiContext(settings["database_connection"].Value);
            ApplyConfiguration();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerTick;
            timer.Start();

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

        private void OnTimerTick(object? sender, EventArgs? e)
        {
            context = new TrenazerpilkarskiContext(settings["database_connection"].Value);

            /*
             * Odświerzanie danych
             */
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

        private void ApplyConfiguration()
        {
            LogoImage.Source = new BitmapImage(new Uri(settings["logo_image_source"].Value, UriKind.RelativeOrAbsolute));

            /*
             * TODO naprawić
            Uri path = new Uri("Fonts/Ojuju-Regular.ttf", UriKind.Relative);

            if(true)
            {
                System.Windows.Media.FontFamily font = new System.Windows.Media.FontFamily(path, "test");
                this.FontFamily = font;

            } else
            {
                MessageBox.Show("Nie udało się załadować czcionki z pliku. Czcionka została ustawiona na domyślną.", "Wystąpił problem!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }
    }
}
