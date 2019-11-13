using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;

namespace SubtractFigureEffect
{
    public class MainWindow : ActiveWindow
    {
        public override void InitWindow()
        {
            // window attr
            SetParameters("MainWindow", "SubtractEffectExample", 600, 500);
            SetAntiAliasingQuality(MSAA.MSAA8x);
            IsCentered = true;

            // create items
            int diameter = 200;

            IBaseItem cRed = ItemsFactory.GetCircle(diameter, Color.FromArgb(255, 94, 94));
            IBaseItem cGreen = ItemsFactory.GetCircle(diameter, Color.FromArgb(16, 180, 111));
            IBaseItem cBlue = ItemsFactory.GetCircle(diameter, Color.FromArgb(10, 162, 232));

            ItemsFactory.SetCircleAlignment(cRed, ItemAlignment.Top);
            ItemsFactory.SetCircleAlignment(cGreen, ItemAlignment.Left, ItemAlignment.Bottom);
            ItemsFactory.SetCircleAlignment(cBlue, ItemAlignment.Right, ItemAlignment.Bottom);

            // add items to window
            AddItems(cRed, cGreen, cBlue, ItemsFactory.GetLabel("Vector Subtraction Effect"));

            // init effects
            Model.InitEffects(cRed, cGreen, cBlue);

            // switch effects button
            ButtonToggle switchEffectBtn = ItemsFactory.GetSwitchButton();
            switchEffectBtn.EventToggle += (sender, args) =>
            {
                if (switchEffectBtn.IsToggled())
                {
                    Model.AddEffects();
                }
                else
                {
                    Model.RemoveEffects();
                }
            };
            AddItem(switchEffectBtn);
        }
    }
}