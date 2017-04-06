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
        Vector2 position;
        Texture2D texture;
        
        public GameObject(Texture2D texture, Vector2 position)
        {
            this.position = position;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position);
        }
    }
}
