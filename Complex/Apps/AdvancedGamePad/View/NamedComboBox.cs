using System.Collections.Generic;

using SpaceVIL;
using SpaceVIL.Core;

namespace AdvancedGamePad.View
{
    public class NamedComboBox : HorizontalStack
    {
        public EventCommonMethod SelectionChanged = null;
        internal Label Name { get; private set; }
        public ComboBox ComboBoxItem { get; private set; }
        private List<IBaseItem> _itemList = new List<IBaseItem>();
        public NamedComboBox(string name)
        {
            SetHeightPolicy(SizePolicy.Fixed);
            SetHeight(30);
            SetSpacing(10, 0);

            Name = new Label(name);
            Name.SetMaxWidth(150);

            ComboBoxItem = new ComboBox();
        }

        private bool _isInit = false;
        public override void InitElements()
        {
            AddItems(
                Name,
                ComboBoxItem
            );

            if (_itemList.Count != 0)
                ComboBoxItem.AddItems(_itemList.ToArray());

            ComboBoxItem.SelectionChanged += () =>
            {
                SelectionChanged?.Invoke();
            };

            _isInit = true;
        }

        public override void AddItem(IBaseItem item)
        {
            if (item is MenuItem)
            {
                if (_isInit)
                    ComboBoxItem.AddItem(item);
                else
                    _itemList.Add(item);
            }
            else
                base.AddItem(item);
        }

        public override bool RemoveItem(IBaseItem item)
        {
            if (item is MenuItem)
            {
                _itemList.Remove(item);
                return ComboBoxItem.RemoveItem(item);
            }
            else
                return base.RemoveItem(item);
        }

        public void SetText(string text)
        {
            Name.SetText(text);
        }

        public string GetTextSelection()
        {
            return ComboBoxItem.GetText();
        }

        public int GetCurrentIndex()
        {
            return ComboBoxItem.GetCurrentIndex();
        }

        public void SetCurrentIndex(int index)
        {
            ComboBoxItem.SetCurrentIndex(index);
        }
    }
}