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
            player = new Player();
            
            }
        public override void Draw(SpriteBatch spritebatch)
        {
            player.Draw(spritebatch);
        }
    }
}
