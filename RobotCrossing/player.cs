using Microsoft.Xna.Framework;
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
        int currentState = 0;
        int[] currentStateImageCount = {7,7,7,7,8,8,8,8,9,9,9,9,6,6,6,6,13,13,13,13,6};
        bool animating = false;

        int spriteWidth = 64;
        int spriteHeight = 64;

        Texture2D texture;
        public Vector2 position;
        
        public Player(GameWindow window)
        {
            texture = TextureManager.getTexture2D("player");
             position = new Vector2(window.ClientBounds.Width/2, window.ClientBounds.Height/2);
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
                    currentFrame %= currentStateImageCount[currentState];
                }
                else
                {
                    currentFrame = 0;
                }
            }
           

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= 4;
                currentState = 9;
                animating = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += 4;
                currentState = 11;
                animating = true;
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= 4;
                currentState = 8;
                animating = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += 4;
                currentState = 10;
                animating = true;
            }
            else
            {
                animating = false;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle: new Rectangle(currentFrame * spriteWidth, currentState * spriteHeight, spriteWidth, spriteHeight));
        }
    }
}
