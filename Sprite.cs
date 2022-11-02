/* Program name: project-2-space-invaders-legin8
Project file name: Sprite.cs
Author: Nigel Maynard
Date: 25/10/22
Language: C#
Platform: Microsoft Visual Studio 2022
Purpose: Class work
Description: Assessment game: Space Invaders
Known Bugs:
Additional Features:
*/

using System.Drawing;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    // This is the base class of all sprites that will display on the screen
    // Abstract class
    public abstract class Sprite
    {
        // protected class variables, not all are assigned here
        protected Bitmap spriteImage;
        protected Form form;
        protected PictureBox spriteBox;
        protected Rectangle formRectangle;
        protected int spriteSize;
        protected EDirection spriteEDirection;

        // Gets and sets
        public EDirection SpriteEDirection { get => spriteEDirection; set => spriteEDirection = value; }
        public PictureBox SpriteBox { get => spriteBox; set => spriteBox = value; }

        // Class constructor
        public Sprite(int spriteSize, Form form, int xPosition, int yPosition)
        {
            // These are assigned here as they are the same for all children
            this.spriteSize = spriteSize;
            this.form = form;
            formRectangle = form.ClientRectangle;
        }

        // This is the base version for how to make a sprite inside a PictureBox that displays on the screen
        public virtual void MakeSprite(int xPosition, int yPosition)
        {
            // Creates the PictureBox, sets size, location, picture, SizeMode, adds to the controls to be added to the screen.
            SpriteBox = new PictureBox();
            SpriteBox.Width = spriteSize;
            SpriteBox.Height = spriteSize;
            SpriteBox.Image = spriteImage;
            SpriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SpriteBox.Location = new Point(xPosition, yPosition);
            form.Controls.Add(SpriteBox);
        }

        // This Will move the sprite, each class will be different movement so it will be blank
        public abstract void MoveSprite();

        // This Removes the sprite from the screen, aka the Controls and sets the spriteBox to null
        public virtual void RemoveSprite(Sprite sprite)
        {
            form.Controls.Remove(sprite.SpriteBox);
            sprite.SpriteBox = null;
        }
    }
}
