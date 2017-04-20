using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RobotCrossing.MainGame.GameObjects;

namespace RobotCrossing.MainGame.Tiles
{
    class RockFarm : Tile
    {
        Random rnd;
        double lastspawn = 0;
        double spawnperiod = 1;


        public override void LoadContent(GameWindow window)
        {
            rnd = new Random();
            objects = new List<GameObject>();

            backgroundTexture = TextureManager.getTexture2D("square");
            color = Color.Black;

        }
        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalSeconds >= (lastspawn + spawnperiod))
            {
                objects.Add(new Rock(new Vector2(rnd.Next(1000), rnd.Next(1000)), (float).1));
                lastspawn = gameTime.TotalGameTime.TotalSeconds;
            }
        }

    }
}
