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
        double lastspawn = 0;
        double spawnperiod=5;
        Random rnd = new Random();
        List<GameObject> objects = new List<GameObject>();
        public override void Update(GameTime gameTime){
            player.Update(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds >= (lastspawn+spawnperiod))
            {
                objects.Add(new GameObject(TextureManager.getTexture2D("cursor"), new Vector2(rnd.Next(100), rnd.Next(100))));
                lastspawn = gameTime.TotalGameTime.TotalSeconds;
            }
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
            foreach (GameObject obj in objects)
            {
                obj.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
