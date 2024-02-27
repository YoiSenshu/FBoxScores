using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBoxScores.Util
{

    public class Score
    {
        public string GameName { get; set; }
        public string TotalScorePercentage { get; set; }
     

        public Score(string gameName, string totalScorePercentage)
        {
            this.GameName = gameName;
            this.TotalScorePercentage = totalScorePercentage;
        }
    }
}
