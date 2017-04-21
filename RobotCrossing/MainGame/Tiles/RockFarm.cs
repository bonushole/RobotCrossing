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
        public override void Update(GameTime gameTime, Player player)
        {
            if (gameTime.TotalGameTime.TotalSeconds >= (lastspawn + spawnperiod))
            {
                objects.Add(new Rock(new Vector2(rnd.Next((int)position.X, (int)(position.X + size.X)), rnd.Next((int)position.Y, (int)(position.Y + size.Y))), (float).1));
                lastspawn = gameTime.TotalGameTime.TotalSeconds;
            }
        }

    }
}
