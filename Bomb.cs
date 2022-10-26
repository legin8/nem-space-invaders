using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace project_2_space_invaders_legin8
{
    internal class Bomb : Sprite
    {
        private const int TIMETODIE = 0;
        private Random random;
        private int timeToLive;
        private SoundPlayer bombSound;

        public Bomb(int spriteSize, Form form, int xPosition, int yPosition, Random random) :
            base(spriteSize, form, xPosition, yPosition)
        {
            this.random = random;
            spriteImage = Properties.Resources.Bomb;
            bombSound = new SoundPlayer(@"..\..\Resources\bomb.wav");
            MakeSprite(xPosition, yPosition);
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

        public override void MoveSprite(string direction)
        {
            SpriteBox.Top += 10;
            timeToLive--;
            if (timeToLive == TIMETODIE)
            {
                RemoveSprite(this);
                bombSound.Play();
            }
            if (spriteBox != null && spriteBox.Bottom >= formRectangle.Bottom)
            {
                RemoveSprite(this);
                bombSound.Play();
            }
        }
    }
}
