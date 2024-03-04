using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBoxScores.Util
{
    public class Miejsce
    {
        public int miejsce {  get; set; }
        public string imieINazwisko {  get; set; }
        public string klub {  get; set; }
        public int punkty {  get; set; }

        public Miejsce(int miejsce, string imieINazwisko, string klub, int punkty)
        {
            this.miejsce = miejsce;
            this.imieINazwisko = imieINazwisko;
            this.klub = klub;
            this.punkty = punkty;
        }
    }
}
