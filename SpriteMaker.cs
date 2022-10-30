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
        private const int SCALEOFSPRITE = 26, SPEED = 5;
        private Form form;
        private Rectangle formRectangle;

        private int spriteSize;
        public SpriteMaker(Form form)
        {
            this.form = form;
            formRectangle = form.ClientRectangle;

            spriteSize = form.Width / SCALEOFSPRITE;
        }


        // This makes and Returns a new player Sprite
        // Puts the player Sprite at bottom of the form in the middle
        public Player MakePlayer() => new Player(spriteSize, form, form.ClientRectangle.Width / 2, form.ClientRectangle.Height - spriteSize);


        // This makes and Returns a new List of enemy Sprites
        // This is called from the constructor and makes the enemies
        // 4 rows and 10 columns
        public List<Sprite> MakeEnemies()
        {
            // Local variables for making the enemies in a grid
            const int COLUMNS = 10, ROWS = 4;
            List<Sprite> tempEnemyList = new List<Sprite>();
            const double ENEMYGAP = 1.5;
            int xPosition = formRectangle.Left, yPosition;

            // This loop is for the rows of enemies
            for (int i = 0; i < COLUMNS; i++)
            {
                yPosition = formRectangle.Top + spriteSize;

                // This loop is for the columns enemies
                for (int j = 0; j < ROWS; j++)
                {
                    tempEnemyList.Add(new Enemy(spriteSize, form, xPosition, yPosition, SPEED));
                    yPosition += (int)(spriteSize * ENEMYGAP);
                }
                // This moves to the next column
                xPosition += spriteSize + (int)(spriteSize * ENEMYGAP);
            }
            return tempEnemyList;
        }




    }
}
