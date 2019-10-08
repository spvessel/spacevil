using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;

namespace StylingExample
{
    public class MainWindow : ActiveWindow
    {
        // implement ActiveWindow class
        public override void InitWindow()
        {
            // add style for our Picture class to the current theme
            DefaultsService.GetDefaultTheme().AddDefaultCustomItemStyle(typeof(Picture), StyleFactory.GetPictureStyle());
            // slightly change style for WrapGrid
            StyleFactory.UpdateWrapGridStyle();

            // window attr
            SetParameters("MainWindow", "StylingExample", 800, 600);
            SetAntiAliasingQuality(MSAA.MSAA8x);

            // create WrapGrid as main layout
            WrapGrid layout = new WrapGrid(160, 120, Orientation.Horizontal);
            layout.SetStretch(true);

            // add WrapGrid to window
            AddItem(layout);

            // create and add 8 Picture items, the style for Picture class is automatically applied from the current theme
            layout.AddItems(
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.User, EmbeddedImageSize.Size64x64),
                            "User Image"),
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.Add, EmbeddedImageSize.Size64x64),
                            "Add Image"),
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.Eraser, EmbeddedImageSize.Size64x64),
                            "Eraser Image"),
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.Filter, EmbeddedImageSize.Size64x64),
                            "Filter Image"),
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.Gear, EmbeddedImageSize.Size64x64),
                            "Gear Image"),
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.Home, EmbeddedImageSize.Size64x64),
                            "Home Image"),
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.Refresh, EmbeddedImageSize.Size64x64),
                            "Refresh Image"),
                    new Picture(DefaultsService.GetDefaultImage(EmbeddedImage.Pencil, EmbeddedImageSize.Size64x64),
                            "Pencil Image")
                            );
        }
    }
}