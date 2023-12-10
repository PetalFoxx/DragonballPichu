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
    internal class FormButton : UIPanel
    {
        string name;
        Asset<Texture2D> texture;
        UIImage icon;
        Boolean unlock;
        public Boolean deactivated;
        public FormButton(string name, Asset<Texture2D> texture, Boolean unlock)
        {
            this.name = name;
            this.texture = texture;
            this.icon = new UIImage(texture);
            this.unlock = unlock;
            this.deactivated = !unlock;
            if(name == "baseForm")
            {
                deactivated = false;
            }
        }

        public override void OnInitialize()
        {
            OnLeftClick += this.OnButtonClick;
            Width.Set(50, 0);
            Height.Set(50, 0);
            //HAlign = 0.5f;
            Top.Set(0, 0);
            Left.Set(0, 0);
            icon.Height.Set(50, 0);
            icon.Width.Set(50, 0);
            icon.OnLeftClick += this.OnButtonClick;
            icon.OnMouseOver += this.OnHover;

            Append(icon);
        }

        public override void OnDeactivate()
        {
            deactivated = true;
        }

        public override void OnActivate()
        {
            deactivated = false;
        }

        private void OnHover(UIMouseEvent evt, UIElement listeningElement)
        {
            if (deactivated)
            {
                return;
            }
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            modSystem.MyFormsStatsUI.formHoverText = name;
        }

        private void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (deactivated)
            {
                return;
            }
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (unlock)
            {
                if (modPlayer.useFormPoints(name))
                {
                    modSystem.MyFormsStatsUI.unlockForm(name);
                }   
            }
            else
            {
                modPlayer.setSelectedForm(this.name);
                modSystem.MyFormsStatsUI.addStatButtons(modPlayer.getSelectedFormID());
            }
            //modSystem.MyFormsStatsUI.removeStatButtons(modPlayer.getSelectedFormID());
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            /*bool overflowHidden = OverflowHidden;
            bool useImmediateMode = UseImmediateMode;
            RasterizerState rasterizerState = spriteBatch.GraphicsDevice.RasterizerState;
            Rectangle scissorRectangle = spriteBatch.GraphicsDevice.ScissorRectangle;
            SamplerState anisotropicClamp = SamplerState.AnisotropicClamp;
            if (useImmediateMode || OverrideSamplerState != null)
            {
                spriteBatch.End();
                spriteBatch.Begin(useImmediateMode ? SpriteSortMode.Immediate : SpriteSortMode.Deferred, BlendState.AlphaBlend, (OverrideSamplerState != null) ? OverrideSamplerState : anisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
                DrawSelf(spriteBatch);
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
            }
            else
            {
                DrawSelf(spriteBatch);
            }*/


            /*if (overflowHidden)
            {
                spriteBatch.End();
                Rectangle scissorRectangle2 = Rectangle.Intersect(GetClippingRectangle(spriteBatch), spriteBatch.GraphicsDevice.ScissorRectangle);
                spriteBatch.GraphicsDevice.ScissorRectangle = scissorRectangle2;
                spriteBatch.GraphicsDevice.RasterizerState = OverflowHiddenRasterizerState;
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, anisotropicClamp, DepthStencilState.None, OverflowHiddenRasterizerState, null, Main.UIScaleMatrix);
            }*/
            //DrawSelf(spriteBatch);
            DrawChildren(spriteBatch);
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
            if (deactivated)
            {
                return;
            }
            base.DrawSelf(spriteBatch);
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            if (deactivated)
            {
                return;
            }
            base.DrawChildren(spriteBatch);
        }

    }
}
