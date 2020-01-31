using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;

namespace OpenGLLayerExample
{
    public class ImagedButton : ButtonCore
    {
        ImageItem _image = null;

        public ImagedButton(Bitmap image, float imageRotationAngle)
        {
            SetBackground(0xFF, 0xB5, 0x6F);
            SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            SetSize(30, 30);
            SetAlignment(ItemAlignment.Right, ItemAlignment.Bottom);
            SetPadding(5, 5, 5, 5);
            SetShadow(5, 0, 2, Color.Black);
            IsFocusable = false;

            _image = new ImageItem(image, false);
            _image.SetRotationAngle(imageRotationAngle);
            _image.SetColorOverlay(Color.White);
        }

        public override void InitElements()
        {
            base.InitElements();
            AddItem(_image);
        }
    }
}