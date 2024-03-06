using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBoxScores.Util
{
    public class Place
    {
        public int PlaceNumber {  get; set; }
        public string Name {  get; set; }
        public string Surname {  get; set; }
        public string? ClubName {  get; set; }
        public int Points {  get; set; }
        public int PointsPercentage {  get; set; } // 1% = 1, 100% = 100

        public Place(int placeNumber, string name, string surname, string? clubName, int points, int pointsPercentage)
        {
            PlaceNumber = placeNumber;
            Name = name;
            Surname = surname;
            ClubName = clubName;
            Points = points;
            PointsPercentage = pointsPercentage;
        }
    }
}
