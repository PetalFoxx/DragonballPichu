using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonballPichu.Common.Systems;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DragonballPichu.Common.GUI
{
    public class FormButton : UIPanel
    {
        public string name;
        private FormButtonIcon icon;
        Boolean unlock;

        public FormButton(string name, Asset<Texture2D> texture, Boolean unlock)
        {
            this.name = name;
           // this.texture = texture;
            this.icon = new FormButtonIcon(texture);
            this.unlock = unlock;
        }

        public override void OnInitialize()
        {
            //OnLeftClick += this.OnButtonClick;
            Width.Set(55, 0);
            Height.Set(55, 0);
            //HAlign = 0.5f;
            Top.Set(0, 0);
            Left.Set(0, 0);
            icon.Height.Set(50, 0);
            icon.Width.Set(50, 0);
            icon.OnLeftClick += this.OnButtonClick;
            icon.OnMouseOver += this.OnHover;

            Append(icon);
        }

        public FormButtonIcon getIcon()
        {
            return icon;
        }


        public void OnHover(UIMouseEvent evt, UIElement listeningElement)
        {
            if (isVisible())
            {
                DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
                modSystem.MyFormsStatsUI.formHoverText = name;
            }
           
        }

        public void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (isVisible())
            {
                var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
                DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
                if (unlock)
                {
                    if (modPlayer.useFormPoints(name))
                    {
                        modSystem.MyFormsStatsUI.unlockForm(name);
                        //BorderColor = Colors.Green;
                    }
                }
                else
                {
                    modPlayer.setSelectedForm(this.name);
                    modSystem.MyFormsStatsUI.addStatButtons(modPlayer.getSelectedFormID());
                }
            }
            
            //modSystem.MyFormsStatsUI.removeStatButtons(modPlayer.getSelectedFormID());
            
        }

        public Boolean isVisible()
        {
            if(name == "baseForm") { return true; }
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (unlock)
            {
                return modSystem.MyFormsStatsUI.visibleUnlocks.Contains(name);
            }
            else
            {
                return modPlayer.unlockedForms.Contains(name);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible())
            {
                DrawChildren(spriteBatch);
            }
            
            /*if (overflowHidden)
            {
                rasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
                spriteBatch.End();
                spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle;
                spriteBatch.GraphicsDevice.RasterizerState = rasterizerState;
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, rasterizerState, null, Main.UIScaleMatrix);
            }*/
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (isVisible())
            {
                base.DrawSelf(spriteBatch);
            }
            
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            base.DrawChildren(spriteBatch);
        }

    }
}
