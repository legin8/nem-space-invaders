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
        private const int SPEED = 5, SCALEOFSPRITE = 26;
        private Rectangle formRectangle;
        private Form form;

        private Player player;
        private List<Enemy> enemies;
        private Shot[] shots;

        private int spriteSize, enemyDownCounter;
        private bool goRight;

        // Class Constructor
        public Controller(Rectangle formRectangle, Form form, Random random)
        {
            this.formRectangle = formRectangle;
            this.form = form;
            spriteSize = formRectangle.Width / SCALEOFSPRITE;

            player = new Player(spriteSize, form, formRectangle.Width / 2, formRectangle.Bottom - spriteSize, Properties.Resources.player, formRectangle, this, random);
            enemies = new List<Enemy>();
            makeEnemy();
            shots = new Shot[15];

            for (int i = 0; i < shots.Length; i++) shots[i] = new Shot(spriteSize, form, player.SpriteBox.Left, player.SpriteBox.Top - spriteSize, Properties.Resources.shot, random);

            goRight = true;
            
            
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
                    enemies.Add(new Enemy(spriteSize, form, xPosition, yPosition, Properties.Resources.enemy, SPEED));
                    yPosition += (int) (spriteSize * ENEMYGAP);
                }
                // This moves to the next column
                xPosition += spriteSize + (int)(spriteSize * ENEMYGAP);
            }
        }


        // This runs the game using the timer tick from the form
        public void RunGame()
        {
            // Calls method that moves the enemys
            moveEnemy();
            // Removes shot at its random time
            foreach (Shot shot in shots) if (shot != null && shot.TimeToLive == 0) DestroySprite(shot);
            // Moves the shot each timer tick
            foreach (Shot shot in shots) if (shot.SpriteBox != null) shot.SpriteBox.Top -= 10;
            // Calls method for colision detection between PictureBoxs
            ColisionDetection();
        }

        public void ColisionDetection()
        {
            // Checks if the shot is hitting the top of the screen and bring the shot closer to its random death
            foreach (Shot shot in shots)
            {
                if (shot.SpriteBox != null)
                {
                    shot.TimeToLive--;
                    if (shot.SpriteBox.Top <= formRectangle.Top) DestroySprite(shot);
                }
            }

            // Checks if the shots are hitting the enemy
            foreach (Shot shot in shots)
            {
                foreach (Enemy enemy in enemies)
                {
                    // This starts by checking if shot and enemy PictureBox exist before checking the rest
                    // if they don't exist it won't look passed the first false
                    if (shot.SpriteBox != null && enemy.SpriteBox != null &&
                        shot.SpriteBox.Top <= enemy.SpriteBox.Bottom && shot.SpriteBox.Top >= enemy.SpriteBox.Top &&
                        shot.SpriteBox.Left <= enemy.SpriteBox.Right && shot.SpriteBox.Right >= enemy.SpriteBox.Left)
                    {
                        DestroySprite(shot);
                        DestroySprite(enemy);
                    }
                }
            }
        }

        // Destroy Enemy works by first removing the enemy picturebox from the control and then sets the
        // enemy picture box to null
        public void DestroySprite(Sprite sprite)
        {
            form.Controls.Remove(sprite.SpriteBox);
            sprite.SpriteBox = null;
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

        // Moves the player left or right
        public void MovePlayer(bool moveLeft)
        {
            player.MovePlayer(moveLeft);
        }

        // Fires a shot from the player
        public void Shot()
        {
            for (int i = 0; i < shots.Length; i++)
            {
                if (shots[i].SpriteBox == null)
                {
                    shots[i].MakeShot(player.SpriteBox.Left, player.SpriteBox.Top - spriteSize);
                    break;
                }
            }
        } 
    }
}
