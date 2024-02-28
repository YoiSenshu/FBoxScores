using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBox.Entities
{
    public class GameRecordPlayer//Score of Player in a gameRecord
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public virtual Player? Player { get; set; }
        public int? GameRecordId { get; set; }
        public virtual GameRecord? GameRecord { get; set; }
        public float TotalScore { get; set; }
        public float TotalScorePercentage { get; set; }//TotalScore divided by max score in game
        public string ExerciseScoreList { get; set; } 
        //array of scores in exercises
        //score1|score2|...
    }
}
