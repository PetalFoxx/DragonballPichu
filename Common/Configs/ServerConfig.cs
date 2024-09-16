using DragonballPichu.Common.Systems;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Composition.Convention;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.Config;

namespace DragonballPichu.Common.Configs
{
    internal class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;


        //[Header("Test")]


        [Header("Ki")]

        //multiply max ki
        [SliderColor(0, 0, 205)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float maxKiMulti; //implemented!
        //multiply all ki gaining
        [SliderColor(0, 0, 205)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float kiGainMulti; //implemented!
        //multiply all ki draining
        [SliderColor(0, 0, 205)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float kiDrainMulti; //implemented!

        [Header("Resources")]

        //multiply all exp gained
        [SliderColor(0, 191, 255)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float expMulti; //implemented!
        //multiply all form points gained
        [SliderColor(138, 43, 226)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float formPointsMulti; //implemented!

        [Header("Base")]

        //multiply the base attack stat
        [SliderColor(197, 179, 88)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float baseAttackMulti; //implemented!
        //multiply the base defense stat
        [SliderColor(197, 179, 88)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float baseDefenseMulti; //implemented!
        //multiply the base speed stat
        [SliderColor(197, 179, 88)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float baseSpeedMulti; //implemented!

        [Header("Forms")]

        //multiply form attack increase
        [SliderColor(255, 215, 0)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float formAttackMulti; //implemented!
        //multiply form defense addition
        [SliderColor(255, 215, 0)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float formDefenseMulti; //implemented!
        //multiply form speed increase
        [SliderColor(255, 215, 0)]
        [Increment(.1f)]
        [Range(0f, 10f)]
        [DefaultValue(1f)]
        public float formSpeedMulti; //implemented!


        [Header("Progression")]

        /*
        Hardcore Form Locking (pick a character and stick with them)
        Mediumcore Form Locking (Path + Kaioken)
        Softcore Form Locking (Path + Stackable Forms that make sense)
        Default Settings (Path + Other paths that can't stack forms have their form points raised)
        Cheater Settings (No form points penalty)
        Hacker Settings (All forms can be stacked) */
        //hardcore / mediumcore / softcore / default / cheater / hacker

        [DefaultValue("Default")]
        [DrawTicks]
        [SliderColor(0,0,0)]
        [OptionStrings(new string[] { "Default", "Hardcore", "Mediumcore", "Softcore", "Cheater" })]
        public string formLocking;



        //requires this level in the previous form(s) in order to progress to the next

        [Increment(1)]
        [Range(0, 100)]
        [DefaultValue(0)]
        [Slider]
        public int requiredLevelToAdvanceForm; //implemented!

        [DefaultValue("default")]
        [DrawTicks]
        [SliderColor(0, 0, 0)]
        [OptionStrings(new string[] {"1.1^e", "default", "x^e", "1.1^x" })]
        public string levelScaling; //implemented!

        [DefaultValue(false)]
        public bool allowUIInPreHardmode; //implemented!

        //anything in here you cannot unlock or transform into
        public List<string> disabledForms = new List<string>(); //implemented!

        

        [Header("Extras")]

        //sets it to use the randomly assigned specials rather than the ones that make sense
        [DefaultValue(false)]
        public bool useRandomSpecials; //implemented!

        [DefaultValue(false)]
        public bool hackerStackEverything; //implemented!

        //allows the client to change things such as amount of form points and base form level
        [DefaultValue(false)]
        public bool allowClientCheating; //implemented!

        //changes charging to give less while moving instead of slowing you down
        [DefaultValue(false)]
        public bool lessKiInsteadOfSlowingCharge; //implemented!

    }
}
