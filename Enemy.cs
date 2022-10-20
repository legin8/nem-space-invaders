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
        private readonly Bitmap BITMAP = Properties.Resources.enemy;
        private int speed;

        public Enemy(int spriteSize, Form form, int xPosition, int yPosition, Bitmap sprite, int speed) : base (spriteSize, form, xPosition, yPosition, sprite)
        {
            this.speed = speed;
            MakeSprite(xPosition, yPosition, BITMAP);
        }


        public void MoveRight()
        {
            SpriteBox.Left += speed;
        }

        public void MoveLeft()
        {
            SpriteBox.Left -= speed;
        }

        public void MoveDown()
        {
            SpriteBox.Top += speed;
        }
    }
}
