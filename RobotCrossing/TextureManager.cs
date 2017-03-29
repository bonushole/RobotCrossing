using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCrossing
{
    class TextureManager
    {
        static ContentManager contentManager;
        public static void Initialize(ContentManager content)
        {
            contentManager = content;
        }
        public static Texture2D getTexture2D(string name)
        {
            return contentManager.Load<Texture2D>(name);
        }
        public static SpriteFont getSpriteFont(string name)
        {
            return contentManager.Load<SpriteFont>(name);
        }
    }
}
