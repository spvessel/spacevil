using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Decorations;

namespace StylingExample
{
    public class Picture : VerticalStack
    {
        private ImageItem _imagePicture = null;
        private Label _imageName = null;

        public Picture(Bitmap image, String name)
        {
            _imagePicture = new ImageItem(image);
            _imageName = new Label(name);

            // get style from current theme and apply it
            SetStyle(DefaultsService.GetDefaultStyle(typeof(Picture)));

            // as you can see, no properties are set here, such as size, color, alignment and etc, 
            // you have to provide a style, for example, adding a style to the current theme
        }

        public override void InitElements()
        {
            _imagePicture.KeepAspectRatio(true);
            AddItems(_imagePicture, _imageName);
        }

        // define style application rules for Picture class
        public override void SetStyle(Style style)
        {
            if (style == null)
                return;
            // set style for current item: Picture class
            base.SetStyle(style);

            // set style for ImageItem
            Style innerStyle = style.GetInnerStyle("image");
            if (innerStyle != null)
                _imagePicture.SetStyle(innerStyle);

            // set style for Label
            innerStyle = style.GetInnerStyle("text");
            if (innerStyle != null)
                _imageName.SetStyle(innerStyle);
        }
    }
}