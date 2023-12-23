using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonballPichu.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DragonballPichu.Common.GUI
{
    public class StatButton : UIPanel
    {
        public string text;
        public string statName;
        public float statIncreasePerClick;
        UIText sideText = new UIText("",textScale: .8f);
        public StatButton(string text, string statName, float statIncreasePerClick)
        {
            this.text = text;
            this.statName = statName;
            this.statIncreasePerClick = statIncreasePerClick;
        }

        public override void OnInitialize()
        {
            OnLeftClick += this.OnButtonClick;
            Width.Set(35, 0);
            Height.Set(35, 0);
            //HAlign = 0.5f;
            Top.Set(0, 0);
            Left.Set(0, 0);
            sideText.SetText(text);
            sideText.Left.Set(25,0);
            Append(sideText);
        }

        public bool isVisible()
        {
            return !(getModifiedStatName() == "NOT USEFUL" || getModifiedStatName() == "");
        }

        public string getModifiedStatName()
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            if(modPlayer == null) { return ""; }
            Stat stat = modPlayer.getStat(statName);
            if(stat == null) { return ""; }
            string modifiedStatName;
            if (statName.Contains("Form"))
            {
                modifiedStatName = stat.name;
                modifiedStatName = modifiedStatName.Replace("Mult", "");
                modifiedStatName = modifiedStatName.Replace("Divide", "");
                if (modifiedStatName == "Special")
                {
                    //modifiedStatName = FormTree.nameToSpecial[statName.Substring(0, statName.IndexOf("Form"))][0];
                    //modifiedStatName = modPlayer.nameToStats[statName.Substring(0, statName.IndexOf("Form"))].specialEffectValue[0];
                    modifiedStatName = FormTree.getSpecial(statName.Substring(0, statName.IndexOf("Form")))[0];
                    switch (modifiedStatName)
                    {
                        case "Kaio-Efficient":
                            modifiedStatName = "Kaioken Drain Reduction";
                            break;
                        case "Ki Power":
                            modifiedStatName = "NOT USEFUL";
                            break;
                        case "HP Power":
                            modifiedStatName = "NOT USEFUL";
                            break;
                        case "Dodge":
                            modifiedStatName = "Dodge Cost Reduction";
                            break;
                        case "Regen":
                            modifiedStatName = "Life Regen";
                            break;
                        case "DR":
                            modifiedStatName = "Defense Effectiveness";
                            break;
                        case "Ki Attack Master":
                            modifiedStatName = "Ki Attack Charge Speed";
                            break;
                        case "Aura Defense":
                            modifiedStatName = "Aura Defense Gain";
                            break;
                        case "Special Compatibility":
                            modifiedStatName = "Stack Efficiency";
                            break;
                        case "Second Wind":
                            modifiedStatName = "Second Wind Level";
                            break;
                        case "Frantic Ki Regen":
                            modifiedStatName = "Regen and Ki Max at low HP";
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                modifiedStatName = statName;
            }
            return modifiedStatName;
        }

        public override void Update(GameTime gameTime)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            
            base.Update(gameTime);
            Stat stat = modPlayer.getStat(statName);
            string modifiedStatName = getModifiedStatName();
            


            if(stat ==  null)
            {
                sideText.SetText(text + " " + statName);
            }
            else
            {
                //"MultKi" "DivideDrain" "MultDamage" "MultSpeed" "MultDefense" "Special"
                string toText = "";
                switch (stat.name)
                {
                    case "MultKi":
                        toText = text + " " + modifiedStatName;
                        sideText.SetText(toText);
                        return;
                    case "DivideDrain":
                        toText = text + " " + modifiedStatName;
                        sideText.SetText(toText);
                        return;
                    case "MultDamage": //////////////////////////////
                        toText = text + " " + modifiedStatName;
                        sideText.SetText(toText);
                        return;
                    case "MultSpeed":
                        toText = text + " " + modifiedStatName;
                        sideText.SetText(toText);
                        return;
                    case "MultDefense": ///////////////////////////////
                        toText = text + " " + modifiedStatName;
                        sideText.SetText(toText);
                        return;
                    case "Special": ////////////////////////////
                        toText = text + " " + modifiedStatName;
                        sideText.SetText(toText);
                        return;
                    default:
                        toText = text + " " + modifiedStatName + ": " + statIncreasePerClick + "/" + stat.getValue();
                        sideText.SetText(toText);
                        return;

                }

                
                

            }
            /*
             else
            {
                if(statName != null && statName.Contains("Form"))
                {
                    String[] parsed = statName.Split("Form");
                    if (parsed.Length == 2)
                    {
                        string toText = text;
                        toText += " ";
                        toText += parsed[1];
                        sideText.SetText(toText);
                    }
                }

                
                
                //toText += ": +";
                //toText += statIncreasePerClick;
                //toText += "/";
                //toText += stat.getValue();
                
                

            }*/
        }

        public void OnButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!isVisible())
            {
                return;
            }
            var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            Stat stat = modPlayer.getStat(statName);
            if(stat != null)
            {
                if (statName.Contains("Form"))
                {
                    String[] info = statName.Split("Form");
                    FormStats stats = modPlayer.nameToStats[info[0]];
                    if (stats.usePoint())
                    {
                        stat.increaseValue(statIncreasePerClick);
                    }
                }
                else
                {
                    if (modPlayer.usePoint())
                    {
                        stat.increaseValue(statIncreasePerClick);
                    }
                }
            }
            
            //var modPlayer = Main.LocalPlayer.GetModPlayer<DragonballPichuPlayer>();
            //modPlayer.setSelectedForm(this.name);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isVisible())
            {
                return;
            }
            base.Draw(spriteBatch);
        }
    }

}
