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
        UIPanel formsPanel;
        UIPanel statsPanel;
        UIGrid formChooserPanel;

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

        Dictionary<string, Dictionary<string, object>> nameToUnlockTreeInfo = new Dictionary<string, Dictionary<string, object>>()
        {
            { "FSSJ",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 1  }, { "hAlign", 0.0714f * 0 }, { "arrows", new List<GUIArrow>() { new GUIArrow(false, Color.Red, false, "FSSJ", "SSJ1") } } } },//
            { "UI",         new Dictionary<string, object>(){ { "vAlign", 0.0625f * 1  }, { "hAlign", 0.0714f * 9 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "Ikari",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 1  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "Divine",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 2  }, { "hAlign", 0.0714f * 7 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "PU",         new Dictionary<string, object>(){ { "vAlign", 0.0625f * 2  }, { "hAlign", 0.0714f * 12 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ1",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 3  }, { "hAlign", 0.0714f * 0 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJ1G4",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 3  }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "FLSSJ",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 3  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "Evil",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 3  }, { "hAlign", 0.0714f * 11 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ1G2",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 4  }, { "hAlign", 0.0714f * 0 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJ2",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 4  }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ1",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 4  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "Rampaging",  new Dictionary<string, object>(){ { "vAlign", 0.0625f * 4  }, { "hAlign", 0.0714f * 11 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ1G3",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 5  }, { "hAlign", 0.0714f * 0 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJ3",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 5  }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ2",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 5  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "Berserk",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 5  }, { "hAlign", 0.0714f * 11 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ4",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 6  }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ3",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 6  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ4LB",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 7  }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJG",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 7  }, { "hAlign", 0.0714f * 3 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ4",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 7  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ5",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 8  }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJ5G4",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 8  }, { "hAlign", 0.0714f * 2 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "FSSJB",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 8  }, { "hAlign", 0.0714f * 3 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ4LB",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 8  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJRage",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 9  }, { "hAlign", 0.0714f * 0 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJB1",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 9  }, { "hAlign", 0.0714f * 3 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJB1G4",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 9  }, { "hAlign", 0.0714f * 4 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJR1",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 9  }, { "hAlign", 0.0714f * 6 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJR1G4",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 9  }, { "hAlign", 0.0714f * 7 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ5G2",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 10 }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//
            
            { "SSJ6",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 11  }, { "hAlign", 0.0714f * 2 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJB1G2",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 11  }, { "hAlign", 0.0714f * 3 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJB",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 11  }, { "hAlign", 0.0714f * 5 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJR1G2",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 11  }, { "hAlign", 0.0714f * 6 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ5",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 11  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJB1G3",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 12  }, { "hAlign", 0.0714f * 3 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJB2",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 12  }, { "hAlign", 0.0714f * 4 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJR1G3",    new Dictionary<string, object>(){ { "vAlign", 0.0625f * 12  }, { "hAlign", 0.0714f * 6 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJR2",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 12  }, { "hAlign", 0.0714f * 7 }, { "arrows", new List<GUIArrow>() { } } } },//
            
            { "SSJ5G3",     new Dictionary<string, object>(){ { "vAlign", 0.0625f * 13  }, { "hAlign", 0.0714f * 1 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "SSJ7",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 14  }, { "hAlign", 0.0714f * 2 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJBE",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 14  }, { "hAlign", 0.0714f * 3 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJB3",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 14  }, { "hAlign", 0.0714f * 4 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "SSJR3",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 14  }, { "hAlign", 0.0714f * 7 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "DR",         new Dictionary<string, object>(){ { "vAlign", 0.0625f * 14  }, { "hAlign", 0.0714f * 8 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "TUI",        new Dictionary<string, object>(){ { "vAlign", 0.0625f * 14  }, { "hAlign", 0.0714f * 9 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ6",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 14  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "Beast",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 15  }, { "hAlign", 0.0714f * 12 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "UE",         new Dictionary<string, object>(){ { "vAlign", 0.0625f * 15  }, { "hAlign", 0.0714f * 13 }, { "arrows", new List<GUIArrow>() { } } } },//

            { "UILB",       new Dictionary<string, object>(){ { "vAlign", 0.0625f * 16  }, { "hAlign", 0.0714f * 9 }, { "arrows", new List<GUIArrow>() { } } } },//
            { "LSSJ7",      new Dictionary<string, object>(){ { "vAlign", 0.0625f * 16  }, { "hAlign", 0.0714f * 10 }, { "arrows", new List<GUIArrow>() { } } } },//
        };


        public static List<string> forms = new List<string>() { "FSSJ","FSSJB","SSJ1","SSJ1G2","SSJ1G3","SSJ1G4","SSJ2","SSJ3","SSJRage","SSJ4","SSJ4LB","SSJ5","SSJ5G2","SSJ5G3","SSJ5G4","SSJ6","SSJ7","FLSSJ","Ikari","LSSJ1","LSSJ2","LSSJ3","LSSJ4","LSSJ4LB","LSSJ5","LSSJ6","LSSJ7","SSJG","LSSJB","SSJB1","SSJB1G2","SSJB1G3","SSJB1G4","SSJB2","SSJB3","SSJBE","SSJR1","SSJR1G2","SSJR1G3","SSJR1G4","SSJR2","SSJR3","Divine","DR","Evil","Rampaging","Berserk","PU","Beast","UE","UI","UILB","TUI"
        };

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
            formChooserPanel.Top.Set(0, .5f); //500
            formChooserPanel.Height.Set(0, .46f); //450
            formChooserPanel.Left.Set(0, .05f); //100
            formChooserPanel.Width.Set(0, .24f); //450
            formChooserPanel.MaxHeight.Set(0, .45f); //450
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

            forms.ForEach(form => createFormButtons(form));








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
            List<GUIArrow> arrows = (List<GUIArrow>)formUnlockTreeInfo["arrows"];

            formUnlockButton.HAlign = VAlignFormUnlock;
            formUnlockButton.VAlign = HAlignFormUnlock;
            arrows.ForEach(formUnlockButton.icon.Append);
            
            formsPanel.Append(formUnlockButton);
            nameToFormUnlockButton.Add(form, formUnlockButton);

            FormButton formChooseButton = new FormButton(form, ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/"+form+"Buff"), false);
            formChooseButton.deactivated = true;
            nameToFormChooseButton.Add(form, formChooseButton);
        }

        public void unlockForm(string form)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if (!modPlayer.unlockedForms.Contains(form))
            {
                modPlayer.unlockedForms.Add(form);
            }
            
            nameToFormUnlockButton[form].Deactivate();
            nameToFormUnlockButton[form].Remove();
            formChooserPanel.Add(nameToFormChooseButton[form]);
            nameToFormChooseButton[form].Activate();
            //FormButton formButton = new FormButton(form, ModContent.Request<Texture2D>("DragonballPichu/Content/Buffs/"+form+"Buff"), false);
            //formChooserPanel.(formButton);
        }

        public void addBaseStatButtons()
        {
            statsPanel.Append(kiMaxStatButton); statsPanel.Append(chargeKiGainStatButton); statsPanel.Append(kiGainStatButton); statsPanel.Append(baseAttackButton); statsPanel.Append(baseDefenseButton); statsPanel.Append(baseSpeedButton);

            //kiMaxStatButton.Activate(); chargeKiGainStatButton.Activate(); kiGainStatButton.Activate();
        }

        public void removeBaseStatButtons()
        {
            kiMaxStatButton.Remove(); chargeKiGainStatButton.Remove(); kiGainStatButton.Remove(); baseAttackButton.Remove(); baseDefenseButton.Remove(); baseSpeedButton.Remove();

            //kiMaxStatButton.Deactivate(); chargeKiGainStatButton.Deactivate(); kiGainStatButton.Deactivate();
        }

        public void addFormStatButtons(string formName)
        {
            /*float increaseKiAmt = .1f;
            float increaseDrainAmt = .1f;
            float increaseDamageAmt = .1f;
            float increaseSpeedAmt = .1f;
            float increaseDefenseAmt = .1f;
            float increaseSpecialAmt = 0;*/

            //formMultiplyKi.statName = formName + "FormMultKi";
            //formDivideDrain.statName = formName + "FormDivideDrain";
            formIncreaseDamage.statName = formName + "FormMultDamage";
            //formIncreaseSpeed.statName = formName + "FormMultSpeed";
            formIncreaseDefense.statName = formName + "FormMultDefense";
            formSpecial.statName = formName + "FormSpecial";

            statsPanel.Append(formIncreaseDamage);  statsPanel.Append(formIncreaseDefense); statsPanel.Append(formSpecial); //statsPanel.Append(formMultiplyKi); statsPanel.Append(formDivideDrain); statsPanel.Append(formIncreaseSpeed);
            statsPanel.Append(formInfoText);
            /*formMultiplyKi.Activate();
            formDivideDrain.Activate();
            formIncreaseDamage.Activate();
            formIncreaseSpeed.Activate();
            formIncreaseDefense.Activate();
            formSpecial.Activate();*/
        }

        public void removeFormStatButtons()
        {
            /*formMultiplyKi.Deactivate();
            formDivideDrain.Deactivate();
            formIncreaseDamage.Deactivate();
            formIncreaseSpeed.Deactivate();
            formIncreaseDefense.Deactivate();
            formSpecial.Deactivate();*/
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
            foreach (string form in modPlayer.unlockedForms)
            {
                if (!nameToFormChooseButton.Keys.Contains(form))
                {
                    unlockForm(form);
                }
                
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
