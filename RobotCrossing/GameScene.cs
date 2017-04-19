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
        Shop shop;
        double lastspawn = 0;
        double spawnperiod=1;
        Random rnd = new Random();
        List<GameObject> objects = new List<GameObject>();

        bool displayingInventory = false;
        bool buttonReleased = true;

        bool clickReleased = true;

        bool canInteract = false;

        Texture2D cursor;
        Vector2 cursorOffset;

        public override void Update(GameTime gameTime){

            if (shop.interacting) {
                shop.Update();
            }
            else
            {
                player.Update(gameTime);
            }

            OpenInventory();

            if (gameTime.TotalGameTime.TotalSeconds >= (lastspawn+spawnperiod))
            {
                objects.Add(new GameObject(TextureManager.getTexture2D("rock"), new Vector2(rnd.Next(1000), rnd.Next(1000)), (float).1));
                lastspawn = gameTime.TotalGameTime.TotalSeconds;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                if (clickReleased)
                {
                    foreach (InventorySlot slot in player.inventory)
                    {
                        if (slot.rectangle.Contains(Mouse.GetState().Position.ToVector2()))
                        {
                            slot.selected = !slot.selected;
                        }
                    }
                }
                clickReleased = false;
            }
            else
            {
                clickReleased = true;
            }

            
           
        }
        public void OpenInventory()
        {
            if (shop.range.Intersects(new Rectangle((int)player.position.X, (int)player.position.Y, player.texture.Width, player.texture.Height)))
            {
                canInteract = true;
                if (Keyboard.GetState().IsKeyDown(Keys.H))
                {
                    shop.interacting = true;
                    displayingInventory = true;
                }
            }
            else
            {
                canInteract = false;
            }
            if (!shop.interacting)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    if (buttonReleased == true)
                    {
                        displayingInventory = !displayingInventory;
                    }
                    buttonReleased = false;
                }
                else
                {
                    buttonReleased = true;
                }
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
                    if (player.AddItem(thing))
                    {
                        objects.Remove(thing);
                    }
                        break;
                    }
                }

        }
        public override void LoadContent(GameWindow window){
            player = new Player(window, PickUp);
            cursor = TextureManager.getTexture2D("cursor");
            cursorOffset = new Vector2(16, 16);
            shop = new Shop(new Vector2(0, 0));
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
            shop.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
            if (canInteract)
            {
                spriteBatch.Draw(TextureManager.getTexture2D("square"), destinationRectangle: new Rectangle(0, window.ClientBounds.Height - 80, window.ClientBounds.Width, 80));
                spriteBatch.DrawString(TextureManager.getSpriteFont("spriteFont"),"press H to interact", new Vector2(0, window.ClientBounds.Height-70), Color.Black);
            }
            if (displayingInventory)
            {
                spriteBatch.Draw(TextureManager.getTexture2D("square"), destinationRectangle: new Rectangle(10,10, window.ClientBounds.Width-20, window.ClientBounds.Height-20));
                player.DrawInventory(spriteBatch);
                spriteBatch.Draw(cursor, Mouse.GetState().Position.ToVector2() - cursorOffset, scale: new Vector2((float)0.1, (float)0.1));

                
            }
            spriteBatch.End();
        }
    }
}
