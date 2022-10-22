using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace project_2_space_invaders_legin8
{
    internal class Player : Sprite
    {
        private readonly Bitmap SPRITEIMAGE = Properties.Resources.player;
        private const int SPEEDOFPLAYER = 20;

        
        public Player(int spriteSize, Form form, Rectangle formRectangle, int xPosition, int yPosition) :
            base (spriteSize, form, formRectangle)
        {
            this.formRectangle = formRectangle;
            this.spriteSize = spriteSize;
            MakeSprite(xPosition, yPosition, SPRITEIMAGE);
        }

        public override void MoveSprite(string direction)
        {
            if (direction == "LEFT")
            {
                if (spriteBox.Left != formRectangle.Left) spriteBox.Left -= SPEEDOFPLAYER;
                if (spriteBox.Left < formRectangle.Left) spriteBox.Left = formRectangle.Left;
            }
            if (direction == "RIGHT")
            {
                if (spriteBox.Right != formRectangle.Right) spriteBox.Left += SPEEDOFPLAYER;
                if (spriteBox.Right > formRectangle.Right) spriteBox.Left = formRectangle.Right - spriteBox.Width;
            }
        }
    }
}
