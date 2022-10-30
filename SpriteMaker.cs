using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class SpriteMaker
    {
        // Class variables
        private const int SCALEOFSPRITE = 26, SPEED = 5;
        private readonly Form form;
        private readonly Random random;
        private readonly int spriteSize;

        public SpriteMaker(Form form, Random random)
        {
            this.form = form;
            this.random = random;
            spriteSize = form.Width / SCALEOFSPRITE;
        }


        // This makes and Returns a new player Sprite
        // Puts the player Sprite at bottom of the form in the middle
        public Sprite MakePlayer() => new Player(spriteSize, form, form.ClientRectangle.Width / 2, form.ClientRectangle.Height - spriteSize);


        // This makes and Returns a Shot comming from infront of the player Sprite
        public Sprite MakeShot(Sprite player) => new Shot(spriteSize, form, random, player.SpriteBox.Left, player.SpriteBox.Top - spriteSize);

        // This makes and Returns a Bomb comming from below the Enemy Sprite
        public Sprite MakeBomb(Sprite enemy) => new Bomb(spriteSize, form, enemy.SpriteBox.Left, enemy.SpriteBox.Bottom, random);


        // This makes and Returns a new List of enemy Sprites
        // This is called from the constructor and makes the enemies
        // 4 rows and 10 columns
        public List<Sprite> MakeEnemies()
        {
            // Local variables for making the enemies in a grid
            const int COLUMNS = 10, ROWS = 4;
            List<Sprite> tempEnemyList = new List<Sprite>();
            const double ENEMYGAP = 1.5;
            int xPosition = form.ClientRectangle.Left, yPosition;

            // This loop is for the rows of enemies
            for (int i = 0; i < COLUMNS; i++)
            {
                // This sets the starting point for the enemy to the top of the form
                yPosition = form.ClientRectangle.Top + spriteSize;

                // This loop is for the columns enemies
                for (int j = 0; j < ROWS; j++)
                {
                    tempEnemyList.Add(new Enemy(spriteSize, form, xPosition, yPosition, SPEED));
                    yPosition += (int)(spriteSize * ENEMYGAP);
                }
                // This moves to the next column to the Right
                xPosition += spriteSize + (int)(spriteSize * ENEMYGAP);
            }
            return tempEnemyList;
        }
    }
}
