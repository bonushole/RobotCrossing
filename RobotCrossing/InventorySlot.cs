using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing
{
    class InventorySlot
    {
        public Rectangle rectangle;
        public GameObject item;
        public bool selected = false;
        public Texture2D selectionBox = TextureManager.getTexture2D("square"); 
    }
}
