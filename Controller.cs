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
        private Form form1;
        private Player player;
        private List<Enemy> enemies;
        private int spriteSize, enemyDownCounter;
        private bool goRight;

        // Class Gets and Sets
        public List<Enemy> GetEnemies { get => enemies; set => enemies = value; } // check if I need this set later!!!!!!!!!!!!
        public Rectangle FormRectangle { get => formRectangle; set => formRectangle = value; }
        public int SpriteSize { get => spriteSize; set => spriteSize = value; }

        // Class Constructor
        public Controller(Rectangle formRectangle, Form form1, Random random)
        {
            this.formRectangle = formRectangle;
            this.form1 = form1;
            spriteSize = formRectangle.Width / SCALEOFSPRITE;
            player = new Player(formRectangle, form1, spriteSize, this, random);
            enemies = new List<Enemy>();
            makeEnemy();
            goRight = true;
        }

        // This updates the formRectangle on resizing the form
        public void ReSizeScreen(Rectangle formRec)
        {
            FormRectangle = formRec;
            SpriteSize = formRec.Width / SCALEOFSPRITE;
        }

        // This is called from the constructor and makes the enemies
        private void makeEnemy()
        {
            const int COLUMNS = 10, ROWS = 4;
            int x = formRectangle.Left, y, index = 0;
            PictureBox tempPictureBox;

            // This loop is for the rows of enemies
            for (int i = 0; i < COLUMNS; i++)
            {
                y = formRectangle.Top + spriteSize;

                // This loop is for the columns enemies
                for (int j = 0; j < ROWS; j++)
                {
                    tempPictureBox = new PictureBox();
                    tempPictureBox.Width = spriteSize;
                    tempPictureBox.Height = spriteSize;
                    tempPictureBox.Image = Properties.Resources.enemy;
                    tempPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    tempPictureBox.Location = new Point(x, y);
                    enemies.Add(new Enemy(formRectangle,spriteSize, tempPictureBox, SPEED));
                    form1.Controls.Add(tempPictureBox);

                    // Code above is for each enemy, below is for spacing them out and the index number

                    index++;
                    y += spriteSize * 2;
                }
                // This moves to the next column
                x += spriteSize + (int)(spriteSize * 1.4);
            }
        }

        // This runs the game
        public void RunGame()
        {
            PictureBox[] tempPictureBoxes = player.GetShots;
            moveEnemy();

            for (int i = 0; i < player.GetShots.Length; i++) if (tempPictureBoxes[i] != null) tempPictureBoxes[i].Top -= 10;
            player.GetRidOfShot();
        }

        // Code for moving the enemies
        private void moveEnemy()
        {
            bool isSideOfScreen = false;
            // Only one of these will run each timer tick
            // These both check if any enemy is at the edge of the screen
            if (goRight) foreach (Enemy enemy in enemies) if (enemy != null && enemy.GetPictureBox != null && enemy.GetPictureBox.Right >= formRectangle.Right) isSideOfScreen = true;
            if (!goRight) foreach (Enemy enemy in enemies) if (enemy != null && enemy.GetPictureBox != null && enemy.GetPictureBox.Left <= formRectangle.Left) isSideOfScreen = true;

            // This will move each enemy Right
            if (!isSideOfScreen && goRight) foreach (Enemy enemy in enemies) if (enemy.GetPictureBox != null && enemy.GetPictureBox.Right <= formRectangle.Right) enemy.MoveRight();

            // This will move each enemy Left
            if (!isSideOfScreen && !goRight) foreach (Enemy enemy in enemies) if (enemy.GetPictureBox != null && enemy.GetPictureBox.Left >= formRectangle.Left) enemy.MoveLeft();

            // This moves the enemy down the size of the sprites
            if (isSideOfScreen && enemyDownCounter <= spriteSize)
            {
                foreach (Enemy enemy in enemies) if (enemy.GetPictureBox != null) enemy.MoveDown();
                enemyDownCounter += SPEED;
            }

            // Stops the enemys from moving down once they have gone their own size and inverts goRight
            if (enemyDownCounter >= spriteSize)
            {
                enemyDownCounter = 0;
                goRight = !goRight;
            }
        }

        // Destroy Enemy
        public void DestroyEnemy(int enemy)
        {
            form1.Controls.Remove(enemies[enemy].GetPictureBox);
            enemies[enemy].GetPictureBox = null;
        }

        // Moves the player left or right
        public void MovePlayer(bool moveLeft)
        {
            player.MovePlayer(moveLeft);
        }

        // Fires a shot from the player
        public void Shot()
        {
            player.Shot();
        }
        
    }
}
