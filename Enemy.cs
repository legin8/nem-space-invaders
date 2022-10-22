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

        public Enemy(int spriteSize, Form form, int xPosition, int yPosition, int speed) : base (spriteSize, form)
        {
            this.speed = speed;
            MakeSprite(xPosition, yPosition, SPRITEIMAGE);
        }

        public override void MoveSprite(string direction)
        {
            if (direction == "RIGHT") SpriteBox.Left += speed;
            if (direction == "LEFT") SpriteBox.Left -= speed;
            if (direction == "DOWN") SpriteBox.Top += speed;
        }
    }
}
