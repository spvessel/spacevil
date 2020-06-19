using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;

namespace AdvancedGamePad.View
{
    public class ImagedButton : Prototype
    {
        private SpaceVIL.Rectangle _underline = null;
        private ImageItem _image = null;

        public ImagedButton(Bitmap image)
        {
            _underline = Factory.Items.GetUnderline();
            _image = new ImageItem(image, false);

            SetStyle(DefaultsService.GetDefaultStyle(typeof(SpaceVIL.ButtonCore)));
        }

        public override void InitElements()
        {
            base.InitElements();

            _image.IsFocusable = false;
            _image.KeepAspectRatio(true);
            _image.SetMargin(3, 3, 3, 3);

            AddItems(_image, _underline);

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