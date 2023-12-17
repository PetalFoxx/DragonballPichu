using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DragonballPichu.Content.Buffs
{
    public class SSJ1G3Buff : TransformationBuff
    {
        public static new readonly int DefenseBonus = 25;
        public static new readonly float KiDrain = 35;
        public static new readonly float SpeedBonus = .75f;
        public static new readonly float DamageBonus = 1.6f;
        public static new readonly string name = "SSJ1G3";
        public static new readonly Boolean isStackable = false;
        public static new readonly List<string> special = new List<string>() { "Kaio-Efficient", "" };

        public override LocalizedText Description => base.Description;

        public override void Update(Player player, ref int buffIndex)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();

            

            
            float formDefenseMastery = modPlayer.getStat(name + "FormMultDefense").getValue();
            float formDamageMastery = modPlayer.getStat(name + "FormMultDamage").getValue();

            int defenseToAdd = (int)(DefenseBonus * formDefenseMastery);
            player.statDefense += defenseToAdd;
            
            player.GetDamage(DamageClass.Generic) *= (1 + ((DamageBonus-1) * formDamageMastery));
        }
    }
}
