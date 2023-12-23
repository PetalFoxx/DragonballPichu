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

namespace DragonballPichu
{
    public class DragonballPichuPlayer : ModPlayer
    {
        public Boolean FSSJUnlockCondition = true;
        public Boolean SSJ1UnlockCondition = true;
        public Boolean SSJ1G2UnlockCondition = true;
        public Boolean SSJ1G3UnlockCondition = true;
        public Boolean SSJ1G4UnlockCondition = true;
        public Boolean SSJ2UnlockCondition = true;
        public Boolean SSJ3UnlockCondition = true;
        public Boolean SSJRageUnlockCondition = true;
        public Boolean SSJ4UnlockCondition = true;
        public Boolean SSJ4LBUnlockCondition = true;
        public Boolean SSJ5UnlockCondition = true;
        public Boolean SSJ5G2UnlockCondition = true;
        public Boolean SSJ5G3UnlockCondition = true;
        public Boolean SSJ5G4UnlockCondition = true;
        public Boolean SSJ6UnlockCondition = true;
        public Boolean SSJ7UnlockCondition = true;
        public Boolean FLSSJUnlockCondition = true;
        public Boolean IkariUnlockCondition = true;
        public Boolean LSSJ1UnlockCondition = true;
        public Boolean LSSJ2UnlockCondition = true;
        public Boolean LSSJ3UnlockCondition = true;
        public Boolean LSSJ4UnlockCondition = true;
        public Boolean LSSJ4LBUnlockCondition = true;
        public Boolean LSSJ5UnlockCondition = true;
        public Boolean LSSJ6UnlockCondition = true;
        public Boolean LSSJ7UnlockCondition = true;
        public Boolean SSJGUnlockCondition = true;
        public Boolean LSSJBUnlockCondition = true;
        public Boolean FSSJBUnlockCondition = true;
        public Boolean SSJB1UnlockCondition = true;
        public Boolean SSJB1G2UnlockCondition = true;
        public Boolean SSJB1G3UnlockCondition = true;
        public Boolean SSJB1G4UnlockCondition = true;
        public Boolean SSJB2UnlockCondition = true;
        public Boolean SSJB3UnlockCondition = true;
        public Boolean SSJBEUnlockCondition = true;
        public Boolean SSJR1UnlockCondition = true;
        public Boolean SSJR1G2UnlockCondition = true;
        public Boolean SSJR1G3UnlockCondition = true;
        public Boolean SSJR1G4UnlockCondition = true;
        public Boolean SSJR2UnlockCondition = true;
        public Boolean SSJR3UnlockCondition = true;
        public Boolean DivineUnlockCondition = true;
        public Boolean DRUnlockCondition = true;
        public Boolean EvilUnlockCondition = true;
        public Boolean RampagingUnlockCondition = true;
        public Boolean BerserkUnlockCondition = true;
        public Boolean PUUnlockCondition = true;
        public Boolean BeastUnlockCondition = true;
        public Boolean UEUnlockCondition = true;
        public Boolean UIUnlockCondition = true;
        public Boolean TUIUnlockCondition = true;
        public Boolean UILBUnlockCondition = true;

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

        public string currentBuff = "";
        public int currentBuffID = -1;

        public List<string> stackedBuffs = new List<string>();
        public List<int> stackedBuffIDs = new List<int>();

        public List<string> unlockedForms = new List<string>();
        public List<string> unlockLoaded = new List<string>();

        
        

        public Dictionary<string, int> formToUnlockPoints = new Dictionary<string, int>()
        {
            { "FSSJ", 1 },
            { "SSJ1", 1 },
            { "SSJ1G2", 1 },
            { "FSSJB", 1 },
            { "SSJ1G3", 1 },
            { "SSJ1G4", 1 },
            { "SSJ2", 1 },
            { "SSJ3", 1 },
            { "SSJRage", 1 },
            { "SSJ4", 1 },
            { "SSJ4LB", 1 },
            { "SSJ5", 1 },
            { "SSJ5G2", 1 },
            { "SSJ5G3", 1 },
            { "SSJ5G4", 1 },
            { "SSJ6", 1 },
            { "SSJ7", 1 },
            { "FLSSJ", 1 },
            { "Ikari", 1 },
            { "LSSJ1", 1 },
            { "LSSJ2", 1 },
            { "LSSJ3", 1 },
            { "LSSJ4", 1 },
            { "LSSJ4LB", 1 },
            { "LSSJ5", 1 },
            { "LSSJ6", 1 },
            { "LSSJ7", 1 },
            { "SSJG", 1 },
            { "LSSJB", 1 },
            { "SSJB1", 1 },
            { "SSJB1G2", 1 },
            { "SSJB1G3", 1 },
            { "SSJB1G4", 1 },
            { "SSJB2", 1 },
            { "SSJB3", 1 },
            { "SSJBE", 1 },
            { "SSJR1", 1 },
            { "SSJR1G2", 1 },
            { "SSJR1G3", 1 },
            { "SSJR1G4", 1 },
            { "SSJR2", 1 },
            { "SSJR3", 1 },
            { "Divine", 1 },
            { "DR", 1 },
            { "Evil", 1 },
            { "Rampaging", 1 },
            { "Berserk", 1 },
            { "PU", 1 },
            { "Beast", 1 },
            { "UE", 1 },
            { "UI", 1 },
            { "TUI", 1 },
            { "UILB", 1 }
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

            Dictionary<string, Boolean> formToUnlockCondition = new Dictionary<string, Boolean>()
        {
            { "FSSJ", FSSJUnlockCondition },
            { "SSJ1", SSJ1UnlockCondition },
            { "SSJ1G2", SSJ1G2UnlockCondition },
            { "FSSJB", FSSJBUnlockCondition },
            { "SSJ1G3", SSJ1G3UnlockCondition },
            { "SSJ1G4", SSJ1G4UnlockCondition },
            { "SSJ2", SSJ2UnlockCondition },
            { "SSJ3", SSJ3UnlockCondition },
            { "SSJRage", SSJRageUnlockCondition },
            { "SSJ4", SSJ4UnlockCondition },
            { "SSJ4LB", SSJ4LBUnlockCondition },
            { "SSJ5", SSJ5UnlockCondition },
            { "SSJ5G2", SSJ5G2UnlockCondition },
            { "SSJ5G3", SSJ5G3UnlockCondition },
            { "SSJ5G4", SSJ5G4UnlockCondition },
            { "SSJ6", SSJ6UnlockCondition },
            { "SSJ7", SSJ7UnlockCondition },
            { "FLSSJ", FLSSJUnlockCondition },
            { "Ikari", IkariUnlockCondition },
            { "LSSJ1", LSSJ1UnlockCondition },
            { "LSSJ2", LSSJ2UnlockCondition },
            { "LSSJ3", LSSJ3UnlockCondition },
            { "LSSJ4", LSSJ4UnlockCondition },
            { "LSSJ4LB", LSSJ4LBUnlockCondition },
            { "LSSJ5", LSSJ5UnlockCondition },
            { "LSSJ6", LSSJ6UnlockCondition },
            { "LSSJ7", LSSJ7UnlockCondition },
            { "SSJG", SSJGUnlockCondition },
            { "LSSJB", LSSJBUnlockCondition },
            { "SSJB1", SSJB1UnlockCondition },
            { "SSJB1G2", SSJB1G2UnlockCondition },
            { "SSJB1G3", SSJB1G3UnlockCondition },
            { "SSJB1G4", SSJB1G4UnlockCondition },
            { "SSJB2", SSJB2UnlockCondition },
            { "SSJB3", SSJB3UnlockCondition },
            { "SSJBE", SSJBEUnlockCondition },
            { "SSJR1", SSJR1UnlockCondition },
            { "SSJR1G2", SSJR1G2UnlockCondition },
            { "SSJR1G3", SSJR1G3UnlockCondition },
            { "SSJR1G4", SSJR1G4UnlockCondition },
            { "SSJR2", SSJR2UnlockCondition },
            { "SSJR3", SSJR3UnlockCondition },
            { "Divine", DivineUnlockCondition },
            { "DR", DRUnlockCondition },
            { "Evil", EvilUnlockCondition },
            { "Rampaging", RampagingUnlockCondition },
            { "Berserk", BerserkUnlockCondition },
            { "PU", PUUnlockCondition },
            { "Beast", BeastUnlockCondition },
            { "UE", UEUnlockCondition },
            { "UI", UIUnlockCondition },
            { "TUI", TUIUnlockCondition },
            { "UILB", UILBUnlockCondition }
        };
            if (getFormPoints() >= formToUnlockPoints[formName] && formToUnlockCondition[formName])
            {
                formPoints-= formToUnlockPoints[formName];
                spendFormPoints += formToUnlockPoints[formName];
                return true;
            }
            return false;
        }
        public void gainFormPoints(NPC npc)
        {
            formPoints++;
        }

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

        /*public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if(modifiers.FinalDamage > Player.statLife)
            {

            }
            base.ModifyHitByNPC(npc, ref modifiers);
        }*/

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {

            Entity entity;
            damageSource.TryGetCausingEntity(out entity);
            if (entity is NPC && ((NPC)entity).boss)
            {
                if (!unlockedForms.Contains("FSSJ"))
                {
                    DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
                    modSystem.MyFormsStatsUI.unlockForm("FSSJ");
                    currentBuff = "FSSJ";
                    currentBuffID = FormTree.formNameToID(currentBuff);
                    isTransformed = true;
                    Player.statLife = Player.statLifeMax2 / 2;
                    return false;
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
            base.OnHitByNPC(npc, hurtInfo);
        }

        public Boolean tryAddNewBossToCompendium(NPC npc)
        {
            if (enemyCompendium.Contains(npc.TypeName) || !npc.boss)
            {
                return false;
            }

            enemyCompendium.Add(npc.TypeName);
            return true;
        }

        public string getSelectedForm()
        {
            return selectedForm;
            
        }
        public void setSelectedForm(string form)
        {
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

        public override void PostUpdateBuffs()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
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

        }


        public override void PreUpdateBuffs()
        {
            setKiDrain(0);
            base.PreUpdateBuffs();
            if (isTransformed && currentBuffID != -1)
            {
                cleanStacked();
                Player.AddBuff(currentBuffID, 2);
                nameToStats[currentBuff].gainExperience(1 / 20f);
                gainExperience(1 / 20f);
                foreach (int stackedBuffID in stackedBuffIDs)
                {
                    Player.AddBuff(stackedBuffID, 2);
                }
            }
            else
            {
                kiDrain.setValue(0);
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
