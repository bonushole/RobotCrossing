﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing
{
    class Player
    {
        
        int frameDuration = 50;
        int frameTotalTime = 0;
        int currentFrame = 0;
        int currentState = 2;
        public int currentDirection = 0;
        int[] currentStateImageCount = {7,8,9,6,13,6};
        bool animating = false;

        public int spriteWidth = 64;
        public int spriteHeight = 64;

        public Texture2D texture;
        public Vector2 position;

        Action pickUp;

        public List<GameObject> inventory = new List<GameObject>();

        public Player(GameWindow window, Action pickUp)
        {
            texture = TextureManager.getTexture2D("player");
             position = new Vector2(window.ClientBounds.Width/2, window.ClientBounds.Height/2);
            this.pickUp = pickUp;
           // position = new Vector2(90,90);
        }
        public void Update(GameTime gameTime)
        {
            frameTotalTime += gameTime.ElapsedGameTime.Milliseconds;

            if (frameTotalTime >= frameDuration)
            {
                if (animating)
                {
                    frameTotalTime = 0;
                    currentFrame++;
                    if (currentState == 2)
                    {
                        currentFrame %= currentStateImageCount[currentState];
                    }
                    else if(currentFrame == currentStateImageCount[currentState])
                    {
                        currentState = 2;
                    }

                }
                else
                {
                    currentFrame = 0;
                }
            }
            if (currentState == 2) {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    pickUp();
                    currentState = 1;
                    currentFrame = 0;
                    animating = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    position.X -= 4;
                    currentDirection = 1;
                    animating = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    position.X += 4;
                    currentDirection = 3;
                    animating = true;

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    position.Y -= 4;
                    currentDirection = 0;
                    animating = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    position.Y += 4;
                    currentDirection = 2;
                    animating = true;
                }
                else
                {
                    animating = false;
                }
            }

        }
        public void addItem(GameObject item)
        {
            item.position.Y = 20;
            item.position.X = 10 + ((item.texture.Width*item.scale) * inventory.Count());
            inventory.Add(item);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle: new Rectangle(currentFrame * spriteWidth, ((4 * currentState)+ currentDirection) * spriteHeight, spriteWidth, spriteHeight));
        }
    }
}
