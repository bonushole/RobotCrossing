using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RobotCrossing.MainGame.GameObjects
{
    abstract class InteractiveObject : GameObject
    {
        public bool interacting;
        public Rectangle range;

        public InteractiveObject(Vector2 position, float scale = 1) : base(position, scale)
        {

        }
        public abstract void Update(Player player, Action OpenInventory);

    }
}
