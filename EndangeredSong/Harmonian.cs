﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace EndangeredSong
{
    class Harmonian : Sprite
    {
        //string musicFile;
        bool isHid;
        int maxX;
        int maxY;      

        public Harmonian(int x, int y, int width, int height, int maxX, int maxY)
	    {
            
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
            this.maxX = maxX;
            this.maxY = maxY;
	    }
        public Vector2 getPosition()
        {
            return this.pos;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Harmonian.png");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }

        public void Update(Controls controls, GameTime gameTime)
        {
            Move(controls);
        }

        public void Move(Controls controls)
        {

            if (controls.isPressed(Keys.D, Buttons.DPadRight) && this.pos.X < maxX-this.dim.X)
                this.pos.X += 10;
            if (controls.isPressed(Keys.A, Buttons.DPadLeft) && this.pos.X > 0)
                this.pos.X -= 10;
            if (controls.isPressed(Keys.W, Buttons.DPadUp) && this.pos.Y > 0)
                this.pos.Y -= 10;
            if (controls.isPressed(Keys.S, Buttons.DPadDown) && this.pos.Y < maxY-this.dim.Y)
                this.pos.Y += 10;

        }
       
    }
}