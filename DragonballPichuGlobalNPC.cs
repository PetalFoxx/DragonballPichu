using DragonballPichu.Common.GUI;
using DragonballPichu.Common.Systems;
using Ionic.Zlib;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Humanizer.On;
using static tModPorter.ProgressUpdate;

namespace DragonballPichu
{
    public class DragonballPichuGlobalNPC : GlobalNPC
    {
        //public static List<NPC> spawnedBosses = new List<NPC>();
        public static List<string> minibossNames = new List<string>()
        {
            "Dark Mage",
            "Ogre",
            "Betsy",
            "Flying Dutchman",
            "Mourning Wood",
            "Pumpking",
            "Everscream",
            "Santa-NK",
            "Ice Queen",
            "Martian Saucer",
            "Solar Pillar",
            "Nebula Pillar",
            "Vortex Pillar",
            "Stardust Pillar",
            "Giant Clam",
            "Cyber Draedon",
            "Clamitas",
            "Earth Elemental",
            "Onyx Kinsman",
            "Aether Valkyrie",
            "Cragmaw Mire",
            "Armored Digger",
            "Life Slime",
            "Great Sand Shark",
            "Plaguebringer",
            "Yggdrasil Ent",
            "Plague Emperor",
            "Charm Gar",
            "Distortion Essence Sphere",
            "Mechawyrm",
            "Empyreon",
            "Mauler",
            "Colossal Squid",
            "Reaper Shark",
            "Eidolon Wyrm",
            "The General",
            "Nuclear Terror",
            "La Ruga",
            "Lav'e Rugamarius",
            "Primordial Rainbow Slime",
        };

        public static Dictionary<string, int> formPointsValue = new Dictionary<string, int>()
        {
            { "King Slime",              1   },
            { "Eye of Cthulhu",          1   },
            { "Eater of Worlds",         2   },
            { "Brain of Cthulhu",        2   },
            { "Queen Bee",               1   },
            { "Skeletron",               2   },
            { "Deerclops",               1   },
            { "Wall of Flesh",           2   },
            { "Queen Slime",             5   },
            { "Retinazer",               2   },
            { "Spazmatism",              3   },
            { "The Destroyer",           5   },
            { "Skeletron Prime",         5   },
            { "Mechdusa",                15  },
            { "Plantera",                15  },
            { "Golem",                   25  },
            { "Duke Fishron",            50  },
            { "Empress of Light",        50  },
            { "Lunatic Cultist",         20  },
            { "Moon Lord's Core",        100 },
            { "Dark Mage",               5   },
            { "Ogre",                    15  },
            { "Betsy",                   100 },
            { "Flying Dutchman",         25  },
            { "Mourning Wood",           25  },
            { "Pumpking",                25  },
            { "Everscream",              25  },
            { "Santa-NK",                25  },
            { "Ice Queen",               25  },
            { "Martian Saucer",          25  },
            { "Solar Pillar",            25  },
            { "Nebula Pillar",           25  },
            { "Vortex Pillar",           25  },
            { "Stardust Pillar",         25  },
            { "Desert Scourge",           1  },
            { "Crabulon",                 1  },
            { "The Hive Mind",            1  },
            { "The Perforator Hive",      1  },
            { "Aquatic Scourge",          5  },
            { "The Slime God",            1  },
            { "The Inventors",            1  },
            { "Wulfrum Excavator",        1  },
            { "Monstro",                  1  },
            { "Acidsighter",              1  },
            { "Goozma (Pre-Hardmode)",    1  },
            { "Birdest Up, Kulu-Yharon-Ku",100}, //as much as Yharon
            { "Cumulomenace",             5  },
            { "Cryogen",                  5  },
            { "Pyrogen",                  5  },
            { "Ionogen",                  10 },
            { "Phytogen",                 10 },
            { "Pathogen",                 10 },
            { "Oxygen",                   25 },
            { "Carcinogen",               25 },
            { "Hydrogen",                 25 },
            { "Aetherial Guardian, Olistene",5},
            { "Brimstone Elemental",      5  },
            { "Calamitas",                10 },
            { "Serial Designation N",     10 },
            { "The Forbidden Lantern",    10 },
            { "The Derellect",            10 },
            { "Polyphemalus",             100},
            { "Leviathan and Anahita",    15 },
            { "Astrum Aureus",            15 },
            { "The Plaguebringer Goliath",50 },
            { "Ravager",                  25 },
            { "The Magnificent Lightning Avian",50},
            { "Artificial Sentience System, AX-P",25},
            { "Astrum Deus",              50 },
            { "Yharim Worm",              25 },
            { "Profaned Guardians",       50 },
            { "Providence, the Profaned Goddess",100},
            { "Ceaseless Void",           50 },
            { "Storm Weaver",             50 },
            { "Signus, Envoy of the Devourer",50},
            { "Polterghast",              100},
            { "The Starborn Moth",        50 },
            { "Profusion, the Viral God", 50 },
            { "The Devourer of Gods",     100 },
            { "Horseslimes of the Goopocalypse",50},
            { "Jungle Dragon, Yharon",    100},
            { "The Wall of Bronze",       100},
            { "The Wall of Gifts",        100},
            { "The Fabricational Quartet",250},
            { "XP- Hypnos",               250},
            { "Supreme Calamitas",        250},
            { "XB- Losbaf",               100},
            { "XC- Oizys",                250},
            { "Goozma (Godseeker Mode)",  250},
            { "Godseeker, Yharim",        250},
            { "Adult Eidolon Wyrm",       250},
            { "Vision of the Tyrant",     250},
            { "The Entropic God, Noxus",  250},
            { "Disgusting Creature: Dron",250},
            { "Esserwyrm",                250},
            { "Reactive Guardian, Sodis", 250},
            { "Nihil, the Fathomless Seeker",250},
            { "Exodygen",                 250},
            { "Xeroc, the Nameless Deity",250},
            { "Giant Clam",               1  },
            { "Cyber Draedon",            3  },
            { "Clamitas",                 5  },
            { "Earth Elemental",          1  },
            { "Onyx Kinsman",             2  },
            { "Aether Valkyrie",          1  },
            { "Cragmaw Mire",             5  },
            { "Armored Digger",           5  },
            { "Life Slime",               5  },
            { "Great Sand Shark",         1  },
            { "Plaguebringer",            1  },
            { "Yggdrasil Ent",            4  },
            { "Plague Emperor",           25 },
            { "Charm Gar",                15 },
            { "Distortion Essence Sphere",25 },
            { "Mechawyrm",                25 },
            { "Empyreon",                 25 },
            { "Mauler",                   25 },
            { "Colossal Squid",           25 },
            { "Reaper Shark",             25 },
            { "Eidolon Wyrm",             25 },
            { "The General",              15 },
            { "Nuclear Terror",           25 },
            { "La Ruga",                  25 },
            { "Lav'e Rugamarius",         50 },
            { "Primordial Rainbow Slime", 100}
        };

        public static bool isBossOrMiniBoss(NPC npc)
        {
            if (npc.boss) { return true; }
            if (minibossNames.Contains(npc.TypeName)) {  return true; }

            return false;
        }


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
            if (newEnemy && isBossOrMiniBoss(npc) && !modPlayer.bossesThatHitYou.Contains(npc))
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
            if (isBossOrMiniBoss(npc) && modPlayer.bossesThatHitYou.Contains(npc))
            {
                modPlayer.bossesThatHitYou.Remove(npc);
                //Main.NewText(npc + "died after hitting you");
            }
            
            base.OnKill(npc);
            

           
            
            float xpToGain = npc.lifeMax / 10f;
            //if (newEnemy)
            //{
            //    xpToGain *= 10;
            //}


            if (newEnemy)
            {
                if (isBossOrMiniBoss(npc) && formPointsValue.Keys.Contains(npc.TypeName))
                {
                    //Main.NewText(npc.TypeName + " has a value of " + formPointsValue[npc.TypeName]);
                    Main.NewText("Gained " + formPointsValue[npc.TypeName] + " form points for killing " + npc.TypeName);
                }
                if (isBossOrMiniBoss(npc) && !formPointsValue.Keys.Contains(npc.TypeName))
                {
                    Main.NewText("Couldn't find point value for " + npc.TypeName);
                }
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

        /*public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (!npc.active) { base.OnSpawn(npc, source); return; }
            if (isBossOrMiniBoss(npc) && formPointsValue.Keys.Contains(npc.TypeName))
            {
                Main.NewText(npc.TypeName + " has a value of " + formPointsValue[npc.TypeName]);
            }
            else if (isBossOrMiniBoss(npc))
            {
                Main.NewText(npc.TypeName + " is a boss or miniboss not with a point value");
            }
            else if ( ! isBossOrMiniBoss(npc)  && formPointsValue.Keys.Contains(npc.TypeName))
            {
                Main.NewText(npc.TypeName + " isn't a boss or in list of minibosses but has a point value");
            }

            //if (npc.)
            
        }*/


    }
}
