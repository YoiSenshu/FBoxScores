using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBox.Entities
{
    public class GameConfig
    {
        public int Id { get; set; }
        public  string  Name { get; set; }//Display Name
        public float  StartDelay { get; set; } //For countdown before start of game
        public String Exercises { get; set; } 
        //format-> exerciseId,Weight,PlayerSwitchDelay,AfterDelay|exerciseId,Weight,PlayerSwitchDelay,AfterDelay|..
        public virtual List<GameRecord> GameRecords { get; set; } = new List<GameRecord>();
    }
}
