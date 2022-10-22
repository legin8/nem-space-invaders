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
        private readonly Bitmap SPRITEIMAGE = Properties.Resources.Bomb;
        private Random random;
        private int timeToLive;

        public int TimeToLive { get => timeToLive; set => timeToLive = value; }

        public Bomb(int spriteSize, Form form, Rectangle formRectangle, Random random) : base(spriteSize, form, formRectangle)
        {
            this.random = random;
        }

        public void MakeSprite(int xPosition, int yPosition)
        {
            spriteBox = new PictureBox();
            spriteBox.Width = spriteSize;
            spriteBox.Height = spriteSize;
            spriteBox.Image = SPRITEIMAGE;
            spriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            spriteBox.Location = new Point(xPosition, yPosition);
            timeToLive = random.Next(1, 71);
            form.Controls.Add(spriteBox);
        }

        public override void MoveSprite(string direction)
        {
            SpriteBox.Top += 10;
        }
    }
}
