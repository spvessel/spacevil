using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;
using SpaceVIL.Common;

namespace CharacterEditor
{
    internal class MainWindow : ActiveWindow
    {
        internal ListBox ItemList = new ListBox();
        internal TextArea ItemText = new TextArea();
        internal ButtonCore BtnGenerate;
        internal ButtonCore BtnSave;
        internal SpinItem NumberCount;

        public override void InitWindow()
        {
            SetParameters(nameof(CharacterEditor), nameof(CharacterEditor), 1000, 600, false);
            IsCentered = true;

            //title
            TitleBar title = new TitleBar(nameof(CharacterEditor));
            title.SetIcon(
                DefaultsService.GetDefaultImage(EmbeddedImage.User, EmbeddedImageSize.Size32x32), 20, 20);

            //layout
            VerticalStack layout = ItemFactory.GetStandardLayout(title.GetHeight());

            //////////////////////////////////////////////////////////////////////////////
            HorizontalStack toolbar = ItemFactory.GetToolbar();
            VerticalSplitArea splitArea = ItemFactory.GetSplitArea();
            BtnGenerate = ItemFactory.GetToolbarButton();
            BtnSave = ItemFactory.GetToolbarButton();
            NumberCount = ItemFactory.GetSpinItem();
            ItemText.SetStyle(StyleFactory.GetTextAreaStyle());
            //////////////////////////////////////////////////////////////////////////////

            //adding
            AddItems(title, layout);
            layout.AddItems(toolbar, splitArea);
            splitArea.AssignLeftItem(ItemList);
            splitArea.AssignRightItem(ItemText);
            toolbar.AddItems(BtnGenerate, BtnSave, ItemFactory.GetVerticalDivider(), NumberCount);
            BtnGenerate.AddItem(ItemFactory.GetToolbarIcon(
                DefaultsService.GetDefaultImage(EmbeddedImage.Refresh, EmbeddedImageSize.Size32x32)
            ));
            BtnSave.AddItem(ItemFactory.GetToolbarIcon(
                DefaultsService.GetDefaultImage(EmbeddedImage.Diskette, EmbeddedImageSize.Size32x32)
            ));
        }
    }
}