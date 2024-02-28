using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBox.Entities
{
    public class GameRecord
    {
        public int Id { get; set; }
        public virtual GameConfig? GameConfig { get;set; }
        public int? GameConfigId { get; set; }
        public DateTime? StartDate { get; set; }
        public virtual List<GameRecordPlayer> Scores { get; set; } = new List<GameRecordPlayer>();
    }
}
