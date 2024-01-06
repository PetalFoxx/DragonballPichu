using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameInput;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using DragonballPichu.Common.Systems;
using DragonballPichu.Common;
using DragonballPichu.Content.Buffs;
using DragonballPichu.Common.GUI;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using log4net.Core;
using Terraria.DataStructures;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;

namespace DragonballPichu
{
    public class DragonballPichuPlayer : ModPlayer
    {

        float curKi = 0;

        public Stat maxKi = new Stat("maxKi", 100);
        public Stat kiGain = new Stat("kiGain", 0);
        public Stat kiDrain = new Stat("kiDrain", 0);
        public Stat chargeKiGain = new Stat("chargingKiGain", 1);
        public Stat baseAttack = new Stat("baseAttack", 1);
        public Stat baseDefense = new Stat("baseDefense", 0);
        public Stat baseSpeed = new Stat("baseSpeed", 1);

        float experience = 0;
        int level = 0;
        int points = 0;
        int levelInAll = 0;
        int formPoints = 0;
        int spendFormPoints = 0;
        Boolean firstTimeOpenMenu = true;

        List<int> errorIDsList = new List<int>();

        public string currentBuff = "";
        public int currentBuffID = -1;

        public List<string> stackedBuffs = new List<string>();
        public List<int> stackedBuffIDs = new List<int>();

        public List<string> unlockedForms = new List<string>();
        public List<string> unlockLoaded = new List<string>();

        public List<NPC> bossesThatHitYou = new List<NPC>();


        public Dictionary<string, int> formToUnlockPoints = new Dictionary<string, int>()
        {
            { "FSSJ", 0 },
            { "SSJ1", 4 },
            { "SSJ1G2", 0 },
            { "FSSJB", 0 },
            { "SSJ1G3", 0 },
            { "SSJ1G4", 0 },
            { "SSJ2", 0 },
            { "SSJ3", 0 },
            { "SSJRage", 200 },
            { "SSJ4", 0 },
            { "SSJ4LB", 0 },
            { "SSJ5", 100 },
            { "SSJ5G2", 0 },
            { "SSJ5G3", 0 },
            { "SSJ5G4", 0 },
            { "SSJ6", 200 },
            { "SSJ7", 300 },
            { "FLSSJ", 0 },
            { "Ikari", 2 },
            { "LSSJ1", 0 },
            { "LSSJ2", 0 },
            { "LSSJ3", 0 },
            { "LSSJ4", 0 },
            { "LSSJ4LB", 0 },
            { "LSSJ5", 100 },
            { "LSSJ6", 200 },
            { "LSSJ7", 300 },
            { "SSJG", 0 },
            { "LSSJB", 0 },
            { "SSJB1", 0 },
            { "SSJB1G2", 0 },
            { "SSJB1G3", 0 },
            { "SSJB1G4", 0 },
            { "SSJB2", 200 },
            { "SSJB3", 300 },
            { "SSJBE", 0 },
            { "SSJR1", 0 },
            { "SSJR1G2", 0 },
            { "SSJR1G3", 0 },
            { "SSJR1G4", 0 },
            { "SSJR2", 200 },
            { "SSJR3", 300 },
            { "Divine", 0 },
            { "DR", 500 },
            { "Evil", 4 },
            { "Rampaging", 10 },
            { "Berserk", 50 },
            { "PU", 4 },
            { "Beast", 500 },
            { "UE", 500 },
            { "UI", 100 },
            { "TUI", 200 },
            { "UILB", 300 }
        };

        public Dictionary<string, bool> formToUnlockCondition= new Dictionary<string, bool>()
        {
            { "FSSJ", false },
            { "SSJ1", true },
            { "SSJ1G2", false },
            { "FSSJB", false },
            { "SSJ1G3", false },
            { "SSJ1G4", false },
            { "SSJ2", false },
            { "SSJ3", false },
            { "SSJRage", true },
            { "SSJ4", false },
            { "SSJ4LB", false },
            { "SSJ5", true },
            { "SSJ5G2", false },
            { "SSJ5G3", false },
            { "SSJ5G4", false },
            { "SSJ6", true },
            { "SSJ7", true },
            { "FLSSJ", false },
            { "Ikari", true },
            { "LSSJ1", false },
            { "LSSJ2", false },
            { "LSSJ3", false },
            { "LSSJ4", false },
            { "LSSJ4LB", false },
            { "LSSJ5", true },
            { "LSSJ6", true },
            { "LSSJ7", true },
            { "SSJG", false },
            { "LSSJB", false },
            { "SSJB1", false },
            { "SSJB1G2", false },
            { "SSJB1G3", false },
            { "SSJB1G4", false },
            { "SSJB2", true },
            { "SSJB3", true },
            { "SSJBE", false },
            { "SSJR1", false },
            { "SSJR1G2", false },
            { "SSJR1G3", false },
            { "SSJR1G4", false },
            { "SSJR2", true },
            { "SSJR3", true },
            { "Divine", false },
            { "DR", true },
            { "Evil", false },
            { "Rampaging", false },
            { "Berserk", true },
            { "PU", true },
            { "Beast", true },
            { "UE", true },
            { "UI", true },
            { "TUI", true },
            { "UILB", true }
        };

        public List<string> enemyCompendium = new List<string>();

        public Dictionary<string, FormStats> nameToStats = new Dictionary<string, FormStats>();

        public Dictionary<string, Stat> stats = new Dictionary<string, Stat>();

        bool isCharging = false;
        bool isHoldingTransformKey = false;
        bool isHoldingAlternateKey = false;
        bool isHoldingRevertKey = false;
        public bool isTransformed = false;
        float secondWindTime = 0;
        float secondWindCooldown = 0;

        int selectedFormID = -1;//ModContent.BuffType<SSJ1Buff>();
        String selectedForm = "baseForm";

        public bool formLevelCompare(string form, bool higherOrEqual, int value)
        {
            int formLevel = getFormLevel(form);
            if (higherOrEqual)
            {
                return formLevel >= value;
            }
            else
            {
                return formLevel <= value;
            }
        }

        public int getFormLevel(string form)
        {
            return nameToStats[form].getLevel();
        }

        public bool hasBeaten(string enemy)
        {
            return enemyCompendium.Contains(enemy);
        }

        public void setUnlockCondition(string form, bool val)
        {
            formToUnlockCondition[form] = val;
        }

        public bool getUnlockCondition(string form)
        {
            
            return formToUnlockCondition[form];
        }

        public int getTotalSSJBGradesLevel()
        {
            int total = 0;
            total += nameToStats["SSJB1"].getLevel();
            total += nameToStats["SSJB1G2"].getLevel();
            total += nameToStats["SSJB1G3"].getLevel();
            total += nameToStats["SSJB1G4"].getLevel();
            return total;
        }

        public Stat getStat(string statName)
        {

            if (!stats.ContainsKey("maxKi"))
            {
                stats.Add("maxKi", maxKi);
                stats.Add("kiGain", kiGain);
                stats.Add("kiDrain", kiDrain);
                stats.Add("chargeKiGain", chargeKiGain);
                stats.Add("baseAttack", baseAttack);
                stats.Add("baseDefense", baseDefense);
                stats.Add("baseSpeed", baseSpeed);


            }
            if (nameToStats.Count == 0)
            {
                FormTree.forms.ForEach(form =>
                {
                    nameToStats.Add(form, new FormStats(form));
                });
            }

            if (statName == null)
            {
                return null;
            }
            if (statName.Contains("Form"))
            {
                String[] info = statName.Split("Form");
                FormStats stats = nameToStats[info[0]];
                if (stats != null)
                {
                    Stat stat = stats.getStat(info[1]);
                    return stat;
                }

            }
            if (stats.ContainsKey(statName)){
                return stats.GetValueOrDefault(statName);
            }
            return null;
        }

        public int getFormPoints()
        {
            return formPoints;
        }

        public override void UpdateLifeRegen()
        {
            base.UpdateLifeRegen();
            Player.lifeRegen += (int)getFormSpecialRegen();
        }
        public Boolean useFormPoints(string formName)
        {
            if (formName == null || !FormTree.forms.Contains(formName) || unlockedForms.Contains(formName))
            {
                return false;
            }

            if (!isTransformed)
            {
                switch ((getCurKi() >= 1000))
                {
                    case true:
                        setUnlockCondition("Ikari", true); break;
                    case false:
                        setUnlockCondition("Ikari", false); break;
                }
            }

            

            
            if (getFormPoints() >= formToUnlockPoints[formName] && getUnlockCondition(formName))
            {
                formPoints-= formToUnlockPoints[formName];
                spendFormPoints += formToUnlockPoints[formName];
                return true;
            }
            return false;
        }
        public void gainFormPoints(NPC npc)
        {
            if (DragonballPichuGlobalNPC.formPointsValue.Keys.Contains(npc.TypeName))
            {
                formPoints += DragonballPichuGlobalNPC.formPointsValue[npc.TypeName];
            }
            else
            {
                formPoints++;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public record struct KaiokenPlayerData(float Mastery, double Strain, bool Formed);

        public KaiokenPlayerData getKaiokenModData(Mod kaioken)
        {
           
            var pointer = kaioken.Call(true, Player.whoAmI) is IntPtr ptr ? ptr : throw new Exception($"Can't Call {kaioken.DisplayName}");
            var data = Marshal.PtrToStructure<KaiokenPlayerData>(pointer);
            
            //data.Strain += 1;
            //Marshal.StructureToPtr(data, pointer, true);
            //kaioken.Call(false, Player.whoAmI, pointer);
            return data;
        }

        public void increaseKaiokenModStrain(Mod kaioken, float strainToAdd)
        {
            var pointer = kaioken.Call(true, Player.whoAmI) is IntPtr ptr ? ptr : throw new Exception($"Can't Call {kaioken.DisplayName}");
            var data = Marshal.PtrToStructure<KaiokenPlayerData>(pointer);
            
            data.Strain += strainToAdd;
            Marshal.StructureToPtr(data, pointer, true);
            kaioken.Call(false, Player.whoAmI, pointer);

        }

        public float getKaiokenModStrainGainWithModifiers()
        {
            float baseStrainPerUpdate = .33f;
            float formMulti = getTotalFormsCount();
            float specialMulti = getFormSpecialKaiokenCost();
            if (stackedBuffs.Contains("Kaio-ken"))
            {
                formMulti -= 1;
            }




            
            float strain = baseStrainPerUpdate * formMulti * specialMulti;
            return strain;
        }
        /*
         if (ModLoader.TryGetMod("KaiokenMod", out var kaioken))
            {

            } 
        */


        /*public override void PostUpdateBuffs()
        {
            if (ModLoader.TryGetMod("KaiokenMod", out var kaioken))
            {
                var pointer = kaioken.Call(true, Player.whoAmI) is IntPtr ptr ? ptr : throw new Exception($"Can't Call {kaioken.DisplayName}");
                var data = Marshal.PtrToStructure<KaiokenPlayerData>(pointer);
                
                data.Strain += 1;
                Marshal.StructureToPtr(data, pointer, true);
                kaioken.Call(false, Player.whoAmI, pointer);
            }

            
        }*/

        public float getFranticRegenLevel(string form)
        {
            float toReturn = 0;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Frantic Ki Regen")
                {
                    //return getStat(form + "FormSpecial").getValue();
                    string secondWind = buffSpecial[1];
                    toReturn = ((float)Double.Parse(secondWind)) * getStat(form + "FormSpecial").getValue();
                    //toReturn = FormsStatsUI.invertFraction(toReturn);
                }
            }
            return toReturn;
        }

        public float getFranticRegenTotalLevel()
        {
            float total = 0;
            total += getFranticRegenLevel(currentBuff);


            foreach (string form in stackedBuffs)
            {
                total += getFranticRegenLevel(form);
            }
            return total;
        }

        

        public float getKiRegenFromFranticRegen()
        {
            return ((1 - getHPPercentage()) * getFranticRegenTotalLevel());
        }

        public float getSecondWindLevel(string form)
        {
            float toReturn = 0;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Second Wind")
                {
                    //return getStat(form + "FormSpecial").getValue();
                    string secondWind = buffSpecial[1];
                    toReturn = ((float)Double.Parse(secondWind)) * getStat(form + "FormSpecial").getValue();
                    //toReturn = FormsStatsUI.invertFraction(toReturn);
                }
            }
            return toReturn;
        }

        public float getSecondWindTotalLevel()
        {
            float total = 0;
            total += getSecondWindLevel(currentBuff);


            foreach (string form in stackedBuffs)
            {
                total += getSecondWindLevel(form);
            }
            return total;
        }

        public float getSecondWindBoost()
        {
            return 1 + (getSecondWindTotalLevel()/10);
        }

        public bool getWithinSecondWindThreshold()
        {
            return Player.statLife < 100 + getSecondWindTotalLevel() * 20;
            //return 0.5f;
        }

        public float getSecondWindTime()
        {
            return getSecondWindTotalLevel() * 60;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= getFormSpecialDamageMult();
            modifiers.FinalDamage *= getBaseAttack();
            if (secondWindTime > 0)
            {
                modifiers.FinalDamage *= getSecondWindBoost();
            }
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            
            //base.ModifyHurt(ref modifiers);
        }

        public override void PreUpdate()
        {
            if(Main.npc == null || Main.npc.Length == 0)
            {
                base.PreUpdate();
                return;
            }

            List<NPC> activeBossesThatHitYou = new List<NPC>();
            foreach (NPC boss in bossesThatHitYou)
            {
                if (boss.active)
                {
                    activeBossesThatHitYou.Add(boss);
                }
            }
            bossesThatHitYou = activeBossesThatHitYou;

            base.PreUpdate();
        }

        /*public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if(modifiers.FinalDamage > Player.statLife)
            {

            }
            base.ModifyHitByNPC(npc, ref modifiers);
        }*/
        public bool reviveUnlockTransform(string form)
        {
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            modSystem.MyFormsStatsUI.unlockForm(form);
            currentBuff = form;
            currentBuffID = FormTree.formNameToID(currentBuff);
            isTransformed = true;
            Player.statLife = Player.statLifeMax2 / 2;
            return false;
        }


        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            
            Entity entity;
            damageSource.TryGetCausingEntity(out entity);
            if (entity is NPC && DragonballPichuGlobalNPC.isBossOrMiniBoss((((NPC)entity))))
            {
                NPC npc = (NPC)entity;
                if (!unlockedForms.Contains("FSSJ"))
                {
                    return reviveUnlockTransform("FSSJ");
                }
                if (npc.TypeName == "Wall of Flesh")
                {
                    if (currentBuff == "SSJ1G4")
                    {
                        return reviveUnlockTransform("SSJ2");
                    }
                    else if (currentBuff == "FLSSJ")
                    {
                        return reviveUnlockTransform("LSSJ1");
                    }
                }
                if (npc.TypeName == "Retinazer" || npc.TypeName == "Spazmatism" || npc.TypeName == "The Destroyer" || npc.TypeName == "Skeletron Prime" || npc.TypeName == "Mechdusa")
                {
                    if (currentBuff == "SSJ2")
                    {
                        return reviveUnlockTransform("SSJ3");
                    }
                    else if (currentBuff == "LSSJ1")
                    {
                        return reviveUnlockTransform("LSSJ2");
                    }
                }
                if (npc.TypeName == "Plantera")
                {
                    if (currentBuff == "SSJ3")
                    {
                        return reviveUnlockTransform("SSJ4");
                    }
                    else if (currentBuff == "LSSJ2")
                    {
                        return reviveUnlockTransform("LSSJ3");
                    }
                }
                if (npc.TypeName == "Golem")
                {
                    if (currentBuff == "SSJ4")
                    {
                        return reviveUnlockTransform("SSJ4LB");
                    }
                    else if (currentBuff == "LSSJ3")
                    {
                        return reviveUnlockTransform("LSSJ4");
                    }
                }
                if (npc.TypeName == "Lunatic Cultist")
                {
                    return reviveUnlockTransform("SSJG");
                }
                if (npc.TypeName == "Moon Lord" || npc.TypeName == "Moon Lord's Core")
                {
                    if (currentBuff == "FSSJB")
                    {
                        return reviveUnlockTransform("SSJB1");
                    }
                    else if (currentBuff == "LSSJ4")
                    {
                        return reviveUnlockTransform("LSSJ4LB");
                    }
                    else if ((currentBuff == "SSJG" || currentBuff == "LSSJ3") && (unlockedForms.Contains("SSJG") && unlockedForms.Contains("LSSJ3")))
                    {
                        return reviveUnlockTransform("LSSJB");
                    }
                }
                return true;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
        }

        public override bool ImmuneTo(PlayerDeathReason damageSource, int cooldownCounter, bool dodgeable)
        {
            //Entity entity = null;
            //damageSource.TryGetCausingEntity(out entity);
            if(secondWindTime > 0)
            {
                return true;
            }
            int dodgeTier = getHighestFormDodgeTier();
            switch (dodgeTier)
            {
                case 1:
                    return doDodge1();
                case 2:
                    return doDodge2();
                default:
                    break;
            }
            return base.ImmuneTo(damageSource, cooldownCounter, dodgeable);
        }
        public bool doDodge1()
        {
            float specialDodgeCostTotal = getTotalSpecialDodgeCost();
            float dodgeCostLiteral = getMaxKi() * .01f;
            dodgeCostLiteral /= specialDodgeCostTotal;
            float kiPercentage = getKiPercentage();
            Random r = new Random();
            float rand = (float)r.NextDouble();
            bool doesDodge = (.9 + kiPercentage / 10) > rand;
            if(getCurKi() > dodgeCostLiteral && doesDodge)
            {
                decreaseKi(dodgeCostLiteral * 20);
                return true;
            }
            return false;
        }

        public bool doDodge2()
        {
            float specialDodgeCostTotal = getTotalSpecialDodgeCost();
            float dodgeCostLiteral = 25;
            dodgeCostLiteral /= specialDodgeCostTotal;
            if (getCurKi() > dodgeCostLiteral)
            {
                decreaseKi(dodgeCostLiteral * 20);
                return true;
            }
            return false;
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }

        




        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if(getSecondWindTotalLevel() > 0 && getWithinSecondWindThreshold() && secondWindCooldown == 0) 
            {
                secondWindTime = getSecondWindTime();
            }
            if (DragonballPichuGlobalNPC.isBossOrMiniBoss(npc))
            {
                bossesThatHitYou.Add(npc);
                
            }
            base.OnHitByNPC(npc, hurtInfo);
        }

        public Boolean tryAddNewBossToCompendium(NPC npc)
        {
            if(DragonballPichuGlobalNPC.isBossOrMiniBoss(npc) && !enemyCompendium.Contains(npc.TypeName))
            {
                enemyCompendium.Add(npc.TypeName);
                return true;
            }

            return false;
            
        }

        public string getSelectedForm()
        {
            return selectedForm;
            
        }

        public void printToLog(string text)
        {
            Mod.Logger.Debug(text);
        }

        public void setSelectedForm(string form)
        {
            if(selectedForm != form)
            {
                Main.NewText("Selected " + form);
                printToLog("Selected " + form);
            }
            selectedForm = form;
            selectedFormID = FormTree.formNameToID(form);
        }
        public int getSelectedFormID()
        {
            return selectedFormID;
        }

        public override void PostUpdateRunSpeeds()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            float speedMulti = 1;
            if(currentBuffID != -1 && isTransformed)
            {
                speedMulti *= (FormTree.nameToSpeedBonus[currentBuff] * modPlayer.getStat(currentBuff + "FormMultSpeed").getValue());
                foreach (var buff in stackedBuffs)
                {
                    speedMulti *= (FormTree.nameToSpeedBonus[buff] * modPlayer.getStat(buff + "FormMultSpeed").getValue());
                }
            }
            speedMulti *= getBaseSpeed();
            if (isCharging)
            {
                speedMulti /= 25f;
            }
            Player.runAcceleration *= speedMulti;
            Player.maxRunSpeed *= speedMulti;
            base.PostUpdateRunSpeeds();
        }



        public float getCurKi() { return curKi; }
        public void setCurKi(float kiAmt) { curKi = (Math.Clamp(kiAmt, 0, maxKi.getValue())); }
        public void increaseKi(float kiAmt) { setCurKi((getCurKi() + kiAmt/20f)); }
        public void decreaseKi(float kiAmt) { setCurKi(getCurKi() - kiAmt/20f); }
        public float getMaxKi() { return maxKi.getValue(); }
        public void setMaxKi(float kiAmt) {  maxKi.setValue((Math.Clamp(kiAmt, 0, float.MaxValue))); }
        public void increaseKiMax(float kiAmt) { maxKi.increaseValue(kiAmt); }
        public void decreaseKiMax(float kiAmt) { maxKi.decreaseValue(kiAmt); }
        public float getKiGain() { return kiGain.getValue(); }
        public void setKiGain(float kiAmt) { kiGain.setValue((Math.Clamp(kiAmt, 0, float.MaxValue))); }
        public void increaseKiGain(float kiAmt) { kiGain.increaseValue(kiAmt); }
        public void decreaseKiGain(float kiAmt) { kiGain.decreaseValue(kiAmt); }
        public float getKiDrain() { return kiDrain.getValue(); }
        public void setKiDrain(float kiAmt) { kiDrain.setValue((Math.Clamp(kiAmt, 0, float.MaxValue))); }
        public void increaseKiDrain(float kiAmt) { kiDrain.increaseValue(kiAmt); }
        public void decreaseKiDrain(float kiAmt) { kiDrain.decreaseValue(kiAmt); }
        public float getChargeKiGain() { return chargeKiGain.getValue(); }
        public void setChargeKiGain(float kiAmt) { chargeKiGain.setValue((Math.Clamp(kiAmt, 0, float.MaxValue))); }
        public void increaseChargeKiGain(float kiAmt) { chargeKiGain.increaseValue(kiAmt); }
        public void decreaseChargeKiGain(float kiAmt) { chargeKiGain.decreaseValue(kiAmt); }
        public float getBaseAttack() { return baseAttack.getValue(); }
        public void setBaseAttack(float amt) { baseAttack.setValue(amt); }
        public void increaseBaseAttack(float amt) { baseAttack.increaseValue(amt); }
        public void decreaseBaseAttack(float amt) { baseAttack.decreaseValue(amt); }
        public float getBaseDefense() { return baseDefense.getValue(); }
        public void setBaseDefense(float amt) { baseDefense.setValue(amt); }
        public void increaseBaseDefense(float amt) { baseDefense.increaseValue(amt); }
        public void decreaseBaseDefense(float amt) { baseDefense.decreaseValue(amt); }
        public float getBaseSpeed() { return baseSpeed.getValue(); }
        public void setBaseSpeed(float amt) { baseSpeed.setValue(amt); }
        public void increaseBaseSpeed(float amt) { baseSpeed.increaseValue(amt); }
        public void decreaseBaseSpeed(float amt) { baseSpeed.decreaseValue(amt); }
        public float sumKiDrain()
        {
            if(currentBuff == null)
            {
                return 0;
            }
            float sum = 0;
            if (!FormTree.nameToKiDrain.Keys.Contains(currentBuff))
            {
                return 0;
            }

            float currentBuffKiDrain = FormTree.nameToKiDrain[currentBuff];
            float currentBuffFormDivideDrain = getStat(currentBuff + "FormDivideDrain").getValue();
            sum += currentBuffKiDrain / currentBuffFormDivideDrain;
            foreach (string buff in stackedBuffs)
            {
                if (!FormTree.nameToKiDrain.Keys.Contains(buff))
                {
                    return 0;
                }
                float stackedBuffKiDrain = FormTree.nameToKiDrain[buff];
                float stackedBuffDivideDrain = getStat(buff + "FormDivideDrain").getValue();
                sum += stackedBuffKiDrain / stackedBuffDivideDrain;
            }
            return sum;
        }
        public float getKiPercentage() { return (float)getCurKi() / (float)getMaxKi(); }

        public void printAllStacked()
        {
            string toPrint = "";
            if (stackedBuffs.Count > 0)
            {
                foreach (string buff in stackedBuffs)
                {
                    toPrint += buff + " ";
                }
            }
            if (currentBuff != null)
            {
                toPrint += currentBuff;
            }
            if (!toPrint.Equals(""))
            {
                Main.NewText(toPrint);
                printToLog(toPrint);
            }
        }

        public override void PostUpdateBuffs()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if(modPlayer == null)
            {
                base.PostUpdateBuffs();
                return;
            }
            // printKaiokenModData();
            base.PreUpdateBuffs();
            float formDrain = sumKiDrain();
            float maxKiMulti = 1f;
            float maxKiAddition = 0;
            if (currentBuffID != -1)
            {
                //formDrain /= modPlayer.getStat(currentBuff + "FormDivideDrain").getValue();
                maxKiMulti *= modPlayer.getStat(currentBuff + "FormMultKi").getValue();
                foreach (var buff in stackedBuffs)
                {
                    maxKiMulti *= modPlayer.getStat(buff + "FormMultKi").getValue();
                }
            }
            else
            {
                maxKiMulti = 1f;
            }



            if (isTransformed)
            {
                //printAllStacked();
                float stackedFormsMulti = (stackedBuffs.Count() + 1);
                stackedFormsMulti *= getFormSpecialStackCost();
                decreaseKi(formDrain * stackedFormsMulti);
                maxKi.multiplier = maxKiMulti;
            }
            else
            {
                maxKi.multiplier = 1f;
            }

            if (getCurKi() <= 0)
            {
                if (currentBuffID != -1 || stackedBuffIDs.Count > 0)
                {
                    Main.NewText("Reverting to base");
                    printToLog("Reverting to base");
                }
                isTransformed = false;

                stackedBuffs.Clear();
                stackedBuffIDs.Clear();
                currentBuff = null;
                currentBuffID = -1;
            }
            float kiGainFrantic = getKiRegenFromFranticRegen();

            if (isCharging)
                increaseKi(getChargeKiGain());
            increaseKi(kiGain.getValue() + kiGainFrantic);


            if (ModLoader.TryGetMod("KaiokenMod", out var kaioken))
            {
                if (getKaiokenModData(kaioken).Formed && getTotalFormsCount() > 0)
                {
                    float strain = getKaiokenModStrainGainWithModifiers();
                    increaseKaiokenModStrain(kaioken, strain);
                }
                
            }

            if (!modPlayer.unlockedForms.Contains("SSJBE") && currentBuff.Contains("SSJB1") && modPlayer.getTotalSSJBGradesLevel() > 40)
            {
                FormStats.unlockAndTransform("SSJBE");
            }
            else if (!modPlayer.unlockedForms.Contains("TUI") && modPlayer.nameToStats["UI"].MultDefense.getValue() >= 3)
            {
                modPlayer.setUnlockCondition("TUI", true);
            }
            else if (!modPlayer.unlockedForms.Contains("UILB") && modPlayer.nameToStats["TUI"].MultDamage.getValue() >= 3)
            {
                modPlayer.setUnlockCondition("UILB", true);
            }
            else if (!modPlayer.unlockedForms.Contains("Beast") && modPlayer.nameToStats["PU"].MultKi.getValue() >= 1.5)
            {
                modPlayer.setUnlockCondition("Beast", true);
            }
            else if (!modPlayer.unlockedForms.Contains("UE") && modPlayer.getBaseAttack() >= 1.5)
            {
                modPlayer.setUnlockCondition("UE", true);
            }
        }

        public int getTotalFormsCount()
        {
            int count = 0;
            if(currentBuff != null && currentBuff != "baseForm")
            {
                count++;
            }
            count += stackedBuffs.Count;

            return count;
        }


        public void addBuff(int buffID)
        {
            if (!Player.HasBuff(buffID))
            {
                Main.NewText("Transforming into " +FormTree.IDToFormName(buffID));
                printToLog("Transforming into " + FormTree.IDToFormName(buffID));
            }
            Player.AddBuff(buffID, 3);
        }

        public override void PreUpdateBuffs()
        {
            setKiDrain(0);
            base.PreUpdateBuffs();
            if (isTransformed && currentBuffID != -1 && FormTree.nameToFormID.Values.Contains(currentBuffID))
            {
                cleanStacked();
                
                //Player.AddBuff(currentBuffID, 2);
                addBuff(currentBuffID);
                nameToStats[currentBuff].gainExperience(1 / 20f);
                gainExperience(1 / 20f);
                foreach (int stackedBuffID in stackedBuffIDs)
                {
                    if (FormTree.nameToFormID.Values.Contains(stackedBuffID))
                    {
                        addBuff(stackedBuffID);
                    }
                    else
                    {
                        if (!errorIDsList.Contains(stackedBuffID))
                        {
                            Main.NewText("Refusing to add a stackedBuffID that does not correlate to any form in FormTree.nameToFormID.Values " + stackedBuffID);
                            printToLog("Refusing to add a stackedBuffID that does not correlate to any form in FormTree.nameToFormID.Values " + stackedBuffID);
                            errorIDsList.Add(stackedBuffID);
                        }
                        
                    }
                    //Player.AddBuff(stackedBuffID, 2);
                }
            }
            else
            {
                kiDrain.setValue(0);
            }

            if(Player.shimmerWet && !unlockedForms.Contains("FLSSJ") && ( currentBuff == "SSJ1" || currentBuff == "SSJ1G4" ) && unlockedForms.Contains("Ikari"))
            {
                FormStats.unlockAndTransform("FLSSJ");
            }
            else if(Player.shimmerWet && !unlockedForms.Contains("SSJR1") && (currentBuff == "SSJB1" || currentBuff == "SSJB1G4"))
            {
                FormStats.unlockAndTransform("SSJR1");
            }
        }

        public override void UpdateEquips()
        {
            base.PostUpdate();
            Player.statDefense += (int)getBaseDefense();
            if (isCharging)
            {
                Player.statDefense += (int)getChargeDefense();
            }
            float currentEffectiveness = Player.DefenseEffectiveness.Value;
            float desiredEffectiveness = currentEffectiveness + getFormSpecialDR();
            Player.DefenseEffectiveness *= desiredEffectiveness / currentEffectiveness;
            //Player.DefenseEffectiveness; //* getFormSpecialDR();
                //.Value += getFormSpecialDR();
                //+= getFormSpecialDR();
        }
        public override void PostUpdate()
        {
            if (secondWindCooldown > 0)
            {
                secondWindCooldown--;
            }
            if(secondWindTime > 0)
            {
                secondWindTime--;
                if (secondWindTime == 0) {
                    secondWindCooldown = 3600;
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (KeybindSystem.ChargeKeybind.JustPressed) { isCharging = true; }
            if (KeybindSystem.ChargeKeybind.JustReleased) { isCharging = false; }

            if (KeybindSystem.AltFormKeybind.JustPressed) { isHoldingAlternateKey = true; }
            if (KeybindSystem.AltFormKeybind.JustReleased) { isHoldingAlternateKey = false; }

            if (KeybindSystem.TransformKeybind.JustPressed) { isHoldingTransformKey = true; }
            if (KeybindSystem.TransformKeybind.JustReleased) { isHoldingTransformKey = false; }

            if (KeybindSystem.RevertFormKeybind.JustPressed) { isHoldingRevertKey = true; }
            if (KeybindSystem.RevertFormKeybind.JustReleased) { isHoldingRevertKey = false; }


            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (KeybindSystem.TransformKeybind.JustPressed)
            {
                if (isHoldingAlternateKey && isCharging)
                {
                    if(FormTree.canAltAdvance(selectedForm)){
                        string altAdvancedForm = FormTree.altAdvanceForm(currentBuff);
                        if (altAdvancedForm != null && unlockedForms.Contains(altAdvancedForm) && !stackedBuffs.Contains(altAdvancedForm))
                        {
                            selectedForm = FormTree.altAdvanceForm(currentBuff);
                            selectedFormID = FormTree.formNameToID(selectedForm);

                            if (unlockedForms.Contains(selectedForm))
                            {
                                currentBuffID = selectedFormID;
                                currentBuff = selectedForm;
                                modSystem.MyFormsStatsUI.addStatButtons(modPlayer.getSelectedFormID());
                            }
                        }
                    }
                }
                if (isHoldingAlternateKey)
                {
                    if (FormTree.canAdvance(selectedForm))
                    {
                        string advancedForm = FormTree.advanceForm(currentBuff);
                        if (advancedForm != null && unlockedForms.Contains(advancedForm) && !stackedBuffs.Contains(advancedForm))
                        {
                            selectedForm = FormTree.advanceForm(selectedForm);
                            selectedFormID = FormTree.formNameToID(selectedForm);
                            if (unlockedForms.Contains(selectedForm))
                            {
                                currentBuffID = selectedFormID;
                                currentBuff = selectedForm;
                                modSystem.MyFormsStatsUI.addStatButtons(modPlayer.getSelectedFormID());
                            }
                        }

                    }
                    

                }
                else
                {
                    if (unlockedForms.Contains(selectedForm))
                    {
                        //if current buff is taken, selectedform is stackable, and selected form is not the same as current form, put it in the stacked forms
                        if (currentBuffID!=-1 && FormTree.nameToIsStackable[selectedForm] && currentBuffID != selectedFormID && !stackedBuffs.Contains(selectedForm))
                        {
                            stackedBuffs.Add(selectedForm);
                            stackedBuffIDs.Add(selectedFormID);
                            cleanStacked();
                        }
                        //if current buff is taken, selected form isn't stackable, selected form is not the same as current form, but current form is stackable, move current form to stacked forms, then set selected to current
                        else if (currentBuffID!=-1 && !FormTree.nameToIsStackable[selectedForm] && currentBuffID != selectedFormID && FormTree.nameToIsStackable[currentBuff] && !stackedBuffs.Contains(currentBuff))
                        {
                            
                            stackedBuffs.Add(currentBuff);
                            stackedBuffIDs.Add(currentBuffID);
                            cleanStacked();
                            currentBuff = selectedForm;
                            currentBuffID = selectedFormID;
                        }
                        else
                        {
                            currentBuffID = selectedFormID;
                            currentBuff = selectedForm;
                        }
                        modSystem.MyFormsStatsUI.addStatButtons(modPlayer.getSelectedFormID());
                    }
                    isTransformed = true;
                }
                
            }
            if (KeybindSystem.RevertFormKeybind.JustPressed)
            {
                if (isHoldingAlternateKey)
                {
                    if (FormTree.canReverse(selectedForm))
                    {
                        string reversedForm = FormTree.reverseForm(currentBuff);
                        if (reversedForm != null && unlockedForms.Contains(reversedForm) && !stackedBuffs.Contains(reversedForm))
                        {
                            selectedForm = FormTree.reverseForm(currentBuff);
                            selectedFormID = FormTree.formNameToID(selectedForm);
                            if (unlockedForms.Contains(selectedForm))
                            {
                                currentBuffID = selectedFormID;
                                currentBuff = selectedForm;
                            }
                        }
                            
                    }
                        
                }
                else
                {
                    currentBuff = null;
                    currentBuffID = -1;
                    isTransformed = false;
                    Main.NewText("Reverting to base");
                    printToLog("Reverting to base");
                    stackedBuffs.Clear();
                    stackedBuffIDs.Clear();
                }
            }

            if (KeybindSystem.ShowFormMenuKeybind.JustPressed)
            {
                ModContent.GetInstance<DragonballPichuUISystem>().MyFormsStatsUI.switchVisibility("choose");
            }
            if (KeybindSystem.ShowUnlockMenuKeybind.JustPressed)
            {
                ModContent.GetInstance<DragonballPichuUISystem>().MyFormsStatsUI.switchVisibility("unlock");
            }
            if (KeybindSystem.ShowStatsMenuKeybind.JustPressed)
            {
                ModContent.GetInstance<DragonballPichuUISystem>().MyFormsStatsUI.switchVisibility("stats");
                if (firstTimeOpenMenu)
                {
                    firstTimeOpenMenu = false;
                    modSystem.MyFormsStatsUI.removeFormStatButtons();
                }
            }
            if (KeybindSystem.AdminGiveFormPoint.JustPressed)
            {
                formPoints++;
            }
        }

        public void cleanStacked()
        {
            List<string> stackedNew = new List<string>();
            List<int> stackedNewIDs = new List<int>();
            foreach (var item in stackedBuffs)
            {
                if(item != null && !stackedNew.Contains(item) && !(currentBuff.Equals(item)))
                {
                    stackedNew.Add(item);
                } 
            }
            foreach (var item in stackedBuffIDs)
            {
                if (item != -1 && !stackedNewIDs.Contains(item) && !(currentBuffID.Equals(item)))
                {
                    stackedNewIDs.Add(item);
                }
            }
            stackedBuffIDs = stackedNewIDs;
            stackedBuffs = stackedNew;
        }

        public float expNeededToAdvanceLevel()
        {
            float expNeeded = ((float)Math.Pow(level + 1, 1.5)) * 100;
            return expNeeded;
        }
        public void levelUp()
        {

            float expNeeded = expNeededToAdvanceLevel();
            while (experience > expNeeded)
            {
                experience -= expNeeded;
                increaseLevel();
                expNeeded = expNeededToAdvanceLevel();
            }
        }
        public void gainExperience(float experience)
        {
            this.experience += experience;
            levelUp();
        }
        public void increaseLevel()
        {
            level += 1;
            points++;
            if (level % 5 == 0)
            {
                levelInAll++;
                increaseAll(1);
            }
        }
        public float getExperience()
        {
            return experience;
        }

        public void setLevel(int level)
        {
            this.level = level;
            this.points = level;
            levelInAll = (level / 5);
            increaseAll(levelInAll);
        }
        public int getLevel()
        {
            return level;
        }
        public int getPoints()
        {
            return points;
        }
        public Boolean usePoint()
        {
            if (points >= 1)
            {
                points--;
                return true;
            }
            return false;
        }
        public void respec()
        {
            points = level;
            setChargeKiGain(1);
            setKiGain(0);
            setMaxKi(100);
            setBaseAttack(1);
            setBaseDefense(0);
            setBaseSpeed(1);

            increaseAll(levelInAll);
        }
        public void increaseAll(int num)
        {
            increaseChargeKiGain(1f * num);
            increaseKiGain(.1f * num);
            increaseKiMax(100f * num);
            increaseBaseAttack(.01f * num);
            increaseBaseDefense(.25f * num);
            increaseBaseSpeed(.01f * num);
        }



        public float getHPPowerMult(string form)
        {
            float toReturn = 1;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "HP Power")
                {
                    float hpPercent = getHPPercentage();
                    string hpPower = buffSpecial[1];
                    List<string> hpPowerSplit = hpPower.Split("-").ToList();
                    float hpPowerMin = (float)Double.Parse(hpPowerSplit[0]);
                    float hpPowerMax = (float)Double.Parse(hpPowerSplit[1]);
                    float hpPowerRange = hpPowerMax - hpPowerMin;
                    toReturn = hpPowerMin + (hpPowerRange * hpPercent);
                    Math.Clamp(toReturn, hpPowerMin, hpPowerMax);
                }
            }
            return toReturn;
        }


        public float getKiPowerMult(string form)
        {
            float toReturn = 1;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Ki Power")
                {
                    float kiPercent = getKiPercentage();
                    string kiPower = buffSpecial[1];
                    List<string> kiPowerSplit = kiPower.Split("-").ToList();
                    float kiPowerMin = (float)Double.Parse(kiPowerSplit[0]);
                    float kiPowerMax = (float)Double.Parse(kiPowerSplit[1]);
                    float kiPowerRange = kiPowerMax - kiPowerMin;
                    toReturn = kiPowerMin + (kiPowerRange * kiPercent);
                    Math.Clamp(toReturn, kiPowerMin, kiPowerMax);
                }
            }
            return toReturn;
        }

        public float getHPRegenSpecial(string form)
        {
            float toReturn = 1;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Regen")
                {
                    string regen = buffSpecial[1];
                    toReturn = ((float)Double.Parse(regen)) * getStat(form + "FormSpecial").getValue();
                }
            }
            return toReturn;
        }

        

        public float getSpecialDefenseReductionMulti(string form)
        {
            float toReturn = 1;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "DR")
                {
                    string dr = buffSpecial[1];
                    toReturn = ((float)Double.Parse(dr)) * getStat(form + "FormSpecial").getValue();
                }
            }
            return toReturn;
        }

        public float getSpecialStackCostReductionMulti(string form)
        {
            float toReturn = 1;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Special Compatibility")
                {
                    string stackCost = buffSpecial[1];
                    toReturn = ((float)Double.Parse(stackCost)) * getStat(form + "FormSpecial").getValue();
                    toReturn = FormsStatsUI.invertFraction(toReturn);
                }
            }
            return toReturn;
        }

        public float getSpecialChargeDefense(string form)
        {
            float toReturn = 0;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Aura Defense")
                {
                    string chargeDefense = buffSpecial[1];
                    toReturn = ((float)Double.Parse(chargeDefense)) * getStat(form + "FormSpecial").getValue();
                }
            }
            return toReturn;
        }

        public float getFormSpecialDR()
        {
            float total = 0;
            total += getSpecialDefenseReductionMulti(currentBuff);


            foreach (string form in stackedBuffs)
            {
                total += getSpecialDefenseReductionMulti(form);
            }
            return total;

        }

        public float getFormSpecialStackCost()
        {
            float total = 1;
            total *= getSpecialStackCostReductionMulti(currentBuff);


            foreach (string form in stackedBuffs)
            {
                total *= getSpecialStackCostReductionMulti(form);
            }
            return total;

        }

        public float getFormSpecialKaiokenCost()
        {
            float total = 1;
            total *= getSpecialKaiokenCostReductionMulti(currentBuff);


            foreach (string form in stackedBuffs)
            {
                total *= getSpecialKaiokenCostReductionMulti(form);
            }
            return total;

        }

        public float getSpecialKaiokenCostReductionMulti(string form)
        {
            float toReturn = 1;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Kaio-Efficient")
                {
                    string stackCost = buffSpecial[1];
                    toReturn = ((float)Double.Parse(stackCost)) * getStat(form + "FormSpecial").getValue();
                    toReturn = FormsStatsUI.invertFraction(toReturn);
                }
            }
            return toReturn;
        }

        public float getChargeDefense()
        {
            float total = 0;
            total += getSpecialChargeDefense(currentBuff);


            foreach (string form in stackedBuffs)
            {
                total += getSpecialChargeDefense(form);
            }
            return total;
        }


        public float getFormSpecialDamageMult()
        {
            float totalMult = 1;
            totalMult *= getHPPowerMult(currentBuff);
            totalMult *= getKiPowerMult(currentBuff);

            foreach (string form in stackedBuffs)
            {
                totalMult *= getHPPowerMult(form);
                totalMult *= getKiPowerMult(form);
            }
            return totalMult; 
        }

        public float getFormSpecialRegen()
        {
            float total = 0;
            total += getHPRegenSpecial(currentBuff);
            

            foreach (string form in stackedBuffs)
            {
                total += getHPRegenSpecial(form);
            }
            return total;
            
        }

        public int getFormDodgeTier(string form)
        {
            int toReturn = 0;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Dodge")
                {
                    string formDodgeTier = buffSpecial[1];
                    toReturn = (int.Parse(formDodgeTier));
                }
            }
            return toReturn;
        }

        public int getHighestFormDodgeTier()
        {
            int highestDodgeTier = 0;
            int currentDodgeTier = getFormDodgeTier(currentBuff);
            if (currentDodgeTier > highestDodgeTier)
            {
                highestDodgeTier = currentDodgeTier;
            }


            foreach (string form in stackedBuffs)
            {
                int stackedDodgeTier = getFormDodgeTier(form);
                if (stackedDodgeTier > highestDodgeTier)
                {
                    highestDodgeTier = stackedDodgeTier;
                }
            }
            return highestDodgeTier;

        }

        public float getTotalSpecialDodgeCost()
        {
            float total = 1;
            total *= getSpecialDodgeCost(currentBuff);


            foreach (string form in stackedBuffs)
            {
                total *= getSpecialDodgeCost(form);
            }
            return total;
        }

        public float getSpecialDodgeCost(string form)
        {
            float toReturn = 1;
            if (form != null && nameToStats.ContainsKey(form))
            {
                List<string> buffSpecial = FormTree.getSpecial(form);
                if (buffSpecial[0] == "Dodge")
                {

                    toReturn = nameToStats[form].Special.getValue();
                }
            }
            return toReturn;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["experience"] = experience;
            tag["level"] = level;
            tag["unlockedForms"] = unlockedForms;
            tag["enemyCompendium"] = enemyCompendium;
            tag["formPoints"] = formPoints;
            tag["spendFormPoints"] = spendFormPoints;
            tag["unlockConditionsKeys"] = formToUnlockCondition.Keys.ToList();
            tag["unlockConditionsValues"] = formToUnlockCondition.Values.ToList();
            List<string> forms = new List<string>() { "FSSJ","SSJ1","SSJ1G2","SSJ1G3","SSJ1G4","SSJ2","SSJ3","SSJRage","SSJ4","SSJ4LB","SSJ5","SSJ5G2","SSJ5G3","SSJ5G4","SSJ6","SSJ7","FLSSJ","Ikari","LSSJ1","LSSJ2","LSSJ3","LSSJ4","LSSJ4LB","LSSJ5","LSSJ6","LSSJ7","SSJG","LSSJB","FSSJB","SSJB1","SSJB1G2","SSJB1G3","SSJB1G4","SSJB2","SSJB3","SSJBE","SSJR1","SSJR1G2","SSJR1G3","SSJR1G4","SSJR2","SSJR3","Divine","DR","Evil","Rampaging","Berserk","PU","Beast","UE","UI","UILB","TUI"
};
            foreach (string form in forms)
            {
                if (nameToStats.ContainsKey(form))
                {
                    tag[(form + "level")] = nameToStats[form].getLevel();
                    tag[(form + "experience")] = nameToStats[form].getExperience();
                    tag[(form + "specialname")] = nameToStats[form].specialEffectValue[0];
                    tag[(form + "specialvalue")] = nameToStats[form].specialEffectValue[1];

                }

            }
        }

        public float getHPPercentage()
        {
            return Player.statLife / Player.statLifeMax2;
        }

        public override void LoadData(TagCompound tag)
        {
            if (!stats.ContainsKey("maxKi"))
            {
                stats.Add("maxKi", maxKi);
                stats.Add("kiGain", kiGain);
                stats.Add("kiDrain", kiDrain);
                stats.Add("chargeKiGain", chargeKiGain);
                stats.Add("baseAttack", baseAttack);
                stats.Add("baseDefense", baseDefense);
                stats.Add("baseSpeed", baseSpeed);
                
                

            }
            if (nameToStats.Count == 0)
            {
                FormTree.forms.ForEach(form =>
                {
                    nameToStats.Add(form, new FormStats(form));
                });
            }
            if (tag.ContainsKey("experience"))
            {
                experience = tag.GetFloat("experience");
            }
            if (tag.ContainsKey("level"))
            {
                setLevel(tag.GetInt("level"));
            }
            if (tag.ContainsKey("unlockedForms"))
            {
                unlockLoaded = tag.Get<List<String>>("unlockedForms");
            }
            if (tag.ContainsKey("enemyCompendium"))
            {
                enemyCompendium = tag.Get<List<String>>("enemyCompendium");
            }
            if (tag.ContainsKey("formPoints"))
            {
                formPoints = tag.GetInt("formPoints");
            }
            if (tag.ContainsKey("spendFormPoints"))
            {
                spendFormPoints = tag.GetInt("spendFormPoints");
            }

            if(tag.ContainsKey("unlockConditionsKeys") && tag.ContainsKey("unlockConditionsValues"))
            {
                var names = tag.Get<List<string>>("unlockConditionsKeys");
                var values = tag.Get<List<bool>>("unlockConditionsValues");
                formToUnlockCondition = names.Zip(values, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
            }

            List<string> forms = new List<string>() { "FSSJ","SSJ1","SSJ1G2","SSJ1G3","SSJ1G4","SSJ2","SSJ3","SSJRage","SSJ4","SSJ4LB","SSJ5","SSJ5G2","SSJ5G3","SSJ5G4","SSJ6","SSJ7","FLSSJ","Ikari","LSSJ1","LSSJ2","LSSJ3","LSSJ4","LSSJ4LB","LSSJ5","LSSJ6","LSSJ7","SSJG","LSSJB","FSSJB","SSJB1","SSJB1G2","SSJB1G3","SSJB1G4","SSJB2","SSJB3","SSJBE","SSJR1","SSJR1G2","SSJR1G3","SSJR1G4","SSJR2","SSJR3","Divine","DR","Evil","Rampaging","Berserk","PU","Beast","UE","UI","UILB","TUI"
};
            foreach (string form in forms)
            {
                if (nameToStats.ContainsKey(form))
                {
                    if (tag.ContainsKey((form + "level")))
                    {
                        nameToStats[form].setLevel(tag.Get<int>((form + "level")));
                    }
                    if (tag.ContainsKey((form + "experience")))
                    {
                        nameToStats[form].gainExperience(tag.Get<float>((form + "experience")));
                    }
                    if (tag.ContainsKey((form + "specialname")))
                    {
                        nameToStats[form].specialEffectValue[0] = tag.Get<string>((form+"specialname"));
                    }
                    if (tag.ContainsKey((form + "specialvalue")))
                    {
                        nameToStats[form].specialEffectValue[1] = tag.Get<string>((form + "specialvalue"));
                    }
                }

            }

        }

        /*public override void SaveData(TagCompound tag)
        {
            tag["experience"] =  experience;
            tag["level"] = level;
            tag["unlockedForms"] = unlockedForms;
            tag["enemyCompendium"] = enemyCompendium;
            tag["formPoints"] = formPoints;
            foreach (string form in FormTree.forms)
            {
                tag[form + "experience"] = nameToStats[form].getExperience();
                tag[form + "level"] = nameToStats[form].getLevel();
            }
            //base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            List<string> toLoad = new List<string>()
            {
                "experience","level","unlockedForms","enemyCompedium","formPoints"
            };
            foreach (string item in toLoad)
            {
                if (tag.ContainsKey(item))
                {
                    loadBaseItem(tag, item);
                }
            }
            foreach (string form in FormTree.forms)
            {
                if(tag.ContainsKey(form+"experience") && tag.ContainsKey(form + "level"))
                {
                    loadForm(tag, form);
                }
            }
            base.LoadData(tag);
        }*/


    }
}
