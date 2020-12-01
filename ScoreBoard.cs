using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class ScoreBoard
    {
        public ScoreBoard()
        {

        }
        public int showScore(int healthcar, int healthbat, int healthdes, int healthsub, int healthpat)//just adds up health which is the score
        {
            return healthcar + healthbat + healthdes + healthsub + healthpat;
        }
        
    }
}
