using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace project_2_space_invaders_legin8
{
    internal class Enemy : Sprite
    {
        private readonly Bitmap SPRITEIMAGE = Properties.Resources.enemy;
        private int speed;

        public Enemy(int spriteSize, Form form, Rectangle formRectangle, int xPosition, int yPosition, int speed) : 
            base (spriteSize, form, formRectangle)
        {
            this.speed = speed;
            this.formRectangle = formRectangle;
            MakeSprite(xPosition, yPosition, SPRITEIMAGE);
        }

        public override void MoveSprite(string direction)
        {
            if (direction == "RIGHT") spriteBox.Left += speed;
            if (direction == "LEFT") spriteBox.Left -= speed;
            if (direction == "DOWN") spriteBox.Top += speed;

            if (spriteBox.Left < formRectangle.Left) spriteBox.Left = formRectangle.Left;
            if (spriteBox.Right > formRectangle.Right) spriteBox.Left = formRectangle.Right - spriteSize;
            //if (spriteBox.Bottom > formRectangle.Bottom) spriteBox.Top = formRectangle.Bottom - spriteSize;
        }
    }
}
