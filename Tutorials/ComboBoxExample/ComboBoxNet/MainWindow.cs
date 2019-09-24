using System;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;

namespace ComboBoxExample
{
    internal class MainWindow : ActiveWindow
    {
        public override void InitWindow()
        {
            // Window attr
            SetParameters("ComboBoxExample", "ComboBoxExample", 500, 400);

            // Layout attr
            VerticalStack layout = new VerticalStack();
            layout.SetBackground(60, 60, 60);
            layout.SetPadding(30, 30, 30, 30);

            // Creating MenuItems
            MenuItem filterItem = ItemsFactory.GetMenuItem("Open Filter Function Menu",
                DefaultsService.GetDefaultImage(EmbeddedImage.Filter, EmbeddedImageSize.Size32x32));

            MenuItem recycleItem = ItemsFactory.GetMenuItem("Open Recycle Bin",
                DefaultsService.GetDefaultImage(EmbeddedImage.RecycleBin, EmbeddedImageSize.Size32x32));

            MenuItem refreshItem = ItemsFactory.GetMenuItem("Refresh UI",
                DefaultsService.GetDefaultImage(EmbeddedImage.Refresh, EmbeddedImageSize.Size32x32));

            MenuItem addMenuItemItem = ItemsFactory.GetMenuItem("Add New Function...",
                DefaultsService.GetDefaultImage(EmbeddedImage.Add, EmbeddedImageSize.Size32x32));

            // Creating ComboBox
            ComboBox combo = new ComboBox(
                filterItem,
                recycleItem,
                refreshItem,
                addMenuItemItem
            );
            combo.SetAlignment(ItemAlignment.VCenter, ItemAlignment.HCenter);
            combo.SetText("Operations");
            combo.SetStyle(StyleFactory.GetComboBoxStyle());

            // Change event for "addMenuItemItem" - add a new MenuItem into ComboBox
            addMenuItemItem.EventMouseClick = (sender, args) =>
            {
                InputDialog inDialog = new InputDialog("Add new function...", "Add", "NewFunction");
                inDialog.OnCloseDialog += () =>
                {
                    if (inDialog.GetResult() != String.Empty)
                        combo.AddItem(ItemsFactory.GetMenuItem(inDialog.GetResult(),
                            DefaultsService.GetDefaultImage(EmbeddedImage.Import, EmbeddedImageSize.Size32x32)));
                };
                inDialog.Show(addMenuItemItem.GetHandler());
            };

            // Add ComboBox to Window
            AddItem(layout);
            layout.AddItem(combo);

            // Decorate our ComboBox with a white dot
            combo.AddItem(ItemsFactory.GetDot());

            //Optionally: Set start index (this method should only be called if ComboBox has already been added)
            // combo.SetCurrentIndex(0);
        }
    }
}