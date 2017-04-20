using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobotCrossing.MainGame.GameObjects;
using RobotCrossing.MainGame.Tiles;
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

        Tile tile;
        Tile[,] tiles = new Tile[3,3];

        bool displayingInventory = false;
        bool buttonReleased = true;

        bool clickReleased = true;

        bool canInteract = false;

        Texture2D cursor;
        Vector2 cursorOffset;

        public override void Update(GameTime gameTime){

            if (shop.interacting) {
                shop.Update(player);
                if (!shop.interacting)
                {
                    displayingInventory = false;
                }
            }
            else
            {
                player.Update(gameTime);
            }

            OpenInventory();

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
                        foreach (InventorySlot slot in player.inventory) {

                            slot.selected = false;

                        }
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
        
        public override void LoadContent(GameWindow window){
            player = new Player(window, PickUp);
            cursor = TextureManager.getTexture2D("cursor");
            cursorOffset = new Vector2(16, 16);
            shop = new Shop(new Vector2(0, 0));
            tile = new CenterTile();

            tile.LoadContent(window);
           // player.Player(window);
            
            }
        public void PickUp()
        {
            tile.PickUp(player);
        }
        public override void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            spriteBatch.Begin(transformMatrix:
              Matrix.CreateTranslation(
                  new Vector3(((player.position.X * -1) - (player.spriteWidth / 2)) + window.ClientBounds.Width / 2, ((player.position.Y * -1) - player.spriteHeight) + window.ClientBounds.Height / 2, 0)));

            tile.Draw(spriteBatch);

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
