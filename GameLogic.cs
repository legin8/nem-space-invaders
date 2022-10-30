using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class GameLogic
    {
        // Class Variables
        private const int RESETCOUNTER = 0, MAXSHOTS = 15;
        private Form form;
        private SpriteMaker spriteMaker;
        private Random random;
        private Sprite player;
        private List<Sprite> enemies;
        private List<Sprite> shots;
        private List<Sprite> bombs;
        private int spriteSize, enemyDownCounter;
        private bool isSideOfScreen, goRight;

        public GameLogic(Form form, Random random, Sprite player, SpriteMaker spriteMaker, List<Sprite> enemies, List<Sprite> shots,
            List<Sprite> bombs, int scaleOfSprite)
        {
            this.form = form;
            this.random = random;
            this.player = player;
            this.spriteMaker = spriteMaker;
            this.enemies = enemies;
            this.shots = shots;
            this.bombs = bombs;
            spriteSize = form.ClientRectangle.Width / scaleOfSprite;
            isSideOfScreen = false;
            goRight = true;
            
        }


        // This calls the logic for moving and checking the enemy sprites
        public void MoveLogic(int speed)
        {
            // Moves the enemies
            moveEnemies(speed);
            movementConditionsChecker();
            // Moves the shots and bombs
            foreach (Shot shot in shots) if (shot.SpriteBox != null) shot.MoveSprite("UP");
            foreach (Bomb bomb in bombs) if (bomb.SpriteBox != null) bomb.MoveSprite("DOWN");
        }


        // This moves the enemies either left, right or down each time.
        private void moveEnemies(int speed)
        {
            // This will move each enemy Left or Right if not at the side of the screen
            if (!isSideOfScreen) foreach (Enemy enemy in enemies)
                {
                    if (goRight) enemy.MoveSprite("RIGHT");
                    if (!goRight) enemy.MoveSprite("LEFT");
                }

            // This moves the enemies down, then increases the down counter
            // If any enemy is at the side of the screen and down counter less than the sprite size
            if (isSideOfScreen && enemyDownCounter <= spriteSize)
            {
                foreach (Enemy enemy in enemies) enemy.MoveSprite("DOWN");
                enemyDownCounter += speed;
            }
        }


        // This checks any enemy is at the side of the screen or needs to stop going down.
        private void movementConditionsChecker()
        {
            // Checks if any enemies are at the side if not already at the side
            // Doesn't run if already at the side of the screen.
            if (!isSideOfScreen) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox.Right >= form.ClientRectangle.Right ||
                        enemy.SpriteBox.Left <= form.ClientRectangle.Left)
                    {
                        isSideOfScreen = true;
                    }
                }

            // Stops the enemys from moving down once they have gone their own size and inverts goRight and isSideOfScreen
            if (enemyDownCounter >= spriteSize)
            {
                enemyDownCounter = RESETCOUNTER;
                goRight = !goRight;
                isSideOfScreen = !isSideOfScreen;
            }
        }

        // Checks if should creates a new shot unless there is already the max shots on the screen.
        // Fires the shot from the players position
        public void MakeShot()
        {
            if (shots.Count < MAXSHOTS) shots.Add(spriteMaker.MakeShot(player));
        }



        // Drops Bombs on player randomly
        public void BombLogic()
        {
            List<Sprite> enemyBombList = makeEnemyList();

            // This uses the above list to Create the bomb sprits if random number is 99
            for (int i = 0; i < enemyBombList.Count; i++)
            {
                if (random.Next(100) == 99) bombs.Add(spriteMaker.MakeBomb(enemyBombList[i]));
            }
        }

        // Returns a list of Enemies that have no other enemy sprites under them.
        private List<Sprite> makeEnemyList()
        {
            // this creates a list of the enemies on the bottom
            List<Sprite> tempEnemies = new List<Sprite>();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (i == enemies.Count - 1 && enemies[i].SpriteBox != null)
                {
                    tempEnemies.Add(enemies[i]);
                    break;
                }
                else if (enemies[i].SpriteBox != null && enemies[i + 1].SpriteBox != null && enemies[i].SpriteBox.Bottom >= enemies[i + 1].SpriteBox.Bottom)
                {
                    tempEnemies.Add(enemies[i]);
                }
            }
            return tempEnemies;
        }
    }
}
