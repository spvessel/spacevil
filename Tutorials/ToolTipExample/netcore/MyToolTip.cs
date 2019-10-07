using System;
using SpaceVIL;
using SpaceVIL.Core;

namespace ToolTipExample
{
    // this is an example of creating a custom simple tooltip
    public class MyToolTip : Prototype, IFloating
    {
        Label _label = null;
        public MyToolTip(CoreWindow window, String text)
        {
            // MyToolTip attr
            SetVisible(false);
            SetBackground(35, 35, 35);
            IsFocusable = false;
            // Label attr
            _label = new Label(text, false);
            _label.SetMargin(10, 0, 10, 0);
            // add MyToolTip to ItemsLayoutBox: it is necessary for IFloating items
            ItemsLayoutBox.AddItem(window, this, LayoutType.Floating);
        }

        private bool _init = false;
        public override void InitElements()
        {
            if (!_init)
            {
                AddItems(
                    ItemsFactory.GetVerticalDivider(),
                    _label
                    );
                _init = true;
            }
        }

        // implement IFloating
        public void Hide()
        {
            SetVisible(false);
        }

        public void Hide(MouseArgs args)
        {
            Hide();
        }

        public bool IsOutsideClickClosable()
        {
            return false;
        }

        public void SetOutsideClickClosable(bool value) { }

        public void Show()
        {
            SetVisible(true);
        }

        public void Show(IItem sender, MouseArgs args)
        {
            InitElements();
            IBaseItem item = sender as IBaseItem;
            SetPosition(item.GetX() + item.GetWidth(), item.GetY());
            SetHeight(item.GetHeight());
            SetWidth(_label.GetTextWidth() + _label.GetMargin().Left + _label.GetMargin().Right);
            Show();
        }
    }
}