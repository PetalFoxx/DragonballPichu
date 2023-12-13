using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SteelSeries.GameSense;
using Terraria.GameContent;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using DragonballPichu.Common.Systems;

namespace DragonballPichu.Common.GUI
{

    internal class GUIArrow : UIElement
    {
        const int ARROWWIDTH = 4;
        Color color;
        Boolean deactivated;
        string destinationForm;
        string originForm;
        bool horizontalFirst;


        public GUIArrow(Color color, string destinationForm)
        {
            this.color = color;
            this.destinationForm = destinationForm;
            this.originForm = "baseForm";
            this.destinationForm = destinationForm;
            this.horizontalFirst = false;
        }

        public GUIArrow(Color color, string originForm, string destinationForm)
        {
            this.color = color;
            this.destinationForm = destinationForm;
            this.originForm = originForm;
            this.destinationForm = destinationForm;
            this.horizontalFirst = false;
        }

        //horizontalFirst does nothing currently, but I could if needed allow you to set it to true
        public GUIArrow(Color color, string originForm, string destinationForm, bool horizontalFirst)
        {
            this.color = color;
            this.destinationForm = destinationForm;
            this.originForm = originForm;
            this.destinationForm = destinationForm;
            this.horizontalFirst = horizontalFirst;
        }

        public override void OnDeactivate()
        {
            deactivated = true;
        }

        public override void OnActivate()
        {
            deactivated = false;
        }

        private void DrawArrowFromLeft(SpriteBatch spriteBatch)
        {
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (modSystem == null) { return; }
            //if (!modSystem.MyFormsStatsUI.nameToFormUnlockButton.ContainsKey(originForm)) { return; }
            if (!modSystem.MyFormsStatsUI.nameToFormUnlockButton.ContainsKey(destinationForm)) { return; }
            //FormButton originFormUnlockButton = modSystem.MyFormsStatsUI.nameToFormUnlockButton[originForm];
            FormButton destinationUnlockFormButton = modSystem.MyFormsStatsUI.nameToFormUnlockButton[destinationForm];
            CalculatedStyle dimensions = GetDimensions();
            CalculatedStyle destination = destinationUnlockFormButton.icon.GetDimensions();
            int centerLine = (int)(dimensions.Y + destinationUnlockFormButton.icon.Height.Pixels / 3);
            Point point = new Point((int)(modSystem.MyFormsStatsUI.formsPanel.GetDimensions().X+3), centerLine - 2);
            Point point2 = new Point((int)destination.X - 2, centerLine + 2);
            //Point point2 = new Point(point.X + 100, point.Y + 20);
            int width = point2.X - point.X;
            int height = point2.Y - point.Y;
            //spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 100, 100, 4), this.color);
            //spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(point.X, point.Y, width, height), this.color);
            drawBox(spriteBatch, point, point2);
        }




        private void drawBox(SpriteBatch spriteBatch, Point topLeft, Point bottomRight)
        {
            int width = Math.Abs(bottomRight.X - topLeft.X);
            int height = Math.Abs(bottomRight.Y - topLeft.Y);


            spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(topLeft.X, topLeft.Y, width, height), this.color);
        }

        private void DrawArrow(SpriteBatch spriteBatch, bool horizontalFirst)
        {
            FormButton originButton;
            FormButton destinationButton;
            UIImage originIcon;
            UIImage destinationIcon;
            CalculatedStyle originDimensions;
            CalculatedStyle destinationDimension;
            int originTop;
            int originBottom;
            int originLeft;
            int originRight;
            int destinationTop;
            int destinationBottom;
            int destinationLeft;
            int destinationRight;
            int originMidX;
            int originMidY;
            int destinationMidX;
            int destinationMidY;
            bool isOriginXLess;
            bool isOriginYLess;
            bool isOriginXEqual;
            bool isOriginYEqual;
            DragonballPichuUISystem modSystem = ModContent.GetInstance<DragonballPichuUISystem>();
            if (modSystem == null) { return; }
            if (originForm == null || originForm == "baseForm")
            {
                if (!modSystem.MyFormsStatsUI.nameToFormUnlockButton.ContainsKey(destinationForm)) { return; }
                destinationButton = modSystem.MyFormsStatsUI.nameToFormUnlockButton[destinationForm];
                destinationIcon = destinationButton.icon;
                destinationDimension = destinationIcon.GetDimensions();
                destinationTop = (int)destinationDimension.Y;
                destinationBottom = (int)destinationDimension.Y + (int)destinationDimension.Height;
                destinationLeft = (int)destinationDimension.X;
                destinationRight = (int)destinationDimension.X + (int)destinationDimension.Width;
                destinationMidX = (destinationLeft + destinationRight) / 2;
                destinationMidY = (destinationTop + destinationBottom) / 2;
                Point topLeft = new Point((int)(modSystem.MyFormsStatsUI.formsPanel.GetDimensions().X + 3), destinationMidY - ARROWWIDTH / 2);
                Point bottomRight = new Point((int)destinationLeft - 2, destinationMidY + ARROWWIDTH / 2);
                drawBox(spriteBatch, topLeft, bottomRight);
                return;
            }
            if (!modSystem.MyFormsStatsUI.nameToFormUnlockButton.ContainsKey(originForm)) { return; }
            if (!modSystem.MyFormsStatsUI.nameToFormUnlockButton.ContainsKey(destinationForm)) { return; }
            originButton = modSystem.MyFormsStatsUI.nameToFormUnlockButton[originForm];
            destinationButton = modSystem.MyFormsStatsUI.nameToFormUnlockButton[destinationForm];
            originIcon = originButton.icon;
            destinationIcon = destinationButton.icon;
            originDimensions = originIcon.GetDimensions();
            destinationDimension = destinationIcon.GetDimensions();
            originTop = (int)originDimensions.Y;
            originBottom = (int)originDimensions.Y + (int)originDimensions.Height;
            originLeft = (int)originDimensions.X;
            originRight = (int)originDimensions.X + (int)originDimensions.Width;
            destinationTop = (int)destinationDimension.Y;
            destinationBottom = (int)destinationDimension.Y + (int)destinationDimension.Height;
            destinationLeft = (int)destinationDimension.X;
            destinationRight = (int)destinationDimension.X + (int)destinationDimension.Width;
            originMidX = (originLeft + originRight) / 2;
            originMidY = (originTop + originBottom) / 2;
            destinationMidX = (destinationLeft + destinationRight) / 2;
            destinationMidY = (destinationTop + destinationBottom) / 2;
            isOriginXLess = false;
            isOriginYLess = false;
            isOriginXEqual = false;
            isOriginYEqual = false;

            if (originMidX == destinationMidX) { isOriginXEqual = true; }
            else if(originMidX < destinationMidX) { isOriginXLess = true; }

            if (originMidY == destinationMidY) { isOriginYEqual = true; }
            else if (originMidY < destinationMidY) { isOriginYLess = true; }

            //X and Y being equal cannot occur, that would go to the same!

            //origin is directly above
            if (isOriginXEqual)
            {
                int topY;
                int bottomY;
                //origin above
                if (isOriginYLess)
                {
                    //drawBox(spriteBatch, new Point(originMidX - ARROWWIDTH / 2, originBottom), new Point(originMidX + ARROWWIDTH / 2, destinationTop));
                    topY = originBottom;
                    bottomY = destinationTop;
                }
                //origin below
                else
                {
                    //drawBox(spriteBatch, new Point(originMidX - ARROWWIDTH / 2, destinationBottom), new Point(originMidX + ARROWWIDTH / 2, originTop));
                    topY = destinationBottom;
                    bottomY = originTop;
                }
                drawBox(spriteBatch, new Point(originMidX - ARROWWIDTH / 2, topY+2), new Point(originMidX + ARROWWIDTH / 2, bottomY-2));
            }
            else if (isOriginYEqual)
            {
                int leftX;
                int rightX;
                //origin left
                if (isOriginXLess)
                {
                    leftX = originRight;
                    rightX = destinationLeft;
                }
                //origin right
                else
                {
                    leftX = destinationRight;
                    rightX = originLeft;
                }
                drawBox(spriteBatch, new Point(leftX+2,originMidY - ARROWWIDTH / 2), new Point(rightX-2,originMidY + ARROWWIDTH / 2));
            }
            else
            {
                int x1;
                int x2;
                int x3;
                int x4;
                int y1;
                int y2;
                int y3;
                int y4;
                //can be changed to !horizontalFirst if needed, and add else for horizontal first implementation
                if (true)
                {
                    //above
                    if (isOriginYLess)
                    {

                        x1 = originMidX - ARROWWIDTH / 2;
                        y1 = originBottom + 2;

                        x2 = originMidX + ARROWWIDTH / 2;
                        y2 = destinationMidY + ARROWWIDTH / 2;

                        //left
                        if (isOriginXLess)
                        {
                            x3 = originMidX - ARROWWIDTH / 2;
                            y3 = destinationMidY - ARROWWIDTH / 2;

                            x4 = destinationLeft - 2;
                            y4 = destinationMidY + ARROWWIDTH / 2;
                        }
                        //right
                        else
                        {
                            x3 = destinationRight + 2;
                            y3 = destinationMidY - ARROWWIDTH / 2;

                            x4 = x2;
                            y4 = y2;
                        }
                        drawBox(spriteBatch, new Point(x1,y1), new Point(x2,y2));
                        drawBox(spriteBatch, new Point(x3,y3), new Point(x4,y4));
                    }
                    //below
                    else
                    {
                        x1 = originMidX - ARROWWIDTH / 2;
                        y1 = destinationMidY - ARROWWIDTH / 2;

                        x2 = originMidX + ARROWWIDTH / 2;
                        y2 = originTop - 2;
                        if (isOriginXLess)
                        {
                            x3 = originMidX - ARROWWIDTH / 2;
                            y3 = destinationMidY - ARROWWIDTH / 2;

                            x4 = destinationLeft - 2;
                            y4 = destinationMidY + ARROWWIDTH / 2;
                        }
                        //right
                        else
                        {
                            x3 = destinationRight + 2;
                            y3 = destinationMidY - ARROWWIDTH / 2;

                            x4 = originMidX + ARROWWIDTH / 2;
                            y4 = destinationMidY + ARROWWIDTH / 2;
                        }
                        drawBox(spriteBatch, new Point(x1, y1), new Point(x2, y2));
                        drawBox(spriteBatch, new Point(x3, y3), new Point(x4, y4));
                    }
                }


            }
            //drawBox(spriteBatch, new Point(originLeft, originTop), new Point(originRight, originBottom));
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (deactivated || destinationForm == null)
            {
                return;
            }
            base.DrawSelf(spriteBatch);
            DrawArrow(spriteBatch, horizontalFirst);
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            if (deactivated)
            {
                return;
            }
            base.DrawChildren(spriteBatch);
        }
    }
}
