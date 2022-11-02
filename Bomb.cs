/* Program name: project-2-space-invaders-legin8
Project file name: Bomb.cs
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
using System.Drawing;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    // This class is a child of the Sprite class
    // It holds the logic for making the player
    public class Bomb : Sprite
    {
        private const int TIMETODIE = 0;
        private Random random;
        private int timeToLive;

        // Class constructor
        public Bomb(int spriteSize, Form form, int xPosition, int yPosition, Random random) :
            base(spriteSize, form, xPosition, yPosition)
        {
            this.random = random;
            spriteImage = Properties.Resources.Bomb; // Bomb picture
            MakeSprite(xPosition, yPosition);
        }

        // Override for the MakeSprite Method from the sprite class.
        // The difference is the timeToLive variable.
        public override void MakeSprite(int xPosition, int yPosition)
        {
            spriteBox = new PictureBox();
            spriteBox.Width = spriteSize;
            spriteBox.Height = spriteSize;
            spriteBox.Image = spriteImage;
            spriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            spriteBox.Location = new Point(xPosition, yPosition);
            timeToLive = random.Next(1, 71);
            form.Controls.Add(spriteBox);
        }

        // This overrides the moveSprite method from the sprite class
        // moves and checks if it should be removed using the timeToLive variable.
        public override void MoveSprite()
        {
            SpriteBox.Top += 10;
            timeToLive--;
            if (timeToLive == TIMETODIE)
            {
                RemoveSprite(this);
            }
            if (spriteBox != null && spriteBox.Bottom >= formRectangle.Bottom)
            {
                RemoveSprite(this);
            }
        }
    }
}
