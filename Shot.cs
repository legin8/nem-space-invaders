using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class Shot : Sprite
    {
        private const int TIMETODIE = 0;
        private int timeToLive;
        private Random random;

        public Shot(int spriteSize, Form form, Random random, int xPosition, int yPosition) : base (spriteSize, form, xPosition, yPosition)
        {
            this.random = random;
            spriteImage = Properties.Resources.shot;
        }

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
        public override void MoveSprite(string direction)
        {
            spriteBox.Top -= 10;
            timeToLive--;
            if (timeToLive == TIMETODIE) RemoveSprite(this);
            if (spriteBox != null && spriteBox.Top <= formRectangle.Top) RemoveSprite(this);
        }
    }
}
