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

        public Bomb(int spriteSize, Form form, Random random) : base(spriteSize, form)
        {

        }

        public override void MoveSprite(string direction)
        {
            SpriteBox.Top += 10;
        }
    }
}
