﻿using System;
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
        protected Form form;
        protected PictureBox spriteBox;
        protected int spriteSize;
        protected Bitmap bitmap;

        public PictureBox SpriteBox { get => spriteBox; set => spriteBox = value; }
        public Sprite(int spriteSize, Form form, int xPosition, int yPosition, Bitmap sprite)
        {
            this.spriteSize = spriteSize;
            this.form = form;
            bitmap = sprite;
        }

        protected virtual void MakeSprite(int xPosition, int yPosition, Bitmap BITMAP)
        {
            SpriteBox = new PictureBox();
            SpriteBox.Width = spriteSize;
            SpriteBox.Height = spriteSize;
            SpriteBox.Image = BITMAP;
            SpriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            SpriteBox.Location = new Point(xPosition, yPosition);
            form.Controls.Add(SpriteBox);
        }
    }
}
