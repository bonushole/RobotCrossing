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
        static Random rnd = new Random();
        public override void LoadContent(GameWindow window)
        {
            color = Color.White;
            String[]backgrounds = {"GrassTile", "BeachTile", "DeadTile", "DesertTile"};

            backgroundTexture = TextureManager.getTexture2D(backgrounds[rnd.Next(0,3)]);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            
        }
    }
}
