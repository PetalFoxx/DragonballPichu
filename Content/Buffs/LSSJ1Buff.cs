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
    internal class LSSJ1Buff : TransformationBuff
    {
        public static new readonly int DefenseBonus = 20;
        public static new readonly float KiDrain = 20;
        public static new readonly float SpeedBonus = 1.35f;
        public static new readonly float DamageBonus = 1.4f;
        public static new readonly string name = "LSSJ1";
        public static new readonly Boolean isStackable = false;

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
