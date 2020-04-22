using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace SubtractFigureEffect
{
    public static class ItemsFactory
    {
        public static IBaseItem GetLabel(String text)
        {
            Label label = new Label(text);
            label.SetTextAlignment(ItemAlignment.HCenter, ItemAlignment.Top);
            label.SetPadding(0, 20, 0, 0);
            label.SetFont(DefaultsService.GetDefaultFont(FontStyle.Bold, 19));
            return label;
        }

        public static ButtonToggle GetSwitchButton()
        {
            ButtonToggle btn = new ButtonToggle("Enable Subtract Effect");
            btn.SetSize(200, 40);
            btn.SetAlignment(ItemAlignment.HCenter, ItemAlignment.Bottom);
            btn.SetMargin(0, 0, 0, 20);
            btn.SetBorderRadius(btn.GetHeight() / 2);
            btn.EventToggle += (sender, args) =>
            {
                if (btn.IsToggled())
                {
                    btn.SetText("Disable Subtract Effect");
                }
                else
                {
                    btn.SetText("Enable Subtract Effect");
                }
            };
            return btn;
        }

        public static IBaseItem GetCircle(int diameter, Color color)
        {
            Ellipse circle = new Ellipse(64);
            circle.SetSize(diameter, diameter);
            circle.SetBackground(color);
            circle.SetAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            circle.SetShadow(5, 0, 0, Color.Black);
            circle.SetShadowExtension(2, 2);
            return circle;
        }

        public static void SetCircleAlignment(IBaseItem circle, params ItemAlignment[] alignment)
        {
            List<ItemAlignment> list = new List<ItemAlignment>(alignment.ToArray());

            int offset = circle.GetWidth() / 3;

            if (list.Contains(ItemAlignment.Top))
            {
                circle.SetMargin(circle.GetMargin().Left, circle.GetMargin().Top - offset + 10, circle.GetMargin().Right,
                        circle.GetMargin().Bottom);
            }

            if (list.Contains(ItemAlignment.Bottom))
            {
                circle.SetMargin(circle.GetMargin().Left, circle.GetMargin().Top, circle.GetMargin().Right,
                        circle.GetMargin().Bottom - offset);
            }

            if (list.Contains(ItemAlignment.Left))
            {
                circle.SetMargin(circle.GetMargin().Left - offset, circle.GetMargin().Top, circle.GetMargin().Right,
                        circle.GetMargin().Bottom);
            }

            if (list.Contains(ItemAlignment.Right))
            {
                circle.SetMargin(circle.GetMargin().Left, circle.GetMargin().Top, circle.GetMargin().Right - offset,
                        circle.GetMargin().Bottom);
            }
        }

        public static IEffect GetCircleEffect(IBaseItem circle, IBaseItem subtract)
        {
            int diameter = circle.GetHeight();
            float scale = 1.1f;
            int diff = (int)(diameter * scale - diameter) / 2;
            int xOffset = subtract.GetX() - circle.GetX() - diff;
            int yOffset = subtract.GetY() - circle.GetY() - diff;

            SubtractFigure effect = new SubtractFigure(
                    new Figure(false, GraphicsMathService.GetEllipse(diameter, diameter, 0, 0, 64)));
            effect.SetAlignment(ItemAlignment.VCenter, ItemAlignment.HCenter);
            effect.SetSizeScale(scale, scale);
            effect.SetPositionOffset(xOffset, yOffset);

            return effect;
        }

        public static SubtractFigure GetCircleCenterEffect(IBaseItem circle)
        {
            float scale = 0.4f;
            int diameter = (int)(circle.GetHeight() * scale);
            SubtractFigure effect = new SubtractFigure(
                    new Figure(true, GraphicsMathService.GetEllipse(diameter, diameter, 0, 0, 64)));
            effect.SetAlignment(ItemAlignment.VCenter, ItemAlignment.HCenter);
            return effect;
        }
    }
}