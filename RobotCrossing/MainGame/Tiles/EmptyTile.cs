using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RobotCrossing.MainGame.Tiles
{
    class EmptyTile : Tile
    {
        public override void LoadContent(GameWindow window)
        {
            color = Color.White;
            backgroundTexture = TextureManager.getTexture2D("square");
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
