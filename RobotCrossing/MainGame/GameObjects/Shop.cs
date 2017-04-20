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
    class Shop : GameObject
    {
        public Rectangle range;
        public bool interacting;

        public Shop(Vector2 position, float scale =1) : base(position, scale)
        {
            texture = TextureManager.getTexture2D("square");
            range = new Rectangle((int)position.X -30, (int)position.Y - 30, texture.Width + 60, texture.Height + 60);
        
        }

        public void Update(Player player)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                foreach(InventorySlot slot in player.inventory)
                {
                    if (slot.selected)
                    {
                        slot.item = null;
                        interacting = false;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
            {
                interacting = false;
            }

        }
    }
}
