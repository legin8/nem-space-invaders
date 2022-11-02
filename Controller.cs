/* Program name: project-2-space-invaders-legin8
Project file name: Controller.cs
Author: Nigel Maynard
Date: 25/10/22
Language: C#
Platform: Microsoft Visual Studio 2022
Purpose: Class work
Description: Assessment game: Space Invaders
Known Bugs:
Additional Features:
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    // This Class is where we hold the lists and is connected to all other classes
    // Inputs from the form are passed through here.
    public class Controller
    {
        // Class Variables
        private const int SPEED = 3, SCALEOFSPRITE = 26;

        // Reference to the form1 and the classes responsible for running the game
        private Form form;
        private SpriteMaker spriteMaker;
        private GameLogic gameLogic;

        // Sprite Classes
        private Sprite player;
        private List<Sprite> enemies;
        private List<Sprite> shots;
        private List<Sprite> bombs;
        
        // This is used to tell if the game is over or not and who won if it's over.
        // playerWin isn't assigned because the default value is false and it's only changed if the player wins.
        private bool playGame, playerWin;

        // Sets the data for knowing if the game is over or not and the winner
        // Only sets no need for a get as this value only does anything in this class
        public bool PlayGame { set => playGame = value; }
        public bool PlayerWin { set => playerWin = value; }

        // Class Constructor
        // The code for making the Lists and player is being called from the SpriteMaker class,
        // this is to make the code easier to read by breaking it up.
        public Controller(Form form, Random random)
        {
            this.form = form;
            spriteMaker = new SpriteMaker(form, random, SCALEOFSPRITE);
            player = spriteMaker.MakePlayer();
            enemies = spriteMaker.MakeEnemies(SPEED, this);
            // shots and bombs are created here but remain empty until using input or random at run time.
            shots = new List<Sprite>();
            bombs = new List<Sprite>();
            gameLogic = new GameLogic(form, random, this, player, spriteMaker, enemies, shots, bombs, SCALEOFSPRITE);
            playGame = true;
        }


        // This runs the game using the timer tick from the form
        // This returns true or false to the form where it's being called, false will stop the timer.
        // The return keyword in both ifs stop the rest of the code being looked at as well.
        // The extra return keyword under to 2 ifs will never run, it's only there because it has to return in all conditions.
        public bool RunGame()
        {
            // This will run the game while the player and enemies exist
            if (playGame)
            {
                // Calls method that runs normal game play
                gameLogic.GameSpriteLogic(SPEED);
                return true;
            }

            // This will play the End game and show the score
            if (!playGame)
            {
                bool winnerIs = playerWin ? true : false;
                form.Controls.Clear();
                new EndGame(form, player == null);
                new HighScore(winnerIs, form);
                return false;
            }
            return false;
        }


        // Calls the logic in gameLogic to fire a shot
        public void Shot()
        {
            gameLogic.MakeShot();
        }


        // Sets the new direction for the player to move and then calls the method in the player that moves itself.
        public void MovePlayer(EDirection direction)
        {
            player.SpriteEDirection = direction;
            player.MoveSprite();
        }
    }
}
