using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class AI : Ships
    {

        
        public int healthCar = 5;//ship health
        public int healthBat = 4;
        public int healthDes = 3;
        public int healthSub = 3;
        public int healthPat = 2;

       

        bool lastAttack;//attack
        int directionloc;
        int[] direction = { -1, 1, -10, 10 };//direction for both placement and attack
        
        bool isOpen = true;

        int lastIndexHold;
        public String[] CarrierLocation = new string[5] { "0", "0", "0", "0", "X"};//hold the location uses "X" to figure if the array is full
        public String[] BattleshipLocation = new string[4] { "0", "0", "0", "X" };
        public String[] DestroyerLocation = new string[3] { "0", "0", "X" };
        public String[] SubmarineLocation = new string[3] { "0", "0", "X" };
        public String[] PatrolboatLocation = new string[2] { "0", "X" };



        List<String> attackPosition = new List<String> {//list for the ai attacking the player
                "Q01", "Q2", "Q3", "Q4", "Q5", "Q6", "Q7", "Q8", "Q9", "Q10",
                "R01", "R2", "R3", "R4", "R5", "R6", "R7", "R8", "R9", "R10",
                "S01", "S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "S10",
                "T01", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10",
                "U01", "U2", "U3", "U4", "U5", "U6", "U7", "U8", "U9", "U10",
                "V01", "V2", "V3", "V4", "V5", "V6", "V7", "V8", "V9", "V10",
                "W01", "W2", "W3", "W4", "W5", "W6", "W7", "W8", "W9", "W10",
                "X01", "X2", "X3", "X4", "X5", "X6", "X7", "X8", "X9", "X10",
                "Y01", "Y2", "Y3", "Y4", "Y5", "Y6", "Y7", "Y8", "Y9", "Y10",
                "Z01", "Z2", "Z3", "Z4", "Z5", "Z6", "Z7", "Z8", "Z9", "Z10"
                };

        List<String> aiPosition = new List<String> {//list for the ai choosing spots
                "A01", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10",
                "B01", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10",
                "C01", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10",
                "D01", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10",
                "E01", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10",
                "F01", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10",
                "G01", "G2", "G3", "G4", "G5", "G6", "G7", "G8", "G9", "G10",
                "H01", "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10",
                "I01", "I2", "I3", "I4", "I5", "I6", "I7", "I8", "I9", "I10",
                "J01", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J10"
                };
        List<String> holdaiPosition = new List<string>();//copys into another list incase the function needs to be reset
        
        private readonly Random random = new Random();
        public AI()//constructor for ai has all the poisitions 
        {

            holdaiPosition = aiPosition;
            _CarrierLocation();
            _BattleshipLocation();
            _DestroyerLocation();
            _SubmarineLocation();
            _PatrolboatLocation();

        }

        public override String[] _CarrierLocation()
        {
            CarrierLocation[0] = "B2";
            CarrierLocation[1] = "B3";
            CarrierLocation[2] = "B4";
            CarrierLocation[3] = "B5";
            CarrierLocation[4] = "B6";
            /*calculatePos(CarrierLocation);//this is for the random placement of the AI's ships. I couldn't get this to fully work, but if you uncomment and comment out the static locations it will work
            if (CarrierLocation.Contains("X") || CarrierLocation.Contains("0"))
            {
                calculatePos(CarrierLocation);
            }*/
            return CarrierLocation;
        }
        public override String[] _BattleshipLocation()
        {
            
            BattleshipLocation[0] = "D4";
            BattleshipLocation[1] = "E4";
            BattleshipLocation[2] = "F4";
            BattleshipLocation[3] = "G4";
            /*calculatePos(BattleshipLocation);
            if(BattleshipLocation.Contains("X") || CarrierLocation.Contains("0"))
            {
                calculatePos(BattleshipLocation);
            }*/
            return BattleshipLocation;
        }
        public override String[] _DestroyerLocation()
        {
            DestroyerLocation[0] = "G9";
            DestroyerLocation[1] = "G8";
            DestroyerLocation[2] = "G7";
            /*calculatePos(DestroyerLocation);
            if (DestroyerLocation.Contains("X") || CarrierLocation.Contains("0"))
            {
                calculatePos(DestroyerLocation);
            }*/
            return DestroyerLocation;
        }

        public override String[] _SubmarineLocation()
        {
            SubmarineLocation[0] = "H5";
            SubmarineLocation[1] = "H6";
            SubmarineLocation[2] = "H7";
            /*calculatePos(SubmarineLocation);
            if (SubmarineLocation.Contains("X") || CarrierLocation.Contains("0"))
            {
                calculatePos(SubmarineLocation);
            }*/
            return SubmarineLocation;
        }

        public override String[] _PatrolboatLocation()
        {
            /*PatrolboatLocation[0] = "J2";
            PatrolboatLocation[1] = "J3";*/
            /*calculatePos(PatrolboatLocation);
            if (PatrolboatLocation.Contains("X") || CarrierLocation.Contains("0"))
            {
                calculatePos(PatrolboatLocation);
            }*/
            return PatrolboatLocation;
        }
        
        public void removeHealth(int whichHit)//ship health decrement when hit
        {
            if(whichHit == 1)
            {
                healthCar = healthCar-1;
            }
            if(whichHit == 2)
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
        public string aiAttack(bool isHit, bool isHitTwice)//this is the AI attacking function
        {
            string holdLocation = "X";
            int holdIndex = random.Next(attackPosition.Count);//chooses random location on the board
            holdLocation = attackPosition[holdIndex];


            lastAttack = isHit;
            if (lastAttack == true && isHitTwice != true)
            {

                directionloc = random.Next(direction.Length);//chooses a direction
                directionloc = direction[directionloc];
                holdIndex = lastIndexHold;

                holdIndex += directionloc;



                if (holdIndex > 99)
                {
                    holdIndex -= 11;
                }
                if (holdIndex < 1)
                {
                    holdIndex += 11;
                }
                holdLocation = attackPosition[holdIndex];//gives the actual choosen list at the index randomly choosen
                attackPosition.Remove(holdLocation);//removes it from the list
                attackPosition.Insert(holdIndex, "X");//replaces with X, once it goes back to Form1, Form1 will check if it is an X and will rerun this function until it gets a proper location


                lastIndexHold = holdIndex;

                return holdLocation;

            }
            if (lastAttack == true && isHitTwice == true)//this is if it attacked before and hit to check another direction around it
            {
                holdIndex = lastIndexHold;
                holdIndex += directionloc;
                if (holdIndex > 99)
                {
                    holdIndex -= 11;
                }
                if (holdIndex < 1)
                {
                    holdIndex += 11;
                }
                holdLocation = attackPosition[holdIndex];
                attackPosition.Remove(holdLocation);
                attackPosition.Insert(holdIndex, "X");


                lastIndexHold = holdIndex;

                return holdLocation;
            }

            if (holdLocation != "X" && lastAttack == false)
            {

                lastIndexHold = holdIndex;
                attackPosition.Remove(holdLocation);
                attackPosition.Insert(holdIndex, "X");
                return holdLocation;
            }
            return holdLocation;
        }
        public String[] calculatePos(String[] ship)//this is for choosing placement at the beginning of the game, sometimes works.
        {
            int directionIndex;
            int x = 0;
            
            int directionHold = 0;
            int holdIndex = 1;
            String holdLocation;


            while (ship[ship.Length-1] == "X")//the "X" will determine if the array is full or not
            {
                if (isOpen == true)
                {
                    x = 0;
                    directionIndex = random.Next(direction.Length);//i used this direction so that the ships would spawn next to one another, like in the actual game
                    directionHold = direction[directionIndex];
                    holdIndex = random.Next(aiPosition.Count);//give random index inside list
                    holdLocation = aiPosition[holdIndex];//give that random number out
                    aiPosition.Remove(holdLocation);//remove it
                    aiPosition.Insert(holdIndex, "X");//replace with "X"
                    ship[0] = holdLocation;//insert
                    if (ship[0] == "X")//check to make sure location isn't taken with the "X" value
                    {
                        ship[0] = "0";
                        isOpen = true;
                    }
                    else
                    {
                        isOpen = false;
                        x++;
                    }
                }
                holdIndex += directionHold;//iterates
                
                if (holdIndex >= 0 && holdIndex < aiPosition.Count && ship[0] != "0" && ship[0] != "X")//makes sure it isn't out of bounds
                {

                    holdLocation = aiPosition[holdIndex];//hold the location
                    aiPosition.Remove(holdLocation);//remove it
                    aiPosition.Insert(holdIndex, "X");//replace
                    
                    ship[x] = holdLocation;//lock it in
                    if(ship[x] == "X" && ship.Last() == "X")//again checking to make sure the location doesn't have an "X" value in it
                    {
                        x = 0;
                        //Array.Clear(ship, 0, ship.Length);
                        //ship[ship.Length - 1] = "X";
                        for(int k=0; k<ship.Length-1;k++)
                        {
                            ship[k] = "X";
                        }
                        aiPosition = holdaiPosition;
                        isOpen = true;

                    }
                    x++;


                    if (ship.Any(s => s.Contains("01")) && ship.Any(z => z.Contains("10")))//my attempt to stop the ships from having half of it on the bottom and another half on the top
                    {//doesn't completly work correctly
                        x = 0;
                        //Array.Clear(ship, 0, ship.Length);
                        for (int k = 0; k < ship.Length - 1; k++)
                        {
                            ship[k] = "X";
                        }
                        //ship[ship.Length - 1] = "X";
                        aiPosition = holdaiPosition;
                        isOpen = true;
                    }

                }
                else
                {
                    x = 0;
                    //Array.Clear(ship, 0, ship.Length);
                    //ship[ship.Length - 1] = "X";
                    for (int k = 0; k < ship.Length - 1; k++)
                    {
                        ship[k] = "X";
                    }
                    aiPosition = holdaiPosition;
                    isOpen = true;

                }
                
                
                    

                
            }
            holdaiPosition = aiPosition;//sets the current to the master list
            return ship;
        }
    }
}
