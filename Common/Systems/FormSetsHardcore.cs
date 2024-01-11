using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace DragonballPichu.Common.Systems
{
    internal class FormSetsHardcore : FormSets
    {
        public static string[] Broly =        { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "Ikari", "FLSSJ", "LSSJ1", "LSSJ2", "LSSJ3", "LSSJ4", "LSSJ4LB", "SSJG", "LSSJB"};
        public static string[] FusedZamasu =  { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJG","SSJR1", "SSJR1G2", "SSJR1G3", "SSJR1G4", "Divine","DR", "Evil", "Kaio-ken"};
        public static string[] Shallot =      { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJ3","SSJG", "FSSJB", "SSJB1", "SSJB1G2", "SSJB1G3", "SSJB1G4", "Ikari"};
        public static string[] Beat =         { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJ3","SSJ4","SSJ4LB", "SSJG", "FSSJB", "SSJB1", "SSJB1G2", "SSJB1G3", "SSJB1G4", "UI", "TUI"};
        public static string[] SuperWarrior = { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJ3","SSJG","FSSJB","SSJB1", "SSJB1G2", "SSJB1G3", "SSJB1G4", "SSJBE", "PU", "Beast","Evil","UI","TUI", "Kaio-ken"};
        public static string[] SuperGoku =    { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJ3","SSJG", "FSSJB", "SSJB1", "SSJB1G2", "SSJB1G3", "SSJB1G4", "UI","TUI","UILB","PU","Kaio-ken"};
        public static string[] GTGoku =       { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJ3","SSJ4","SSJ4LB","PU","Kaio-ken"};
        public static string[] FutureTrunks = { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJRage"};
        public static string[] Gohan =        { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJ3","SSJ4","PU","Beast"};
        public static string[] SuperVegeta =  { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJG","FSSJB","SSJB1", "SSJB1G2", "SSJB1G3", "SSJB1G4", "SSJBE","Evil","UE"};
        public static string[] XenoVegeta =   { "FSSJ", "SSJ1", "SSJ1G2", "SSJ1G3", "SSJ1G4", "SSJ2","SSJ3","SSJ4","SSJ4LB","Evil"};

        /*
        
         
         */
        public static new string[] get(string name)
        {
            switch (name)
            {
                case "Broly":
                    return Broly;
                case "FusedZamasu":
                    return FusedZamasu;
                case "Shallot":
                    return Shallot;
                case "Beat":
                    return Beat;
                case "SuperWarrior":
                    return SuperWarrior;
                case "SuperGoku":
                    return SuperGoku;
                case "GTGoku":
                    return GTGoku;
                case "FutureTrunks":
                    return FutureTrunks;
                case "Gohan":
                    return Gohan;
                case "SuperVegeta":
                    return SuperVegeta;
                case "XenoVegeta":
                    return XenoVegeta;
            }
            Main.NewText("Did not find the set " + name);
            return null;
        }
    }
}
