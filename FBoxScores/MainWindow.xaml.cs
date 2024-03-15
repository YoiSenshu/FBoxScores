using FBox.Entities;
using FBoxScores.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FBoxScores
{
    /*
     * TODO:
     * Dodać możliwość zmiany czcionki.
     * Powiększyć 3 pierwsze wiersze w tabelach.
     * Przenieść właściwości do styli.
     * Uzupełnić xaml dla 2 zakładek.
     * Pobierać style z konfiguracji.
     * Podpiąć kod do pól (działanie aplikacji)
     */

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KeyValueConfigurationCollection settings;
        private TrenazerpilkarskiContext context;
        private DispatcherTimer timer = new DispatcherTimer();
        public ObservableCollection<PlayerEffectiveness> EffectivenessList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            settings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;
            context = new TrenazerpilkarskiContext(settings["database_connection"].Value);
            ApplyConfiguration();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += OnTimerTick;
            timer.Start();

            EffectivenessPlayerComboBox.ItemsSource = context.Players.ToList();
        }

        /// <summary>
        /// Odświeża dane z bazy danych i uzupełnia pola.
        /// </summary>
        private void RefreshData()
        {
            context = new TrenazerpilkarskiContext(settings["database_connection"].Value);

            RefreshLastGameSection();
            RefreshPlayerEffectivitySection();
            //
        }

        private void RefreshLastGameSection()
        {
            var latestGameRecord = context.GameRecords.Include("GameConfig").OrderByDescending(g => g.Id).FirstOrDefault();

            if (latestGameRecord != null && latestGameRecord.GameConfig != null)
            {
                // Ustaw nazwę gry jako tytuł okna
                LastGameName.Content = latestGameRecord.GameConfig.Name;

                // Pobierz graczy i ich wyniki związane z najnowszym rekordem gry
                var playerScores = context.Scores.Include("Player")
                    .Where(s => s.GameRecordId == latestGameRecord.Id)
                    .OrderByDescending(s => s.TotalScore)
                    .ToList();

                // Wyświetl listę graczy i ich wyników w kontrolce ListBox
                List<PlayerPlace> places = new List<PlayerPlace>();
                LastGameScoreboard.ItemsSource = null;

                for(int i = 0; i < playerScores.Count; i++)
                {
                    var score = playerScores[i];
                    if (score.Player == null) continue;
                    Player player = score.Player;

                    places.Add(new PlayerPlace(i + 1, player.Name, player.Surname, null, (int)score.TotalScore, (int)score.TotalScorePercentage));
                }

                LastGameScoreboard.ItemsSource = places;
            }
        }

        private void RefreshPlayerEffectivitySection()
        {
            if(EffectivenessPlayerComboBox.SelectedItem == null)
            {
                EffectivenessPlayerNameLabel.Content = "Wybierz zawodnika...";
                EffectivenessPlayerComboBox.ItemsSource = context.Players.ToList();
                EffectivenessScoreboard.ItemsSource = null;
                return;
            }

            Player player = (Player) EffectivenessPlayerComboBox.SelectedItem;
            var sortedScores = sortPlayerScoresByGameConfigs(player);

            EffectivenessPlayerNameLabel.Content = player.Name + " " + player.Surname;
            List<PlayerEffectiveness> effectiveness = new List<PlayerEffectiveness>();
            EffectivenessScoreboard.ItemsSource = null;

            foreach (var entry in sortedScores)
            {
                effectiveness.Add(new PlayerEffectiveness(entry.Key.Name, entry.Value));
            }

            EffectivenessScoreboard.ItemsSource = effectiveness;
        }

        /// <summary>
        /// Sortuje GameRecordPlayer'y podanego gracza według GameConfig'ów.
        /// </summary>
        private Dictionary<GameConfig, List<GameRecordPlayer>> sortPlayerScoresByGameConfigs(Player player)
        {
            var sorted = new Dictionary<GameConfig, List<GameRecordPlayer>>();
            var scores = context.Players.Where(p => p.Id == player.Id).First().Scores;

            foreach (var score in scores)
            {
                context.Entry(score).Reference(s => s.GameRecord).Load();

                if (score.GameRecord == null)
                {
                    continue;
                }

                context.Entry(score.GameRecord).Reference(gr => gr.GameConfig).Load();

                if (score.GameRecord.GameConfig == null)
                {
                    continue;
                }

                if (!sorted.ContainsKey(score.GameRecord.GameConfig))
                {
                    sorted.Add(score.GameRecord.GameConfig, new List<GameRecordPlayer>());
                }

                sorted[score.GameRecord.GameConfig].Add(score);
            }

            return sorted;
        }

        private void OnTimerTick(object? sender, EventArgs? e)
        {
            RefreshData();
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

        private void EffectivenessPlayerNameLabel_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            EffectivenessPlayerNameLabel.Visibility = Visibility.Collapsed;
            EffectivenessPlayerComboBox.Visibility = Visibility.Visible;
        }

        private void EffectivenessPlayerComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            EffectivenessPlayerNameLabel.Visibility = Visibility.Visible;
            EffectivenessPlayerComboBox.Visibility = Visibility.Collapsed;
            RefreshPlayerEffectivitySection();
        }

        private void ApplyConfiguration()
        {
            LogoImage.Source = new BitmapImage(new Uri(settings["logo_image_source"].Value, UriKind.RelativeOrAbsolute));

            try
            {
                FontFamily font = new FontFamily(settings["font_name"].Value);
                FontFamily = font;
            } catch(Exception ex)
            {
                // nie załadowano czcionki
            }

            SectionOneBorder.Height = int.Parse(settings["height"].Value);
            SectionTwoBorder.Height = int.Parse(settings["height"].Value);
            SectionThreeBorder.Height = int.Parse(settings["height"].Value);
        }
    }
}
