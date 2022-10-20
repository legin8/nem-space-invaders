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
        protected Form form;
        protected PictureBox pictureBox;
        protected int spriteSize;

        protected PictureBox PictureBox => pictureBox;
        public Sprite(int spriteSize, Form form, int x, int y)
        {
            this.spriteSize = spriteSize;
            this.form = form;
        }

    }
}
