using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace project_2_space_invaders_legin8
{
    internal class Controller
    {
        // Class Variables
        private const int SPEED = 5, SCALEOFSPRITE = 26;
        private Form form;

        private SpriteMaker spriteMaker;
        private GameLogic gameLogic;

        // Sprite Classes
        private Sprite player;
        private List<Sprite> enemies;
        private List<Sprite> shots;
        private List<Sprite> bombs;
        private EndGame endGame;
        private HighScore highScore;

        private bool playGame;

        public bool PlayGame { get => playGame; set => playGame = value; }
        // Class Constructor
        public Controller(Form form, Random random)
        {
            spriteMaker = new SpriteMaker(form, random, SCALEOFSPRITE);
            player = spriteMaker.MakePlayer();
            enemies = spriteMaker.MakeEnemies(SPEED);
            shots = new List<Sprite>();
            bombs = new List<Sprite>();
            gameLogic = new GameLogic(form, random, this, player, spriteMaker, enemies, shots, bombs, SCALEOFSPRITE);


            this.form = form;
            playGame = true;
        }


        // This runs the game using the timer tick from the form
        public bool RunGame()
        {
            if (playGame)
            {
                // Calls method that runs normal game play
                gameLogic.GameSpriteLogic(SPEED);
                return true;
            }
            if (!playGame)
            {
                form.Controls.Clear();
                endGame = new EndGame(form, player == null);
                highScore = new HighScore(player == null, form);
                return false;
            }
            return false;
        }


        // Fires a shot from the players position, Called from the Form
        public void Shot()
        {
            gameLogic.MakeShot();
        }


        // Moves the player left or right using the logic in the player class
        public void MovePlayer(bool moveLeft)
        {
            if (moveLeft && player != null) player.MoveSprite("LEFT");
            if (!moveLeft && player != null) player.MoveSprite("RIGHT");
        }
    }
}
