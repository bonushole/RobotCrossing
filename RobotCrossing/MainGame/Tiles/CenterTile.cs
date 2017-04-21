using Microsoft.Xna.Framework;
using RobotCrossing.MainGame.GameObjects;
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
            color = Color.Red;
            interactiveObjects = new List<InteractiveObject>();
            interactiveObjects.Add(new Shop(new Vector2(100,100)));
           
        }
        public override void Update(GameTime gameTime, Player player)
        {
            foreach(InteractiveObject thing in interactiveObjects)
            {
                if (thing.interacting)
                {
                    thing.Update(player);
                }
            }
        }
    }
}
