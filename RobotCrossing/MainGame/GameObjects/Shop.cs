using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobotCrossing.MainGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing
{
    class Shop : InteractiveObject
    {
        public Shop(Vector2 position, float scale =1) : base(position, scale)
        {
            texture = TextureManager.getTexture2D("square");
            range = new Rectangle((int)position.X -30, (int)position.Y - 30, texture.Width + 60, texture.Height + 60);
        
        }

        public override void Update(Player player, Action OpenInventory)
        {
            interacting = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                foreach(InventorySlot slot in player.inventory)
                {
                    if (slot.selected)
                    {
                        player.money += 10;

                        slot.item = null;
                    }
                }
                interacting = false;
                OpenInventory();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
            {
                interacting = false;
                OpenInventory();
            }

        }
    }
}
