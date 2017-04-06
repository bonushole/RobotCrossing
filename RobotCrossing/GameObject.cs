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
        float scale;
        
        public GameObject(Texture2D texture, Vector2 position, float scale = 1 )
        {
            this.position = position;
            this.texture = texture;
            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, scale:new Vector2(scale, scale));
        }
    }
}
