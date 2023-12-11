using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DragonballPichu.Common.GUI
{
    class DragonballPichuUISystem : ModSystem 
    {
        public UserInterface MyInterface;
        public FormsStatsUI MyFormsStatsUI;
        public GameTime _lastUpdateUiGameTime;

        public override void Load()
        {
            base.Load();
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyFormsStatsUI = new FormsStatsUI();
                MyFormsStatsUI.Activate(); // Activate calls Initialize() on the UIState if not initialized and calls OnActivate, then calls Activate on every child element.
            }

            
        }

        public override void Unload()
        {
            base.Unload();
            //MyFormsStatsUI?.SomeKindOfUnload(); // If you hold data that needs to be unloaded, call it in OO-fashion
            MyFormsStatsUI = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "DragonballPichu: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }

        public void ShowMyUI()
        {
            MyInterface?.SetState(MyFormsStatsUI);
        }

        public void HideMyUI()
        {
            MyInterface?.SetState(null);
        }

        public void switchVisibility()
        {
            if (MyInterface.CurrentState == MyFormsStatsUI)
            {
                HideMyUI();
            }
            else
            {
                ShowMyUI();
            }
        }
    }
}
