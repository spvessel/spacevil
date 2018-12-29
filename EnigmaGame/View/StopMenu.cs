using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace EnigmaGame.View
{
    internal class StopMenu : VerticalStack
    {
        public StopMenu()
        {
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetHeight(200);
            SetBackground(0, 0, 0, 220);
            SetAlignment(ItemAlignment.VCenter | ItemAlignment.HCenter);
            SetPadding(0, 50, 0, 0);
            SetSpacing(0, 50);
            SetShadow(10, 0, 0, Color.Black);
        }

        public override void InitElements()
        {
            Label title = new Label("WELCOME TO ENIGMAGAME!");
            title.SetHeightPolicy(SizePolicy.Fixed);
            title.SetHeight(30);
            title.SetTextAlignment(ItemAlignment.Top | ItemAlignment.HCenter);
            title.SetFont(Common.MoireFont);
            title.SetForeground(Common.Selected);

            Label manual = new Label("PRESS (SPACE) TO PLAY OR (ESCAPE) TO EXIT");
            manual.SetStyle(title.GetCoreStyle());
            manual.SetFont(Common.MoireFont);
            manual.SetTextAlignment(ItemAlignment.Top | ItemAlignment.HCenter);
            manual.SetForeground(Common.Selected);

            AddItems(
                title,
                manual
            );
        }
    }
}