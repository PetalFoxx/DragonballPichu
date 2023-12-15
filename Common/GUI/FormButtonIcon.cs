using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;

namespace DragonballPichu.Common.GUI
{
    public class FormButtonIcon : UIImage
    {
        public Boolean drawChildren = true;
        public FormButtonIcon(Asset<Texture2D> texture) : base(texture)
        {
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            if(drawChildren)
            {
                base.DrawChildren(spriteBatch);
            }
            
        }
    }
}
