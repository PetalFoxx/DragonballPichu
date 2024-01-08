using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DragonballPichu.Content.Prefixes
{
    public class ExperienceGainPrefix : ModPrefix
    {
        public virtual float Power => 1f;
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override bool CanRoll(Item item)
        {
            return true;
        }
        public override float RollChance(Item item)
        {
            return 5f;
        }

        
        public override void ApplyAccessoryEffects(Player player)
        {
            DragonballPichuPlayer modPlayer = player.GetModPlayer<DragonballPichuPlayer>();
            modPlayer.accessoryExperienceMulti *= 1.1f;
            base.ApplyAccessoryEffects(player);
        }
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.05f * Power;
        }
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixWeaponAwesomeDescription", AdditionalTooltip.Value)
            {
                IsModifier = true,
            };
        }
        public static LocalizedText PowerTooltip { get; private set; }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override void SetStaticDefaults()
        {
            PowerTooltip = Mod.GetLocalization($"{LocalizationCategory}.{nameof(PowerTooltip)}");
            _ = AdditionalTooltip;
        }
    }
}