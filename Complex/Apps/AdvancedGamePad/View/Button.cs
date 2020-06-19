using SpaceVIL;

namespace AdvancedGamePad.View
{
    public class Button : ButtonCore
    {
        private Rectangle _underline = null;

        public Button(string name) : base(name)
        {
            _underline = Factory.Items.GetUnderline();
        }

        public override void InitElements()
        {
            base.InitElements();

            AddItem(_underline);
            
            EventMouseHover += (sender, args) =>
            {
                _underline.SetVisible(true);
            };
            EventMouseLeave += (sender, args) =>
            {
                _underline.SetVisible(false);
            };
        }
    }
}