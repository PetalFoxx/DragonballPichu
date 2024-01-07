using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;

namespace DragonballPichu.Content.Buffs
{
    public class TransformationBuff : ModBuff
    {
        public static readonly int DefenseBonus = 0;
        public static readonly float KiDrain = 0;
        public static readonly float SpeedBonus = 1;
        public static readonly float DamageBonus = 1;
        public static readonly string name = "ExampleTransformation";
        public static readonly Boolean isStackable = true;
        public static readonly List<string> special = new List<string>() { "", "" };
        public static readonly string UnlockHint = "";

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
