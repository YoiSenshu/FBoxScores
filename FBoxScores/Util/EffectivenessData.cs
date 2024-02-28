using FBox.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBoxScores.Util
{

    public class EffectivenessData
    {
        public string GameName { get; set; }
        public string AverageScorePercentage { get; set; }
     

        public EffectivenessData(string gameConfigName, List<GameRecordPlayer> records)
        {
            this.GameName = gameConfigName;
            float totalScorePercentageSum = 0;

            foreach (var record in records)
            {
                totalScorePercentageSum += record.TotalScorePercentage;
            }

            this.AverageScorePercentage = Math.Round(totalScorePercentageSum / records.Count, 1) + "%";
        }
    }
}
