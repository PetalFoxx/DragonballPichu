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
    public class UIBuff : TransformationBuff
    {
        public static new readonly int DefenseBonus = 0;
        public static new readonly float KiDrain = 5;
        public static new readonly float SpeedBonus = 1;
        public static new readonly float DamageBonus = 1;
        public static new readonly string name = "UI";
        public static new readonly Boolean isStackable = true;
        public static new readonly List<string> special = new List<string>() { "Dodge", "1" };
        public static new readonly string UnlockHint = "Focus on nothing but you, your opponent, and the fight; This is the pinnacle of martial arts";

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
