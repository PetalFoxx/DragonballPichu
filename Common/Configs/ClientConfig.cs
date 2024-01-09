using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace DragonballPichu.Common.Configs
{
    internal class ClientConfig : ModConfig
    {
        [JsonIgnore]
        List<Object> previous_configs = new List<Object>();
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Qol")]


        //makes charging a toggle instead of holding
        [DefaultValue(false)]
        public bool isChargingToggle; //implemented!

        //makes the ki bar hide when over 95% full
        [DefaultValue(true)]
        public bool hideKiBarWhenFull; //implemented!

        [Header("Cheats")]


        //changes form point amount to the given number, disabled by default, enable in server config
        [Range(-1, 10000)]
        [DefaultValue(-1)]
        public int formPoints; //implemented!

        //changes base form's level to the given number, disabled by default, enable in server config
        [Range(-1, 10000)]
        [DefaultValue(-1)]
        public int baseLevel; //implemented!

        //changes all form's level to the given number, disabled by default, enable in server config
        [Range(-1, 10000)]
        [DefaultValue(-1)]
        public int allFormLevel; //implemented!

        public override void OnChanged()
        {
            if(Main.LocalPlayer == null) { return; }
            DragonballPichuPlayer modPlayer;
            Main.LocalPlayer.TryGetModPlayer<DragonballPichuPlayer>(out modPlayer);
            if (modPlayer == null) { return; }
            //if(modPlayer == null) { return; }
            if (formPoints != -1 && ModContent.GetInstance<ServerConfig>().allowClientCheating)
            {
                Main.NewText("Set form points to " + formPoints + " from " + modPlayer.formPoints);
                modPlayer.printToLog("Set form points to " + formPoints + " from " + modPlayer.formPoints);
                modPlayer.formPoints = formPoints;
                formPoints = -1;
            }
            if (baseLevel != -1 && ModContent.GetInstance<ServerConfig>().allowClientCheating)
            {
                Main.NewText("Set base form level to " + baseLevel + " from " + modPlayer.getLevel());
                modPlayer.printToLog("Set base form level to " + baseLevel + " from " + modPlayer.getLevel());
                modPlayer.setLevel(baseLevel);
                baseLevel = -1;
            }
            if (allFormLevel != -1 && ModContent.GetInstance<ServerConfig>().allowClientCheating)
            {
                Main.NewText("Set all forms' level to " + allFormLevel);
                modPlayer.printToLog("Set all forms' level to " + allFormLevel);
                modPlayer.setLevelOfAllForms(allFormLevel);
                allFormLevel = -1;
            }


            base.OnChanged();
        }
    }
}
