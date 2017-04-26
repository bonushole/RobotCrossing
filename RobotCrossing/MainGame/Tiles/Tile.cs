using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotCrossing.MainGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing
{
    abstract class Tile
    {
        public Texture2D backgroundTexture;
        public Vector2 position;
        public Color color;
        public Vector2 size = new Vector2(1000, 1000);

        public List<GameObject> objects;
        public List<InteractiveObject> interactiveObjects;

        public bool canInteract;
        public bool interacting;

        public abstract void LoadContent(GameWindow window);
        public abstract void Update(GameTime gameTime, Player player);

        public void MasterUpdate(GameTime gameTime, Player player)
        {
            Update(gameTime, player);
            canInteract = false;
            interacting = false;

            if (interactiveObjects != null)
            {
                foreach (InteractiveObject thing in interactiveObjects)
                {
                    if (thing.interacting)
                    {
                        interacting = true;
                    }
                    if (thing.range.Intersects(new Rectangle(player.position.ToPoint(), new Point(player.texture.Width, player.texture.Height))))
                    {
                        canInteract = true;
                    }
                }
            }
        }

        public void ObjectInteract(Player player)
        {
            foreach (InteractiveObject thing in interactiveObjects)
            {
                if (thing.range.Intersects(new Rectangle(player.position.ToPoint(), new Point(player.texture.Width, player.texture.Height))))
                {
                    interacting = true;
                    thing.Update(player);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, destinationRectangle: new Rectangle(position.ToPoint(), size.ToPoint()), color: color);

            if (objects != null)
            {
                foreach (GameObject thing in objects)
                {
                    thing.Draw(spriteBatch);
                }
            }
            if (interactiveObjects != null)
            {
                foreach (GameObject thing in interactiveObjects)
                {
                    thing.Draw(spriteBatch);
                }
            }
            
        }
        public void PickUp(Player player)
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
    }
}
