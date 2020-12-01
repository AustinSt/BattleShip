using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    abstract class Ships : IGameEngine//abstract class
    {

        
        
            
        
        
        public abstract String[] _CarrierLocation();
        public abstract String[] _BattleshipLocation();


        public abstract String[] _DestroyerLocation();
        public abstract String[] _SubmarineLocation();
        public abstract String[] _PatrolboatLocation();

        

        
        
        
    }
}
