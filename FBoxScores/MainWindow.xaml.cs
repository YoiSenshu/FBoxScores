using FBOX.Entities;
using FBoxScores.Util;
using Google.Protobuf;
using System;
using System.Collections.Generic;
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

namespace FBoxScores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TrenazerpilkarskiContext context;
        public List<Score> PlayerScores = new List<Score>();

        public MainWindow()
        {
            InitializeComponent();

            this.context = new TrenazerpilkarskiContext();
            DataGridZawodnicy.ItemsSource = context.Players.ToList();
            PlayerSelection.ItemsSource = context.Players.ToList();
        }

        /// <summary>
        /// Ładuje wyniki gier podanego gracza i wyświetla je w zakładce "Zawodnik".
        /// </summary>
        public void LoadPlayerData(Player player)
        {
            var data = this.context.Scores.Where(score => score.Player == player).ToList();
            this.PlayerScores.Clear();
            

            foreach (GameRecordPlayer score in data)
            {
                this.context.Entry(score).Reference(s => s.GameRecord).Load();
                this.context.Entry(score.GameRecord).Reference(r => r.GameConfig).Load();
                this.PlayerScores.Add(new Score(score.GameRecord.GameConfig.Name, Math.Round(score.TotalScorePercentage * 100, 1) + "%" ));
                
            }
            ScoreData.ItemsSource = null; //dla refhreshu danych
            ScoreData.ItemsSource = this.PlayerScores;
            PlayerNameText.Text = player.Name + " " + player.Surname;
        }

        private void PlayerSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PlayerSelection.SelectedValue == null)
            {
                return;
            }
            this.LoadPlayerData((Player) PlayerSelection.SelectedValue);
        }
    }
}
