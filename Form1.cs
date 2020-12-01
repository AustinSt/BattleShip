using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip
{
    public partial class BattleShip : Form
    {
        int holdInputs = 0;
        int holdCounter = 0;

        String[] playerCarrier = new String[5];//holds the location that user chooses
        String[] playerBattleShip = new String[4];
        String[] playerDestroyer = new String[3];
        String[] playerSubmarine = new String[3];
        String[] playerPatrol = new String[2];

        String getAttack;
        String aiAttack;
        bool ishit = false;//for hit detection
        bool ishitTwice = false;
        bool attackOnce = true;
        
        
        AI Ai = new AI();//constructors
        Player player = new Player();
        ScoreBoard score = new ScoreBoard();

        bool isOpen = false;
        
        public BattleShip()
        {
           InitializeComponent();

            MessageBox.Show("Welcome to BattleShip!\nChoose your ships location!\nLargest from smallest ship on the bottom panel\n(5)Carrier\n(4)Battleship\n(3)Destroyer\n(3)Submarine\n(2)Patrol Boat");
            
            String[] holdCar = Ai._CarrierLocation();//this assigns the locations to layout panel after the placement has been figured out
            String[] holdBat = Ai._BattleshipLocation();
            String[] holdDes = Ai._DestroyerLocation();
            String[] holdSub = Ai._SubmarineLocation();
            String[] holdPat = Ai._PatrolboatLocation();

            tableLayoutPanel1.Enabled = false;//stops user from attacking before choosing ships
            button1.Enabled = false;//stops user from starting game before
            foreach (Control ctrl in tableLayoutPanel1.Controls)//assigns tags to all the ships for hit detection
            {
                for (int x = 0; x < holdCar.Length; x++)
                {
                    if (ctrl.Name == holdCar[x])
                    {
                        //ctrl.BackColor = Color.Purple;//remove this to not see markers this is for debug
                        ctrl.Tag = "aiCarrier";
                    }
                }
                for (int y = 0; y < holdBat.Length; y++)
                {
                    if (ctrl.Name == holdBat[y])
                    {
                        //ctrl.BackColor = Color.Purple;//remove this to not see markers this is for debug
                        ctrl.Tag = "aiBattleship";
                    }
                }
                for (int x = 0; x < holdDes.Length; x++)
                {
                    if (ctrl.Name == holdDes[x])
                    {
                        //ctrl.BackColor = Color.Purple;//remove this to not see markers this is for debug
                        ctrl.Tag = "aiDestroyer";
                    }
                }
                for (int x = 0; x < holdSub.Length; x++)
                {
                    if (ctrl.Name == holdSub[x])
                    {
                        //ctrl.BackColor = Color.Purple;//remove this to not see markers this is for debug
                        ctrl.Tag = "aiSub";
                    }
                }
                for (int x = 0; x < holdPat.Length; x++)
                {
                    if (ctrl.Name == holdPat[x])
                    {
                        //ctrl.BackColor = Color.Purple;//remove this to not see markers this is for debug
                        ctrl.Tag = "aiPatrol";
                    }
                }
                
            }
            
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }


        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

        }




        private void label_Click(object sender, MouseEventArgs e)
        {

            Label clickedLabel = sender as Label;
            
            //this is hit detection function for the top side 
            if(isOpen == false)//stops the user from attacking before choosing their ships
            {
                MessageBox.Show("Please choose your ships first!");
            }
            else
            {
                getAttack = clickedLabel.Name;
                var getTag = clickedLabel.Tag;
                
                
                if (getTag == "aiCarrier")//check for hit detection
                {
                    
                    Ai.removeHealth(1);
                    //MessageBox.Show(getTag.ToString());//debug
                    //MessageBox.Show(Ai.healthCar.ToString());
                    clickedLabel.BackColor = Color.Red;
                    clickedLabel.Enabled = false;
                }
                else if (getTag == "aiBattleship")
                {
                    
                    Ai.removeHealth(2);
                    //MessageBox.Show(getTag.ToString());
                    //MessageBox.Show(Ai.healthBat.ToString());
                    clickedLabel.BackColor = Color.Red;
                    clickedLabel.Enabled = false;
                }
                else if (getTag == "aiDestroyer")
                {
                    
                    Ai.removeHealth(3);
                    //MessageBox.Show(getTag.ToString());
                    //MessageBox.Show(Ai.healthDes.ToString());
                    clickedLabel.BackColor = Color.Red;
                    clickedLabel.Enabled = false;
                }
                else if (getTag == "aiSub")
                {
                    
                    Ai.removeHealth(4);
                    //MessageBox.Show(getTag.ToString());
                    //MessageBox.Show(Ai.healthSub.ToString());
                    clickedLabel.BackColor = Color.Red;
                    clickedLabel.Enabled = false;
                }
                else if (getTag == "aiPatrol")
                {
                    
                    Ai.removeHealth(5);
                    //MessageBox.Show(getTag.ToString());
                    //MessageBox.Show(Ai.healthPat.ToString());
                    clickedLabel.BackColor = Color.Red;
                    clickedLabel.Enabled = false;
                }
                else
                {
                    clickedLabel.BackColor = Color.White;
                    clickedLabel.Enabled = false;
                }
                attackOnce = true;//stop the ai from attacking more than once
                aiAttacking();//function for ai attack
                timer1.Start();//not really used, but can be
            }

            

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void ClickLabel2(object sender, MouseEventArgs e)
        {
            Label clickedLabel2 = sender as Label;
            
            //this is for user inputs for where to put ships, the user doesn't have much constraints in terms of where to place the ships, but that can be added on later.
            if (holdInputs < 18 && holdInputs < 5)
            {
                clickedLabel2.Tag = "Carrier";
                clickedLabel2.BackColor = Color.Orange;
                playerCarrier[holdInputs] = clickedLabel2.Name;
                
                
            }
            if (holdInputs < 18 && holdInputs <= 9 && holdInputs > 5)
            {
                if(holdInputs == 5)
                {
                    holdCounter = 0;
                }
                clickedLabel2.Tag = "Battleship";
                clickedLabel2.BackColor = Color.Blue;
                playerBattleShip[holdCounter] = clickedLabel2.Name;
            }
            if (holdInputs < 18 && holdInputs <= 12 && holdInputs > 9)
            {

                if (holdInputs == 10)
                {
                    holdCounter = 0;
                }
                clickedLabel2.Tag = "Destroyer";
                clickedLabel2.BackColor = Color.Purple;
                playerDestroyer[holdCounter] = clickedLabel2.Name;
                
            }
            if (holdInputs < 18 && holdInputs <= 15 && holdInputs > 12)
            {
                if (holdInputs == 13)
                {
                    holdCounter = 0;
                }
                clickedLabel2.Tag = "Sub";
                clickedLabel2.BackColor = Color.Green;
                playerSubmarine[holdCounter] = clickedLabel2.Name;
                
            }
            if (holdInputs < 18 && holdInputs < 18 && holdInputs > 15)
            {
                if (holdInputs == 16)
                {
                    holdCounter = 0;
                }
                clickedLabel2.Tag = "Patrol";
                clickedLabel2.BackColor = Color.Yellow;
                playerPatrol[holdCounter] = clickedLabel2.Name;
                
            }
            holdInputs++;  
            if (holdInputs == 18)
            {
                player.CarrierLocation= playerCarrier;
                player.BattleshipLocation = playerBattleShip;
                player.DestroyerLocation = playerDestroyer;
                player.SubmarineLocation = playerSubmarine;
                player.PatrolboatLocation = playerPatrol;
                MessageBox.Show("All ships chosen!");
                clickedLabel2.Enabled = false;
                isOpen = true;
                button1.Enabled = true;
            }


            
        }

        public void ButtonClick(object sender, MouseEventArgs e)
        {
            Button buttonClick = sender as Button;//button for starting the game, opens up the top layoutpanel to attack
            timer1.Enabled = true;
            tableLayoutPanel1.Enabled = true;
            if(isOpen == true)
            {
                buttonClick.Enabled = false;
            }

           
            MessageBox.Show("Player goes first! Choose a box in the top section!");
            
            
        }

        public void aiAttacking()//function for ai attacking, actual logic is in AI class.
        {


            aiAttack = Ai.aiAttack(ishit, ishitTwice);
            
            if (aiAttack == "X")
            {
                aiAttacking();
            }
            if (attackOnce == true)//checks to see if the hit matches
            {
                attackOnce = false;
                foreach (Control control in tableLayoutPanel2.Controls)
                {
                    if (control.Name.Contains(aiAttack))
                    {
                        if (control.Tag == "Carrier")
                        {
                            player.removeHealth(1);
                            control.BackColor = Color.Red;
                            if(ishit == true)
                            {
                                ishitTwice = true;
                            }
                            ishit = true;
                        }
                        else if (control.Tag == "Battleship")
                        {
                            player.removeHealth(2);
                            control.BackColor = Color.Red;
                            if (ishit == true)
                            {
                                ishitTwice = true;
                            }
                            ishit = true;

                        }
                        else if (control.Tag == "Destroyer")
                        {
                            player.removeHealth(3);
                            control.BackColor = Color.Red;
                            if (ishit == true)
                            {
                                ishitTwice = true;
                            }
                            ishit = true;
                        }
                        else if (control.Tag == "Sub")
                        {
                            player.removeHealth(4);
                            control.BackColor = Color.Red;
                            if (ishit == true)
                            {
                                ishitTwice = true;
                            }
                            ishit = true;
                        }
                        else if (control.Tag == "Patrol")
                        {
                            player.removeHealth(5);
                            control.BackColor = Color.Red;
                            if (ishit == true)
                            {
                                ishitTwice = true;
                            }
                            ishit = true;
                        }
                        else if (control.Tag == null)
                        {
                            control.BackColor = Color.White;
                            ishit = false;
                            ishitTwice = false;
                        }
                    }
                }
            }

            checkForWinner();
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            
            

        }
        private void checkForWinner()//checks if there is a winner/updates score sent to scoreboard 
        {

            foreach (Control ctrl in tableLayoutPanel3.Controls)
            {
                if (ctrl.Name == "playerScore")
                {
                    
                    ctrl.Text = score.showScore(player.healthCar, player.healthBat, player.healthDes, player.healthSub, player.healthPat).ToString();
                }
            }
            foreach (Control control in tableLayoutPanel3.Controls)
            {
                if (control.Name == "enemyScore")
                {
                    control.Text = score.showScore(Ai.healthCar, Ai.healthBat, Ai.healthDes, Ai.healthSub, Ai.healthPat).ToString();
                }
            }



            if (Ai.healthCar == 0 && Ai.healthBat == 0 && Ai.healthDes == 0 && Ai.healthSub == 0 && Ai.healthPat == 0)//checks the health of ships
            {
                MessageBox.Show("You won!");
                Close();
            }
           
            if (player.healthCar == 0 && player.healthBat == 0 && player.healthDes == 0 && player.healthSub == 0 && player.healthPat == 0)
            {
                MessageBox.Show("You lost!");
                Close();
            }
        }

        private void playerScore_Click(object sender, EventArgs e)
        {

        }

        private void ScoreBoard_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
