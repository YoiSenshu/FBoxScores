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
     * - Poprawić ogólny wygląd.
     * - Powiększyć napisy w listach.
     * - Dodać możliwość wyboru danych w 1 i 2 zakładce.
     * - Usunąć możliwość klikania w elementy list w 1 i 2 zakładce.
     * - Dodać tytuł zakładki nr 2.
     * - Zmienić nazwy metod np. dodać przedrostki w zależności od zakładki.
     */


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private TrenazerpilkarskiContext context;
        private DispatcherTimer timer = new DispatcherTimer();

        public List<Miejsce> miejsca = new List<Miejsce>()
        {
            new Miejsce(1, "rfef", "fewe", 7),
            new Miejsce(2, "ewfwef", "ewffew", 5),
            new Miejsce(3, "wegrewgrew", "ewfewf", 4),
            new Miejsce(4, "ergreg", "ewffew", 3),
            new Miejsce(5, "ewfewf", "ewfwe", 1)
        };

        public TestWindow()
        {
            InitializeComponent();
            this.context = new TrenazerpilkarskiContext();

            var list = new List<Miejsce>()
            {
                new Miejsce(1, "rfef", "fewe", 7),
                new Miejsce(2, "ewfwef", "ewffew", 5),
                new Miejsce(3, "wegrewgrew", "ewfewf", 4),
                new Miejsce(4, "ergreg", "ewffew", 3),
                new Miejsce(5, "ewfewf", "ewfwe", 1)
            };

            ScoresDataGrid.ItemsSource = list;

            /*InitializeTimer();

            // Skuteczność
            Effectiveness_ScoreDataGrid.ItemsSource = new List<EffectivenessData>();
            Effectiveness_PlayerSelection.ItemsSource = context.Players.ToList();*/
        }/*

        /// <summary>
        /// 
        /// </summary>
        private void InitializeTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1); // Ustawienie interwału na 1 sekundę
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Metoda wywoływana przez timer co 1 sekundę. Odświeża dane.
        /// </summary>
        private void Timer_Tick(object? sender, EventArgs e)
        {
            this.context = new TrenazerpilkarskiContext();

            LoadGameData();
            CalculateAveragePercentage();

            showPlayerEffectiveness(); // Odświeża zakładkę "Skuteczność" (DZIAŁA)
        }

        private void LoadGameData()
        {
            try
            {
                // Pobierz najnowszy rekord gry
                var latestGameRecord = context.GameRecords.Include("GameConfig").OrderByDescending(g => g.Id).FirstOrDefault();

                if (latestGameRecord != null && latestGameRecord.GameConfig != null)
                {
                    // Ustaw nazwę gry jako tytuł okna
                    lblGameTitle.Content = latestGameRecord.GameConfig.Name;

                    // Pobierz graczy i ich wyniki związane z najnowszym rekordem gry
                    var playerScores = context.Scores.Include("Player")
                        .Where(s => s.GameRecordId == latestGameRecord.Id)
                        .OrderByDescending(s => s.TotalScore)
                        .ToList();

                    // Wyświetl listę graczy i ich wyników w kontrolce ListBox
                    lbPlayerScores.Items.Clear();
                    for (int i = 0; i < playerScores.Count; i++)
                    {
                        var playerScore = playerScores[i];
                        var player = playerScore.Player;

                        if (playerScore.Player != null)
                        {
                            lbPlayerScores.Items.Add($"{i + 1}. {player.Name} {player.Surname} - {playerScore.TotalScore} pkt");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }

        private void CalculateAveragePercentage()
        {
            try
            {
                var playerScores = context.Scores.Include("Player").ToList();

                var results = playerScores
                    .GroupBy(s => s.Player.Name + " " + s.Player.Surname)
                    .Select(group => new
                    {
                        PlayerName = group.Key,
                        AveragePercentage = group.Average(s => s.TotalScorePercentage)
                    })
                    .ToList();

                lbAveragePercentage.ItemsSource = results.Select(r => $"{r.PlayerName}: {r.AveragePercentage}%");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }




        /*
         * SKUTECZNOSC
         */

        /// <summary>
        /// Zakładka "Skuteczność"
        /// Wyświetla średnie wyniki procentowe gier podanego gracza.
        /// </summary>
        /*private void showPlayerEffectiveness()
        {
            if (Effectiveness_PlayerSelection.SelectedIndex < 0)
            {
                return;
            }

            Player player = (Player)Effectiveness_PlayerSelection.SelectedValue;

            // Sprawdzenie czy wybrany gracz istnieje w bazie danych. Jeśli gracz nie istnieje, to lista rozwijana zostanie odświeżona. NIE TESTOWANO
            if (!context.Players.Any(p => p.Id == player.Id))
            {
                Effectiveness_PlayerSelection.ItemsSource = context.Players.ToList();
                Effectiveness_PlayerSelection.SelectedIndex = -1;
                Effectiveness_ScoreDataGrid.ItemsSource = null;
                Effectiveness_PlayerNameText.Content = "Wybierz zawodnika";
                return;
            }

            // Wyświetlanie nazwy gracza i sortowanie danych.
            Effectiveness_PlayerNameText.Content = player.ToString();
            var effectivenessDataList = new List<EffectivenessData>();
            var sortedScores = sortPlayerScoresByGameConfigs(player);

            // Dodawanie do listy posortowanych danych.
            foreach (var entry in sortedScores)
            {
                effectivenessDataList.Add(new EffectivenessData(entry.Key.Name, entry.Value));
            }

            // Wyświetlanie przetworzonych danych w tabeli.
            Effectiveness_ScoreDataGrid.ItemsSource = effectivenessDataList;
        }

        /// <summary>
        /// Sortuje GameRecordPlayer'y podanego gracza według GameConfig'ów.
        /// </summary>
        private Dictionary<GameConfig, List<GameRecordPlayer>> sortPlayerScoresByGameConfigs(Player player)
        {
            var sorted = new Dictionary<GameConfig, List<GameRecordPlayer>>();
            var scores = player.Scores;

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
        }*/

        /*
         * EVENTS
         */

        /*private void PlayerSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showPlayerEffectiveness();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Stop();
            context.Dispose();
        }*/
    }
}
