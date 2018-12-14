using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;

namespace CustomChance
{
    internal class ButtonStand : ButtonCore
    {
        SpaceVIL.Rectangle _stand;

        public ButtonStand() : base() { }
        public ButtonStand(String text) : base(text) { }

        public override void InitElements()
        {
            base.InitElements();
            _stand = new SpaceVIL.Rectangle();
            _stand.SetBackground(Color.White);
            _stand.SetHeight(3);
            _stand.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            _stand.SetAlignment(ItemAlignment.Bottom | ItemAlignment.HCenter);
            _stand.SetVisible(false);
            AddItem(_stand);
        }

        public override void SetMouseHover(bool value)
        {
            base.SetMouseHover(value);
            if (value)
                _stand.SetVisible(true);
            else
                _stand.SetVisible(false);
        }
    }
}