using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonballPichu.Common.GUI;
using DragonballPichu.Common.Systems;
using DragonballPichu.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using static Humanizer.On;

namespace DragonballPichu.Common.GUI
{
    public class FormsStatsUI : UIState
    {
        public string formHoverText = "None!";
        public UIPanel formsPanel;
        public UIPanel statsPanel;
        public UIPanel formChooserPanel;

        StatButton kiMaxStatButton;
        StatButton kiGainStatButton;
        StatButton chargeKiGainStatButton;
        StatButton baseDefenseButton;
        StatButton baseAttackButton;
        StatButton baseSpeedButton;

        //StatButton formMultiplyKi;
        //StatButton formDivideDrain;
        StatButton formIncreaseDamage;
        //StatButton formIncreaseSpeed;
        StatButton formIncreaseDefense;
        StatButton formSpecial;

        Boolean isFormButtons = false;

        UIText formInfoText;
        UIText statsPanelText;
        UIText formChooserText;
        UIText formsPanelText;

        UIPanel respecStatsButton;
        public int statsPanelID;

        public Dictionary<string, FormButton> nameToFormUnlockButton = new Dictionary<string, FormButton>();
        public Dictionary<string, FormButton> nameToFormChooseButton = new Dictionary<string, FormButton>();

        private List<GUIArrow> unlockArrows = new List<GUIArrow>(){
            new GUIArrow(Color.Coral, "FSSJ"),
            new GUIArrow(Color.Yellow, "FSSJ", "SSJ1"),
            new GUIArrow(Color.Black, "UI"),
            new GUIArrow(Color.LightGray, "UI", "TUI"),
            new GUIArrow(Color.Maroon, "Ikari"),
            new GUIArrow(Color.SpringGreen, "Ikari", "FLSSJ"),
            new GUIArrow(Color.LightPink,  "Divine",  "DR"),
            new GUIArrow(Color.Black, "PU"),
            new GUIArrow(Color.LightSlateGray, "PU", "Beast"),
            new GUIArrow(Color.Yellow, "SSJ1", "SSJ1G2"),
            new GUIArrow(Color.Yellow, "SSJ1", "SSJ1G4"),
            new GUIArrow(Color.Yellow, "SSJ1G4", "SSJ2"),
            new GUIArrow(Color.LimeGreen, "FLSSJ", "LSSJ1"),
            new GUIArrow(Color.BlueViolet, "Evil"),
            new GUIArrow(Color.BlueViolet, "Evil", "Rampaging"),
            new GUIArrow(Color.Yellow, "SSJ1G2", "SSJ1G3"),
            new GUIArrow(Color.Yellow, "SSJ2", "SSJ3"),
            new GUIArrow(Color.LimeGreen, "LSSJ1", "LSSJ2"),
            new GUIArrow(Color.BlueViolet, "Rampaging", "Berserk"),
            new GUIArrow(Color.SkyBlue, "SSJ1G3", "SSJRage"),
            new GUIArrow(Color.Maroon, "SSJ3", "SSJ4"),
            new GUIArrow(Color.LimeGreen, "LSSJ2", "LSSJ3"),
            new GUIArrow(Color.DarkRed, "SSJ4", "SSJ4LB"),
            new GUIArrow(Color.LimeGreen, "LSSJ3", "LSSJ4"),
            new GUIArrow(Color.DarkCyan, "LSSJ3", "LSSJB"),
            new GUIArrow(Color.LightGray , "SSJ4LB", "SSJ5"),
            new GUIArrow(Color.Red, "SSJG"),
            new GUIArrow(Color.Yellow, "SSJG", "FSSJB"),
            new GUIArrow(Color.Fuchsia, "SSJG", "SSJR1"),
            new GUIArrow(Color.DarkCyan, "SSJG", "LSSJB"),
            new GUIArrow(Color.LimeGreen, "LSSJ4", "LSSJ4LB"),
            new GUIArrow(Color.LightGray, "SSJ5", "SSJ5G2"),
            new GUIArrow(Color.LightGray, "SSJ5", "SSJ5G4"),
            new GUIArrow(Color.LightGray, "SSJ5G4", "SSJ6"),
            new GUIArrow(Color.DeepSkyBlue, "FSSJB", "SSJB1"),
            new GUIArrow(Color.LimeGreen, "LSSJ4LB", "LSSJ5"),
            new GUIArrow(Color.DeepSkyBlue, "SSJB1", "SSJB1G2"),
            new GUIArrow(Color.DeepSkyBlue, "SSJB1", "SSJB1G4"),
            new GUIArrow(Color.DeepSkyBlue, "SSJB1G4", "SSJB2"),
            new GUIArrow(Color.Fuchsia, "SSJR1", "SSJR1G2"),
            new GUIArrow(Color.Fuchsia, "SSJR1", "SSJR1G4"),
            new GUIArrow(Color.Fuchsia, "SSJR1G4", "SSJR2"),
            new GUIArrow(Color.HotPink,  "SSJR1G4",  "Divine"),
            new GUIArrow(Color.LightGray, "SSJ5G2", "SSJ5G3"),
            new GUIArrow(Color.LightGray, "SSJ6", "SSJ7"),
            new GUIArrow(Color.DeepSkyBlue , "SSJB1G2", "SSJB1G3"),
            new GUIArrow(Color.Fuchsia, "SSJR1G2", "SSJR1G3"),
            new GUIArrow(Color.LimeGreen, "LSSJ5", "LSSJ6"),
            new GUIArrow(Color.Blue, "SSJB1G3", "SSJBE"),
            new GUIArrow(Color.DeepSkyBlue, "SSJB2", "SSJB3"),
            new GUIArrow(Color.Fuchsia, "SSJR2", "SSJR3"),
            new GUIArrow(Color.LightGray, "TUI", "UILB"),
            new GUIArrow(Color.LimeGreen, "LSSJ6", "LSSJ7"),
            new GUIArrow(Color.Purple, "UE"),
        };

        Dictionary<string, Dictionary<string, object>> nameToUnlockTreeInfo = new Dictionary<string, Dictionary<string, object>>()
        {
            { "FSSJ", new Dictionary<string, object>(){ { "horizontal", 0.0625f * 1 },{ "vertical", 0.0714f * 0 } } },
            { "UI", new Dictionary<string, object>(){ { "horizontal", 0.0625f * 1 },{ "vertical", 0.0714f * 9 } } },
            { "Ikari", new Dictionary<string, object>(){ { "horizontal", 0.0625f * 1 },{ "vertical", 0.0714f * 10 } } },

            { "Divine", new Dictionary<string, object>(){ { "horizontal",0.0625f*2},{ "vertical",0.0714f*7} } },
            { "PU", new Dictionary<string, object>(){ { "horizontal",0.0625f*2},{ "vertical",0.0714f*12} } },

            { "SSJ1", new Dictionary<string, object>(){ { "horizontal",0.0625f*3},{ "vertical",0.0714f*0} } },
            { "SSJ1G4", new Dictionary<string, object>(){ { "horizontal",0.0625f*3},{ "vertical",0.0714f*1} } },
            { "FLSSJ", new Dictionary<string, object>(){ { "horizontal",0.0625f*3},{ "vertical",0.0714f*10} } },
            { "Evil", new Dictionary<string, object>(){ { "horizontal",0.0625f*3},{ "vertical",0.0714f*11} } },

            { "SSJ1G2", new Dictionary<string, object>(){ { "horizontal",0.0625f*4},{ "vertical",0.0714f*0} } },
            { "SSJ2", new Dictionary<string, object>(){ { "horizontal",0.0625f*4},{ "vertical",0.0714f*1} } },
            { "LSSJ1", new Dictionary<string, object>(){ { "horizontal",0.0625f*4},{ "vertical",0.0714f*10} } },
            { "Rampaging", new Dictionary<string, object>(){ { "horizontal",0.0625f*4},{ "vertical",0.0714f*11} } },

            { "SSJ1G3", new Dictionary<string, object>(){ { "horizontal",0.0625f*5},{ "vertical",0.0714f*0} } },
            { "SSJ3", new Dictionary<string, object>(){ { "horizontal",0.0625f*5},{ "vertical", 0.0714f * 1 } } },
            { "LSSJ2", new Dictionary<string, object>(){ { "horizontal",0.0625f*5},{ "vertical",0.0714f*10} } },
            { "Berserk", new Dictionary<string, object>(){ { "horizontal",0.0625f*5},{ "vertical",0.0714f*11} } },

            { "SSJ4", new Dictionary<string, object>(){ { "horizontal",0.0625f*6},{ "vertical",0.0714f*1} } },
            { "LSSJ3", new Dictionary<string, object>(){ { "horizontal",0.0625f*6},{ "vertical",0.0714f*10} } },

            { "SSJ4LB", new Dictionary<string, object>(){ { "horizontal",0.0625f*7},{ "vertical",0.0714f*1} } },
            { "SSJG", new Dictionary<string, object>(){ { "horizontal",0.0625f*7},{ "vertical",0.0714f*3} } },
            { "LSSJ4", new Dictionary<string, object>(){ { "horizontal",0.0625f*7},{ "vertical",0.0714f*10} } },

            { "SSJ5", new Dictionary<string, object>(){ { "horizontal",0.0625f*8},{ "vertical",0.0714f*1} } },
            { "SSJ5G4", new Dictionary<string, object>(){ { "horizontal",0.0625f*8},{ "vertical",0.0714f*2} } },
            { "FSSJB", new Dictionary<string, object>(){ { "horizontal",0.0625f*8},{ "vertical",0.0714f*3} } },
            { "LSSJ4LB", new Dictionary<string, object>(){ { "horizontal",0.0625f*8},{ "vertical",0.0714f*10} } },

            { "SSJRage", new Dictionary<string, object>(){ { "horizontal",0.0625f*9},{ "vertical",0.0714f*0} } },
            { "SSJB1", new Dictionary<string, object>(){ { "horizontal",0.0625f*9},{ "vertical",0.0714f*3} } },
            { "SSJB1G4", new Dictionary<string, object>(){ { "horizontal",0.0625f*9},{ "vertical",0.0714f*4} } },
            { "SSJR1", new Dictionary<string, object>(){ { "horizontal",0.0625f*9},{ "vertical",0.0714f*6} } },
            { "SSJR1G4", new Dictionary<string, object>(){ { "horizontal",0.0625f*9},{ "vertical",0.0714f*7} } },

            { "SSJ5G2", new Dictionary<string, object>(){ { "horizontal",0.0625f*10},{ "vertical", 0.0714f * 1} } },

            { "SSJ6", new Dictionary<string, object>(){ { "horizontal",0.0625f*11},{ "vertical",0.0714f*2} } },
            { "SSJB1G2", new Dictionary<string, object>(){ { "horizontal",0.0625f*11},{ "vertical",0.0714f*3} } },
            { "LSSJB", new Dictionary<string, object>(){ { "horizontal",0.0625f*11},{ "vertical",0.0714f*5} } },
            { "SSJR1G2", new Dictionary<string, object>(){ { "horizontal",0.0625f*11},{ "vertical",0.0714f*6} } },
            { "LSSJ5", new Dictionary<string, object>(){ { "horizontal",0.0625f*11},{ "vertical",0.0714f*10} } },

            { "SSJB1G3", new Dictionary<string, object>(){ { "horizontal",0.0625f*12},{ "vertical",0.0714f*3} } },
            { "SSJB2", new Dictionary<string, object>(){ { "horizontal",0.0625f*12},{ "vertical",0.0714f*4} } },
            { "SSJR1G3", new Dictionary<string, object>(){ { "horizontal",0.0625f*12},{ "vertical",0.0714f*6} } },
            { "SSJR2", new Dictionary<string, object>(){ { "horizontal",0.0625f*12},{ "vertical", 0.0714f * 7 } } },

            { "SSJ5G3", new Dictionary<string, object>(){ { "horizontal",0.0625f*13},{ "vertical",0.0714f*1} } },

            { "SSJ7", new Dictionary<string, object>(){ { "horizontal",0.0625f*14},{ "vertical",0.0714f*2} } },
            { "SSJBE", new Dictionary<string, object>(){ { "horizontal",0.0625f*14},{ "vertical",0.0714f*3} } },
            { "SSJB3", new Dictionary<string, object>(){ { "horizontal",0.0625f*14},{ "vertical",0.0714f*4} } },
            { "SSJR3", new Dictionary<string, object>(){ { "horizontal",0.0625f*14},{ "vertical",0.0714f*7} } },
            { "DR", new Dictionary<string, object>(){ { "horizontal",0.0625f*14},{ "vertical",0.0714f*8} } },
            { "TUI", new Dictionary<string, object>(){ { "horizontal",0.0625f*14},{ "vertical", 0.0714f * 9 } } },
            { "LSSJ6", new Dictionary<string, object>(){ { "horizontal",0.0625f*14},{ "vertical",0.0714f*10} } },

            { "Beast", new Dictionary<string, object>(){ { "horizontal",0.0625f*15},{ "vertical",0.0714f*12} } },
            { "UE", new Dictionary<string, object>(){ { "horizontal",0.0625f*15},{ "vertical",0.0714f*13} } },

            { "UILB", new Dictionary<string, object>(){ { "horizontal",0.0625f*16},{ "vertical",0.0714f*9} } },
            { "LSSJ7", new Dictionary<string, object>(){ { "horizontal",0.0625f*16},{ "vertical",0.0714f*10} } },
        };

        public Dictionary<string, bool> nameToVisible = new Dictionary<string, bool>() 
        {
            { "choose", false },
            { "unlock", false },
            { "stats", false }
        };


        Dictionary<string, Dictionary<string, object>> nameToFormChooseInfo = new Dictionary<string, Dictionary<string, object>>()
        {

            //horizontal: 0-9
            //vertical: 0-14
            { "baseForm", new Dictionary<string, object>(){ { "horizontal", 0.1f * 0 },{ "vertical", 0.066f * 0 } } },
            { "FSSJ", new Dictionary<string, object>(){ { "horizontal", 0.1f * 1 },{ "vertical", 0.066f * 0 } } },
            { "SSJ1", new Dictionary<string, object>(){ { "horizontal",0.1f*2},{ "vertical",0.066f*0} } },
            { "SSJ1G2", new Dictionary<string, object>(){ { "horizontal",0.1f*3},{ "vertical",0.066f*0} } },
            { "SSJ1G3", new Dictionary<string, object>(){ { "horizontal",0.1f*4},{ "vertical",0.066f*0} } },
            { "SSJ1G4", new Dictionary<string, object>(){ { "horizontal",0.1f*5},{ "vertical",0.066f*0} } },
            { "SSJ2", new Dictionary<string, object>(){ { "horizontal",0.1f*6},{ "vertical",0.066f*0} } },
            { "SSJ3", new Dictionary<string, object>(){ { "horizontal",0.1f*7},{ "vertical", 0.066f * 0 } } },
            { "SSJRage", new Dictionary<string, object>(){ { "horizontal",0.1f*8},{ "vertical",0.066f*0} } },

            { "SSJ4", new Dictionary<string, object>(){ { "horizontal",0.1f*0},{ "vertical",0.066f*1} } },
            { "SSJ4LB", new Dictionary<string, object>(){ { "horizontal",0.1f*1},{ "vertical",0.066f*1} } },
            { "SSJ5", new Dictionary<string, object>(){ { "horizontal",0.1f*2},{ "vertical",0.066f*1} } },
            { "SSJ5G2", new Dictionary<string, object>(){ { "horizontal",0.1f*3},{ "vertical", 0.066f * 1} } },
            { "SSJ5G3", new Dictionary<string, object>(){ { "horizontal",0.1f*4},{ "vertical",0.066f*1} } },
            { "SSJ5G4", new Dictionary<string, object>(){ { "horizontal",0.1f*5},{ "vertical",0.066f*1} } },
            { "SSJ6", new Dictionary<string, object>(){ { "horizontal",0.1f*6},{ "vertical",0.066f*1} } },
            { "SSJ7", new Dictionary<string, object>(){ { "horizontal",0.1f*7},{ "vertical",0.066f*1} } },

            { "Ikari", new Dictionary<string, object>(){ { "horizontal", 0.1f * 0 },{ "vertical", 0.066f * 2} } },
            { "FLSSJ", new Dictionary<string, object>(){ { "horizontal",0.1f*1},{ "vertical",0.066f * 2} } },
            { "LSSJ1", new Dictionary<string, object>(){ { "horizontal",0.1f*2},{ "vertical",0.066f * 2} } },
            { "LSSJ2", new Dictionary<string, object>(){ { "horizontal",0.1f*3},{ "vertical",0.066f * 2} } },
            { "LSSJ3", new Dictionary<string, object>(){ { "horizontal",0.1f*4},{ "vertical",0.066f * 2} } },
            { "LSSJ4", new Dictionary<string, object>(){ { "horizontal",0.1f*5},{ "vertical",0.066f * 2} } },
            { "LSSJ4LB", new Dictionary<string, object>(){ { "horizontal",0.1f*6},{ "vertical",0.066f * 2} } },
            { "LSSJ5", new Dictionary<string, object>(){ { "horizontal",0.1f*7},{ "vertical",0.066f * 2} } },
            { "LSSJ6", new Dictionary<string, object>(){ { "horizontal",0.1f*8},{ "vertical",0.066f * 2} } },
            { "LSSJ7", new Dictionary<string, object>(){ { "horizontal",0.1f*9},{ "vertical",0.066f * 2} } },

            { "SSJG", new Dictionary<string, object>(){ { "horizontal",0.1f*0},{ "vertical",0.066f*3} } },
            { "FSSJB", new Dictionary<string, object>(){ { "horizontal",0.1f*1},{ "vertical",0.066f*3} } },
            { "SSJB1", new Dictionary<string, object>(){ { "horizontal",0.1f*2},{ "vertical",0.066f*3} } },
            { "SSJB1G2", new Dictionary<string, object>(){ { "horizontal",0.1f*3},{ "vertical",0.066f*3} } },
            { "SSJB1G3", new Dictionary<string, object>(){ { "horizontal",0.1f*4},{ "vertical",0.066f*3} } },
            { "SSJB1G4", new Dictionary<string, object>(){ { "horizontal",0.1f*5},{ "vertical",0.066f*3} } },
            { "SSJB2", new Dictionary<string, object>(){ { "horizontal",0.1f*6},{ "vertical",0.066f*3} } },
            { "SSJB3", new Dictionary<string, object>(){ { "horizontal",0.1f*7},{ "vertical",0.066f*3} } },
            { "SSJBE", new Dictionary<string, object>(){ { "horizontal",0.1f*8},{ "vertical",0.066f*3} } },
            { "LSSJB", new Dictionary<string, object>(){ { "horizontal",0.1f*9},{ "vertical",0.066f * 3} } },

            { "SSJR1", new Dictionary<string, object>(){ { "horizontal",0.1f*0},{ "vertical",0.066f*4} } },
            { "SSJR1G2", new Dictionary<string, object>(){ { "horizontal",0.1f*1},{ "vertical",0.066f*4} } },
            { "SSJR1G3", new Dictionary<string, object>(){ { "horizontal",0.1f*2},{ "vertical",0.066f*4} } },
            { "SSJR1G4", new Dictionary<string, object>(){ { "horizontal",0.1f*3},{ "vertical",0.066f*4} } },
            { "SSJR2", new Dictionary<string, object>(){ { "horizontal",0.1f*4},{ "vertical", 0.066f * 4 } } },
            { "SSJR3", new Dictionary<string, object>(){ { "horizontal",0.1f*5},{ "vertical",0.066f*4} } },
            { "Divine", new Dictionary<string, object>(){ { "horizontal",0.1f*6},{ "vertical",0.066f*4} } },
            { "DR", new Dictionary<string, object>(){ { "horizontal",0.1f*7},{ "vertical",0.066f*4} } },

            { "Evil", new Dictionary<string, object>(){ { "horizontal",0.1f*0},{ "vertical",0.066f*5} } },
            { "Rampaging", new Dictionary<string, object>(){ { "horizontal",0.1f*1},{ "vertical",0.066f*5} } },
            { "Berserk", new Dictionary<string, object>(){ { "horizontal",0.1f*2},{ "vertical",0.066f*5} } },

            { "UI", new Dictionary<string, object>(){ { "horizontal", 0.1f * 0 },{ "vertical", 0.066f * 6 } } },
            { "TUI", new Dictionary<string, object>(){ { "horizontal",0.1f*1},{ "vertical", 0.066f * 6 } } },
            { "UILB", new Dictionary<string, object>(){ { "horizontal",0.1f*2},{ "vertical",0.066f*6} } },

            { "PU", new Dictionary<string, object>(){ { "horizontal",0.1f*0},{ "vertical",0.066f*7} } },
            { "Beast", new Dictionary<string, object>(){ { "horizontal",0.1f*1},{ "vertical",0.066f*7} } },

            { "UE", new Dictionary<string, object>(){ { "horizontal",0.1f*0},{ "vertical",0.066f*8} } },







































        };


        public static List<string> forms = new List<string>() { "FSSJ","FSSJB","SSJ1","SSJ1G2","SSJ1G3","SSJ1G4","SSJ2","SSJ3","SSJRage","SSJ4","SSJ4LB","SSJ5","SSJ5G2","SSJ5G3","SSJ5G4","SSJ6","SSJ7","FLSSJ","Ikari","LSSJ1","LSSJ2","LSSJ3","LSSJ4","LSSJ4LB","LSSJ5","LSSJ6","LSSJ7","SSJG","LSSJB","SSJB1","SSJB1G2","SSJB1G3","SSJB1G4","SSJB2","SSJB3","SSJBE","SSJR1","SSJR1G2","SSJR1G3","SSJR1G4","SSJR2","SSJR3","Divine","DR","Evil","Rampaging","Berserk","PU","Beast","UE","UI","UILB","TUI"
        };
        public static List<string> treeStarterForms = new List<string>() {};
        //"FSSJ", "SSJG", "UI", "Ikari", "Evil", "PU", "UE" 
        //FSSJ: when you spawn a boss
        //SSJG: when getting ssj3/lssj3/berserk
        //UI: when getting ssjg
        //Ikari: get 1000 ki max
        //Evil: when kill bunny
        //PU: once level 5
        //UE: when getting ssjg
        public List<string> visibleUnlocks = new List<string>();

        public override void OnInitialize()
        {
            Color borderMain = Colors.Orchid;
            Color backgroundMain = Colors.Pink;
            Color borderRespec = Colors.Teal;
            Color backgroundRespec = Colors.Turquoise;




            formsPanel = new UIPanel();
            backgroundMain.A = formsPanel.BackgroundColor.A;
            backgroundRespec.A = formsPanel.BackgroundColor.A;
            formsPanel.Top.Set(0, .1f); //100
            formsPanel.Height.Set(0, .6f); // 600
            formsPanel.Left.Set(0, .32f); //600
            formsPanel.Width.Set(0, .48f); //900
            formsPanel.BorderColor = borderMain;
            formsPanel.BackgroundColor = backgroundMain;
            Append(formsPanel);

            //1875 
            //995

            statsPanel = new UIPanel();
            statsPanel.Top.Set(0, .7f); //700
            statsPanel.Height.Set(0, .256f); //250
            statsPanel.Left.Set(0, .32f); //600
            statsPanel.Width.Set(650, 0); //900
            statsPanel.BorderColor = borderMain;
            statsPanel.BackgroundColor = backgroundMain;
            Append(statsPanel);

            formChooserPanel = new UIPanel();
            formChooserPanel.Top.Set(0, .3f); //500
            formChooserPanel.Height.Set(0, .65f); //450
            formChooserPanel.Left.Set(0, .05f); //100
            formChooserPanel.Width.Set(0, .24f); //450
            formChooserPanel.MaxHeight.Set(0, .65f); //450
            formChooserPanel.MaxWidth.Set(0, .24f); //450
            formChooserPanel.BorderColor = borderMain;
            formChooserPanel.BackgroundColor = backgroundMain;
            Append(formChooserPanel);

            //respec stats button///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            respecStatsButton = new UIPanel();
            respecStatsButton.OnLeftClick += OnRespecButtonClick;
            respecStatsButton.Height.Set(35, 0);
            respecStatsButton.Width.Set(35, 0);
            respecStatsButton.HAlign = .4f;
            respecStatsButton.VAlign = .9f;
            respecStatsButton.BorderColor = borderRespec;
            respecStatsButton.BackgroundColor = backgroundRespec;
            statsPanel.Append(respecStatsButton);



            statsPanelText = new UIText("Stats Panel!"); // 1
            statsPanelText.Left.Set(0, 2.5f);
            //statsPanelText.HAlign = 1f; // 1
            statsPanelText.VAlign = 0.5f; // 1
            respecStatsButton.Append(statsPanelText);

            formChooserText = new UIText("Form Chooser Panel!"); // 1
            formChooserText.HAlign = 0.5f; // 1
            formChooserText.VAlign = 1f; // 1
            formChooserPanel.Append(formChooserText);

            formsPanelText = new UIText("Form Unlock Panel!"); // 1
            formsPanelText.HAlign = 0.5f; // 1
            formsPanelText.VAlign = 0.0f; // 1
            //formsPanelText.Top.Set(-100, 0);
            formsPanel.Append(formsPanelText);

            formInfoText = new UIText("Form Info:\nForm Info:\nForm Info:\nForm Info:\nForm Info:\nForm Info:\nForm Info:");
            formInfoText.HAlign = .6f;
            formInfoText.VAlign = 0;
            statsPanel.Append(formInfoText);





            //UIGrid formsGrid = new UIGrid();
            //formsGrid.Height = formChooserPanel.Height;
            //formsGrid.Width = formChooserPanel.Width;
            //formChooserPanel.Append(formsGrid);

            FormButton baseFormButton = new FormButton("baseForm", ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/BuffTemplate"), false);
            Dictionary<string, object> baseFormInfo = nameToFormChooseInfo["baseForm"];
            baseFormButton.VAlign = (float)baseFormInfo["vertical"];
            baseFormButton.HAlign = (float)baseFormInfo["horizontal"];
            formChooserPanel.Append(baseFormButton);

            foreach (string form in forms)
            {
                createFormButtons(form);
                //nameToFormUnlockButton[form].Activate();
            }

            foreach (string form in treeStarterForms)
            {
                visibleUnlocks.Add(form);
            }

            foreach (GUIArrow arrow in unlockArrows)
            {
                formsPanel.Append(arrow);
            }









            //unlockForm("SSJ1");
            //unlockForm("FSSJ");

            kiMaxStatButton = new StatButton("Increase", "maxKi", 100);
            kiMaxStatButton.HAlign = 0;
            kiMaxStatButton.VAlign = 0;
            kiMaxStatButton.BorderColor = borderMain;
            kiMaxStatButton.BackgroundColor = backgroundMain;
            statsPanel.Append(kiMaxStatButton);

            chargeKiGainStatButton = new StatButton("Increase", "chargeKiGain", 1);
            chargeKiGainStatButton.HAlign = 0;
            chargeKiGainStatButton.VAlign = .3f;
            chargeKiGainStatButton.BorderColor = borderMain;
            chargeKiGainStatButton.BackgroundColor = backgroundMain;
            statsPanel.Append(chargeKiGainStatButton);

            kiGainStatButton = new StatButton("Increase", "kiGain", .1f);
            kiGainStatButton.HAlign = .5f;
            kiGainStatButton.VAlign = 0;
            kiGainStatButton.BorderColor = borderMain;
            kiGainStatButton.BackgroundColor = backgroundMain;
            statsPanel.Append(kiGainStatButton);

            baseDefenseButton = new StatButton("Increase", "baseDefense", .25f);
            baseDefenseButton.HAlign = .5f;
            baseDefenseButton.VAlign = .3f;
            baseDefenseButton.BorderColor = borderMain;
            baseDefenseButton.BackgroundColor = backgroundMain;
            statsPanel.Append(baseDefenseButton);

            baseAttackButton = new StatButton("Increase", "baseAttack", .01f);
            baseAttackButton.HAlign = 0f;
            baseAttackButton.VAlign = .6f;
            baseAttackButton.BorderColor = borderMain;
            baseAttackButton.BackgroundColor = backgroundMain;
            statsPanel.Append(baseAttackButton);

            baseSpeedButton = new StatButton("Increase", "baseSpeed", .01f);
            baseSpeedButton.HAlign = .5f;
            baseSpeedButton.VAlign = .6f;
            baseSpeedButton.BorderColor = borderMain;
            baseSpeedButton.BackgroundColor = backgroundMain;
            statsPanel.Append(baseSpeedButton);

            //baseDefenseButton;
            //baseAttackButton;
            //baseSpeedButton;



            /*formMultiplyKi = new StatButton("Increase", "SSJ1FormMultKi", .1f);
            formMultiplyKi.HAlign = 0;
            formMultiplyKi.VAlign = 0;
            statsPanel.Append(formMultiplyKi);
            formDivideDrain = new StatButton("Increase", "SSJ1FormDivideDrain", .1f);
            formDivideDrain.HAlign = 0;
            formDivideDrain.VAlign = .3f;
            statsPanel.Append(formDivideDrain);
            formIncreaseSpeed = new StatButton("Increase", "SSJ1FormMultSpeed", .1f);
            formIncreaseSpeed.HAlign = .5f;
            formIncreaseSpeed.VAlign = 0;
            statsPanel.Append(formIncreaseSpeed);
             */

            formIncreaseDamage = new StatButton("Increase", "SSJ1FormMultDamage", .1f);
            formIncreaseDamage.HAlign = 0;
            formIncreaseDamage.VAlign = 0;
            formIncreaseDamage.BorderColor = borderMain;
            formIncreaseDamage.BackgroundColor = backgroundMain;
            statsPanel.Append(formIncreaseDamage);

            formIncreaseDefense = new StatButton("Increase", "SSJ1FormMultDefense", .1f);
            formIncreaseDefense.HAlign = 0f;
            formIncreaseDefense.VAlign = .3f;
            formIncreaseDefense.BorderColor = borderMain;
            formIncreaseDefense.BackgroundColor = backgroundMain;
            statsPanel.Append(formIncreaseDefense);

            formSpecial = new StatButton("Increase", "SSJ1FormSpecial", .1f);
            formSpecial.HAlign = 0f;
            formSpecial.VAlign = .6f;
            formSpecial.BorderColor = borderMain;
            formSpecial.BackgroundColor = backgroundMain;
            statsPanel.Append(formSpecial);

            //addFormStatButtons("SSJ1");

            addBaseStatButtons();
            addFormStatButtons("SSJ1");
            //removeFormStatButtons();

            //removeBaseStatButtons();
            //addFormStatButtons("SSJ1");
            //modPlayer.setSelectedForm(this.name);
            //modSystem.MyFormsStatsUI.addStatButtons(modPlayer.getSelectedFormID());
            HideMyUI("choose");
            HideMyUI("unlock");
            HideMyUI("stats");
        }


        public void createFormButtons(string form)
        {
            FormButton formUnlockButton = new FormButton(form, ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/" + form + "Buff"), true);

            Dictionary<string, object> formUnlockTreeInfo = nameToUnlockTreeInfo[form];

            float VAlignFormUnlock = (float)formUnlockTreeInfo["vertical"];
            float HAlignFormUnlock = (float)formUnlockTreeInfo["horizontal"];
            //List<GUIArrow> arrows = (List<GUIArrow>)formUnlockTreeInfo["arrows"];

            formUnlockButton.HAlign = HAlignFormUnlock;
            formUnlockButton.VAlign = VAlignFormUnlock;
            //arrows.ForEach(formUnlockButton.getIcon().Append);

            formsPanel.Append(formUnlockButton);
            formUnlockButton.Activate();
            nameToFormUnlockButton.Add(form, formUnlockButton);

            FormButton formChooseButton = new FormButton(form, ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/" + form + "Buff"), false);

            Dictionary<string, object> formChooseInfo = nameToFormChooseInfo[form];
            float VAlignFormChoose = (float)formChooseInfo["vertical"];
            float HAlignFormChoose = (float)formChooseInfo["horizontal"];


            formChooseButton.HAlign = HAlignFormChoose;
            formChooseButton.VAlign = VAlignFormChoose;

            formChooserPanel.Append(formChooseButton);
            formChooseButton.Activate();
            nameToFormChooseButton.Add(form, formChooseButton);
        }

        public void unlockForm(string form)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (!modPlayer.unlockedForms.Contains(form))
            {
                Main.NewText("Unlocked " + form);
                modPlayer.printToLog("Unlocked "+form);
                modPlayer.unlockedForms.Add(form);
            }
            if (!visibleUnlocks.Contains(form))
            {
                visibleUnlocks.Add(form);
            }
            List<GUIArrow> arrows = getArrowsOriginIsForm(form);
            foreach (GUIArrow arrow in arrows)
            {
                if (!visibleUnlocks.Contains(arrow.destinationForm))
                {
                    visibleUnlocks.Add(arrow.destinationForm);
                }
            }
            if((form == "SSJ3" || form == "LSSJ3" || form == "Berserk") && !modSystem.MyFormsStatsUI.visibleUnlocks.Contains("SSJG"))
            {
                modSystem.MyFormsStatsUI.visibleUnlocks.Add("SSJG");
            }
            if((form == "SSJG") && !modSystem.MyFormsStatsUI.visibleUnlocks.Contains("UI"))
            {
                modSystem.MyFormsStatsUI.visibleUnlocks.Add("UI");
            }
            if((form == "SSJG") && !modSystem.MyFormsStatsUI.visibleUnlocks.Contains("UE"))
            {
                modSystem.MyFormsStatsUI.visibleUnlocks.Add("UE");
            }
        }

        public void addBaseStatButtons()
        {
            statsPanel.Append(kiMaxStatButton); statsPanel.Append(chargeKiGainStatButton); statsPanel.Append(kiGainStatButton); statsPanel.Append(baseAttackButton); statsPanel.Append(baseDefenseButton); statsPanel.Append(baseSpeedButton);
        }

        public void removeBaseStatButtons()
        {
            kiMaxStatButton.Remove(); chargeKiGainStatButton.Remove(); kiGainStatButton.Remove(); baseAttackButton.Remove(); baseDefenseButton.Remove(); baseSpeedButton.Remove();
        }

        public void addFormStatButtons(string formName)
        {

            formIncreaseDamage.statName = formName + "FormMultDamage";

            formIncreaseDefense.statName = formName + "FormMultDefense";
            formSpecial.statName = formName + "FormSpecial";
            statsPanel.Append(formIncreaseDamage); statsPanel.Append(formIncreaseDefense); statsPanel.Append(formSpecial);//statsPanel.Append(formMultiplyKi); statsPanel.Append(formDivideDrain); statsPanel.Append(formIncreaseSpeed);
            statsPanel.Append(formInfoText);
        }

        public void removeFormStatButtons()
        {
            formIncreaseDamage.Remove(); formIncreaseDefense.Remove(); formSpecial.Remove(); //formMultiplyKi.Remove(); formDivideDrain.Remove(); formIncreaseSpeed.Remove();
            formInfoText.Remove();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            // If this code is in the panel or container element, check it directly
            if (formsPanel.ContainsPoint(Main.MouseScreen) && nameToVisible["unlock"])
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (formChooserPanel.ContainsPoint(Main.MouseScreen) && nameToVisible["choose"])
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (statsPanel.ContainsPoint(Main.MouseScreen) && nameToVisible["stats"])
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            foreach (UIElement element in Elements)
            {
                if(element.Equals(formsPanel) && nameToVisible["unlock"])
                {
                    element.Draw(spriteBatch);
                }
                if (element.Equals(formChooserPanel) && nameToVisible["choose"])
                {
                    element.Draw(spriteBatch);
                }
                if (element.Equals(statsPanel) && nameToVisible["stats"])
                {
                    element.Draw(spriteBatch);
                }
            }
            //base.DrawChildren(spriteBatch);
        }

        public void OnRespecButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (isFormButtons)
            {

                string selectedForm = modPlayer.getSelectedForm();
                modPlayer.nameToStats[selectedForm].respec();
            }
            else
            {
                modPlayer.respec();
            }
        }

        private List<GUIArrow> getArrowsOriginIsForm(string form)
        {
            List<GUIArrow> toReturn = new List<GUIArrow>();
            foreach (GUIArrow arrow in unlockArrows)
            {
                if (arrow.originForm == null && arrow.destinationForm == form)
                {
                    toReturn.Add(arrow);
                }
                else if (arrow.originForm == form)
                {
                    toReturn.Add(arrow);
                }
            }

            return toReturn;
        }

        private List<GUIArrow> getArrowsDestinationIsForm(string form)
        {
            List<GUIArrow> toReturn = new List<GUIArrow>();
            foreach (GUIArrow arrow in unlockArrows)
            {
                if (arrow.destinationForm == form)
                {
                    toReturn.Add(arrow);
                }
            }

            return toReturn;
        }

        public void addStatButtons(int buffID)
        {
            if (buffID == -1)
            {
                removeFormStatButtons();
                addBaseStatButtons();
                isFormButtons = false;
            }
            else
            {
                removeBaseStatButtons();
                string formName = FormTree.IDToFormName(buffID);
                addFormStatButtons(formName);
                isFormButtons = true;
            }
        }

        public int round(float num)
        {
            return (int)Math.Round(num);
        }

        public float roundTens(float num)
        {
            return (float)((Math.Round(num * 10)) / 10f);
        }

        public static float invertFraction(float num)
        {
            return 1 / num;
        }

        public string formSpecialDisplayText()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            string selectedForm = modPlayer.getSelectedForm();
            FormStats stats = modPlayer.nameToStats[selectedForm];
            string toReturn = "x\nSpecial: ";
            //List<string> special = FormTree.nameToSpecial[selectedForm];
            List<string> special = FormTree.getSpecial(selectedForm); 
            string specialName = special[0];
            string specialValue = special[1];
            float specialValueFloat;
            float specialValueFloatInverted;
            switch (specialName)
            {
                case "Kaio-Efficient":
                    specialValueFloat = (float)Double.Parse(specialValue);
                    specialValueFloatInverted = invertFraction(stats.Special.getValue() * specialValueFloat);
                    toReturn += "Kaioken Cost Reduced: " + roundTens((1 - specialValueFloatInverted) * 100) + "%";
                    break;
                case "Ki Power":
                    toReturn += specialName + " " + specialValue;
                    break;
                case "HP Power":
                    toReturn += specialName + " " + specialValue;
                    break;
                case "Dodge":
                    toReturn += specialName + " Type " + specialValue;
                    break;
                case "Regen":
                    specialValueFloat = (float)Double.Parse(specialValue);
                    toReturn += roundTens(stats.Special.getValue() * specialValueFloat) + " Life Regen";
                    break;
                case "DR":
                    specialValueFloat = (float)Double.Parse(specialValue);
                    toReturn += roundTens(stats.Special.getValue() * specialValueFloat) + "% Defense Effectiveness";
                    break;
                case "Ki Attack Master":
                    specialValueFloat = (float)Double.Parse(specialValue);
                    specialValueFloatInverted = invertFraction(stats.Special.getValue() * specialValueFloat);
                    toReturn += "Ki Cooldown Reduced: "+roundTens((1-specialValueFloatInverted)*100)+"%";
                    break;
                case "Aura Defense":
                    toReturn += specialValue + " Defense While Charging";
                    break;
                case "Special Compatibility":
                    specialValueFloat = (float)Double.Parse(specialValue);
                    specialValueFloatInverted = invertFraction(stats.Special.getValue() * specialValueFloat);
                    toReturn += "Stacking Cost Reduced: " + roundTens((1 - specialValueFloatInverted) * 100) + "%";
                    break;
                case "Second Wind":
                    specialValueFloat = (float)Double.Parse(specialValue);
                    toReturn += "Second Wind: Level " + roundTens(stats.Special.getValue() * specialValueFloat);
                    break;
                case "Frantic Ki Regen":
                    specialValueFloat = (float)Double.Parse(specialValue);
                    toReturn += "Frantic Ki Regen: Level " + roundTens(stats.Special.getValue() * specialValueFloat);
                    break;
                default:
                    toReturn = "";
                    break;
            }
        
            //toReturn = "x\nSpecial(Not Implemented): " + roundTens(stats.Special.getValue());
            return toReturn;
            }



        public void ShowMyUI(string name)
        {
            nameToVisible[name] = true;
            
        }

        public void HideMyUI(string name)
        {
            nameToVisible[name] = false;
        }

        public void switchVisibility(string name)
        {
            if (nameToVisible[name])
            {
                HideMyUI(name);
            }
            else
            {
                ShowMyUI(name);
            }
        }

        public override void Update(GameTime gameTime)
        {



            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (modPlayer == null)
            {
                return;
            }


            if (modPlayer.unlockLoaded.Count > 0)
            {
                foreach (string form in FormTree.forms)
                {
                    if (!modPlayer.unlockLoaded.Contains(form) && !treeStarterForms.Contains(form))
                    {
                        visibleUnlocks.Remove(form);
                    }
                }
                foreach (string form in modPlayer.unlockLoaded)
                {
                    unlockForm(form);
                }
                modPlayer.unlockLoaded.Clear();
            }

            if (modPlayer.loadVisibleUnlocks.Count > 0)
            {
                visibleUnlocks.AddRange(modPlayer.loadVisibleUnlocks);
                modPlayer.loadVisibleUnlocks.Clear();
            }
            base.Update(gameTime);
            string newStatsPanelText = "";
            string newUnlockPanelText = "";
            string newFormInfoText = "";
            int selectedFormID = modPlayer.getSelectedFormID();
            string selectedForm = modPlayer.getSelectedForm();
            if(selectedForm == null)
            {
                return;
            }
            if (selectedFormID != -1 && modPlayer.nameToStats.Keys.Contains(selectedForm))
            {
                FormStats stats = modPlayer.nameToStats[selectedForm];
                //newStatsPanelText = "<- Respec " + selectedForm + ", level " + stats.getLevel() + ": " + stats.getPoints() + " Points! To next: " + round(stats.getExperience()) + "/" + round(stats.expNeededToAdvanceLevel());
                newStatsPanelText = "Respec " +selectedForm+ "(" + stats.getPoints() + "/" + stats.getLevel() + "), (" + round(stats.getExperience()) + "/" + round(stats.expNeededToAdvanceLevel()) + ")";
                newFormInfoText = "Drain: " + roundTens((FormTree.nameToKiDrain[selectedForm] / stats.DivideDrain.getValue()) - modPlayer.kiGain.getValue()) + " ki/s\nMultiply Ki: " + stats.MultKi.getValue() + "x\nDamage: " + roundTens(FormTree.nameToDamageBonus[selectedForm] * stats.MultDamage.getValue()) + "x\nDefense: +" + ((int)(FormTree.nameToDefenseBonus[selectedForm] * stats.MultDefense.getValue())) + "\nSpeed: " + roundTens(FormTree.nameToSpeedBonus[selectedForm] * stats.MultSpeed.getValue()) + formSpecialDisplayText();
            }
            else 
            {
                newStatsPanelText = "Respec " + "Base Form(" +modPlayer.getPoints()+ "/" + modPlayer.getLevel() + "), (" + round(modPlayer.getExperience()) + "/" + round(modPlayer.expNeededToAdvanceLevel())+")";
            }
            newUnlockPanelText = "Last Hovered Over Form: "+formHoverText+"     Form Points: "+modPlayer.getFormPoints();
            statsPanelText.SetText(newStatsPanelText,.8f,false);
            formsPanelText.SetText(newUnlockPanelText,.8f,false);
            formInfoText.SetText(newFormInfoText, 1f, false);
            formInfoText.HAlign = .8f;
        }
    }
}
