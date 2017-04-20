using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing.MainGame.Tiles
{
    class CenterTile : Tile
    {
        public override void LoadContent(GameWindow gameWindow)
        {
            backgroundTexture = TextureManager.getTexture2D("square");
            position = new Vector2(0,0);
            color = Color.White;
           
        }
        public override void Update(GameTime gameTime)
        {

        }
    }
}
