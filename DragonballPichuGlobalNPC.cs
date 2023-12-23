using DragonballPichu.Common.GUI;
using DragonballPichu.Common.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DragonballPichu
{
    public class DragonballPichuGlobalNPC : GlobalNPC
    {
        //public static List<NPC> spawnedBosses = new List<NPC>();
        
        public override void OnKill(NPC npc)
        {
            
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            Boolean newEnemy = modPlayer.tryAddNewBossToCompendium(npc);
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();

            if (npc.townNPC)
            {
                Random r = new Random();
                if (r.Next(100) < modPlayer.getLevel() * 5)
                {
                    if (modPlayer.unlockedForms.Contains("FSSJ") && !modPlayer.unlockedForms.Contains("SSJ1"))
                    {
                        modSystem.MyFormsStatsUI.unlockForm("SSJ1");
                        modPlayer.currentBuff = "SSJ1";
                        modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                        modPlayer.isTransformed = true;
                    }
                }
                
            }
            if (newEnemy && npc.boss && !modPlayer.bossesThatHitYou.Contains(npc))
            {
                if (!modPlayer.unlockedForms.Contains("UI"))
                //if(true)
                {
                    //Main.NewText(npc + "died");
                    modPlayer.stackedBuffs.Clear();
                    modPlayer.stackedBuffIDs.Clear();
                    modSystem.MyFormsStatsUI.unlockForm("UI");
                    modPlayer.currentBuff = "UI";
                    modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                    modPlayer.isTransformed = true;
                }
            }
            if (npc.boss && modPlayer.bossesThatHitYou.Contains(npc))
            {
                modPlayer.bossesThatHitYou.Remove(npc);
                //Main.NewText(npc + "died after hitting you");
            }
            
            base.OnKill(npc);
            

           
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
