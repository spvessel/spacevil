using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace ToolTipExample
{
    public class Tool : Prototype
    {
        ImageItem image = null;

        public Tool(Bitmap icon)
        {
            image = new ImageItem(icon);
            SetSizePolicy(SizePolicy.Fixed, SizePolicy.Expand);
            SetWidth(30);
            SetBackground(Color.Transparent);
            SetPadding(4, 4, 4, 4);
            AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(20, 255, 255, 255)));
            AddItemState(ItemStateType.Pressed, new ItemState(Color.FromArgb(40, 40, 40)));
        }

        public Tool(Bitmap icon, String tooltip) : this(icon)
        {
            SetToolTip(tooltip);
        }

        public override void InitElements()
        {
            image.KeepAspectRatio(true);
            AddItem(image);
        }
    }
}