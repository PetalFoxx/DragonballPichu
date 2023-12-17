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
    public class DivineBuff : TransformationBuff
    {
        public static new readonly int DefenseBonus = 0;
        public static new readonly float KiDrain = 10;
        public static new readonly float SpeedBonus = 1.25f;
        public static new readonly float DamageBonus = 1f;
        public static new readonly string name = "Divine";
        public static new readonly Boolean isStackable = true;
        public static new readonly List<string> special = new List<string>() { "Regen", "0.25" };

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
