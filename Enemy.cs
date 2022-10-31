using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Media;

namespace project_2_space_invaders_legin8
{
    internal class Enemy : Sprite
    {
        private int speed;
        private Controller controller;
        private SoundPlayer explosion;

        public Enemy(int spriteSize, Form form, int xPosition, int yPosition, int speed, Controller controller) : 
            base (spriteSize, form, xPosition, yPosition)
        {
            this.speed = speed;
            this.controller = controller;
            spriteImage = Properties.Resources.enemy;
            explosion = new SoundPlayer(Properties.Resources.explosion);
            MakeSprite(xPosition, yPosition);
        }

        public override void MoveSprite()
        {
            if (spriteEDirection == EDirection.RIGHT) spriteBox.Left += speed;
            if (spriteEDirection == EDirection.LEFT) spriteBox.Left -= speed;
            if (spriteEDirection == EDirection.DOWN) spriteBox.Top += speed;

            if (spriteBox.Left < formRectangle.Left) spriteBox.Left = formRectangle.Left;
            if (spriteBox.Right > formRectangle.Right) spriteBox.Left = formRectangle.Right - spriteSize;
            if (spriteBox.Bottom > formRectangle.Bottom)
            {
                spriteBox.Top = formRectangle.Bottom - spriteSize;
                controller.PlayGame = false;
            }
        }

        // This Removes the sprite from the screen
        public override void RemoveSprite(Sprite sprite)
        {
            form.Controls.Remove(sprite.SpriteBox);
            sprite.SpriteBox = null;
            explosion.Play();
        }
    }
}
