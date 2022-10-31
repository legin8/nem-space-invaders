using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace project_2_space_invaders_legin8
{
    public abstract class Sprite
    {
        protected Bitmap spriteImage;
        protected Form form;
        protected PictureBox spriteBox;
        protected Rectangle formRectangle;
        protected int spriteSize;
        protected EDirection spriteEDirection;

        public EDirection SpriteEDirection { get => spriteEDirection; set => spriteEDirection = value; }
        public PictureBox SpriteBox { get => spriteBox; set => spriteBox = value; }
        public Sprite(int spriteSize, Form form, int xPosition, int yPosition)
        {
            this.spriteSize = spriteSize;
            this.form = form;
            formRectangle = form.ClientRectangle;
        }

        public virtual void MakeSprite(int xPosition, int yPosition)
        {
            SpriteBox = new PictureBox();
            SpriteBox.Width = spriteSize;
            SpriteBox.Height = spriteSize;
            SpriteBox.Image = spriteImage;
            SpriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SpriteBox.Location = new Point(xPosition, yPosition);
            form.Controls.Add(SpriteBox);
        }

        // This Will move the sprite, each class will be different movement so it will be blank
        public abstract void MoveSprite();


        // This Removes the sprite from the screen
        public virtual void RemoveSprite(Sprite sprite)
        {
            form.Controls.Remove(sprite.SpriteBox);
            sprite.SpriteBox = null;
        }
    }
}
