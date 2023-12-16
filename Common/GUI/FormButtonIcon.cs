using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonballPichu.Common.Systems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using SteelSeries.GameSense;
using Terraria.UI;



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

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            
            if (((FormButton)Parent).name == "baseForm" || modPlayer.unlockedForms.Contains(((FormButton)Parent).name))
            {
                base.DrawSelf(spriteBatch);
            }
            else
            {
                CalculatedStyle originDimensions = GetDimensions();
                int originTop = (int)originDimensions.Y;
                int originBottom = (int)originDimensions.Y + (int)originDimensions.Height;
                int originLeft = (int)originDimensions.X;
                int originRight = (int)originDimensions.X + (int)originDimensions.Width;
                Point topLeft = new Point(originLeft, originTop);
                Point botRight = new Point(originRight, originBottom);
                drawBox(spriteBatch, topLeft, botRight);
            }
        }

        private void drawBox(SpriteBatch spriteBatch, Point topLeft, Point bottomRight)
        {
            int width = bottomRight.X - topLeft.X;
            int height = bottomRight.Y - topLeft.Y;


            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(topLeft.X, topLeft.Y, width, height), Colors.Black);
        }
    }
}
