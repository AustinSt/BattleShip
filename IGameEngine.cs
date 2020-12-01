using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    interface IGameEngine//interface for the game
    {
        String[] _CarrierLocation();
        String[] _BattleshipLocation();


        String[] _DestroyerLocation();
        String[] _SubmarineLocation();
        String[] _PatrolboatLocation();
    }
}
