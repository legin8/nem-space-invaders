/* Program name: project-2-space-invaders-legin8
Project file name: Shot.cs
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
using System.Media;

namespace project_2_space_invaders_legin8
{
    // This class is a child of the Sprite class
    // It holds the logic for making the shot
    public class Shot : Sprite
    {
        // Class variables
        private const int TIMETODIE = 0;
        private int timeToLive;
        private Random random;
        private SoundPlayer shotSound;

        // Class constructor, makes and adds the sprite to the screen when called
        public Shot(int spriteSize, Form form, Random random, int xPosition, int yPosition) :
            base (spriteSize, form, xPosition, yPosition)
        {
            this.random = random;
            spriteImage = Properties.Resources.shot; // Shot sound
            shotSound = new SoundPlayer(@"..\..\Resources\blaster.wav"); // Shot sound
            shotSound.Play();
            MakeSprite(xPosition, yPosition);
        }

        // Overrider for the MakeSprite class from the sprite class.
        // The difference is the timeToLive is added when it's called.
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

        // This moves the sprite and runs checks on this sprite, checking if it should be removed or not
        public override void MoveSprite()
        {
            spriteBox.Top -= 10;
            timeToLive--;
            if (timeToLive == TIMETODIE) RemoveSprite(this);
            if (spriteBox != null && spriteBox.Top <= formRectangle.Top) RemoveSprite(this);
        }
    }
}
