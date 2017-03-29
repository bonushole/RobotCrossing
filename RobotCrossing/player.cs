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
        int frameDuration = 200;
        int frameTotalTime = 0;
        int currentFrame = 0;
        int currentState = 0;
        int currentStateImageCount = 2;

        int spriteWidth = 64;
        int spriteHeight = 64;

        Texture2D texture;
        public Vector2 position;
        
        public Player()
        {
            texture = TextureManager.getTexture2D("player");
            position = new Vector2(10,10);
        }
        public void Update(GameTime gameTime)
        {
            frameTotalTime += gameTime.ElapsedGameTime.Milliseconds;

            if (frameTotalTime >= frameDuration)
            {
                frameTotalTime = 0;
                currentFrame++;
                currentFrame %= currentStateImageCount;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += 4;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle: new Rectangle(currentFrame * spriteWidth, currentState * spriteHeight, spriteWidth, spriteHeight));
        }
    }
}
