using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RobotCrossing.MainGame.GameObjects
{
    abstract class InteractiveObjects : GameObject
    {
        public InteractiveObjects(Vector2 position, float scale = 1) : base(position, scale)
        {

        }

    }
}
