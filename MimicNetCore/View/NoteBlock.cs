using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace MimicSpace
{
    public class NoteBlock : ResizableItem, IDisposable
    {
        static int count = 0;

        private ButtonCore _palette;
        private ButtonToggle _lock;
        private TextArea _text;
        private Label _note;
        private ButtonCore _btn_close;
        private ContextMenu _palette_menu;

        public NoteBlock()
        {
            SetPassEvents(false);
            SetMinSize(200, 100);
            SetItemName("NoteBlock_" + count);
            SetPadding(4, 4, 4, 4);
            SetBorderRadius(4);
            SetBackground(32, 32, 32);
            count++;

            _palette = new ButtonCore();
            _lock = new ButtonToggle();
            _text = new TextArea();
            _note = new Label();
        }

        public override void InitElements()
        {
            _palette.SetPassEvents(false);
            _palette.SetItemName("Palette");
            _palette.SetAlignment(ItemAlignment.Right | ItemAlignment.Top);
            _palette.SetMargin(0, 40, 0, 0);
            _palette.SetSize(16, 15);
            _palette.SetBackground(255, 128, 128);
            _palette.SetBorderRadius(new CornerRadius(3));

            CustomShape arrow = new CustomShape();
            arrow.SetTriangles(GraphicsMathService.GetTriangle(a: 180));
            arrow.SetBackground(50, 50, 50);
            arrow.SetSize(14, 6);
            arrow.SetMargin(0, 1, 0, 0);
            arrow.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            arrow.SetAlignment(ItemAlignment.HCenter | ItemAlignment.VCenter);

            _lock.SetAlignment(ItemAlignment.Left | ItemAlignment.Top);
            _lock.SetSize(16, 16);
            _lock.SetBorderRadius(new CornerRadius(8));
            _lock.EventToggle += (sender, args) =>
            {
                IsLocked = !IsLocked;
                _text.SetEditable(!_text.IsEditable());
                _btn_close.SetDisabled(!_btn_close.IsDisabled());
            };

            VerticalScrollBar vs = _text.VScrollBar;
            vs.Slider.Handler.RemoveAllItemStates();
            vs.SetArrowsVisible(false);
            vs.SetBackground(Color.Transparent);
            vs.SetPadding(0, 2, 0, 2);
            vs.Slider.Track.SetBackground(Color.Transparent);
            vs.Slider.Handler.SetBorderRadius(new CornerRadius(3));
            vs.Slider.Handler.SetBackground(80, 80, 80, 255);
            vs.Slider.Handler.SetMargin(new Indents(5, 0, 5, 0));

            _text.SetBorderRadius(new CornerRadius(3));
            _text.SetHScrollBarVisible(ScrollBarVisibility.Never);
            _text.SetHeight(25);
            _text.SetAlignment(ItemAlignment.Left | ItemAlignment.Bottom);
            _text.SetBackground(151, 203, 255);
            _text.SetMargin(0, 60, 0, 0);

            _note.SetForeground(180, 180, 180);
            _note.SetHeight(25);
            _note.SetAlignment(ItemAlignment.Left | ItemAlignment.Top);
            _note.SetTextAlignment(ItemAlignment.VCenter | ItemAlignment.Left);
            _note.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            _note.SetText("Add a Note:");
            _note.SetMargin(0, 30, 0, 0);

            _btn_close = new ButtonCore();
            _btn_close.SetBackground(Color.FromArgb(255, 100, 100, 100));
            _btn_close.SetItemName("Close_" + GetItemName());
            _btn_close.SetSize(10, 10);
            _btn_close.SetMargin(0, 0, 0, 0);
            _btn_close.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            _btn_close.SetAlignment(ItemAlignment.Top | ItemAlignment.Right);
            _btn_close.AddItemState(ItemStateType.Hovered, new ItemState()
            {
                Background = Color.FromArgb(125, 255, 255, 255)
            });
            _btn_close.SetCustomFigure(new CustomFigure(false, GraphicsMathService.GetCross(10, 10, 3, 45)));
            _btn_close.EventMouseClick += (sender, args) =>
            {
                Dispose();
            };

            AddItems(_lock, _note, _text, _palette, _btn_close);

            _palette.AddItem(arrow);

            _palette_menu = new ContextMenu(GetHandler());
            _palette_menu.SetBorderRadius(5);
            _palette_menu.SetBorderThickness(1);
            _palette_menu.SetBorderFill(32, 32, 32);
            _palette_menu.SetBackground(60, 60, 60);
            _palette_menu.SetWidth(100);
            _palette_menu.ItemList.SetSelectionVisible(false);
            _palette_menu.ActiveButton = MouseButton.ButtonLeft;

            MenuItem red = InfinityItemsBox.GetMenuItem("Red");
            red.EventMouseClick += (sender, args) =>
            {
                _text.SetBackground(255, 196, 196);
            };
            MenuItem green = InfinityItemsBox.GetMenuItem("Green");
            green.EventMouseClick += (sender, args) =>
            {
                _text.SetBackground(138, 255, 180);
            };
            MenuItem blue = InfinityItemsBox.GetMenuItem("Blue");
            blue.EventMouseClick += (sender, args) =>
            {
                _text.SetBackground(151, 203, 255);
            };
            MenuItem yellow = InfinityItemsBox.GetMenuItem("Yellow");
            yellow.EventMouseClick += (sender, args) =>
            {
                _text.SetBackground(234, 232, 162);
            };
            _palette_menu.AddItems(red, green, blue, yellow);

            //mouse click to show palette
            _palette.EventMouseClick += (_, x) => _palette_menu.Show(_, x);
        }

        public void Dispose()
        {
            GetParent().RemoveItem(this);
        }
    }
}