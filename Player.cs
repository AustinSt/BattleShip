using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Player : Ships
    {
        public int healthCar = 5;
        
        public int healthBat = 4;
        public int healthDes = 3;
        public int healthSub = 3;
        public int healthPat = 2;
        public String[] CarrierLocation;
        public String[] BattleshipLocation;
        public String[] DestroyerLocation;
        public String[] SubmarineLocation;
        public String[] PatrolboatLocation;

        

        public Player()//constructor
        {
            
        }

        public override String[] _CarrierLocation()//these all return the location of the ships
        {
           return CarrierLocation;
        }
        public override String[] _BattleshipLocation()
        {

            
            return BattleshipLocation;
        }
        public override String[] _DestroyerLocation()
        {
            
            return DestroyerLocation;
        }

        public override String[] _SubmarineLocation()
        {
            
            return SubmarineLocation;
        }

        public override String[] _PatrolboatLocation()
        {
            
            return PatrolboatLocation;
        }


        public void removeHealth(int whichHit)//health function
        {
            if (whichHit == 1)
            {
                healthCar = healthCar-1;
            }
            if (whichHit == 2)
            {
                healthBat = healthBat-1;
            }
            if (whichHit == 3)
            {
                healthDes = healthDes-1;
            }
            if (whichHit == 4)
            {
                healthSub = healthSub-1;
            }
            if (whichHit == 5)
            {
                healthPat = healthPat-1;
            }
        }
    }
}
