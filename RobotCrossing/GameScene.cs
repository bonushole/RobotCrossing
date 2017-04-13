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
    class GameScene : Scene
    {
        Player player;
        double lastspawn = 0;
        double spawnperiod=1;
        Random rnd = new Random();
        List<GameObject> objects = new List<GameObject>();

        bool displayingInventory;

        public override void Update(GameTime gameTime){
            player.Update(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds >= (lastspawn+spawnperiod))
            {
                objects.Add(new GameObject(TextureManager.getTexture2D("rock"), new Vector2(rnd.Next(1000), rnd.Next(1000)), (float).1));
                lastspawn = gameTime.TotalGameTime.TotalSeconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                displayingInventory = true;
            }
            else
            {
                displayingInventory = false;
            }
           
        }
        public void PickUp()
        {
                Rectangle grabBox = new Rectangle(player.position.ToPoint(), new Point(player.spriteWidth, player.spriteHeight));
                if (player.currentDirection == 0)
                {
                    grabBox.Offset(0, -player.spriteHeight / 2);
                }
                else if (player.currentDirection == 1)
                {
                    grabBox.Offset(-player.spriteWidth / 2, 0);
                }
                else if (player.currentDirection == 2)
                {
                    grabBox.Offset(0, player.spriteHeight / 2);
                }
                else if (player.currentDirection == 3)
                {
                    grabBox.Offset(player.spriteWidth / 2, 0);
                }

                foreach (GameObject thing in objects)
                {
                    Rectangle thingBox = new Rectangle(thing.position.ToPoint(), new Point((int)(thing.texture.Width * thing.scale), (int)(thing.texture.Height * thing.scale)));
                    if (thingBox.Intersects(grabBox))
                    {
                        player.addItem(thing);
                        objects.Remove(thing);
                        break;
                    }
                }

        }
        public override void LoadContent(GameWindow window){
            player = new Player(window, PickUp);
           // player.Player(window);
            
            }
        public override void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            spriteBatch.Begin(transformMatrix:
              Matrix.CreateTranslation(
                  new Vector3(((player.position.X * -1) - (player.spriteWidth / 2)) + window.ClientBounds.Width / 2, ((player.position.Y * -1) - player.spriteHeight) + window.ClientBounds.Height / 2, 0)));
            foreach (GameObject obj in objects)
            {
                obj.Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            spriteBatch.End();
            if (displayingInventory)
            {
                spriteBatch.Begin();

                spriteBatch.Draw(TextureManager.getTexture2D("square"), destinationRectangle: new Rectangle(10,10, window.ClientBounds.Width-20, window.ClientBounds.Height-20));
                foreach (GameObject item in player.inventory) {
                    item.Draw(spriteBatch);
                }

                spriteBatch.End();
            }
        }
    }
}
