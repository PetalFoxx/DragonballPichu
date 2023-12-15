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
        public UIGrid formChooserPanel;

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
            { "FSSJ", new Dictionary<string, object>(){ { "vAlign", 0.0625f * 1 },{ "hAlign", 0.0714f * 0 } } },
            { "UI", new Dictionary<string, object>(){ { "vAlign", 0.0625f * 1 },{ "hAlign", 0.0714f * 9 } } },
            { "Ikari", new Dictionary<string, object>(){ { "vAlign", 0.0625f * 1 },{ "hAlign", 0.0714f * 10 } } },

            { "Divine", new Dictionary<string, object>(){ { "vAlign",0.0625f*2},{ "hAlign",0.0714f*7} } },
            { "PU", new Dictionary<string, object>(){ { "vAlign",0.0625f*2},{ "hAlign",0.0714f*12} } },

            { "SSJ1", new Dictionary<string, object>(){ { "vAlign",0.0625f*3},{ "hAlign",0.0714f*0} } },
            { "SSJ1G4", new Dictionary<string, object>(){ { "vAlign",0.0625f*3},{ "hAlign",0.0714f*1} } },
            { "FLSSJ", new Dictionary<string, object>(){ { "vAlign",0.0625f*3},{ "hAlign",0.0714f*10} } },
            { "Evil", new Dictionary<string, object>(){ { "vAlign",0.0625f*3},{ "hAlign",0.0714f*11} } },

            { "SSJ1G2", new Dictionary<string, object>(){ { "vAlign",0.0625f*4},{ "hAlign",0.0714f*0} } },
            { "SSJ2", new Dictionary<string, object>(){ { "vAlign",0.0625f*4},{ "hAlign",0.0714f*1} } },
            { "LSSJ1", new Dictionary<string, object>(){ { "vAlign",0.0625f*4},{ "hAlign",0.0714f*10} } },
            { "Rampaging", new Dictionary<string, object>(){ { "vAlign",0.0625f*4},{ "hAlign",0.0714f*11} } },
                                                                                                               
            { "SSJ1G3", new Dictionary<string, object>(){ { "vAlign",0.0625f*5},{ "hAlign",0.0714f*0} } },
            { "SSJ3", new Dictionary<string, object>(){ { "vAlign",0.0625f*5},{ "hAlign", 0.0714f * 1 } } },
            { "LSSJ2", new Dictionary<string, object>(){ { "vAlign",0.0625f*5},{ "hAlign",0.0714f*10} } },
            { "Berserk", new Dictionary<string, object>(){ { "vAlign",0.0625f*5},{ "hAlign",0.0714f*11} } },

            { "SSJ4", new Dictionary<string, object>(){ { "vAlign",0.0625f*6},{ "hAlign",0.0714f*1} } },
            { "LSSJ3", new Dictionary<string, object>(){ { "vAlign",0.0625f*6},{ "hAlign",0.0714f*10} } },
                                                                                                    
            { "SSJ4LB", new Dictionary<string, object>(){ { "vAlign",0.0625f*7},{ "hAlign",0.0714f*1} } },
            { "SSJG", new Dictionary<string, object>(){ { "vAlign",0.0625f*7},{ "hAlign",0.0714f*3} } },
            { "LSSJ4", new Dictionary<string, object>(){ { "vAlign",0.0625f*7},{ "hAlign",0.0714f*10} } },
                                                                                                    
            { "SSJ5", new Dictionary<string, object>(){ { "vAlign",0.0625f*8},{ "hAlign",0.0714f*1} } },
            { "SSJ5G4", new Dictionary<string, object>(){ { "vAlign",0.0625f*8},{ "hAlign",0.0714f*2} } },
            { "FSSJB", new Dictionary<string, object>(){ { "vAlign",0.0625f*8},{ "hAlign",0.0714f*3} } },
            { "LSSJ4LB", new Dictionary<string, object>(){ { "vAlign",0.0625f*8},{ "hAlign",0.0714f*10} } },
                                                                                                    
            { "SSJRage", new Dictionary<string, object>(){ { "vAlign",0.0625f*9},{ "hAlign",0.0714f*0} } }, 
            { "SSJB1", new Dictionary<string, object>(){ { "vAlign",0.0625f*9},{ "hAlign",0.0714f*3} } },
            { "SSJB1G4", new Dictionary<string, object>(){ { "vAlign",0.0625f*9},{ "hAlign",0.0714f*4} } },
            { "SSJR1", new Dictionary<string, object>(){ { "vAlign",0.0625f*9},{ "hAlign",0.0714f*6} } },
            { "SSJR1G4", new Dictionary<string, object>(){ { "vAlign",0.0625f*9},{ "hAlign",0.0714f*7} } },
                                                                                                    
            { "SSJ5G2", new Dictionary<string, object>(){ { "vAlign",0.0625f*10},{ "hAlign", 0.0714f * 1} } },
                                                                                                    
            { "SSJ6", new Dictionary<string, object>(){ { "vAlign",0.0625f*11},{ "hAlign",0.0714f*2} } },
            { "SSJB1G2", new Dictionary<string, object>(){ { "vAlign",0.0625f*11},{ "hAlign",0.0714f*3} } },
            { "LSSJB", new Dictionary<string, object>(){ { "vAlign",0.0625f*11},{ "hAlign",0.0714f*5} } },  
            { "SSJR1G2", new Dictionary<string, object>(){ { "vAlign",0.0625f*11},{ "hAlign",0.0714f*6} } },
            { "LSSJ5", new Dictionary<string, object>(){ { "vAlign",0.0625f*11},{ "hAlign",0.0714f*10} } },
                                                                                                    
            { "SSJB1G3", new Dictionary<string, object>(){ { "vAlign",0.0625f*12},{ "hAlign",0.0714f*3} } },
            { "SSJB2", new Dictionary<string, object>(){ { "vAlign",0.0625f*12},{ "hAlign",0.0714f*4} } },
            { "SSJR1G3", new Dictionary<string, object>(){ { "vAlign",0.0625f*12},{ "hAlign",0.0714f*6} } },
            { "SSJR2", new Dictionary<string, object>(){ { "vAlign",0.0625f*12},{ "hAlign", 0.0714f * 7 } } },
                                                                                                    
            { "SSJ5G3", new Dictionary<string, object>(){ { "vAlign",0.0625f*13},{ "hAlign",0.0714f*1} } }, 
                                                                                                    
            { "SSJ7", new Dictionary<string, object>(){ { "vAlign",0.0625f*14},{ "hAlign",0.0714f*2} } },   
            { "SSJBE", new Dictionary<string, object>(){ { "vAlign",0.0625f*14},{ "hAlign",0.0714f*3} } },  
            { "SSJB3", new Dictionary<string, object>(){ { "vAlign",0.0625f*14},{ "hAlign",0.0714f*4} } },  
            { "SSJR3", new Dictionary<string, object>(){ { "vAlign",0.0625f*14},{ "hAlign",0.0714f*7} } },  
            { "DR", new Dictionary<string, object>(){ { "vAlign",0.0625f*14},{ "hAlign",0.0714f*8} } },     
            { "TUI", new Dictionary<string, object>(){ { "vAlign",0.0625f*14},{ "hAlign", 0.0714f * 9 } } },
            { "LSSJ6", new Dictionary<string, object>(){ { "vAlign",0.0625f*14},{ "hAlign",0.0714f*10} } },
                                                                                                          
            { "Beast", new Dictionary<string, object>(){ { "vAlign",0.0625f*15},{ "hAlign",0.0714f*12} } },       
            { "UE", new Dictionary<string, object>(){ { "vAlign",0.0625f*15},{ "hAlign",0.0714f*13} } },
                                                                                                               
            { "UILB", new Dictionary<string, object>(){ { "vAlign",0.0625f*16},{ "hAlign",0.0714f*9} } },              
            { "LSSJ7", new Dictionary<string, object>(){ { "vAlign",0.0625f*16},{ "hAlign",0.0714f*10} } },            
        };


        public static List<string> forms = new List<string>() { "FSSJ","FSSJB","SSJ1","SSJ1G2","SSJ1G3","SSJ1G4","SSJ2","SSJ3","SSJRage","SSJ4","SSJ4LB","SSJ5","SSJ5G2","SSJ5G3","SSJ5G4","SSJ6","SSJ7","FLSSJ","Ikari","LSSJ1","LSSJ2","LSSJ3","LSSJ4","LSSJ4LB","LSSJ5","LSSJ6","LSSJ7","SSJG","LSSJB","SSJB1","SSJB1G2","SSJB1G3","SSJB1G4","SSJB2","SSJB3","SSJBE","SSJR1","SSJR1G2","SSJR1G3","SSJR1G4","SSJR2","SSJR3","Divine","DR","Evil","Rampaging","Berserk","PU","Beast","UE","UI","UILB","TUI"
        };
        public static List<string> treeStarterForms = new List<string>() { "FSSJ", "SSJG", "UI", "Ikari", "Evil", "PU", "UE" };

        public List<string> visibleUnlocks = new List<string>();

        public override void OnInitialize()
        { 
            formsPanel = new UIPanel();
            formsPanel.Top.Set(0, .1f); //100
            formsPanel.Height.Set(0, .6f); // 600
            formsPanel.Left.Set(0, .32f); //600
            formsPanel.Width.Set(0, .48f); //900
            Append(formsPanel);

            //1875 
            //995

            statsPanel = new UIPanel();
            statsPanel.Top.Set(0, .7f); //700
            statsPanel.Height.Set(0, .256f); //250
            statsPanel.Left.Set(0, .32f); //600
            statsPanel.Width.Set(650, 0); //900
            Append(statsPanel);

            formChooserPanel = new UIGrid();
            formChooserPanel.Top.Set(0, .4f); //500
            formChooserPanel.Height.Set(0, .46f); //450
            formChooserPanel.Left.Set(0, .05f); //100
            formChooserPanel.Width.Set(0, .24f); //450
            formChooserPanel.MaxHeight.Set(0, .6f); //450
            formChooserPanel.MaxWidth.Set(0, .24f); //450
            Append(formChooserPanel);


            statsPanelText = new UIText("Stats Panel!"); // 1
            statsPanelText.HAlign = 0.5f; // 1
            statsPanelText.VAlign = 0.9f; // 1
            statsPanel.Append(statsPanelText);

            formChooserText = new UIText("Form Chooser Panel!"); // 1
            formChooserText.HAlign = 0.5f; // 1
            formChooserText.VAlign = 0.5f; // 1
            formChooserPanel.Append(formChooserText);

            formsPanelText = new UIText("Form Unlock Panel!"); // 1
            formsPanelText.HAlign = 0.5f; // 1
            formsPanelText.VAlign = 0.05f; // 1
            //formsPanelText.Top.Set(-100, 0);
            Append(formsPanelText);

            formInfoText = new UIText("Form Info:\nForm Info:\nForm Info:\nForm Info:\nForm Info:\nForm Info:\nForm Info:");
            formInfoText.HAlign = .6f;
            formInfoText.VAlign = 0;
            statsPanel.Append(formInfoText);


            //respec stats button///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            respecStatsButton = new UIPanel();
            respecStatsButton.OnLeftClick += OnRespecButtonClick;
            respecStatsButton.Height.Set(35,0);
            respecStatsButton.Width.Set(35,0);
            respecStatsButton.HAlign = .05f;
            respecStatsButton.VAlign = .9f;
            statsPanel.Append(respecStatsButton);


            //UIGrid formsGrid = new UIGrid();
            //formsGrid.Height = formChooserPanel.Height;
            //formsGrid.Width = formChooserPanel.Width;
            //formChooserPanel.Append(formsGrid);

            FormButton baseFormButton = new FormButton("baseForm", ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/BuffTemplate"), false);
            formChooserPanel.Add(baseFormButton);

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
            statsPanel.Append(kiMaxStatButton);

            chargeKiGainStatButton = new StatButton("Increase", "chargeKiGain", 1);
            chargeKiGainStatButton.HAlign = 0;
            chargeKiGainStatButton.VAlign = .3f;
            statsPanel.Append(chargeKiGainStatButton);

            kiGainStatButton = new StatButton("Increase", "kiGain", .1f);
            kiGainStatButton.HAlign = .5f;
            kiGainStatButton.VAlign = 0;
            statsPanel.Append(kiGainStatButton);

            baseDefenseButton = new StatButton("Increase", "baseDefense", .25f);
            baseDefenseButton.HAlign = .5f;
            baseDefenseButton.VAlign = .3f;
            statsPanel.Append(baseDefenseButton);

            baseAttackButton = new StatButton("Increase", "baseAttack", .01f);
            baseAttackButton.HAlign = 0f;
            baseAttackButton.VAlign = .6f;
            statsPanel.Append(baseAttackButton);

            baseSpeedButton = new StatButton("Increase", "baseSpeed", .01f);
            baseSpeedButton.HAlign = .5f;
            baseSpeedButton.VAlign = .6f;
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
            statsPanel.Append(formIncreaseDamage);

            formIncreaseDefense = new StatButton("Increase", "SSJ1FormMultDefense", .1f);
            formIncreaseDefense.HAlign = 0f;
            formIncreaseDefense.VAlign = .3f;
            statsPanel.Append(formIncreaseDefense);

            formSpecial = new StatButton("Increase", "SSJ1FormSpecial", .1f);
            formSpecial.HAlign = 0f;
            formSpecial.VAlign = .6f;
            statsPanel.Append(formSpecial);

            //addFormStatButtons("SSJ1");
            
            addBaseStatButtons();
            addFormStatButtons("SSJ1");
            //removeFormStatButtons();

            //removeBaseStatButtons();
            //addFormStatButtons("SSJ1");
            //modPlayer.setSelectedForm(this.name);
            //modSystem.MyFormsStatsUI.addStatButtons(modPlayer.getSelectedFormID());
        }
        

        public void createFormButtons(string form)
        {
            FormButton formUnlockButton = new FormButton(form, ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/"+form+"Buff"), true);
            
            Dictionary<string,object> formUnlockTreeInfo = nameToUnlockTreeInfo[form];
            float VAlignFormUnlock = (float)formUnlockTreeInfo["vAlign"];
            float HAlignFormUnlock = (float)formUnlockTreeInfo["hAlign"];
            //List<GUIArrow> arrows = (List<GUIArrow>)formUnlockTreeInfo["arrows"];

            formUnlockButton.HAlign = VAlignFormUnlock;
            formUnlockButton.VAlign = HAlignFormUnlock;
            //arrows.ForEach(formUnlockButton.getIcon().Append);
            
            formsPanel.Append(formUnlockButton);
            formUnlockButton.Activate();
            nameToFormUnlockButton.Add(form, formUnlockButton);

            FormButton formChooseButton = new FormButton(form, ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/"+form+"Buff"), false);
            formChooserPanel.Add(formChooseButton);
            formChooseButton.Activate();
            nameToFormChooseButton.Add(form, formChooseButton);
        }

        public void unlockForm(string form)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (!modPlayer.unlockedForms.Contains(form))
            {
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

            statsPanel.Append(formIncreaseDamage);  statsPanel.Append(formIncreaseDefense); statsPanel.Append(formSpecial); //statsPanel.Append(formMultiplyKi); statsPanel.Append(formDivideDrain); statsPanel.Append(formIncreaseSpeed);
            statsPanel.Append(formInfoText);
        }

        public void removeFormStatButtons()
        {
            formIncreaseDamage.Remove();  formIncreaseDefense.Remove(); formSpecial.Remove(); //formMultiplyKi.Remove(); formDivideDrain.Remove(); formIncreaseSpeed.Remove();
            formInfoText.Remove();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            // If this code is in the panel or container element, check it directly
            if (formsPanel.ContainsPoint(Main.MouseScreen))
            {
                //Main.LocalPlayer.mouseInterface = true;
            }
            if (formChooserPanel.ContainsPoint(Main.MouseScreen))
            {
                //Main.LocalPlayer.mouseInterface = true;
            }
            if (statsPanel.ContainsPoint(Main.MouseScreen))
            {
                //Main.LocalPlayer.mouseInterface = true;
            }
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
            List<GUIArrow> toReturn = new List<GUIArrow> ();
            foreach (GUIArrow arrow in unlockArrows) 
            {
                if(arrow.originForm == null && arrow.destinationForm == form)
                {
                    toReturn.Add(arrow);
                }
                else if(arrow.originForm == form )
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
            if(buffID == -1)
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

        public override void Update(GameTime gameTime)
        {



            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (modPlayer == null)
            {
                return;
            }


            if (modPlayer.unlockLoaded.Count > 0)
            {
                foreach (string form in modPlayer.unlockLoaded)
                {
                    unlockForm(form);
                }
                modPlayer.unlockLoaded.Clear();
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
                newStatsPanelText = "<- Respec " + selectedForm + ", level " + stats.getLevel() + ": " + stats.getPoints() + " Points! To next: " + round(stats.getExperience()) + "/" + round(stats.expNeededToAdvanceLevel());
                newFormInfoText = "Drain: " + roundTens((FormTree.nameToKiDrain[selectedForm] / stats.DivideDrain.getValue()) - modPlayer.kiGain.getValue()) + " ki/s\nMultiply Ki: " + stats.MultKi.getValue() + "x\nDamage: " + roundTens(FormTree.nameToDamageBonus[selectedForm] * stats.MultDamage.getValue()) + "x\nDefense: +" + ((int)(FormTree.nameToDefenseBonus[selectedForm] * stats.MultDefense.getValue())) + "\nSpeed: " + roundTens(FormTree.nameToSpeedBonus[selectedForm] * stats.MultSpeed.getValue()) + "x\nSpecial(Not Implemented): " + roundTens(stats.Special.getValue());
            }
            else 
            {
                newStatsPanelText = "<- Respec " + "Base Form, level " +modPlayer.getLevel()+ ": " + modPlayer.getPoints() + " Points! To next: " + round(modPlayer.getExperience()) + "/" + round(modPlayer.expNeededToAdvanceLevel());
            }
            newUnlockPanelText = "Last Hovered Over Form: "+formHoverText+"     Form Points: "+modPlayer.getFormPoints();
            statsPanelText.SetText(newStatsPanelText,.8f,false);
            formsPanelText.SetText(newUnlockPanelText,.8f,false);
            formInfoText.SetText(newFormInfoText, 1f, false);
            formInfoText.HAlign = .8f;
        }
    }
}
