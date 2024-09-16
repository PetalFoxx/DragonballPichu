using DragonballPichu.Common.Configs;
using DragonballPichu.Common.GUI;
using DragonballPichu.Common.Systems;
using Ionic.Zlib;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
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
using static Humanizer.In;
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
            { "Trojan Squirrel",          1  },
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
            { "Primordial Rainbow Slime", 100},
            { "Deviantt",                 1  },
            { "Mutant",                  1000},
            { "Astrum Viridis",           30 },
            { "Calamity Elemental",       10 },
            { "Dragonfolly",              50 },
            { "Calamitas Clone",          10 },
            { "XS-01 Artemis",            50 },
            { "XS-03 Apollo",             50 },
            { "XF-09 Ares",               50 },
            { "XF-09 Ares Gauss Nuke",    50 },
            { "XF-09 Ares Laser Cannon",  50 },
            { "XF-09 Ares Plasma Cannon", 50 },
            { "XF-09 Ares Tesla Cannon",  50 },
            { "Anahita",                  5  },
            { "The Leviathan",            10 },
            { "The Old Duke",             100},
            { "Primordial Wyrm",          100},
            { "Guardian Commander",       50 },
            { "Eridanus, Champion of Cosmos",100},
            { "Champion of Earth",        25 },
            { "Champion of Life",         25 },
            { "Champion of Shadow",       25 },
            { "Champion of Spirit",       25 },
            { "Champion of Timber",       25 },
            { "Champion of Will",         25 },
            { "Lifelight",                50 },
            { "Permafrost",               1000},
            { "Banished Baron",           50 },
            { "Abominationn",             250},
        };

        public static bool isBossOrMiniBoss(NPC npc)
        {
            if (npc.boss) { return true; }
            if (minibossNames.Contains(npc.TypeName)) {  return true; }

            return false;
        }


        public override void OnKill(NPC npc)
        {   

            for (int i = 0; i < Main.maxPlayers; i++) { 
                if (!Main.player[i].active) continue;
                DragonballPichuPlayer modPlayer = Main.player[i].GetModPlayer<DragonballPichuPlayer>();
            
                Boolean newEnemy = modPlayer.tryAddNewBossToCompendium(npc);
                DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
                if (npc.TypeName.ToLower().Contains("bunny") || npc.TypeName.ToLower().Contains("rabbit")){
                    if (npc.playerInteraction[modPlayer.Entity.whoAmI]){
                        if(!modSystem.MyFormsStatsUI.visibleUnlocks.Contains("Evil"))
                            modSystem.MyFormsStatsUI.visibleUnlocks.Add("Evil");
                        if (!modPlayer.unlockedForms.Contains("Evil"))
                            modPlayer.setUnlockCondition("Evil", true);
                    }
                }
                if (npc.townNPC)
                {
                    Random r = new Random();
                    if (r.Next(100) < modPlayer.getLevel() * 5)
                    {
                        if (modPlayer.unlockedForms.Contains("FSSJ") && !modPlayer.unlockedForms.Contains("SSJ1"))
                        {
                            if (FormTree.isFormAvailable("SSJ1"))
                            {
                                modSystem.MyFormsStatsUI.unlockForm("SSJ1");
                                modPlayer.currentBuff = "SSJ1";
                                modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                                modPlayer.isTransformed = true;
                            }
                            else
                            {
                                Main.NewText("Can't access " + "SSJ1" + "! You are not on the right character path");
                            }
                            
                        }
                        if (modPlayer.unlockedForms.Contains("SSJ4LB") && !modPlayer.unlockedForms.Contains("SSJ5"))
                        {
                            if (FormTree.isFormAvailable("SSJ5"))
                            {
                                modSystem.MyFormsStatsUI.unlockForm("SSJ5");
                                modPlayer.currentBuff = "SSJ5";
                                modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                                modPlayer.isTransformed = true;
                            }
                            else
                            {
                                Main.NewText("Can't access " + "SSJ5" + "! You are not on the right character path");
                            }
                            
                        }
                        if (modPlayer.unlockedForms.Contains("LSSJ4LB") && !modPlayer.unlockedForms.Contains("LSSJ5"))
                        {
                            if (FormTree.isFormAvailable("LSSJ5"))
                            {
                                modSystem.MyFormsStatsUI.unlockForm("LSSJ5");
                                modPlayer.currentBuff = "LSSJ5";
                                modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                                modPlayer.isTransformed = true;
                            }
                            else
                            {
                                Main.NewText("Can't access " + "LSSJ5" + "! You are not on the right character path");
                            }
                            
                        }
                    }
                    
                }
                if (newEnemy && isBossOrMiniBoss(npc) && !modPlayer.bossesThatHitYou.Contains(npc))
                {
                    if (!modPlayer.unlockedForms.Contains("UI") && (ModContent.GetInstance<ServerConfig>().allowUIInPreHardmode || Main.hardMode))
                    //if(true)
                    {
                        if (FormTree.isFormAvailable("UI"))
                        {
                            modPlayer.stackedBuffs.Clear();
                            modPlayer.stackedBuffIDs.Clear();
                            modSystem.MyFormsStatsUI.unlockForm("UI");
                            modPlayer.currentBuff = "UI";
                            modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                            modPlayer.isTransformed = true;
                        }
                        else
                        {
                            Main.NewText("Can't access " + "UI" + "! You are not on the right character path");
                        }


                    }
                }
                if (isBossOrMiniBoss(npc) && modPlayer.bossesThatHitYou.Contains(npc))
                {
                    modPlayer.bossesThatHitYou.Remove(npc);
                }
                
                base.OnKill(npc);
                

            
                
                float xpToGain = npc.lifeMax / 10f;
                //if (newEnemy)
                //{
                //    xpToGain *= 10;
                //}
                xpToGain *= modPlayer.accessoryExperienceMulti * ModContent.GetInstance<ServerConfig>().expMulti;

                if (newEnemy && npc.playerInteraction[modPlayer.Entity.whoAmI])
                {
                    if (isBossOrMiniBoss(npc) && formPointsValue.Keys.Contains(npc.TypeName))
                    {
                        Main.NewText("Gained " + getFormPointsValue(npc.TypeName) + " form points for killing " + npc.TypeName);
                        modPlayer.printToLog("Gained " + getFormPointsValue(npc.TypeName) + " form points for killing " + npc.TypeName);
                    }
                    if (isBossOrMiniBoss(npc) && !formPointsValue.Keys.Contains(npc.TypeName))
                    {
                        Main.NewText("Couldn't find point value for " + npc.TypeName);
                        modPlayer.printToLog("Couldn't find point value for " + npc.TypeName);
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
                if (isBossOrMiniBoss(npc))
                {
                    if (!modSystem.MyFormsStatsUI.visibleUnlocks.Contains("FSSJ")){
                        modSystem.MyFormsStatsUI.visibleUnlocks.Add("FSSJ");
                    }
                    if (npc.TypeName == "Wall of Flesh")
                    {
                        unlockAndMaybeTransform("SSJ1G4", "SSJ2");
                        unlockAndMaybeTransform("FLSSJ", "LSSJ1");
                    }
                    if(npc.TypeName == "Retinazer" || npc.TypeName == "Spazmatism" || npc.TypeName == "The Destroyer" || npc.TypeName == "Skeletron Prime" || npc.TypeName == "Mechdusa")
                    {
                        unlockAndMaybeTransform("SSJ2", "SSJ3");
                        unlockAndMaybeTransform("LSSJ1", "LSSJ2");
                    }
                    if(npc.TypeName == "Plantera")
                    {
                        unlockAndMaybeTransform("SSJ3", "SSJ4");
                        unlockAndMaybeTransform("LSSJ2", "LSSJ3");
                    }
                    if(npc.TypeName == "Golem")
                    {
                        unlockAndMaybeTransform("SSJ4", "SSJ4LB");
                        unlockAndMaybeTransform("LSSJ3", "LSSJ4");
                    }
                    if(npc.TypeName == "Lunatic Cultist")
                    {
                        unlockAndMaybeTransform("SSJG");
                    }
                    if(npc.TypeName == "Moon Lord" || npc.TypeName == "Moon Lord's Core")
                    {
                        unlockAndMaybeTransform("FSSJB", "SSJB1");
                        unlockAndMaybeTransform("LSSJ4", "LSSJ4LB");
                        unlockAndMaybeTransform("LSSJ3", "SSJG", "LSSJB");
                    }
                }
            }
        }

        public static int getFormPointsValue(string typeName)
        {
            return (int)(formPointsValue[typeName] * ModContent.GetInstance<ServerConfig>().formPointsMulti);
        }

        public static void unlockAndMaybeTransform(string neededForm, string newForm)
        {
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (modPlayer.currentBuff == neededForm)
            {
                FormStats.unlockAndTransform(newForm);
            }
            else if (modPlayer.unlockedForms.Contains(neededForm) && !modPlayer.unlockedForms.Contains(newForm))
            {
                if (FormTree.isFormAvailable(newForm))
                {
                    modSystem.MyFormsStatsUI.unlockForm(newForm);
                }
                else
                {
                    Main.NewText("Can't access " + newForm + "! You are not on the right character path");
                }
                
            }
        }

        public static void unlockAndMaybeTransform(string neededForm, string neededForm2, string newForm)
        {
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if ((modPlayer.currentBuff == neededForm && modPlayer.unlockedForms.Contains(neededForm2)) || (modPlayer.currentBuff == neededForm2 && modPlayer.unlockedForms.Contains(neededForm)))
            {
                FormStats.unlockAndTransform(newForm);
            }
            else if (modPlayer.unlockedForms.Contains(neededForm) && modPlayer.unlockedForms.Contains(neededForm2) && !modPlayer.unlockedForms.Contains(newForm))
            {
                if (FormTree.isFormAvailable(newForm))
                {
                    modSystem.MyFormsStatsUI.unlockForm(newForm);
                }
                else
                {
                    Main.NewText("Can't access " + newForm + "! You are not on the right character path");
                }
            }
        }

        public static void unlockAndMaybeTransform(string newForm)
        {
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (modPlayer.currentBuff == "baseForm" || modPlayer.currentBuff == null)
            {
                FormStats.unlockAndTransform(newForm);
            }
            else if (!modPlayer.unlockedForms.Contains(newForm))
            {
                if (FormTree.isFormAvailable(newForm))
                {
                    modSystem.MyFormsStatsUI.unlockForm(newForm);
                }
                else
                {
                    Main.NewText("Can't access " + newForm + "! You are not on the right character path");
                }
                
            }
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (!npc.active) { base.OnSpawn(npc, source); return; }
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (npc.TypeName.Equals("Wall of Flesh"))
            {
                modPlayer.setUnlockCondition("Rampaging", true);
            }
            else if((npc.TypeName.Equals("Moon Lord") || npc.TypeName == "Moon Lord's Core") && modPlayer.currentBuff.Equals("SSJG") && (!modPlayer.unlockedForms.Contains("FSSJB") || (modPlayer.formSetsSystem.get().Contains("SSJR1"))))
            {
                if (modPlayer.formSetsSystem.get().Contains("SSJR1") && FormTree.isFormAvailable("SSJR1"))
                {
                    modSystem.MyFormsStatsUI.unlockForm("SSJR1");
                    modPlayer.currentBuff = "SSJR1";
                    modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                    modPlayer.isTransformed = true;
                }
                else
                {
                    if (FormTree.isFormAvailable("FSSJB"))
                    {
                        modSystem.MyFormsStatsUI.unlockForm("FSSJB");
                        modPlayer.currentBuff = "FSSJB";
                        modPlayer.currentBuffID = FormTree.formNameToID(modPlayer.currentBuff);
                        modPlayer.isTransformed = true;
                    }
                    else
                    {
                        Main.NewText("Can't access " + "FSSJB" + "! You are not on the right character path");
                    }
                }
            }
            else if(npc.TypeName.Equals("Lunatic Cultist"))
            {
                modPlayer.setUnlockCondition("SSJRage", true);
            }
            
            
        }


    }
}
