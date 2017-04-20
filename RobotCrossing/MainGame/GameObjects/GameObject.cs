using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing
{
    class GameObject
    {
        public Vector2 position;
        public Texture2D texture;
        public float scale;
        
        public GameObject(Vector2 position, float scale = 1 )
        {
            this.position = position;
            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, scale:new Vector2(scale, scale));
        }
    }
}
