using System.Drawing;

using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;

namespace OpenGLLayerExample
{
    public static class Items
    {
        public static ImagedButton GetImagedButton(EmbeddedImage image, float imageRotationAngle) {
        ImagedButton btn = new ImagedButton(DefaultsService.GetDefaultImage(image, EmbeddedImageSize.Size32x32),
                imageRotationAngle);
        return btn;
    }

    public static HorizontalSlider GetSlider() {
        HorizontalSlider slider = new HorizontalSlider();
        slider.SetStep(0.2f);
        slider.SetMinValue(2f);
        slider.SetMaxValue(10f);
        slider.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
        slider.SetSize(150, 30);
        slider.SetAlignment(ItemAlignment.HCenter, ItemAlignment.Bottom);
        slider.SetMargin(40, 0, 40, 0);
        slider.Handler.SetShadow(5, 0, 2, Color.Black);
        return slider;
    }

    public static HorizontalStack GetToolbarLayout() {
        HorizontalStack layout = new HorizontalStack();
        layout.SetContentAlignment(ItemAlignment.HCenter);
        layout.SetAlignment(ItemAlignment.Bottom, ItemAlignment.Left);
        layout.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
        layout.SetHeight(30);
        layout.SetSpacing(5, 0);
        layout.SetMargin(20, 0, 20, 20);
        return layout;
    }
    }
}