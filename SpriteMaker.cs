using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_2_space_invaders_legin8
{
    internal class SpriteMaker
    {
        private const int SCALEOFSPRITE = 26;
        private Form form;
        private Rectangle formRectangle;

        private int spriteSize;
        public SpriteMaker(Form form)
        {
            this.form = form;
            formRectangle = form.ClientRectangle;

            spriteSize = form.Width / SCALEOFSPRITE;
        }

        public Player makePlayer() => new Player(spriteSize, form, form.Width / 2, form.ClientRectangle.Height - spriteSize);
    }
}
