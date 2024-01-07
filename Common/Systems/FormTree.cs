using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DragonballPichu.Content.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace DragonballPichu.Common.Systems
{
    public class FormTree
    {
        public static Dictionary<string, Dictionary<string, string>> formAdvRevs = new Dictionary<string, Dictionary<string, string>>()
        {
        { "baseForm" , makeFormAdvRev("FSSJ", "Ikari", "baseForm")},
        { "FSSJ" ,     makeFormAdvRev("SSJ1", "LFSSJ", "baseForm")},
        { "SSJ1" ,     makeFormAdvRev("SSJ1G4", "SSJ1G2", "FSSJ")},
        { "SSJ1G2" ,   makeFormAdvRev("SSJ1G3", "SSJRage", "SSJ1")},
        { "SSJ1G3" ,   makeFormAdvRev("SSJ1G4", "SSJRage", "SSJ1G2")},
        { "SSJ1G4" ,   makeFormAdvRev("SSJ2","SSJG","SSJ1")},
        { "SSJ2" ,     makeFormAdvRev("SSJ3","SSJRage","SSJ1G4")},
        { "SSJ3" ,     makeFormAdvRev("SSJG","SSJ4","SSJ2")},
        { "SSJRage" ,  makeFormAdvRev(null,null,"SSJ2")},
        { "SSJ4" ,     makeFormAdvRev("SSJ5","SSJ4LB","SSJ3")},
        { "SSJ4LB" ,   makeFormAdvRev("SSJ5","SSJ5","SSJ4")},
        { "SSJ5" ,     makeFormAdvRev("SSJ5G4","SSJ5G2","SSJ4LB")},
        { "SSJ5G2" ,   makeFormAdvRev("SSJ5G3","SSJ5G3","SSJ5")},
        { "SSJ5G3" ,   makeFormAdvRev("SSJ5G4","SSJ5G4","SSJ5G2")},
        { "SSJ5G4" ,   makeFormAdvRev("SSJ6","SSJ6","SSJ5")},
        { "SSJ6" ,     makeFormAdvRev("SSJ7","SSJ7","SSJ5G4")},
        { "SSJ7" ,     makeFormAdvRev(null,null,"SSJ6")},
        { "FLSSJ" ,    makeFormAdvRev("LSSJ1","LSSJ1","Ikari")},
        { "Ikari" ,    makeFormAdvRev("LSSJ1","FLSSJ","baseForm")},
        { "LSSJ1" ,    makeFormAdvRev("LSSJ2","LSSJ5","FLSSJ")},
        { "LSSJ2" ,    makeFormAdvRev("LSSJ3","LSSJ6","LSSJ1")},
        { "LSSJ3" ,    makeFormAdvRev("LSSJ4","SSJG","LSSJ2")},
        { "LSSJ4" ,    makeFormAdvRev("LSSJ5","LSSJ4LB","LSSJ3")},
        { "LSSJ4LB" ,  makeFormAdvRev("LSSJ5","LSSJ7","LSSJ4")},
        { "LSSJ5" ,    makeFormAdvRev("LSSJ6","LSSJ7","LSSJ4LB")},
        { "LSSJ6" ,    makeFormAdvRev("LSSJ7","LSSJ7","LSSJ5")},
        { "LSSJ7" ,    makeFormAdvRev(null,null,"LSSJ6")},
        { "SSJG" ,     makeFormAdvRev("SSJB1","LSSJB","baseForm")},
        { "LSSJB" ,    makeFormAdvRev(null,null,"SSJG")},
        { "FSSJB" ,    makeFormAdvRev("SSJB1","SSJB1","SSJG")},
        { "SSJB1" ,    makeFormAdvRev("SSJB1G4", "SSJB1G2", "SSJG")},
        { "SSJB1G2" ,  makeFormAdvRev("SSJB1G3", "SSJBE", "SSJB1")},
        { "SSJB1G3" ,  makeFormAdvRev("SSJB1G4", "SSJBE", "SSJB1G2")},
        { "SSJB1G4" ,  makeFormAdvRev("SSJB2","SSJBE","SSJB1")},
        { "SSJB2" ,    makeFormAdvRev("SSJB3","SSJB3","SSJB1G4")},
        { "SSJB3" ,    makeFormAdvRev(null, null, "SSJB2")},
        { "SSJBE" ,    makeFormAdvRev(null, null, "SSJB1G3")},
        { "SSJR1" ,    makeFormAdvRev("SSJR1G4","SSJR1G2","SSJG")},
        { "SSJR1G2" ,  makeFormAdvRev("SSJB1G3","DR","SSJR1")},
        { "SSJR1G3" ,  makeFormAdvRev("SSJR1G4","DR","SSJR1G2")},
        { "SSJR1G4" ,  makeFormAdvRev("SSJR2","Divine","SSJR1")},
        { "SSJR2" ,    makeFormAdvRev("SSJR3","DR","SSJR1G4")},
        { "SSJR3" ,    makeFormAdvRev("DR","DR","SSJR2")},
        { "Divine" ,   makeFormAdvRev("DR","DR","SSJR1G4")},
        { "DR" ,       makeFormAdvRev(null,null,"Divine")},
        { "Evil" ,     makeFormAdvRev("Rampaging","Berserk","baseForm")},
        { "Rampaging" ,makeFormAdvRev("Berserk","Berserk","Evil")},
        { "Berserk" ,  makeFormAdvRev(null, null,"Rampaging")},
        { "PU" ,       makeFormAdvRev("Beast","Beast","baseForm")},
        { "Beast" ,    makeFormAdvRev(null,null,"PU")},
        { "UE" ,       makeFormAdvRev(null,null,"baseForm")},
        { "UI" ,       makeFormAdvRev("TUI", "UILB", "baseForm")},
        { "TUI" ,      makeFormAdvRev("UILB", "UILB", "UI")},
        { "UILB" ,     makeFormAdvRev(null,null,"UI")}
        };

        public static List<string> forms = new List<string>() { "FSSJ","SSJ1","SSJ1G2","SSJ1G3","SSJ1G4","SSJ2","SSJ3","SSJRage","SSJ4","SSJ4LB","SSJ5","SSJ5G2","SSJ5G3","SSJ5G4","SSJ6","SSJ7","FLSSJ","Ikari","LSSJ1","LSSJ2","LSSJ3","LSSJ4","LSSJ4LB","LSSJ5","LSSJ6","LSSJ7","SSJG","LSSJB","FSSJB","SSJB1","SSJB1G2","SSJB1G3","SSJB1G4","SSJB2","SSJB3","SSJBE","SSJR1","SSJR1G2","SSJR1G3","SSJR1G4","SSJR2","SSJR3","Divine","DR","Evil","Rampaging","Berserk","PU","Beast","UE","UI","UILB","TUI"
};
        public static Dictionary<String, Dictionary<String, String>> nameToDict = new Dictionary<string, Dictionary<string, string>>()
        {
            { "baseForm", formAdvRevs["baseForm"] },
            { "FSSJ", formAdvRevs["FSSJ"] },
            { "SSJ1", formAdvRevs["SSJ1"] },
            { "SSJ1G2", formAdvRevs["SSJ1G2"] },
            { "FSSJB", formAdvRevs["FSSJB"] },
            { "SSJ1G3", formAdvRevs["SSJ1G3"] },
            { "SSJ1G4", formAdvRevs["SSJ1G4"] },
            { "SSJ2", formAdvRevs["SSJ2"] },
            { "SSJ3", formAdvRevs["SSJ3"] },
            { "SSJRage", formAdvRevs["SSJRage"] },
            { "SSJ4", formAdvRevs["SSJ4"] },
            { "SSJ4LB", formAdvRevs["SSJ4LB"] },
            { "SSJ5", formAdvRevs["SSJ5"] },
            { "SSJ5G2", formAdvRevs["SSJ5G2"] },
            { "SSJ5G3", formAdvRevs["SSJ5G3"] },
            { "SSJ5G4", formAdvRevs["SSJ5G4"] },
            { "SSJ6", formAdvRevs["SSJ6"] },
            { "SSJ7", formAdvRevs["SSJ7"] },
            { "FLSSJ", formAdvRevs["FLSSJ"] },
            { "Ikari", formAdvRevs["Ikari"] },
            { "LSSJ1", formAdvRevs["LSSJ1"] },
            { "LSSJ2", formAdvRevs["LSSJ2"] },
            { "LSSJ3", formAdvRevs["LSSJ3"] },
            { "LSSJ4", formAdvRevs["LSSJ4"] },
            { "LSSJ4LB", formAdvRevs["LSSJ4LB"] },
            { "LSSJ5", formAdvRevs["LSSJ5"] },
            { "LSSJ6", formAdvRevs["LSSJ6"] },
            { "LSSJ7", formAdvRevs["LSSJ7"] },
            { "SSJG", formAdvRevs["SSJG"] },
            { "LSSJB", formAdvRevs["LSSJB"] },
            { "SSJB1", formAdvRevs["SSJB1"] },
            { "SSJB1G2", formAdvRevs["SSJB1G2"] },
            { "SSJB1G3", formAdvRevs["SSJB1G3"] },
            { "SSJB1G4", formAdvRevs["SSJB1G4"] },
            { "SSJB2", formAdvRevs["SSJB2"] },
            { "SSJB3", formAdvRevs["SSJB3"] },
            { "SSJBE", formAdvRevs["SSJBE"] },
            { "SSJR1", formAdvRevs["SSJR1"] },
            { "SSJR1G2", formAdvRevs["SSJR1G2"] },
            { "SSJR1G3", formAdvRevs["SSJR1G3"] },
            { "SSJR1G4", formAdvRevs["SSJR1G4"] },
            { "SSJR2", formAdvRevs["SSJR2"] },
            { "SSJR3", formAdvRevs["SSJR3"] },
            { "Divine", formAdvRevs["Divine"] },
            { "DR", formAdvRevs["DR"] },
            { "Evil", formAdvRevs["Evil"] },
            { "Rampaging", formAdvRevs["Rampaging"] },
            { "Berserk", formAdvRevs["Berserk"] },
            { "PU", formAdvRevs["PU"] },
            { "Beast", formAdvRevs["Beast"] },
            { "UE", formAdvRevs["UE"] },
            { "UI", formAdvRevs["UI"] },
            { "TUI", formAdvRevs["TUI"] },
            { "UILB", formAdvRevs["UILB"] }
        };
        public static Dictionary<Dictionary<String, String>, String> dictToName = new Dictionary<Dictionary<string, string>, string>()
        {
            { formAdvRevs["baseForm"], "baseForm" },
            { formAdvRevs["FSSJ"], "FSSJ" },
            { formAdvRevs["SSJ1"], "SSJ1" },
            { formAdvRevs["SSJ1G2"], "SSJ1G2" },
            { formAdvRevs["FSSJB"], "FSSJB" },
            { formAdvRevs["SSJ1G3"], "SSJ1G3" },
            { formAdvRevs["SSJ1G4"], "SSJ1G4" },
            { formAdvRevs["SSJ2"], "SSJ2" },
            { formAdvRevs["SSJ3"], "SSJ3" },
            { formAdvRevs["SSJRage"], "SSJRage" },
            { formAdvRevs["SSJ4"], "SSJ4" },
            { formAdvRevs["SSJ4LB"], "SSJ4LB" },
            { formAdvRevs["SSJ5"], "SSJ5" },
            { formAdvRevs["SSJ5G2"], "SSJ5G2" },
            { formAdvRevs["SSJ5G3"], "SSJ5G3" },
            { formAdvRevs["SSJ5G4"], "SSJ5G4" },
            { formAdvRevs["SSJ6"], "SSJ6" },
            { formAdvRevs["SSJ7"], "SSJ7" },
            { formAdvRevs["FLSSJ"], "FLSSJ" },
            { formAdvRevs["Ikari"], "Ikari" },
            { formAdvRevs["LSSJ1"], "LSSJ1" },
            { formAdvRevs["LSSJ2"], "LSSJ2" },
            { formAdvRevs["LSSJ3"], "LSSJ3" },
            { formAdvRevs["LSSJ4"], "LSSJ4" },
            { formAdvRevs["LSSJ4LB"], "LSSJ4LB" },
            { formAdvRevs["LSSJ5"], "LSSJ5" },
            { formAdvRevs["LSSJ6"], "LSSJ6" },
            { formAdvRevs["LSSJ7"], "LSSJ7" },
            { formAdvRevs["SSJG"], "SSJG" },
            { formAdvRevs["LSSJB"], "LSSJB" },
            { formAdvRevs["SSJB1"], "SSJB1" },
            { formAdvRevs["SSJB1G2"], "SSJB1G2" },
            { formAdvRevs["SSJB1G3"], "SSJB1G3" },
            { formAdvRevs["SSJB1G4"], "SSJB1G4" },
            { formAdvRevs["SSJB2"], "SSJB2" },
            { formAdvRevs["SSJB3"], "SSJB3" },
            { formAdvRevs["SSJBE"], "SSJBE" },
            { formAdvRevs["SSJR1"], "SSJR1" },
            { formAdvRevs["SSJR1G2"], "SSJR1G2" },
            { formAdvRevs["SSJR1G3"], "SSJR1G3" },
            { formAdvRevs["SSJR1G4"], "SSJR1G4" },
            { formAdvRevs["SSJR2"], "SSJR2" },
            { formAdvRevs["SSJR3"], "SSJR3" },
            { formAdvRevs["Divine"], "Divine" },
            { formAdvRevs["DR"], "DR" },
            { formAdvRevs["Evil"], "Evil" },
            { formAdvRevs["Rampaging"], "Rampaging" },
            { formAdvRevs["Berserk"], "Berserk" },
            { formAdvRevs["PU"], "PU" },
            { formAdvRevs["Beast"], "Beast" },
            { formAdvRevs["UE"], "UE" },
            { formAdvRevs["UI"], "UI" },
            { formAdvRevs["TUI"], "TUI" },
            { formAdvRevs["UILB"], "UILB" }
        };

        public static Dictionary<String, int> nameToFormID = new Dictionary<string, int>()
        {
            { "baseForm", -1 },            
            { "FSSJ", ModContent.BuffType<FSSJBuff>() },
            { "SSJ1", ModContent.BuffType<SSJ1Buff>() },
            { "SSJ1G2", ModContent.BuffType<SSJ1G2Buff>() },
            { "FSSJB", ModContent.BuffType<FSSJBBuff>() },
            { "SSJ1G3", ModContent.BuffType<SSJ1G3Buff>() },
            { "SSJ1G4", ModContent.BuffType<SSJ1G4Buff>() },
            { "SSJ2", ModContent.BuffType<SSJ2Buff>() },
            { "SSJ3", ModContent.BuffType<SSJ3Buff>() },
            { "SSJRage", ModContent.BuffType<SSJRageBuff>() },
            { "SSJ4", ModContent.BuffType<SSJ4Buff>() },
            { "SSJ4LB", ModContent.BuffType<SSJ4LBBuff>() },
            { "SSJ5", ModContent.BuffType<SSJ5Buff>() },
            { "SSJ5G2", ModContent.BuffType<SSJ5G2Buff>() },
            { "SSJ5G3", ModContent.BuffType<SSJ5G3Buff>() },
            { "SSJ5G4", ModContent.BuffType<SSJ5G4Buff>() },
            { "SSJ6", ModContent.BuffType<SSJ6Buff>() },
            { "SSJ7", ModContent.BuffType<SSJ7Buff>() },
            { "FLSSJ", ModContent.BuffType<FLSSJBuff>() },
            { "Ikari", ModContent.BuffType<IkariBuff>() },
            { "LSSJ1", ModContent.BuffType<LSSJ1Buff>() },
            { "LSSJ2", ModContent.BuffType<LSSJ2Buff>() },
            { "LSSJ3", ModContent.BuffType<LSSJ3Buff>() },
            { "LSSJ4", ModContent.BuffType<LSSJ4Buff>() },
            { "LSSJ4LB", ModContent.BuffType<LSSJ4LBBuff>() },
            { "LSSJ5", ModContent.BuffType<LSSJ5Buff>() },
            { "LSSJ6", ModContent.BuffType<LSSJ6Buff>() },
            { "LSSJ7", ModContent.BuffType<LSSJ7Buff>() },
            { "SSJG", ModContent.BuffType<SSJGBuff>() },
            { "LSSJB", ModContent.BuffType<LSSJBBuff>() },
            { "SSJB1", ModContent.BuffType<SSJB1Buff>() },
            { "SSJB1G2", ModContent.BuffType<SSJB1G2Buff>() },
            { "SSJB1G3", ModContent.BuffType<SSJB1G3Buff>() },
            { "SSJB1G4", ModContent.BuffType<SSJB1G4Buff>() },
            { "SSJB2", ModContent.BuffType<SSJB2Buff>() },
            { "SSJB3", ModContent.BuffType<SSJB3Buff>() },
            { "SSJBE", ModContent.BuffType<SSJBEBuff>() },
            { "SSJR1", ModContent.BuffType<SSJR1Buff>() },
            { "SSJR1G2", ModContent.BuffType<SSJR1G2Buff>() },
            { "SSJR1G3", ModContent.BuffType<SSJR1G3Buff>() },
            { "SSJR1G4", ModContent.BuffType<SSJR1G4Buff>() },
            { "SSJR2", ModContent.BuffType<SSJR2Buff>() },
            { "SSJR3", ModContent.BuffType<SSJR3Buff>() },
            { "Divine", ModContent.BuffType<DivineBuff>() },
            { "DR", ModContent.BuffType<DRBuff>() },
            { "Evil", ModContent.BuffType<EvilBuff>() },
            { "Rampaging", ModContent.BuffType<RampagingBuff>() },
            { "Berserk", ModContent.BuffType<BerserkBuff>() },
            { "PU", ModContent.BuffType<PUBuff>() },
            { "Beast", ModContent.BuffType<BeastBuff>() },
            { "UE", ModContent.BuffType<UEBuff>() },
            { "UI", ModContent.BuffType<UIBuff>() },
            { "TUI", ModContent.BuffType<TUIBuff>() },
            { "UILB", ModContent.BuffType<UILBBuff>() }
        };
        public static Dictionary<int, String> formIDToName = new Dictionary<int, string>()
        {
            { -1 , "baseForm" },
            { ModContent.BuffType<FSSJBuff>(), "FSSJ" },
            { ModContent.BuffType<SSJ1Buff>(), "SSJ1" },
            { ModContent.BuffType<SSJ1G2Buff>(), "SSJ1G2" },
            { ModContent.BuffType<FSSJBBuff>(), "FSSJB" },
            { ModContent.BuffType<SSJ1G3Buff>(), "SSJ1G3" },
            { ModContent.BuffType<SSJ1G4Buff>(), "SSJ1G4" },
            { ModContent.BuffType<SSJ2Buff>(), "SSJ2" },
            { ModContent.BuffType<SSJ3Buff>(), "SSJ3" },
            { ModContent.BuffType<SSJRageBuff>(), "SSJRage" },
            { ModContent.BuffType<SSJ4Buff>(), "SSJ4" },
            { ModContent.BuffType<SSJ4LBBuff>(), "SSJ4LB" },
            { ModContent.BuffType<SSJ5Buff>(), "SSJ5" },
            { ModContent.BuffType<SSJ5G2Buff>(), "SSJ5G2" },
            { ModContent.BuffType<SSJ5G3Buff>(), "SSJ5G3" },
            { ModContent.BuffType<SSJ5G4Buff>(), "SSJ5G4" },
            { ModContent.BuffType<SSJ6Buff>(), "SSJ6" },
            { ModContent.BuffType<SSJ7Buff>(), "SSJ7" },
            { ModContent.BuffType<FLSSJBuff>(), "FLSSJ" },
            { ModContent.BuffType<IkariBuff>(), "Ikari" },
            { ModContent.BuffType<LSSJ1Buff>(), "LSSJ1" },
            { ModContent.BuffType<LSSJ2Buff>(), "LSSJ2" },
            { ModContent.BuffType<LSSJ3Buff>(), "LSSJ3" },
            { ModContent.BuffType<LSSJ4Buff>(), "LSSJ4" },
            { ModContent.BuffType<LSSJ4LBBuff>(), "LSSJ4LB" },
            { ModContent.BuffType<LSSJ5Buff>(), "LSSJ5" },
            { ModContent.BuffType<LSSJ6Buff>(), "LSSJ6" },
            { ModContent.BuffType<LSSJ7Buff>(), "LSSJ7" },
            { ModContent.BuffType<SSJGBuff>(), "SSJG" },
            { ModContent.BuffType<LSSJBBuff>(), "LSSJB" },
            { ModContent.BuffType<SSJB1Buff>(), "SSJB1" },
            { ModContent.BuffType<SSJB1G2Buff>(), "SSJB1G2" },
            { ModContent.BuffType<SSJB1G3Buff>(), "SSJB1G3" },
            { ModContent.BuffType<SSJB1G4Buff>(), "SSJB1G4" },
            { ModContent.BuffType<SSJB2Buff>(), "SSJB2" },
            { ModContent.BuffType<SSJB3Buff>(), "SSJB3" },
            { ModContent.BuffType<SSJBEBuff>(), "SSJBE" },
            { ModContent.BuffType<SSJR1Buff>(), "SSJR1" },
            { ModContent.BuffType<SSJR1G2Buff>(), "SSJR1G2" },
            { ModContent.BuffType<SSJR1G3Buff>(), "SSJR1G3" },
            { ModContent.BuffType<SSJR1G4Buff>(), "SSJR1G4" },
            { ModContent.BuffType<SSJR2Buff>(), "SSJR2" },
            { ModContent.BuffType<SSJR3Buff>(), "SSJR3" },
            { ModContent.BuffType<DivineBuff>(), "Divine" },
            { ModContent.BuffType<DRBuff>(), "DR" },
            { ModContent.BuffType<EvilBuff>(), "Evil" },
            { ModContent.BuffType<RampagingBuff>(), "Rampaging" },
            { ModContent.BuffType<BerserkBuff>(), "Berserk" },
            { ModContent.BuffType<PUBuff>(), "PU" },
            { ModContent.BuffType<BeastBuff>(), "Beast" },
            { ModContent.BuffType<UEBuff>(), "UE" },
            { ModContent.BuffType<UIBuff>(), "UI" },
            { ModContent.BuffType<TUIBuff>(), "TUI" },
            { ModContent.BuffType<UILBBuff>(), "UILB" }
        };

        public static Dictionary<string, float> nameToDamageBonus = new Dictionary<string, float>()
        {
            { "FSSJ", FSSJBuff.DamageBonus },
            { "SSJ1", SSJ1Buff.DamageBonus },
            { "SSJ1G2", SSJ1G2Buff.DamageBonus },
            { "FSSJB", FSSJBBuff.DamageBonus },
            { "SSJ1G3", SSJ1G3Buff.DamageBonus },
            { "SSJ1G4", SSJ1G4Buff.DamageBonus },
            { "SSJ2", SSJ2Buff.DamageBonus },
            { "SSJ3", SSJ3Buff.DamageBonus },
            { "SSJRage", SSJRageBuff.DamageBonus },
            { "SSJ4", SSJ4Buff.DamageBonus },
            { "SSJ4LB", SSJ4LBBuff.DamageBonus },
            { "SSJ5", SSJ5Buff.DamageBonus },
            { "SSJ5G2", SSJ5G2Buff.DamageBonus },
            { "SSJ5G3", SSJ5G3Buff.DamageBonus },
            { "SSJ5G4", SSJ5G4Buff.DamageBonus },
            { "SSJ6", SSJ6Buff.DamageBonus },
            { "SSJ7", SSJ7Buff.DamageBonus },
            { "FLSSJ", FLSSJBuff.DamageBonus },
            { "Ikari", IkariBuff.DamageBonus },
            { "LSSJ1", LSSJ1Buff.DamageBonus },
            { "LSSJ2", LSSJ2Buff.DamageBonus },
            { "LSSJ3", LSSJ3Buff.DamageBonus },
            { "LSSJ4", LSSJ4Buff.DamageBonus },
            { "LSSJ4LB", LSSJ4LBBuff.DamageBonus },
            { "LSSJ5", LSSJ5Buff.DamageBonus },
            { "LSSJ6", LSSJ6Buff.DamageBonus },
            { "LSSJ7", LSSJ7Buff.DamageBonus },
            { "SSJG", SSJGBuff.DamageBonus },
            { "LSSJB", LSSJBBuff.DamageBonus },
            { "SSJB1", SSJB1Buff.DamageBonus },
            { "SSJB1G2", SSJB1G2Buff.DamageBonus },
            { "SSJB1G3", SSJB1G3Buff.DamageBonus },
            { "SSJB1G4", SSJB1G4Buff.DamageBonus },
            { "SSJB2", SSJB2Buff.DamageBonus },
            { "SSJB3", SSJB3Buff.DamageBonus },
            { "SSJBE", SSJBEBuff.DamageBonus },
            { "SSJR1", SSJR1Buff.DamageBonus },
            { "SSJR1G2", SSJR1G2Buff.DamageBonus },
            { "SSJR1G3", SSJR1G3Buff.DamageBonus },
            { "SSJR1G4", SSJR1G4Buff.DamageBonus },
            { "SSJR2", SSJR2Buff.DamageBonus },
            { "SSJR3", SSJR3Buff.DamageBonus },
            { "Divine", DivineBuff.DamageBonus },
            { "DR", DRBuff.DamageBonus },
            { "Evil", EvilBuff.DamageBonus },
            { "Rampaging", RampagingBuff.DamageBonus },
            { "Berserk", BerserkBuff.DamageBonus },
            { "PU", PUBuff.DamageBonus },
            { "Beast", BeastBuff.DamageBonus },
            { "UE", UEBuff.DamageBonus },
            { "UI", UIBuff.DamageBonus },
            { "TUI", TUIBuff.DamageBonus },
            { "UILB", UILBBuff.DamageBonus }
        };

        public static Dictionary<string, string> nameToUnlockHint = new Dictionary<string, string>()
        {
            { "FSSJ", FSSJBuff.UnlockHint },
            { "SSJ1", SSJ1Buff.UnlockHint },
            { "SSJ1G2", SSJ1G2Buff.UnlockHint },
            { "FSSJB", FSSJBBuff.UnlockHint },
            { "SSJ1G3", SSJ1G3Buff.UnlockHint },
            { "SSJ1G4", SSJ1G4Buff.UnlockHint },
            { "SSJ2", SSJ2Buff.UnlockHint },
            { "SSJ3", SSJ3Buff.UnlockHint },
            { "SSJRage", SSJRageBuff.UnlockHint },
            { "SSJ4", SSJ4Buff.UnlockHint },
            { "SSJ4LB", SSJ4LBBuff.UnlockHint },
            { "SSJ5", SSJ5Buff.UnlockHint },
            { "SSJ5G2", SSJ5G2Buff.UnlockHint },
            { "SSJ5G3", SSJ5G3Buff.UnlockHint },
            { "SSJ5G4", SSJ5G4Buff.UnlockHint },
            { "SSJ6", SSJ6Buff.UnlockHint },
            { "SSJ7", SSJ7Buff.UnlockHint },
            { "FLSSJ", FLSSJBuff.UnlockHint },
            { "Ikari", IkariBuff.UnlockHint },
            { "LSSJ1", LSSJ1Buff.UnlockHint },
            { "LSSJ2", LSSJ2Buff.UnlockHint },
            { "LSSJ3", LSSJ3Buff.UnlockHint },
            { "LSSJ4", LSSJ4Buff.UnlockHint },
            { "LSSJ4LB", LSSJ4LBBuff.UnlockHint },
            { "LSSJ5", LSSJ5Buff.UnlockHint },
            { "LSSJ6", LSSJ6Buff.UnlockHint },
            { "LSSJ7", LSSJ7Buff.UnlockHint },
            { "SSJG", SSJGBuff.UnlockHint },
            { "LSSJB", LSSJBBuff.UnlockHint },
            { "SSJB1", SSJB1Buff.UnlockHint },
            { "SSJB1G2", SSJB1G2Buff.UnlockHint },
            { "SSJB1G3", SSJB1G3Buff.UnlockHint },
            { "SSJB1G4", SSJB1G4Buff.UnlockHint },
            { "SSJB2", SSJB2Buff.UnlockHint },
            { "SSJB3", SSJB3Buff.UnlockHint },
            { "SSJBE", SSJBEBuff.UnlockHint },
            { "SSJR1", SSJR1Buff.UnlockHint },
            { "SSJR1G2", SSJR1G2Buff.UnlockHint },
            { "SSJR1G3", SSJR1G3Buff.UnlockHint },
            { "SSJR1G4", SSJR1G4Buff.UnlockHint },
            { "SSJR2", SSJR2Buff.UnlockHint },
            { "SSJR3", SSJR3Buff.UnlockHint },
            { "Divine", DivineBuff.UnlockHint },
            { "DR", DRBuff.UnlockHint },
            { "Evil", EvilBuff.UnlockHint },
            { "Rampaging", RampagingBuff.UnlockHint },
            { "Berserk", BerserkBuff.UnlockHint },
            { "PU", PUBuff.UnlockHint },
            { "Beast", BeastBuff.UnlockHint },
            { "UE", UEBuff.UnlockHint },
            { "UI", UIBuff.UnlockHint },
            { "TUI", TUIBuff.UnlockHint },
            { "UILB", UILBBuff.UnlockHint }
        };


        public static Dictionary<string, float> nameToDefenseBonus = new Dictionary<string, float>()
        {
            { "FSSJ", FSSJBuff.DefenseBonus },
            { "SSJ1", SSJ1Buff.DefenseBonus },
            { "SSJ1G2", SSJ1G2Buff.DefenseBonus },
            { "FSSJB", FSSJBBuff.DefenseBonus },
            { "SSJ1G3", SSJ1G3Buff.DefenseBonus },
            { "SSJ1G4", SSJ1G4Buff.DefenseBonus },
            { "SSJ2", SSJ2Buff.DefenseBonus },
            { "SSJ3", SSJ3Buff.DefenseBonus },
            { "SSJRage", SSJRageBuff.DefenseBonus },
            { "SSJ4", SSJ4Buff.DefenseBonus },
            { "SSJ4LB", SSJ4LBBuff.DefenseBonus },
            { "SSJ5", SSJ5Buff.DefenseBonus },
            { "SSJ5G2", SSJ5G2Buff.DefenseBonus },
            { "SSJ5G3", SSJ5G3Buff.DefenseBonus },
            { "SSJ5G4", SSJ5G4Buff.DefenseBonus },
            { "SSJ6", SSJ6Buff.DefenseBonus },
            { "SSJ7", SSJ7Buff.DefenseBonus },
            { "FLSSJ", FLSSJBuff.DefenseBonus },
            { "Ikari", IkariBuff.DefenseBonus },
            { "LSSJ1", LSSJ1Buff.DefenseBonus },
            { "LSSJ2", LSSJ2Buff.DefenseBonus },
            { "LSSJ3", LSSJ3Buff.DefenseBonus },
            { "LSSJ4", LSSJ4Buff.DefenseBonus },
            { "LSSJ4LB", LSSJ4LBBuff.DefenseBonus },
            { "LSSJ5", LSSJ5Buff.DefenseBonus },
            { "LSSJ6", LSSJ6Buff.DefenseBonus },
            { "LSSJ7", LSSJ7Buff.DefenseBonus },
            { "SSJG", SSJGBuff.DefenseBonus },
            { "LSSJB", LSSJBBuff.DefenseBonus },
            { "SSJB1", SSJB1Buff.DefenseBonus },
            { "SSJB1G2", SSJB1G2Buff.DefenseBonus },
            { "SSJB1G3", SSJB1G3Buff.DefenseBonus },
            { "SSJB1G4", SSJB1G4Buff.DefenseBonus },
            { "SSJB2", SSJB2Buff.DefenseBonus },
            { "SSJB3", SSJB3Buff.DefenseBonus },
            { "SSJBE", SSJBEBuff.DefenseBonus },
            { "SSJR1", SSJR1Buff.DefenseBonus },
            { "SSJR1G2", SSJR1G2Buff.DefenseBonus },
            { "SSJR1G3", SSJR1G3Buff.DefenseBonus },
            { "SSJR1G4", SSJR1G4Buff.DefenseBonus },
            { "SSJR2", SSJR2Buff.DefenseBonus },
            { "SSJR3", SSJR3Buff.DefenseBonus },
            { "Divine", DivineBuff.DefenseBonus },
            { "DR", DRBuff.DefenseBonus },
            { "Evil", EvilBuff.DefenseBonus },
            { "Rampaging", RampagingBuff.DefenseBonus },
            { "Berserk", BerserkBuff.DefenseBonus },
            { "PU", PUBuff.DefenseBonus },
            { "Beast", BeastBuff.DefenseBonus },
            { "UE", UEBuff.DefenseBonus },
            { "UI", UIBuff.DefenseBonus },
            { "TUI", TUIBuff.DefenseBonus },
            { "UILB", UILBBuff.DefenseBonus }
        };

        public static Dictionary<string, float> nameToSpeedBonus = new Dictionary<string, float>()
        {
            { "FSSJ", FSSJBuff.SpeedBonus },
            { "SSJ1", SSJ1Buff.SpeedBonus },
            { "SSJ1G2", SSJ1G2Buff.SpeedBonus },
            { "FSSJB", FSSJBBuff.SpeedBonus },
            { "SSJ1G3", SSJ1G3Buff.SpeedBonus },
            { "SSJ1G4", SSJ1G4Buff.SpeedBonus },
            { "SSJ2", SSJ2Buff.SpeedBonus },
            { "SSJ3", SSJ3Buff.SpeedBonus },
            { "SSJRage", SSJRageBuff.SpeedBonus },
            { "SSJ4", SSJ4Buff.SpeedBonus },
            { "SSJ4LB", SSJ4LBBuff.SpeedBonus },
            { "SSJ5", SSJ5Buff.SpeedBonus },
            { "SSJ5G2", SSJ5G2Buff.SpeedBonus },
            { "SSJ5G3", SSJ5G3Buff.SpeedBonus },
            { "SSJ5G4", SSJ5G4Buff.SpeedBonus },
            { "SSJ6", SSJ6Buff.SpeedBonus },
            { "SSJ7", SSJ7Buff.SpeedBonus },
            { "FLSSJ", FLSSJBuff.SpeedBonus },
            { "Ikari", IkariBuff.SpeedBonus },
            { "LSSJ1", LSSJ1Buff.SpeedBonus },
            { "LSSJ2", LSSJ2Buff.SpeedBonus },
            { "LSSJ3", LSSJ3Buff.SpeedBonus },
            { "LSSJ4", LSSJ4Buff.SpeedBonus },
            { "LSSJ4LB", LSSJ4LBBuff.SpeedBonus },
            { "LSSJ5", LSSJ5Buff.SpeedBonus },
            { "LSSJ6", LSSJ6Buff.SpeedBonus },
            { "LSSJ7", LSSJ7Buff.SpeedBonus },
            { "SSJG", SSJGBuff.SpeedBonus },
            { "LSSJB", LSSJBBuff.SpeedBonus },
            { "SSJB1", SSJB1Buff.SpeedBonus },
            { "SSJB1G2", SSJB1G2Buff.SpeedBonus },
            { "SSJB1G3", SSJB1G3Buff.SpeedBonus },
            { "SSJB1G4", SSJB1G4Buff.SpeedBonus },
            { "SSJB2", SSJB2Buff.SpeedBonus },
            { "SSJB3", SSJB3Buff.SpeedBonus },
            { "SSJBE", SSJBEBuff.SpeedBonus },
            { "SSJR1", SSJR1Buff.SpeedBonus },
            { "SSJR1G2", SSJR1G2Buff.SpeedBonus },
            { "SSJR1G3", SSJR1G3Buff.SpeedBonus },
            { "SSJR1G4", SSJR1G4Buff.SpeedBonus },
            { "SSJR2", SSJR2Buff.SpeedBonus },
            { "SSJR3", SSJR3Buff.SpeedBonus },
            { "Divine", DivineBuff.SpeedBonus },
            { "DR", DRBuff.SpeedBonus },
            { "Evil", EvilBuff.SpeedBonus },
            { "Rampaging", RampagingBuff.SpeedBonus },
            { "Berserk", BerserkBuff.SpeedBonus },
            { "PU", PUBuff.SpeedBonus },
            { "Beast", BeastBuff.SpeedBonus },
            { "UE", UEBuff.SpeedBonus },
            { "UI", UIBuff.SpeedBonus },
            { "TUI", TUIBuff.SpeedBonus },
            { "UILB", UILBBuff.SpeedBonus }
        };
        public static Dictionary<string, Boolean> nameToIsStackable = new Dictionary<string, Boolean>()
        {
            { "FSSJ", FSSJBuff.isStackable },
            { "SSJ1", SSJ1Buff.isStackable },
            { "SSJ1G2", SSJ1G2Buff.isStackable },
            { "FSSJB", FSSJBBuff.isStackable },
            { "SSJ1G3", SSJ1G3Buff.isStackable },
            { "SSJ1G4", SSJ1G4Buff.isStackable },
            { "SSJ2", SSJ2Buff.isStackable },
            { "SSJ3", SSJ3Buff.isStackable },
            { "SSJRage", SSJRageBuff.isStackable },
            { "SSJ4", SSJ4Buff.isStackable },
            { "SSJ4LB", SSJ4LBBuff.isStackable },
            { "SSJ5", SSJ5Buff.isStackable },
            { "SSJ5G2", SSJ5G2Buff.isStackable },
            { "SSJ5G3", SSJ5G3Buff.isStackable },
            { "SSJ5G4", SSJ5G4Buff.isStackable },
            { "SSJ6", SSJ6Buff.isStackable },
            { "SSJ7", SSJ7Buff.isStackable },
            { "FLSSJ", FLSSJBuff.isStackable },
            { "Ikari", IkariBuff.isStackable },
            { "LSSJ1", LSSJ1Buff.isStackable },
            { "LSSJ2", LSSJ2Buff.isStackable },
            { "LSSJ3", LSSJ3Buff.isStackable },
            { "LSSJ4", LSSJ4Buff.isStackable },
            { "LSSJ4LB", LSSJ4LBBuff.isStackable },
            { "LSSJ5", LSSJ5Buff.isStackable },
            { "LSSJ6", LSSJ6Buff.isStackable },
            { "LSSJ7", LSSJ7Buff.isStackable },
            { "SSJG", SSJGBuff.isStackable },
            { "LSSJB", LSSJBBuff.isStackable },
            { "SSJB1", SSJB1Buff.isStackable },
            { "SSJB1G2", SSJB1G2Buff.isStackable },
            { "SSJB1G3", SSJB1G3Buff.isStackable },
            { "SSJB1G4", SSJB1G4Buff.isStackable },
            { "SSJB2", SSJB2Buff.isStackable },
            { "SSJB3", SSJB3Buff.isStackable },
            { "SSJBE", SSJBEBuff.isStackable },
            { "SSJR1", SSJR1Buff.isStackable },
            { "SSJR1G2", SSJR1G2Buff.isStackable },
            { "SSJR1G3", SSJR1G3Buff.isStackable },
            { "SSJR1G4", SSJR1G4Buff.isStackable },
            { "SSJR2", SSJR2Buff.isStackable },
            { "SSJR3", SSJR3Buff.isStackable },
            { "Divine", DivineBuff.isStackable },
            { "DR", DRBuff.isStackable },
            { "Evil", EvilBuff.isStackable },
            { "Rampaging", RampagingBuff.isStackable },
            { "Berserk", BerserkBuff.isStackable },
            { "PU", PUBuff.isStackable },
            { "Beast", BeastBuff.isStackable },
            { "UE", UEBuff.isStackable },
            { "UI", UIBuff.isStackable },
            { "TUI", TUIBuff.isStackable },
            { "UILB", UILBBuff.isStackable }
        };
        public static Dictionary<string, float> nameToKiDrain = new Dictionary<string, float>()
        {
            { "FSSJ", FSSJBuff.KiDrain },
            { "SSJ1", SSJ1Buff.KiDrain },
            { "SSJ1G2", SSJ1G2Buff.KiDrain },
            { "FSSJB", FSSJBBuff.KiDrain },
            { "SSJ1G3", SSJ1G3Buff.KiDrain },
            { "SSJ1G4", SSJ1G4Buff.KiDrain },
            { "SSJ2", SSJ2Buff.KiDrain },
            { "SSJ3", SSJ3Buff.KiDrain },
            { "SSJRage", SSJRageBuff.KiDrain },
            { "SSJ4", SSJ4Buff.KiDrain },
            { "SSJ4LB", SSJ4LBBuff.KiDrain },
            { "SSJ5", SSJ5Buff.KiDrain },
            { "SSJ5G2", SSJ5G2Buff.KiDrain },
            { "SSJ5G3", SSJ5G3Buff.KiDrain },
            { "SSJ5G4", SSJ5G4Buff.KiDrain },
            { "SSJ6", SSJ6Buff.KiDrain },
            { "SSJ7", SSJ7Buff.KiDrain },
            { "FLSSJ", FLSSJBuff.KiDrain },
            { "Ikari", IkariBuff.KiDrain },
            { "LSSJ1", LSSJ1Buff.KiDrain },
            { "LSSJ2", LSSJ2Buff.KiDrain },
            { "LSSJ3", LSSJ3Buff.KiDrain },
            { "LSSJ4", LSSJ4Buff.KiDrain },
            { "LSSJ4LB", LSSJ4LBBuff.KiDrain },
            { "LSSJ5", LSSJ5Buff.KiDrain },
            { "LSSJ6", LSSJ6Buff.KiDrain },
            { "LSSJ7", LSSJ7Buff.KiDrain },
            { "SSJG", SSJGBuff.KiDrain },
            { "LSSJB", LSSJBBuff.KiDrain },
            { "SSJB1", SSJB1Buff.KiDrain },
            { "SSJB1G2", SSJB1G2Buff.KiDrain },
            { "SSJB1G3", SSJB1G3Buff.KiDrain },
            { "SSJB1G4", SSJB1G4Buff.KiDrain },
            { "SSJB2", SSJB2Buff.KiDrain },
            { "SSJB3", SSJB3Buff.KiDrain },
            { "SSJBE", SSJBEBuff.KiDrain },
            { "SSJR1", SSJR1Buff.KiDrain },
            { "SSJR1G2", SSJR1G2Buff.KiDrain },
            { "SSJR1G3", SSJR1G3Buff.KiDrain },
            { "SSJR1G4", SSJR1G4Buff.KiDrain },
            { "SSJR2", SSJR2Buff.KiDrain },
            { "SSJR3", SSJR3Buff.KiDrain },
            { "Divine", DivineBuff.KiDrain },
            { "DR", DRBuff.KiDrain },
            { "Evil", EvilBuff.KiDrain },
            { "Rampaging", RampagingBuff.KiDrain },
            { "Berserk", BerserkBuff.KiDrain },
            { "PU", PUBuff.KiDrain },
            { "Beast", BeastBuff.KiDrain },
            { "UE", UEBuff.KiDrain },
            { "UI", UIBuff.KiDrain },
            { "TUI", TUIBuff.KiDrain },
            { "UILB", UILBBuff.KiDrain }
        };


        public static Dictionary<string, List<string>> nameToSpecial= new Dictionary<string, List<string>>()
        {
            { "FSSJ", FSSJBuff.special },
            { "SSJ1", SSJ1Buff.special },
            { "SSJ1G2", SSJ1G2Buff.special },
            { "FSSJB", FSSJBBuff.special },
            { "SSJ1G3", SSJ1G3Buff.special },
            { "SSJ1G4", SSJ1G4Buff.special },
            { "SSJ2", SSJ2Buff.special },
            { "SSJ3", SSJ3Buff.special },
            { "SSJRage", SSJRageBuff.special },
            { "SSJ4", SSJ4Buff.special },
            { "SSJ4LB", SSJ4LBBuff.special },
            { "SSJ5", SSJ5Buff.special },
            { "SSJ5G2", SSJ5G2Buff.special },
            { "SSJ5G3", SSJ5G3Buff.special },
            { "SSJ5G4", SSJ5G4Buff.special },
            { "SSJ6", SSJ6Buff.special },
            { "SSJ7", SSJ7Buff.special },
            { "FLSSJ", FLSSJBuff.special },
            { "Ikari", IkariBuff.special },
            { "LSSJ1", LSSJ1Buff.special },
            { "LSSJ2", LSSJ2Buff.special },
            { "LSSJ3", LSSJ3Buff.special },
            { "LSSJ4", LSSJ4Buff.special },
            { "LSSJ4LB", LSSJ4LBBuff.special },
            { "LSSJ5", LSSJ5Buff.special },
            { "LSSJ6", LSSJ6Buff.special },
            { "LSSJ7", LSSJ7Buff.special },
            { "SSJG", SSJGBuff.special },
            { "LSSJB", LSSJBBuff.special },
            { "SSJB1", SSJB1Buff.special },
            { "SSJB1G2", SSJB1G2Buff.special },
            { "SSJB1G3", SSJB1G3Buff.special },
            { "SSJB1G4", SSJB1G4Buff.special },
            { "SSJB2", SSJB2Buff.special },
            { "SSJB3", SSJB3Buff.special },
            { "SSJBE", SSJBEBuff.special },
            { "SSJR1", SSJR1Buff.special },
            { "SSJR1G2", SSJR1G2Buff.special },
            { "SSJR1G3", SSJR1G3Buff.special },
            { "SSJR1G4", SSJR1G4Buff.special },
            { "SSJR2", SSJR2Buff.special },
            { "SSJR3", SSJR3Buff.special },
            { "Divine", DivineBuff.special },
            { "DR", DRBuff.special },
            { "Evil", EvilBuff.special },
            { "Rampaging", RampagingBuff.special },
            { "Berserk", BerserkBuff.special },
            { "PU", PUBuff.special },
            { "Beast", BeastBuff.special },
            { "UE", UEBuff.special },
            { "UI", UIBuff.special },
            { "TUI", TUIBuff.special },
            { "UILB", UILBBuff.special }
        };

        public static List<string> getSpecial(string form)
        {
            if (nameToSpecial.ContainsKey(form))
            {
                return nameToSpecial[form];
            }
            return null;
        }

        public static int formNameToID(string name)
        {
            if(name == null)
            {
                return -1;
            }
            return nameToFormID[name];
        }

        public static Dictionary<String, String> makeFormAdvRev(string adv, string alt, string rev)
        {
            Dictionary<String, String> toReturn = new Dictionary<String, String>();
            if(adv != null) { toReturn.Add("Main", adv); }
            if(alt != null) { toReturn.Add("Alternate", alt); }
            if(rev != null) { toReturn.Add("Reverse", rev); }
            return toReturn;
        }

        public static string IDToFormName(int id)
        {
            return formIDToName[id];
        }

        public static string advance(Dictionary<String, String> dict)
        {
            if (dict.ContainsKey("Main")){
                return dict["Main"];
            }
            return null;
        }
        public static string altAdvance(Dictionary<String, String> dict)
        {
            if (dict.ContainsKey("Alternate"))
            {
                return dict["Alternate"];
            }
            return null;
        }
        public static string reverse(Dictionary<String, String> dict)
        {
            if (dict.ContainsKey("Reverse"))
            {
                return dict["Reverse"];
            }
            return null;
        }

        public static string advanceForm(string name)
        {
            if (name == null)
            {
                return null;
            }
            if (nameToDict.ContainsKey(name))
            {
                if (advance(nameToDict[name]) != null)
                {
                    return advance(nameToDict[name]);
                }
            }
            return name;
        }
        public static string altAdvanceForm(string name)
        {
            if (name == null)
            {
                return null;
            }
            if (nameToDict.ContainsKey(name))
            {
                if (altAdvance(nameToDict[name]) != null)
                {
                    return altAdvance(nameToDict[name]);
                }
            }
            return name;
                
        }
        public static string reverseForm(string name)
        {
            if(name == null)
            {
                return null;
            }
            if (nameToDict.ContainsKey(name))
            {
                if (reverse(nameToDict[name]) != null)
                {
                    return reverse(nameToDict[name]);
                }
            }
            return name;
            
        }

        public static Boolean canAdvance(string form)
        {
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (form != null && forms.Contains(form) && modPlayer.unlockedForms.Contains(advanceForm(form)))
            {
                return true;
            }
            return false;
        }
        public static Boolean canAltAdvance(string form)
        {
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (form != null && forms.Contains(form) && modPlayer.unlockedForms.Contains(altAdvanceForm(form)))
            {
                return true;
            }
            return false;
        }
        public static Boolean canReverse(string form)
        {
            DragonballPichuPlayer modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            string reverse = reverseForm(form);
            if (form != null && forms.Contains(form) && reverse != null && modPlayer.unlockedForms.Contains(reverse))
            {
                return true;
            }
            return false;
        }
    }
}
