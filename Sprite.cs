using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    public abstract class Sprite
    {
        private PictureBox pictureBox;
        private int spriteSize;
        public Sprite(int spriteSize)
        {
            this.spriteSize = spriteSize;
        }

    }
}
