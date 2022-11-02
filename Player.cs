/* Program name: project-2-space-invaders-legin8
Project file name: Player.cs
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

namespace project_2_space_invaders_legin8
{
    // This class is a child of the Sprite class
    // It holds the logic for making the player
    public class Player : Sprite
    {
        // Class variables
        private const int SPEEDOFPLAYER = 20;
 
        // Class constructor, makes and adds the sprite to the screen when called
        public Player(int spriteSize, Form form, int xPosition, int yPosition) :
            base (spriteSize, form, xPosition, yPosition)
        {
            spriteImage = Properties.Resources.player; // This is the picture used for the player
            MakeSprite(xPosition, yPosition);
        }

        // This overrides the MoveSprite method from the sprite class
        // It moves the player Left and Right only.
        public override void MoveSprite()
        {
            if (spriteEDirection == EDirection.LEFT) spriteBox.Left -= SPEEDOFPLAYER;
            if (spriteEDirection == EDirection.RIGHT) spriteBox.Left += SPEEDOFPLAYER;
        }
    }
}
