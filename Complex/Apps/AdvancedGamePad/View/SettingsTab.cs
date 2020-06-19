using System.Drawing;

using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace AdvancedGamePad.View
{
    public class SettingsTab : Tab
    {
        public SettingsTab(string name) : base(name)
        {
            SetDraggable(false);
            SetClosable(false);
            AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(40, 255, 255, 255)));
        }

        public override void InitElements()
        {
            base.InitElements();
        }
    }
}
