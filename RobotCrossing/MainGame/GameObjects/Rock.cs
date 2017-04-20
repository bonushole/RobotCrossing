using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing.MainGame.GameObjects
{
    class Rock : GameObject
    {
        public Rock(Vector2 position, float scale = 1) : base(position, scale) {

            texture = TextureManager.getTexture2D("rock");

        }
    }
}
