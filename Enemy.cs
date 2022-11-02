/* Program name: project-2-space-invaders-legin8
Project file name: Enemy.cs
Author: Nigel Maynard
Date: 25/10/22
Language: C#
Platform: Microsoft Visual Studio 2022
Purpose: Class work
Description: Assessment game: Space Invaders
Known Bugs:
Additional Features:
*/

using System.Windows.Forms;
using System.Media;

namespace project_2_space_invaders_legin8
{
    // This class is a child of the Sprite class
    // It holds the logic for making the Enemy
    public class Enemy : Sprite
    {
        // Class variables
        private int speed;
        private Controller controller;
        private SoundPlayer explosion; // The sound for the bomb

        // Class constructor, makes and adds the sprite to the screen when called
        public Enemy(int spriteSize, Form form, int xPosition, int yPosition, int speed, Controller controller) : 
            base (spriteSize, form, xPosition, yPosition)
        {
            this.speed = speed;
            this.controller = controller;
            spriteImage = Properties.Resources.enemy; // Picture for the enemy
            explosion = new SoundPlayer(Properties.Resources.explosion);
            MakeSprite(xPosition, yPosition);
        }

        // This overrides the MoveSprite method form the sprite class
        public override void MoveSprite()
        {
            // I moves only 1 of 3 directions
            if (spriteEDirection == EDirection.RIGHT) spriteBox.Left += speed;
            if (spriteEDirection == EDirection.LEFT) spriteBox.Left -= speed;
            if (spriteEDirection == EDirection.DOWN) spriteBox.Top += speed;

            // moves the enemy again if they go over the bounds
            if (spriteBox.Left < formRectangle.Left) spriteBox.Left = formRectangle.Left;
            if (spriteBox.Right > formRectangle.Right) spriteBox.Left = formRectangle.Right - spriteSize;
            // Stops the game if the enemy hits the bottom of the screen
            if (spriteBox.Bottom > formRectangle.Bottom)
            {
                spriteBox.Top = formRectangle.Bottom - spriteSize;
                controller.PlayGame = false;
            }
        }

        // This Removes the sprite from the screen and sets its spriteBox to null
        public override void RemoveSprite(Sprite sprite)
        {
            form.Controls.Remove(sprite.SpriteBox);
            sprite.SpriteBox = null;
            explosion.Play();
        }
    }
}
