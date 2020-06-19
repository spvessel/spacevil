using System;
using System.Collections.Generic;

using SpaceVIL;
using SpaceVIL.Core;

using AdvancedGamePad.Core;
using AdvancedGamePad.Model;

namespace AdvancedGamePad.View
{
    public class KeyBindItem : HorizontalStack
    {
        public EventCommonMethod InputChanged = null;
        private Label _name = null;
        private TextEdit _bind = null;

        public KeyBindItem(string name, string keystroke)
        {
            SetHeightPolicy(SizePolicy.Fixed);
            SetHeight(30);
            SetSpacing(10, 0);

            _name = new Label(name);
            _bind = new TextEdit(keystroke);
            KeySequence = new List<string>();
        }

        public override void InitElements()
        {
            _name.SetMaxWidth(130);

            _bind.SetEditable(false);
            _bind.EventKeyPress += (sender, args) =>
            {
                OnKeyDown((HardwareButtons) args.Key);
            };
            _bind.EventKeyRelease += (sender, args) =>
            {
                OnKeyUp(args);
                InputChanged?.Invoke();
            };
            AddItems(
                _name,
                _bind
            );
        }

        // Making shortcuts
        private string _keyStroketext = string.Empty;
        private int _keyCount = 0;
        public List<string> KeySequence;

        private void SetShortcutSequence()
        {
            _keyStroketext = "";

            foreach (var item in KeySequence)
            {
                if (KeySequence.IndexOf(item) < KeySequence.Count - 1)
                    _keyStroketext += item + "+";
                else
                    _keyStroketext += item;
            }
            _bind.SetText(_keyStroketext);
        }

        private void OnKeyDown(HardwareButtons button)
        {
            string key = Parse.KeysString(button);
            if (button == HardwareButtons.Unknown)
                return;

            if (_keyCount == 0)
            {
                _keyStroketext = String.Empty;
                KeySequence.Clear();
            }

            if (!KeySequence.Contains(key))
            {
                KeySequence.Add(key);
                _keyCount++;
            }
            SetShortcutSequence();
        }

        private void OnKeyUp(KeyArgs args)
        {
            _keyCount--;
        }

        public void SetName(string name)
        {
            _name.SetText(name);
        }

        public string GetText()
        {
            return _bind.GetText();
        }

        public void SetContextMenu(ContextMenu menu)
        {
            if (menu == null)
                return;

            _bind.EventMouseClick += (sender, args) =>
            {
                if (args.Button == MouseButton.ButtonRight)
                {
                    menu.ReturnFocus = _bind;
                    menu.Show(sender, args);
                }
            };
        }
    }
}