using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class Bomb : Sprite
    {
        public Bomb(int spriteSize, Form form, int xPosition, int yPosition) : base(spriteSize, form, xPosition, yPosition)
        {

        }

        public void MakeBomb(int xPosition, int yPosition)
        {
            spriteBox = new PictureBox();
            spriteBox.Width = spriteSize;
            spriteBox.Height = spriteSize;
            spriteBox.Image = Properties.Resources.Bomb;
            spriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            spriteBox.Location = new Point(xPosition, yPosition);
            form.Controls.Add(spriteBox);
        }
    }
}
