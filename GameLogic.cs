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
        private const int RESETCOUNTER = 0;
        private Form form;
        private List<Sprite> enemies;
        private int spriteSize, enemyDownCounter;
        private bool isSideOfScreen, goRight;

        public GameLogic(Form form, List<Sprite> enemies, int scaleOfSprite)
        {
            this.form = form;
            this.enemies = enemies;
            spriteSize = form.ClientRectangle.Width / scaleOfSprite;
            isSideOfScreen = false;
            goRight = true;
        }


        // This calls the logic for moving and checking the enemy sprites
        public void MoveLogic(int speed)
        {
            moveEnemies(speed);
            movementConditionsChecker();
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


    }
}
