using System.Drawing;
using SpaceVIL.Core;
using SpaceVIL;
using SpaceVIL.Decorations;

namespace SimpleImageViewer
{
    public class FullImageViewer : DialogItem
    {
        private ButtonCore _close = new ButtonCore();
        private ImageItem _image;
        public FullImageViewer(ImageItem image)
        {
            _image = image;
        }

        public override void InitElements()
        {
            base.InitElements();
            SetBackground(0, 0, 0, 200);
            
            Window.IsXResizable = false;
            Window.IsYResizable = false;
            Window.IsXFloating = false;
            Window.IsYFloating = false;

            _image.KeepAspectRatio(true);

            _close.SetSize(30, 30);
            _close.SetCustomFigure(new Figure(false, GraphicsMathService.GetCross(30, 30, 3, 45)));
            _close.SetBackground(100, 100, 100);
            _close.SetAlignment(ItemAlignment.Top, ItemAlignment.Right);
            _close.SetMargin(0, 10, 10, 0);

            Window.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            Window.SetMargin(20, 20, 20, 20);
            Window.SetBackground(Color.Transparent);
            AddItems(_close);

            Window.AddItem(_image);

            _close.EventMouseClick += (sender, args) =>
            {
                Close();
            };
        }

        public override void Show(CoreWindow handler)
        {
            base.Show(handler);
        }

        public override void Close()
        {
            if (OnCloseDialog != null)
                OnCloseDialog.Invoke();

            base.Close();
        }
    }
}
