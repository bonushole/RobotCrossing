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

        Tile currentTile;
        Tile[,] nearbyTiles = new Tile[3, 3];
        Tile[,] tiles = new Tile[11,11];

        bool displayingInventory = false;
        bool buttonReleased = true;

        bool clickReleased = true;

        Texture2D cursor;
        Vector2 cursorOffset;

        public override void Update(GameTime gameTime) {

            currentTile.MasterUpdate(gameTime, player, OpenInventory);

            if (!currentTile.interacting) {
                player.Update(gameTime);
                ManageInventory();
            }
            
            

            foreach (Tile tile in tiles)
            {
                Rectangle tileRect = new Rectangle(tile.position.ToPoint(), tile.size.ToPoint());
                if (tileRect.Contains(player.position))
                {
                    currentTile = tile;
                }
            }
            //for (int i = 0; i < tiles.GetLength(1); i++)
            //{
            //    for (int j = 0; j < tiles.GetLength(0); j++)
            //    {
            //        Rectangle tileRect = new Rectangle(tiles[i, j].position.ToPoint(), tiles[i, j].size.ToPoint());
            //        if (tileRect.Contains(player.position))
            //        {
            //            currentTile = tiles[i, j];
            //            //FindNearbyTiles(i, j);
            //        }
            //    }
            //}
            

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
        //public void FindNearbyTiles(int i, int j)
        //{
        //    for (int a = 0; a < nearbyTiles.GetLength(1); a++)
        //    {
        //        for (int b = 0; b < nearbyTiles.GetLength(0); b++)
        //        {
        //            int c = i - (1 + a);
        //            int d = j - (1 + b);
        //            if ((c>=0 && c<tiles.GetLength(0)) && (d>=0 && d<tiles.GetLength(1)))
        //            {
        //                nearbyTiles[a, b] = tiles[c, d];
        //            }
        //            else
        //            {
        //                nearbyTiles[a, b] = null;
        //            }
        //        }
        //    }
        //}
        public void ManageInventory()
        {
            if (currentTile.canInteract)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.H))
                {
                    currentTile.ObjectInteract(player, OpenInventory);
                    OpenInventory();
                }
            }

                if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    if (buttonReleased == true)
                    {
                        OpenInventory();
                    }
                    buttonReleased = false;
                }
                else
                {
                    buttonReleased = true;
                }

        }
        
        public void OpenInventory()
        {
            foreach (InventorySlot slot in player.inventory)
            {

                slot.selected = false;

            }
            displayingInventory = !displayingInventory;
        }

        public override void LoadContent(GameWindow window){
            player = new Player(window, PickUp);
            cursor = TextureManager.getTexture2D("cursor");
            cursorOffset = new Vector2(16, 16);

            for(int i = 0; i < tiles.GetLength(0); i++)
            {
                for(int j = 0; j < tiles.GetLength(1); j++)
                {
                    if ((i ==tiles.GetLength(0)/2) && (j == tiles.GetLength(0)/2))
                    {
                        tiles[i, j] = new CenterTile();
                    }
                    else if (j % 2 == i % 2)
                    {
                        tiles[i, j] = new EmptyTile();
                    }
                    else
                    {
                        tiles[i, j] = new RockFarm();
                    }
                    tiles[i, j].LoadContent(window);
                    tiles[i, j].position = new Vector2(tiles[i,j].size.X * (j - (tiles.GetLength(0)/2)) , tiles[i,j].size.Y*(i - (tiles.GetLength(1) / 2)));

                }
            }
            currentTile = tiles[1, 1];
           // player.Player(window);
            
            }
        public void PickUp()
        {
            currentTile.PickUp(player);
        }
        public override void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            spriteBatch.Begin(transformMatrix:
              Matrix.CreateTranslation(
                  new Vector3(((player.position.X * -1) - (player.spriteWidth / 2)) + window.ClientBounds.Width / 2, ((player.position.Y * -1) - player.spriteHeight) + window.ClientBounds.Height / 2, 0)));

            foreach (Tile tile in tiles)
            {
                if (tile != null) {
                    tile.Draw(spriteBatch);
                }
            }

            player.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.getTexture2D("square"), destinationRectangle: new Rectangle(0,0,100,50));
            spriteBatch.DrawString(TextureManager.getSpriteFont("spriteFont"),"$" + player.money,new Vector2(0,0), Color.Green);

            if (currentTile.canInteract)
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
