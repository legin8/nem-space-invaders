using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace project_2_space_invaders_legin8
{
    internal class Controller
    {
        // Class Variables
        private const int GAPRIGHT = 200, SPEED = 5, SCALEOFSPRITE = 26;
        private Rectangle formRectangle;
        private Form form;
        private Player player;
        private List<Enemy> enemies;
        private Shot[] shots;

        private int spriteSize, enemyDownCounter;
        private bool goRight;

        // Class Gets and Sets
        public List<Enemy> GetEnemies { get => enemies; set => enemies = value; } // check if I need this set later!!!!!!!!!!!!

        // Class Constructor
        public Controller(Rectangle formRectangle, Form form, Random random)
        {
            this.formRectangle = formRectangle;
            this.form = form;
            spriteSize = formRectangle.Width / SCALEOFSPRITE;
            player = new Player(spriteSize, form, formRectangle.Width / 2, formRectangle.Bottom - spriteSize, formRectangle, this, random);
            enemies = new List<Enemy>();
            makeEnemy();
            goRight = true;
            shots = new Shot[15];
        }

        // This updates the formRectangle on resizing the form
        public void ReSizeScreen(Rectangle formRec)
        {
            formRectangle = formRec;
            spriteSize = formRec.Width / SCALEOFSPRITE;
        }

        // This is called from the constructor and makes the enemies
        // 4 rows and 10 columns
        private void makeEnemy()
        {
            const int COLUMNS = 10, ROWS = 4;
            const double ENEMYGAP = 1.5;
            int xPosition = formRectangle.Left, yPosition;
            

            // This loop is for the rows of enemies
            for (int i = 0; i < COLUMNS; i++)
            {
                yPosition = formRectangle.Top + spriteSize;

                // This loop is for the columns enemies
                for (int j = 0; j < ROWS; j++)
                {
                    enemies.Add(new Enemy(spriteSize, form, xPosition, yPosition, SPEED));
                    yPosition += (int) (spriteSize * ENEMYGAP);
                }
                // This moves to the next column
                xPosition += spriteSize + (int)(spriteSize * ENEMYGAP);
            }
        }

        // This runs the game using the timer tick from the form
        public void RunGame()
        {
            moveEnemy();

            foreach (Shot shot in shots) if (shot != null) shot.SpriteBox.Top -= 10;
            ColisionDetection();
        }

        public void ColisionDetection()
        {
            // Checks if the shot is hitting the top of the screen
            foreach (Shot shot in shots)
            {
                if (shot != null)
                {
                    shot.TimeToLive--;
                    if (shot.SpriteBox.Top <= formRectangle.Top) RemoveShot(shot);
                }
                
            }

            // Checks if the shots are hitting the enemy
            foreach (Shot shot in shots)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (enemy != null && shot != null && enemy.SpriteBox != null &&
                        shot.SpriteBox.Top <= enemy.SpriteBox.Bottom && shot.SpriteBox.Top >= enemy.SpriteBox.Top &&
                        shot.SpriteBox.Left <= enemy.SpriteBox.Right && shot.SpriteBox.Right >= enemy.SpriteBox.Left)
                    {
                        RemoveShot(shot);
                        DestroyEnemy(enemy);
                    }
                }
            }

            foreach (Shot shot in shots)
            {
                if (shot != null && shot.TimeToLive == 0)
                {
                    RemoveShot(shot);
                    shot.TimeToLive--;
                }
            }
        }

        private void RemoveShot(Shot shot)
        {
            form.Controls.Remove(shot.SpriteBox);
            shot = null;
        }

        // Code for moving the enemies
        private void moveEnemy()
        {
            const int RESETCOUNTER = 0;
            bool isSideOfScreen = false;

            // Only one of these will run each timer tick
            // These both check if any enemy is at the edge of the screen
            if (goRight) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox != null &&
                        enemy.SpriteBox.Right >= formRectangle.Right)
                    {
                        isSideOfScreen = true;
                    }
                }

            // This only runs if enemies are going left
            if (!goRight) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox != null &&
                        enemy.SpriteBox.Left <= formRectangle.Left)
                    {
                        isSideOfScreen = true;
                    }
                }

            // This will move each enemy Right
            if (!isSideOfScreen && goRight) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox != null &&
                        enemy.SpriteBox.Right <= formRectangle.Right)
                    {
                        enemy.MoveRight();
                    }
                }

            // This will move each enemy Left
            if (!isSideOfScreen && !goRight) foreach (Enemy enemy in enemies)
                {
                    if (enemy.SpriteBox != null &&
                        enemy.SpriteBox.Left >= formRectangle.Left)
                    {
                        enemy.MoveLeft();
                    }
                }

            // This moves the enemy down the size of the sprites
            if (isSideOfScreen && enemyDownCounter <= spriteSize)
            {
                foreach (Enemy enemy in enemies) if (enemy.SpriteBox != null)
                    {
                        enemy.MoveDown();
                    }
                enemyDownCounter += SPEED;
            }

            // Stops the enemys from moving down once they have gone their own size and inverts goRight
            if (enemyDownCounter >= spriteSize)
            {
                enemyDownCounter = RESETCOUNTER;
                goRight = !goRight;
            }
        }

        // Destroy Enemy works by first removing the enemy picturebox from the control and then sets the
        // enemy picture box to null
        public void DestroyEnemy(Enemy enemy)
        {
            form.Controls.Remove(enemy.SpriteBox);
            enemy.SpriteBox = null;
        }

        // Moves the player left or right
        public void MovePlayer(bool moveLeft)
        {
            player.MovePlayer(moveLeft);
        }

        // Fires a shot from the player
        public void Shot()
        {
            foreach (Shot shot in shots)
            {
                if (shot != null) shot.Makeshot();
            }
        } 
    }
}
