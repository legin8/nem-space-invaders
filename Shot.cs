﻿using System;
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
        private int timeToLive;
        private Random random;

        public int TimeToLive { get => timeToLive; set => timeToLive = value; }

        public Shot(int spriteSize, Form form, int x, int y, Random random) : base (spriteSize, form, x, y)
        {
            this.random = random;

        }

        public void Makeshot()
        {
            spriteBox = new PictureBox();
            spriteBox.Width = spriteSize;
            spriteBox.Height = spriteSize;
            spriteBox.Image = Properties.Resources.shot;
            spriteBox.SizeMode = PictureBoxSizeMode.StretchImage;
            spriteBox.Location = new Point(spriteBox.Left, spriteBox.Top - spriteSize);
            timeToLive = random.Next(1, 71);
            form.Controls.Add(spriteBox);
        }
    }
}