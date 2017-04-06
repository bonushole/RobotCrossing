using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing
{
    class GameScene : Scene
    {
        Player player;
        public override void Update(GameTime gameTime){
            player.Update(gameTime);
        }
        public override void LoadContent(GameWindow window){
            player = new Player(window);
           // player.Player(window);
            
            }
        public override void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            spriteBatch.Begin(transformMatrix:
              Matrix.CreateTranslation(
                  new Vector3((player.position.X * -1) + window.ClientBounds.Width / 2, (player.position.Y * -1) + window.ClientBounds.Height / 2, 0)));
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
