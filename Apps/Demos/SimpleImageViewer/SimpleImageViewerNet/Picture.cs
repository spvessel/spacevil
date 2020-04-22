using System;
using System.Drawing;
using SpaceVIL.Common;
using SpaceVIL.Decorations;
using SpaceVIL.Core;
using SpaceVIL;
using System.IO;

namespace SimpleImageViewer
{
    public class Picture : Prototype, IComparable<Picture>
    {
        public ImageItem Image = new ImageItem();
        public Label Name = new Label();
        public String Path = String.Empty;

        public Picture(Bitmap image, String name, String path)
        {
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            SetSpacing(0, 5);
            SetMargin(2, 2, 2, 2);
            SetAlignment(ItemAlignment.Left, ItemAlignment.Top);
            SetBackground(Color.FromArgb(80, 80, 80));
            SetShadow(10, 0, 0, Color.FromArgb(200, 0, 0, 0));

            Image.SetImage(image);
            Name.SetText(name);
            Path = path;
        }

        public int CompareTo(Picture item)
        {
            return this.Path.CompareTo(item.Path);
        }

        public override void InitElements()
        {
            Name.SetHeightPolicy(SizePolicy.Fixed);
            Name.SetHeight(30);
            Name.SetTextAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            Name.SetMargin(5, 0, 5, 0);
            Name.SetAlignment(ItemAlignment.Left, ItemAlignment.Bottom);

            Image.IsHover = false;
            Image.KeepAspectRatio(true);
            Image.SetBackground(32, 32, 32);
            Image.SetMargin(0, 0, 0, 35);

            AddItems(Image, Name);
        }
    }
}
