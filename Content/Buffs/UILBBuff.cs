using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using DragonballPichu.Common.Configs;

namespace DragonballPichu.Content.Buffs
{
    public class UILBBuff : TransformationBuff
    {
        public static new readonly int DefenseBonus = 100;
        public static new readonly float KiDrain = 100;
        public static new readonly float SpeedBonus = 2;
        public static new readonly float DamageBonus = 3;
        public static new readonly string name = "UILB";
        public static new readonly Boolean isStackable = false;
        public static new readonly List<string> special = new List<string>() { "Dodge", "1" };
        public static new readonly string UnlockHint = "Throw your whole body and soul into the fleeting moment of each strike, and allow the flames of your power to rage!";

        public override LocalizedText Description => base.Description;

        public override void Update(Player player, ref int buffIndex)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();

            

            
            float formDefenseMastery = modPlayer.getStat(name + "FormMultDefense").getValue();
            float formDamageMastery = modPlayer.getStat(name + "FormMultDamage").getValue();

            int defenseToAdd = (int)(DefenseBonus * formDefenseMastery *  ModContent.GetInstance<ServerConfig>().formDefenseMulti);
            player.statDefense += defenseToAdd;
            
            player.GetDamage(DamageClass.Generic) *= (1 + ((DamageBonus-1) * formDamageMastery *  ModContent.GetInstance<ServerConfig>().formAttackMulti));
        }
    }
}
