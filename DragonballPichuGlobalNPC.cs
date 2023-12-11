using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DragonballPichu
{
    public class DragonballPichuGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();

            Boolean newEnemy = modPlayer.tryAddNewEnemyToCompendium(npc);
            Boolean boss = npc.boss;
            float xpToGain = npc.lifeMax / 10f;
            //if (newEnemy)
            //{
            //    xpToGain *= 10;
            //}
            if (boss && newEnemy)
            {
                //xpToGain *= 10;
                modPlayer.gainFormPoints(npc);
            }

            if (modPlayer.currentBuffID != -1 && modPlayer.isTransformed)
            {
                modPlayer.gainExperience(xpToGain);
                modPlayer.nameToStats[modPlayer.currentBuff].gainExperience(xpToGain * 2f);
                foreach (var buff in modPlayer.stackedBuffs)
                {
                    modPlayer.nameToStats[buff].gainExperience(xpToGain * 2f);
                }
            }
            else
            {
                modPlayer.gainExperience(xpToGain * 2f);
            }
        }
    }
}
